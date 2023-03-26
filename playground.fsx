open System.IO
open System.Collections.Generic

let dllBasePath = @"c:/repos/csbiology/fsspreadsheet/src"
File.Copy(dllBasePath + "/FsSpreadsheet/bin/Debug/netstandard2.0/FsSpreadsheet.dll", dllBasePath + "/FsSpreadsheet/bin/Debug/netstandard2.0/FsSpreadsheet_Copy.dll", true)
File.Copy(dllBasePath + "/FsSpreadsheet.CsvIO/bin/Debug/netstandard2.0/FsSpreadsheet.CsvIO.dll", dllBasePath + "/FsSpreadsheet.CsvIO/bin/Debug/netstandard2.0/FsSpreadsheet.CsvIO_Copy.dll", true)
File.Copy(dllBasePath + "/FsSpreadsheet.ExcelIO/bin/Debug/netstandard2.0/FsSpreadsheet.ExcelIO.dll", dllBasePath + "/FsSpreadsheet.ExcelIO/bin/Debug/netstandard2.0/FsSpreadsheet.ExcelIO_Copy.dll", true)
File.Copy(@"C:\Repos\nfdi4plants\ArcGraphModel\src\ArcGraphModel\bin\Debug\net6.0\ArcGraphModel.dll", @"C:\Repos\nfdi4plants\ArcGraphModel\src\ArcGraphModel\bin\Debug\net6.0\ArcGraphModel_Copy.dll", true)

#r "nuget: DocumentFormat.OpenXml"
#r "nuget: FSharpAux"
//#r @"C:/Users/olive/.nuget/packages/fsharpaux/1.1.0/lib/net5.0/FSharpAux.dll"

open DocumentFormat.OpenXml
open FSharpAux


#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet/bin/Debug/netstandard2.0/FsSpreadsheet_Copy.dll"
#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet.CsvIO/bin/Debug/netstandard2.0/FsSpreadsheet.CsvIO_Copy.dll"
#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet.ExcelIO/bin/Debug/netstandard2.0/FsSpreadsheet.ExcelIO_Copy.dll"
#r @"C:\Repos\nfdi4plants\ArcGraphModel\src\ArcGraphModel\bin\Debug\net6.0\ArcGraphModel.dll"

open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open FsSpreadsheet.DSL
open ArcGraphModel.CvParam


//let fp = @"C:\Users\olive\OneDrive\CSB-Stuff\NFDI\testARC30\assays\aid\isa.assay.xlsx"
let fp = @"C:\Users\revil\OneDrive\CSB-Stuff\NFDI\testARC30\assays\aid\isa.assay.xlsx"
let wb = FsWorkbook.fromXlsxFile fp
let shts = FsWorkbook.getWorksheets wb

//let mutable testDic = new Dictionary<string,int>()
//testDic.Add("first", 24)
//testDic.Add("second", 1337)
//testDic.Add("third", 69)
//let oldTestDic = testDic
//testDic <- new Dictionary<string,int>()
//let testDic2 = new Dictionary<int,Dictionary<int,string>>()
//let testDic3 = new Dictionary<int,string>()
//testDic3.Add(3,"sheesh")
//testDic3.Add(4,"shnash")
//testDic2.Add(2,testDic3)
//testDic2.Add(6,testDic3)
//testDic2.Values |> Seq.minBy (fun d -> d.Keys |> Seq.min) |> fun d -> d.Keys |> Seq.min

let tbls = FsWorkbook.getTables wb
let tblsFiltered = tbls |> List.filter (fun t -> String.contains "annotationTable" t.Name)
let tbl1 = tblsFiltered.Head
let associatedWorksheet = shts.Head
let fcc = associatedWorksheet.CellCollection
//tbl1.Field("Source Name", fcc).DataCells(fcc, false) |> Seq.length
//tbl1.Field("Source Name", fcc).DataCells(fcc, true)

/// Names that are excluded.
let nodeColumnNames = [
    "Source Name"
    "Sample Name"
    "Raw Data File"
    "Derived Data File"
    "Protocol Type"
    "Protocol REF"
]

//tbl1.RescanRange()
//tbl1.Field("test", fcc).DataCells(fcc, false)
//tbl1.FieldNames

