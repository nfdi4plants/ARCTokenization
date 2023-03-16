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

#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet/bin/Debug/netstandard2.0/FsSpreadsheet_Copy.dll"
#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet.CsvIO/bin/Debug/netstandard2.0/FsSpreadsheet.CsvIO_Copy.dll"
#r "c:/repos/csbiology/fsspreadsheet/src/FsSpreadsheet.ExcelIO/bin/Debug/netstandard2.0/FsSpreadsheet.ExcelIO_Copy.dll"


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

/// Names that are excluded.
let nodeColumnNames = [
    "Source Name"
    "Sample Name"
    "Raw Data File"
    "Derived Data File"
    "Protocol Type"
    "Protocol REF"
]

let columnHeadersRowAddress = tbl1.HeadersRow().RangeAddress.FirstAddress.RowNumber
let columnHeaders = associatedWorksheet.CellCollection.GetCellsInRow columnHeadersRowAddress |> Array.ofSeq
let headersFiltered = columnHeaders |> Array.choose (fun c -> if List.contains c.Value nodeColumnNames |> not then c.Value |> Option.Some else None)
let groupedHeaders = Seq.groupWhen (fun h -> String.contains "[" h) headersFiltered |> Array.ofSeq |> Array.map Array.ofSeq

let headers = tbl1.Field("Source Name", fcc)
headers.DataCells(fcc, true)

// possibly not needed due to FsTableFields
//type FsTable with
//    member this.GetColCellsOfHeader

///// <summary>Takes a header FsCell, an FsTable and the FsCellsCollection of the associated FsWorksheet and returns all FsCells below the header.</summary>
//// TO DO: put into FsTable
//// alternatively: add FsCellsCollection to FsTable (like done in FsRow) which facilitates working 
//// with the associated FsCells but makes it more difficult to maintain: FsWorksheet will need a 
//// `.RescanTables` method which does the same like to its FsRows.
//let getColCellsOfHeader (header : FsCell) (cellsColl : FsCellsCollection) (table : FsTable) =
//    let headerColIndex = header.ColumnNumber
//    let firstValueRow = table.RangeAddress.FirstAddress.RowNumber + 1
//    let lastValueRow = table.RangeAddress.LastAddress.RowNumber
//    cellsColl.GetCells(firstValueRow, headerColIndex, lastValueRow, headerColIndex)

let getKvTriplets (headerTriplets : string [] []) (workbook : FsWorkbook) (table : FsTable) = 
    let workbook = wb
    let table = tbl1
    let headerTriplets = groupedHeaders

    let associatedWorksheet = table.GetWorksheetOfTable workbook
    let columnHeadersRowAddress = table.HeadersRow().RangeAddress.FirstAddress.RowNumber
    //let columnHeadersColAddress = 

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