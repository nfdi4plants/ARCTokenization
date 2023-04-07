namespace ArcGraphModel

open FSharpAux
open FsSpreadsheet
open Param
open FSharp.FGL
open FSharp.FGL.ArrayAdjacencyGraph


module List =   // remove as soon as this is available in next F#Aux NuGet release
    let map4 (mapping : 'T -> 'T -> 'T -> 'T -> 'U) (list1 : 'T list) (list2 : 'T list) (list3 : 'T list) (list4 : 'T list) =
        if list1.Length <> list2.Length || list1.Length <> list3.Length || list1.Length <> list4.Length then
            failwithf "The input lists have different lengths.\n\tlist1.Length = %i; list2.Length = %i; list3.Length = %i; list4.Length = %i" list1.Length list2.Length list3.Length list4.Length
        let rec loop acc nl1 nl2 nl3 nl4 =
            match nl1, nl2, nl3, nl4 with
            | h1 :: t1, h2 :: t2, h3 :: t3, h4 :: t4 ->
                loop (mapping h1 h2 h3 h4 :: acc) t1 t2 t3 t4
            | _ -> List.rev acc
        loop [] list1 list2 list3 list4


/// <summary>
/// Functions to work with FsTables into graph-based models.
/// </summary>
module TableTransform =

    // -----
    // TYPES
    // -----

    /// <summary>
    /// Modelling of the different types of nodes / Building Blocks.
    /// </summary>
    type NodeType =
        | Source
        | Sink
        | ProtocolRef


    // ------
    // VALUES
    // ------

    /// <summary>
    /// The header names of the columns containing Node-related Building Blocks.
    /// </summary>
    let nodeColumnNames = [
        "Source Name"
        "Sample Name"
        "Raw Data File"
        "Derived Data File"
        "Protocol Type"
        "Protocol REF"
    ]


    // ---------
    // FUNCTIONS
    // ---------

    /// <summary>
    /// Returns all data cells from a given header cell.
    /// </summary>
    let getDataCellsOf (fcc : FsCellsCollection) (headerCell : FsCell) = 
        FsCellsCollection.getCellsInColumn headerCell.ColumnNumber fcc 
        |> Seq.toList 
        |> List.skip 1

    /// <summary>
    /// Takes a list of header cells and an FsCellsCollections and returns a list of CvParams according to the information from the FsCells.
    /// If `crStart` is true, it is assumed that the first header cell is a Term containing header cell of a Building Block.
    /// 
    /// This function should only be used for parsing edges, i.e., Term-related Building Blocks.
    /// </summary>
    let parseEdges crStart (*(table : FsTable)*) (fcc : FsCellsCollection) (cl : FsCell list) =
        //let empty() = FsCell.createEmpty ()
        //let getTableFieldOf (table : FsTable) (cell : FsCell) =
        //    table.Fields(fcc) |> ignore

        let rec loop roundOne (cells : FsCell list) = 
            [
                match cells with
                | a :: b :: c :: d :: rest when roundOne && (String.startsWith "Unit" b.Value) ->
                    // a = Value/Name header, b = Unit header, c = TermSourceRef header, d = TermAccessionNumber header
                    //let tfa = FsTableField(a.Value, a.ColumnNumber, )
                    //FsTableField.getDataCells fcc true 
                    let dataCellsVal = getDataCellsOf fcc a
                    let dataCellsUnt = getDataCellsOf fcc b
                    let dataCellsTsr = getDataCellsOf fcc c
                    let dataCellsTan = getDataCellsOf fcc d
                    let cvPars =
                        List.map4 (
                            fun (vl : FsCell) unt tan tsr -> 
                                let valTerm = CvUnit(tan.Value, vl.Value, tsr.Value)
                                CvParam(d.Value, a.Value, c.Value, WithCvUnitAccession (unt.Value, valTerm))
                        ) dataCellsVal dataCellsUnt dataCellsTan dataCellsTsr 
                    yield! cvPars
                    yield! loop false rest
                | a :: b :: c :: rest when roundOne ->
                    // a = Value/Name header, b = TermSourceRef header, c = TermAccessionNumber header
                    let dataCellsVal = getDataCellsOf fcc a
                    let dataCellsTsr = getDataCellsOf fcc b
                    let dataCellsTan = getDataCellsOf fcc c
                    let cvPars =
                        (dataCellsVal, dataCellsTsr, dataCellsTan)
                        |||> List.map3 (
                            fun vl tsr tan ->
                                let valTerm = CvTerm(tan.Value, vl.Value, tsr.Value)
                                CvParam(c.Value, a.Value, b.Value, CvValue valTerm)
                        )
                    yield! cvPars
                    yield! loop false rest
                | a :: b :: rest ->
                    match roundOne with
                    | true  ->
                        // a = Value/Name header, b = TermSourceRef header (assumed, could also be TermAccessionNumber header if TSR column is missing)
                        let dataCellsVal = getDataCellsOf fcc a
                        let dataCellsTsr = getDataCellsOf fcc b
                        let cvPars =
                            (dataCellsVal, dataCellsTsr)
                            ||> List.map2 (
                                fun vl tsr ->
                                    let valTerm = CvTerm("(n/a)", vl.Value, tsr.Value)
                                    CvParam("n/a", a.Value, b.Value, CvValue valTerm)
                            )
                        yield! cvPars
                        yield! loop false rest
                    | false ->
                        // a = TermSourceRef header, b = TermAccessionNumber header
                        let dataCellsTsr = getDataCellsOf fcc a
                        let dataCellsTan = getDataCellsOf fcc b
                        let cvPars =
                            (dataCellsTsr, dataCellsTan)
                            ||> List.map2 (
                                fun tsr tan ->
                                    let valTerm = CvTerm(tan.Value, "n/a", tsr.Value)
                                    CvParam(b.Value, "(n/a)", a.Value, CvValue valTerm)
                            )
                        yield! cvPars
                        yield! loop false rest
                | a :: [] ->
                    match roundOne with
                    | true  ->
                        // a = Value/Name header
                        let dataCellsVal = getDataCellsOf fcc a
                        let cvPars =
                            dataCellsVal
                            |> List.map (
                                fun vl ->
                                    // use this if ParamValue shall be CvValue instead of mere Value
                                    //let valTerm = CvTerm("(n/a)", vl.Value, "(n/a)")
                                    CvParam("(n/a)", a.Value, "(n/a)", Value vl.Value)
                            )
                        yield! cvPars
                    | false ->
                        // a = TermSourceRef header (assumed, could also be TermAccessionNumber header if TSR column is missing)
                        let dataCellsTsr = getDataCellsOf fcc a
                        let cvPars =
                            dataCellsTsr
                            |> List.map (
                                fun tsr ->
                                    CvParam("(n/a)", "(n/a)", tsr.Value, Value "(n/a)")
                            )
                        yield! cvPars
                | [] -> ()
            ]
        loop crStart cl

    /// <summary>
    /// Takes a header cells and an FsCellsCollections and returns a list of CvParams according to the information from the FsCells.
    /// 
    /// This function should only be used for parsing nodes, i.e., input-, output-, and featured columns.
    /// </summary>
    let parseNode fcc headerCell =
        let dataCellsVal = getDataCellsOf fcc headerCell
        dataCellsVal
        |> List.map (
            fun dc ->
                //UserParam(headerCell.Value, ParamValue.Value dc.Value)
                CvParam("(n/a)", headerCell.Value, "(n/a)", ParamValue.Value dc.Value)
        )

    /// <summary>
    /// Takes a CvParam and returns the type of Node it contains.
    /// </summary>
    let getNodeType (cvPar : #IParamBase<'a>) =
        //let castedCvPar = cvPar :?> CvParam<string>     // debatable approach
        //let v = Param.getCvName castedCvPar
        let v = Param.getCvName cvPar
        match v with
        | "Source Name" -> Source
        | "Sample Name"
        | "Raw Data File"
        | "Derived Data File" -> Sink
        | "Protocol REF"
        | "Protocol Type" -> ProtocolRef
        | _ -> failwith $"HeaderCell {v} cannot be parsed to any NodeType."

    /// <summary>
    /// Separates a list of nodes into a tuple in the form of `Source * Sink * ProtocolRef`, tupled together with their respective CvParam lists.
    /// </summary>
    let separateNodes nodesList =

        let groupedNodes = List.groupBy (getNodeType) nodesList

        // long tuple... perhaps rework with anonymous record?
        let rec loop sourceNodeList sinkNodeList protocolRefNodeList gn =
            match gn with
            | [] -> sourceNodeList, sinkNodeList, protocolRefNodeList
            | h :: t ->
                match fst h with
                | Source        -> loop h               sinkNodeList    protocolRefNodeList t
                | Sink          -> loop sourceNodeList  h               protocolRefNodeList t
                | ProtocolRef   -> loop sourceNodeList  sinkNodeList    h                   t
        loop (Source, []) (Sink, []) (ProtocolRef, []) groupedNodes

    /// <summary>
    /// Takes lists of sources, sinks, and edges, and returns them transformed into LVertices and LEdges.
    /// </summary>
    let buildVerticesAndEdges sources edges sinks =

        // iterate through all element lists and build LVertices and LEdges according to their index (i.e., the row)
        let rec loop inputSources inputSinks inputEdges sourceVertices sinkVertices connectedEdges 
            : ((LVertex<'a,'b> list) * (LVertex<'a,'b> list) * (LEdge<'a,'b> list list)) =
            match inputSources, inputSinks, inputEdges with
            | hSrc :: tSrc, hSnk :: tSnk, hEdg :: tEdg ->
                let sourceValue = Param.getValue hSrc
                let sinkValue = Param.getValue hSnk
                let filledSourceVertices = (sourceValue, hSrc) :: sourceVertices
                let filledSinkVertices = (sinkValue, hSnk) :: sinkVertices
                let filledConnEdges = (hEdg |> List.map (fun e -> sourceValue, sinkValue, e)) :: connectedEdges
                loop tSrc tSnk tEdg filledSourceVertices filledSinkVertices filledConnEdges
            | [], [], [] -> sourceVertices, sinkVertices, connectedEdges
            // the lists must be equally long, since even empty cells should be translated into Params
            | _ -> failwith $"Input lists have different lengths: inputSources: {inputSources.Length}; inputSinks: {inputSinks.Length}; inputEdges: {inputEdges.Length}"
        let sourceVertices, sinkVertices, connectedEdges = loop sources sinks edges [] [] []

        // reverse lists so that the index matches original order – makes it easier to comprehend the output later
        List.rev sourceVertices, List.rev sinkVertices, connectedEdges

    /// <summary>
    /// Initializes an ArrayAdjencyGraph with teh given sources and sinks realized as LVertices and the given edges as LEdges.
    /// </summary>
    let initGraphWithElements sources edges sinks =

        // input edges will be a 2D list in the form of: 1st dim = columns, 2nd dim = rows, but we want to invert that
        let invertedEdges = List.transpose edges

        let sourceVertices, sinkVertices, connectedEdges = buildVerticesAndEdges sources invertedEdges sinks

        // we distinct the vertices since they must be unique so they can be added to a graph
        let combinedVertices = 
            sourceVertices @ sinkVertices
            |> List.distinctBy fst

        Graph.create combinedVertices (List.concat connectedEdges)