let columnHeadersRowAddress = tbl1.HeadersRow().RangeAddress.FirstAddress.RowNumber
let columnHeaders = associatedWorksheet.CellCollection.GetCellsInRow columnHeadersRowAddress |> List.ofSeq
let headersFiltered = columnHeaders |> List.filter (fun c -> List.contains c.Value nodeColumnNames |> not)
let groupedHeaders = headersFiltered |> Seq.groupWhen (fun h -> String.contains "[" h.Value) |> List.ofSeq |> List.map List.ofSeq
//let groupedHeadersStr = 

//let ftf = FsTableField()
//let header1 = tbl1.Field("Source Name", fcc)
//header1.DataCells(fcc, true)
//header1.DataCells(fcc, false) |> Seq.length
//header1.Index
//let header2 = tbl1.Field("Parameter [Protocol]", fcc)
//header2.Index
//tbl1.RescanRange()
//tbl1.FieldNames(obj)

// timoCode
let parse crStart (strl : string list) =
    let rec loop (roundOne) s = 
        [
            match s with
            | a :: b :: c :: rest when roundOne ->
                yield (a,b,c)
                yield! loop false rest
            | a :: b :: rest ->
                match roundOne with
                | true  ->
                    yield (a, b, "na")
                    yield! loop false rest
                | false ->
                    yield ("na", a, b)
                    yield! loop false rest
            | a :: [] ->
                match roundOne with
                | true  ->
                    yield (a, "na", "na")
                | false ->
                    yield ("na" ,a, "na")
            | [] -> ()
        ]
    loop crStart strl

//parse true (groupedHeaders |> Array.collect (Array.map (fun c -> c.Value)) |> List.ofArray)
//groupedHeaders |> Array.map (parse true (groupedHeaders |> Array.collect (Array.map (fun c -> c.Value)) |> List.ofArray))

//// for FsCells & CvParams
//type BuildingBlock<'a> =
//    | Triple of ('a * 'a * 'a)
//    | Quadruple of ('a * 'a * 'a * 'a)

