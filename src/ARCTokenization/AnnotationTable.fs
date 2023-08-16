namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet

open System


module internal List =   // remove as soon as this is available in next F#Aux NuGet release
    let map4 (mapping : 'T -> 'T -> 'T -> 'T -> 'U) (list1 : 'T list) (list2 : 'T list) (list3 : 'T list) (list4 : 'T list) =
        if list1.Length <> list2.Length || list1.Length <> list3.Length || list1.Length <> list4.Length then
            failwithf "The input lists have different lengths.\n\tlist1.Length = %i; list2.Length = %i; list3.Length = %i; list4.Length = %i" list1.Length list2.Length list3.Length list4.Length
        let rec loop acc nl1 nl2 nl3 nl4 =
            match nl1, nl2, nl3, nl4 with
            | h1 :: t1, h2 :: t2, h3 :: t3, h4 :: t4 ->
                loop (mapping h1 h2 h3 h4 :: acc) t1 t2 t3 t4
            | _ -> List.rev acc
        loop [] list1 list2 list3 list4

    let inline transposeOrdinary (lists : seq<'T list>) =
        if lists |> Seq.forall (fun t -> t.Length <> (Seq.head lists).Length) then
            failwith "Input lists have different lengths."
        List.init (Seq.head lists).Length (
            fun i ->
                List.init (Seq.length lists) (
                    fun j -> (Seq.item j lists)[i]
                )
        )


/// <summary>
/// Functions to work with FsTables into graph-based models.
/// </summary>
module AnnotationTable =

    // -----
    // TYPES
    // -----

    type TokenizedAnnotationTable = {
        IOColumns : CvParam list list
        TermRelatedBuildingBlocks : CvParam list list
    } with
        static member create io terms =
            {
                IOColumns = io
                TermRelatedBuildingBlocks = terms
            }

    /// <summary>
    /// Modelling of the different types of nodes / Building Blocks.
    /// </summary>
    [<Obsolete>]
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
    [<Obsolete>]
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
    /// Takes an FsWorkbook and returns all Annotation Tables it contains.
    /// </summary>
    let getAnnotationTables workbook =
        let tables = FsWorkbook.getTables workbook |> List.ofArray
        tables |> List.filter (fun t -> String.contains "annotationTable" t.Name)

    /// <summary>
    /// Returns all header cells from a given FsCellsCollection and a given FsTable.
    /// </summary>
    let getHeaderCellsOf (fcc : FsCellsCollection) (table : FsTable) =
        table.Cells fcc
        |> Seq.filter (fun c -> c.RowNumber = table.RangeAddress.FirstAddress.RowNumber)

    /// <summary>
    /// Returns all data cells from a given header cell by using a given FsCellsCollection and a given FsTable.
    /// </summary>
    let getDataCellsOf (fcc : FsCellsCollection) (table : FsTable) (headerCell : FsCell) = 
        let headerColIndex = 
            fcc.GetCells() 
            |> Seq.find (fun t -> t.Value = headerCell.Value) 
            |> fun c -> c.Address.ColumnNumber
        table.GetDataCellsOfColumnAt(fcc, headerColIndex)
        //FsCellsCollection.getCellsInColumn headerCell.ColumnNumber fcc 
        |> Seq.toList 
        //|> List.skip 1

    /// <summary>
    /// Takes a list of header cells and splits them into a tuple of IO column header cells and Term-related Building Block header cells by using a given FsCellsCollection and a given FsTable.
    /// </summary>
    let splitColumns (table : FsTable) (fcc : FsCellsCollection) (cl : FsCell list) =
        cl
        |> List.partition (fun c -> List.contains c.Value nodeColumnNames)

    /// <summary>
    /// Takes a list of header cells from Term-related Building Blocks and groups them into a list of Building Block units.
    /// 
    /// (1 inner list = 1 Building Block unit)
    /// </summary>
    let groupTermRelatedBuildingBlocks (table : FsTable) (fcc : FsCellsCollection) (cl : FsCell list) =
        cl 
        |> Seq.groupWhen (fun h -> String.contains "[" h.Value) 
        |> List.ofSeq 
        |> List.map List.ofSeq

    /// <summary>
    /// Takes a list of header cells and an FsCellsCollections and returns a list of CvParams according to the information from the FsCells.
    /// If `crStart` is true, it is assumed that the first header cell is a Term containing header cell of a Building Block.
    /// 
    /// This function should only be used for parsing Term-related Building Blocks.
    /// </summary>
    let parseTermRelatedBuildingBlocks crStart (table : FsTable) (fcc : FsCellsCollection) (cl : FsCell list) =
        //let empty() = FsCell.createEmpty ()
        //let getTableFieldOf (table : FsTable) (cell : FsCell) =
        //    table.Fields(fcc) |> ignore

        let rec loop roundOne (cells : FsCell list) = 
            [
                match cells with
                // Case: Correct Quadruplet of headers Name, Unit, TSR, TAN
                | a :: b :: c :: d :: rest when roundOne && (String.startsWith "Unit" b.Value) ->
                    // a = Value/Name header, b = Unit header, c = TermSourceRef header, d = TermAccessionNumber header
                    //let tfa = FsTableField(a.Value, a.ColumnNumber, )
                    //FsTableField.getDataCells fcc true 
                    let dataCellsVal = getDataCellsOf fcc table a
                    let dataCellsUnt = getDataCellsOf fcc table b
                    let dataCellsTsr = getDataCellsOf fcc table c
                    let dataCellsTan = getDataCellsOf fcc table d
                    let cvPars =
                        List.map4 (
                            fun (vl : FsCell) unt tan tsr -> 
                                let valTerm = CvUnit.create(accession = tan.Value, name = vl.Value, ref = tsr.Value)
                                CvParam(d.Value, a.Value, c.Value, WithCvUnitAccession (unt.Value, valTerm))
                        ) dataCellsVal dataCellsUnt dataCellsTan dataCellsTsr 
                    yield! cvPars
                    yield! loop false rest
                // Case: Correct Triplet of headers Name, Unit, TSR, TAN
                | a :: b :: c :: rest when roundOne ->
                    // a = Value/Name header, b = TermSourceRef header, c = TermAccessionNumber header
                    let dataCellsVal = getDataCellsOf fcc table a
                    let dataCellsTsr = getDataCellsOf fcc table b
                    let dataCellsTan = getDataCellsOf fcc table c
                    let cvPars =
                        (dataCellsVal, dataCellsTsr, dataCellsTan)
                        |||> List.map3 (
                            fun vl tsr tan ->
                                let valTerm = CvTerm.create(accession = tan.Value, name = vl.Value, ref = tsr.Value)
                                CvParam(c.Value, a.Value, b.Value, CvValue valTerm)
                        )
                    yield! cvPars
                    yield! loop false rest
                // Case: Incorrect Duplet of headers Name, TSR/TAN, or headers TSR, TAN
                | a :: b :: rest ->
                    match roundOne with
                    | true  ->
                        // a = Value/Name header, b = TermSourceRef header (assumed, could also be TermAccessionNumber header if TSR column is missing)
                        let dataCellsVal = getDataCellsOf fcc table a
                        let dataCellsTsr = getDataCellsOf fcc table b
                        let cvPars =
                            (dataCellsVal, dataCellsTsr)
                            ||> List.map2 (
                                fun vl tsr ->
                                    let valTerm = CvTerm.create (accession = "(n/a)", name = vl.Value, ref = tsr.Value)
                                    CvParam("n/a", a.Value, b.Value, CvValue valTerm)
                            )
                        yield! cvPars
                        yield! loop false rest
                    | false ->
                        // a = TermSourceRef header, b = TermAccessionNumber header
                        let dataCellsTsr = getDataCellsOf fcc table a
                        let dataCellsTan = getDataCellsOf fcc table b
                        let cvPars =
                            (dataCellsTsr, dataCellsTan)
                            ||> List.map2 (
                                fun tsr tan ->
                                    let valTerm = CvTerm.create(accession = tan.Value, name = "n/a", ref = tsr.Value)
                                    CvParam(b.Value, "(n/a)", a.Value, CvValue valTerm)
                            )
                        yield! cvPars
                        yield! loop false rest
                // Case: Incorrect Singlet of only header Name or header TSR/TAN
                | a :: [] ->
                    match roundOne with
                    | true  ->
                        // a = Value/Name header
                        let dataCellsVal = getDataCellsOf fcc table a
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
                        let dataCellsTsr = getDataCellsOf fcc table a
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
    /// This function should only be used for parsing input-, output-, and featured columns.
    /// </summary>
    let parseIOColumns fcc table headerCell =
        let dataCellsVal = getDataCellsOf fcc table headerCell
        dataCellsVal
        |> List.map (
            fun dc ->
                //UserParam(headerCell.Value, ParamValue.Value dc.Value)
                CvParam("(n/a)", headerCell.Value, "(n/a)", ParamValue.Value dc.Value)
        )

    /// <summary>
    /// Takes an FsWorkbook and returns a list of worksheet names and their respective IO columns as CvParam lists and Term-related Building Blocks as CvParam lists.
    /// 
    /// (inner CvParam list: CvParams of a column, outer CvParam list: all columns in a worksheet's AnnotationTable)
    /// </summary>
    let parseWorkbook wb =
        let tables = getAnnotationTables wb
        let worksheets = wb.GetWorksheets()
        // get worksheet and its AnnotationTable as tuple
        let worksheetsAndTables =
            tables
            |> Seq.map (
                fun t ->
                    let associatedWs = 
                        worksheets
                        |> Seq.find (
                            fun ws -> 
                                ws.Tables
                                |> Seq.exists (fun t2 -> t2.Name = t.Name)
                        )
                    associatedWs, t
            )
        worksheetsAndTables
        |> Seq.map (
            fun (ws,t) ->
                let ioHeaderCells, termRelatedBuildingBlockHeaderCells = 
                    getHeaderCellsOf ws.CellCollection t 
                    |> List.ofSeq
                    |> splitColumns t ws.CellCollection
                let ioColumns = 
                    ioHeaderCells
                    |> List.map (parseIOColumns ws.CellCollection t)
                let termRelatedBuildingBlocks =
                    termRelatedBuildingBlockHeaderCells
                    |> groupTermRelatedBuildingBlocks t ws.CellCollection
                    |> List.map (parseTermRelatedBuildingBlocks true t ws.CellCollection)
                ws.Name,
                TokenizedAnnotationTable.create ioColumns termRelatedBuildingBlocks
        )
        |> List.ofSeq

    /// <summary>
    /// Takes a CvParam and returns the type of Node it contains.
    /// </summary>
    [<Obsolete>]
    let getNodeType (cvPar : #IParamBase) =
        //let castedCvPar = cvPar :?> CvParam<string>     // debatable approach
        //let v = Param.getCvName castedCvPar
        let v = CvBase.getCvName cvPar
        match v with
        | "Source Name" -> Source
        | "Sample Name"
        | "Raw Data File"
        | "Derived Data File" -> Sink
        | "Protocol REF"
        | "Protocol Type" -> ProtocolRef
        | _ -> failwith $"HeaderCell {v} cannot be parsed to any NodeType."