namespace ArcGraphModel

open FSharpAux
open FsSpreadsheet
open Param


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


module TableTransform =

    let getDataCellsOf (fcc : FsCellsCollection) (cell : FsCell) = FsCellsCollection.getCellsInColumn cell.ColumnNumber fcc |> Seq.toList |> List.skip 1

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

    let parseNode fcc headerCell =
        let dataCellsVal = getDataCellsOf fcc headerCell
        dataCellsVal
        |> List.map (
            fun dc ->
                //UserParam(headerCell.Value, ParamValue.Value dc.Value)
                CvParam("(n/a)", headerCell.Value, "(n/a)", ParamValue.Value dc.Value)
        )

    type NodeType =
        | Source
        | Sink
        | ProtocolRef

    let getNodeType (cvPar : CvParam<string>) =
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