module List =
    let map4 (mapping : 'T -> 'T -> 'T -> 'T -> 'U) (list1 : 'T list) (list2 : 'T list) (list3 : 'T list) (list4 : 'T list) =
        if list1.Length <> list2.Length || list1.Length <> list3.Length || list1.Length <> list4.Length then
            failwithf "The input lists have different lengths.\n\tlist1.Length = %i; list2.Length = %i; list3.Length = %i; list4.Length = %i" list1.Length list2.Length list3.Length list4.Length
        let rec loop acc nl1 nl2 nl3 nl4 =
            match nl1, nl2, nl3, nl4 with
            | h1 :: t1, h2 :: t2, h3 :: t3, h4 :: t4 ->
                loop (mapping h1 h2 h3 h4 :: acc) t1 t2 t3 t4
            | _ -> List.rev acc
        loop [] list1 list2 list3 list4

let parse2 crStart (cl : FsCell list) =
    let empty() = FsCell.createEmpty ()
    let getDataCellsOf (cell : FsCell) = FsCellsCollection.getCellsInColumn cell.ColumnNumber fcc |> Seq.toList
    let rec loop roundOne (s : FsCell list) = 
        [
            match s with
            | a :: b :: c :: d :: rest when roundOne && (String.startsWith "Unit" b.Value) ->
                let dataCellsVal = getDataCellsOf a
                let dataCellsUnt = getDataCellsOf b
                let dataCellsTsr = getDataCellsOf c
                let dataCellsTan = getDataCellsOf d
                let cvPars =
                    List.map4 (
                        fun (vl : FsCell) unt tan tsr -> 
                            let valTerm = CvUnit(tan.Value, vl.Value, tsr.Value)
                                //CvTerm(dc.Value)
                            //CvParam(d.Value, a.Value, c.Value, CvValue valTerm)
                            CvParam(d.Value, a.Value, c.Value, WithCvUnitAccession (unt.Value, valTerm))
                    ) dataCellsVal dataCellsUnt dataCellsTan dataCellsTsr
                yield cvPars
                yield! loop false rest
            | a :: b :: c :: rest when roundOne ->
                let dataCellsVal = getDataCellsOf a
                let dataCellsTsr = getDataCellsOf b
                let dataCellsTan = getDataCellsOf c
                let cvPars =
                    (dataCellsVal, dataCellsTsr, dataCellsTan)
                    |||> List.map3 (
                        fun vl tsr tan ->
                            let valTerm = CvTerm(tan.Value, vl.Value, tsr.Value)
                            CvParam(c.Value, a.Value, b.Value, CvValue valTerm)
                    )
                yield cvPars
                yield! loop false rest
            //| a :: b :: rest ->
            //    match roundOne with
            //    | true  ->
            //        yield (a, b, empty())
            //        yield! loop false rest
            //    | false ->
            //        yield (empty(), a, b)
            //        yield! loop false rest
            //| a :: [] ->
            //    match roundOne with
            //    | true  ->
            //        yield (a, empty(), empty())
            //    | false ->
            //        yield (empty(), a, empty())
            | [] -> ()
        ]
    loop crStart cl

let t2 = tbls.Item 1
let assWs2 = FsTable.getWorksheetOfTable wb t2
let columnHeadersRowAddress2 = t2.HeadersRow().RangeAddress.FirstAddress.RowNumber
let columnHeaders2 = assWs2.CellCollection.GetCellsInRow columnHeadersRowAddress2 |> Array.ofSeq
let headersFiltered2 = columnHeaders2 |> Array.filter (fun c -> List.contains c.Value nodeColumnNames |> not)
let groupedHeaders2 = headersFiltered2 |> Seq.groupWhen (fun h -> String.contains "[" h.Value) |> List.ofSeq |> List.map List.ofSeq

let res = groupedHeaders2 |> List.map (parse2 true)
let antiEmptyChecker str = if str = "" then "(empty)" else str
res 
|> List.collect (
    List.map (fun (c1,c2,c3) -> 
        (antiEmptyChecker c1.Value, antiEmptyChecker c2.Value, antiEmptyChecker c3.Value)
        |> fun (c1n,c2n,c3n) -> 
            printfn "%s    %s    %s" c1n c2n c3n; c1n, c2n, c3n)
)

assWs2.CellCollection.GetCellsInColumn(11)


//let getKvTriplets (headerTriplets : FsCell [] []) (workbook : FsWorkbook) (table : FsTable) = 
//    let workbook = wb
//    let table = tbl1
//    let headerTriplets = groupedHeaders

//    let associatedWorksheet = table.GetWorksheetOfTable workbook
//    let columnHeadersRowAddress = table.HeadersRow().RangeAddress.FirstAddress.RowNumber
//    //let columnHeadersColAddress = 
//    //let columnHeadersValues = table.FieldNames(associatedWorksheet.CellCollection)
//    let columnHeadersValues = 
//        headerTriplets
//        |> Array.map (
//            Array.map (
//                fun c ->
//                    table.Field(c.Value, associatedWorksheet.CellCollection)
//            )
//        )

//    let mutable tripleCounter = 0
//    let mutable doubleCounter = 0
//    //let raiseTc () = 
//    //    if tripleCounter = 3 then tripleCounter <- 1
//    //    else tripleCounter <- tripleCounter + 1
//    let raiseDc () =
//        if doubleCounter = 2 then doubleCounter <- 1
//        else doubleCounter <- doubleCounter + 1
//    let satisfier = String.contains "["

//    headerTriplets
//    |> Array.map (
//        fun ht ->
//            ht
//            |> Array.map (
//                fun h ->
//                    if satisfier h then tripleCounter <- 1
//                    else tripleCounter <- tripleCounter + 1
//                    match tripleCounter with
//                    | 1 -> 
//                    | x when x <= 3 && x > 1 ->
//                    | x when x > 3 ->
//                        raiseDc ()

//            )
//    )

//    let rec loop tripleCounter doubleCounter =
//        if en.MoveNext() then
//            let e = en.Current
//            let updatedTripleCounter = tripleCounter + 1
//            let colI = cellsColl.TryGetCell 

//        else 

    //headerTriplets
    //|> Seq.map (
    //    fun seq1 ->
    //        use en = input.GetEnumerator()
    //        loop id
    //)
    0



// let rec sheesh tripleCounter doubleCounter 