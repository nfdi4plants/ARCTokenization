#r "nuget: FSharpAux"
#r "nuget: DocumentFormat.OpenXml"
//#r @"C:/Users/olive/.nuget/packages/fsharpaux/1.1.0/lib/net5.0/FSharpAux.dll"

open DocumentFormat.OpenXml
open FSharpAux
open System.IO

let dllBasePath = @"c:/repos/csbiology/fsspreadsheet/src"
File.Copy(dllBasePath + "/FsSpreadsheet/bin/Debug/netstandard2.0/FsSpreadsheet.dll", dllBasePath + "/FsSpreadsheet/bin/Debug/netstandard2.0/FsSpreadsheet_Copy.dll", true)
File.Copy(dllBasePath + "/FsSpreadsheet.CsvIO/bin/Debug/netstandard2.0/FsSpreadsheet.CsvIO.dll", dllBasePath + "/FsSpreadsheet.CsvIO/bin/Debug/netstandard2.0/FsSpreadsheet.CsvIO_Copy.dll", true)
File.Copy(dllBasePath + "/FsSpreadsheet.ExcelIO/bin/Debug/netstandard2.0/FsSpreadsheet.ExcelIO.dll", dllBasePath + "/FsSpreadsheet.ExcelIO/bin/Debug/netstandard2.0/FsSpreadsheet.ExcelIO_Copy.dll", true)
File.Copy(@"C:\Repos\nfdi4plants\ArcGraphModel\src\ArcGraphModel\bin\Debug\net6.0\ArcGraphModel.dll", @"C:\Repos\nfdi4plants\ArcGraphModel\src\ArcGraphModel\bin\Debug\net6.0\ArcGraphModel_Copy.dll", true)

#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet/bin/Debug/netstandard2.0/FsSpreadsheet_Copy.dll"
#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet.CsvIO/bin/Debug/netstandard2.0/FsSpreadsheet.CsvIO_Copy.dll"
#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet.ExcelIO/bin/Debug/netstandard2.0/FsSpreadsheet.ExcelIO_Copy.dll"
#r @"C:\Repos\nfdi4plants\ArcGraphModel\src\ArcGraphModel\bin\Debug\net6.0\ArcGraphModel.dll"


open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open FsSpreadsheet.DSL


let fp = @"C:\Users\olive\OneDrive\CSB-Stuff\NFDI\testARC30\assays\aid\isa.assay.xlsx"
let wb = FsWorkbook.fromXlsxFile fp
let shts = FsWorkbook.getWorksheets wb

let tbls = FsWorkbook.getTables wb
let tblsFiltered = tbls |> List.filter (fun t -> String.contains "annotationTable" t.Name)
let tbl1 = tblsFiltered.Head
// TO DO: make this dynamic: add a getWorksheetOfTable function to FsTable
let associatedWorksheet = shts.Head
let fcc = associatedWorksheet.CellCollection
tbl1.Field("Source Name", fcc).DataCells(fcc, false) |> Seq.length
tbl1.Field("Source Name", fcc).DataCells(fcc, true)

/// Names that are excluded.
let nodeColumnNames = [
    "Source Name"
    "Sample Name"
    "Raw Data File"
    "Derived Data File"
    "Protocol Type"
    "Protocol REF"
]

tbl1.RescanRange()
tbl1.Field("test", fcc).DataCells(fcc, false)
//tbl1.FieldNames

let columnHeadersRowAddress = tbl1.HeadersRow().RangeAddress.FirstAddress.RowNumber
let columnHeaders = associatedWorksheet.CellCollection.GetCellsInRow columnHeadersRowAddress |> List.ofSeq
let headersFiltered = columnHeaders |> List.filter (fun c -> List.contains c.Value nodeColumnNames |> not)
let groupedHeaders = headersFiltered |> Seq.groupWhen (fun h -> String.contains "[" h.Value) |> List.ofSeq |> List.map List.ofSeq
//let groupedHeadersStr = 

let headers = tbl1.Field("Source Name", fcc)
//headers.DataCells(fcc, true)
headers.DataCells(fcc, false)

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

let parse2 crStart (cl : FsCell list) =
    let empty() = FsCell.createEmpty ()
    let rec loop roundOne s = 
        [
            match s with
            | a :: b :: c :: rest when roundOne ->
                yield (a, b, c)
                yield! loop false rest
            | a :: b :: rest ->
                match roundOne with
                | true  ->
                    yield (a, b, empty())
                    yield! loop false rest
                | false ->
                    yield (empty(), a, b)
                    yield! loop false rest
            | a :: [] ->
                match roundOne with
                | true  ->
                    yield (a, empty(), empty())
                | false ->
                    yield (empty(), a, empty())
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


let getKvTriplets (headerTriplets : FsCell [] []) (workbook : FsWorkbook) (table : FsTable) = 
    let workbook = wb
    let table = tbl1
    let headerTriplets = groupedHeaders

    let associatedWorksheet = table.GetWorksheetOfTable workbook
    let columnHeadersRowAddress = table.HeadersRow().RangeAddress.FirstAddress.RowNumber
    //let columnHeadersColAddress = 
    //let columnHeadersValues = table.FieldNames(associatedWorksheet.CellCollection)
    let columnHeadersValues = 
        headerTriplets
        |> Array.map (
            Array.map (
                fun c ->
                    table.Field(c.Value, associatedWorksheet.CellCollection)
            )
        )

    let mutable tripleCounter = 0
    let mutable doubleCounter = 0
    //let raiseTc () = 
    //    if tripleCounter = 3 then tripleCounter <- 1
    //    else tripleCounter <- tripleCounter + 1
    let raiseDc () =
        if doubleCounter = 2 then doubleCounter <- 1
        else doubleCounter <- doubleCounter + 1
    let satisfier = String.contains "["

    headerTriplets
    |> Array.map (
        fun ht ->
            ht
            |> Array.map (
                fun h ->
                    if satisfier h then tripleCounter <- 1
                    else tripleCounter <- tripleCounter + 1
                    match tripleCounter with
                    | 1 -> 
                    | x when x <= 3 && x > 1 ->
                    | x when x > 3 ->
                        raiseDc ()

            )
    )

    let rec loop tripleCounter doubleCounter =
        if en.MoveNext() then
            let e = en.Current
            let updatedTripleCounter = tripleCounter + 1
            let colI = cellsColl.TryGetCell 

        else 

    //headerTriplets
    //|> Seq.map (
    //    fun seq1 ->
    //        use en = input.GetEnumerator()
    //        loop id
    //)
    0



// let rec sheesh tripleCounter doubleCounter 