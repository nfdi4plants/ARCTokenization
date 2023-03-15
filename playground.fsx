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


// TO DO: put this into FsExtensions.FsWorkbook
type FsWorkbook with
    /// Takes the path to an Xlsx file and returns the FsWorkbook based on its content.
    static member fromXlsxFile (filePath : string) =
        let sr = new StreamReader(filePath)
        let wb = FsWorkbook.fromXlsxStream sr.BaseStream
        sr.Close()
        wb

let fp = @"C:\Users\olive\OneDrive\CSB-Stuff\NFDI\testARC30\assays\aid\isa.assay.xlsx"
let workbook = FsWorkbook.fromXlsxFile fp
let sheets = FsWorkbook.getWorksheets workbook

// TO DO: put this into FsWorkbook
type FsWorkbook with
    /// <summary>Returns all FsTables from the FsWorkbook.</summary>
    member self.GetTables() =
        self.GetWorksheets()
        |> List.collect (fun s -> s.Tables)

    /// <summary>Returns all FsTables from an FsWorkbook.</summary>
    static member getTables (workbook : FsWorkbook) =
        workbook.GetTables()

let tables = FsWorkbook.getTables workbook
let tablesFiltered = tables |> List.filter (fun t -> String.contains "annotationTable" t.Name)
let table1 = tablesFiltered.Head
// TO DO: make this dynamic: add a getWorksheetOfTable function to FsTable
let associatedWorksheet = sheets.Head

/// Names that are excluded.
let nodeColumnNames = [
    "Source Name"
    "Sample Name"
    "Raw Data File"
    "Derived Data File"
    "Protocol Type"
    "Protocol REF"
]

let columnHeadersRowAddress = table1.HeadersRow().RangeAddress.FirstAddress.RowNumber
let columnHeaders = associatedWorksheet.CellCollection.GetCellsInRow columnHeadersRowAddress |> Array.ofSeq
let headersFiltered = columnHeaders |> Array.choose (fun c -> if List.contains c.Value nodeColumnNames |> not then c.Value |> Option.Some else None)
let groupedHeaders = Seq.groupWhen (fun h -> String.contains "[" h) headersFiltered |> Array.ofSeq |> Array.map Array.ofSeq

let getKvTriplets (headerTriplets : seq<seq<string>>) (workbook : FsWorkbook) (table : FsTable) = 
    let columnHeadersRowAddress = table.HeadersRow().RangeAddress.FirstAddress.RowNumber
    let associatedWorksheet = FsWorkbook.getWorksheets() |> List.find (fun s -> s.Tables |> List.find (fun t -> t.Name = table.Name))
    let rec loop tripleCounter doubleCounter =
        if en.MoveNext() then
            let e = en.Current
            let updatedTripleCounter = tripleCounter + 1
            let colI = cellsColl.TryGetCell 

        else 

    headerTriplets
    |> Seq.map (
        fun seq1 ->
            use en = input.GetEnumerator()
            loop id
    )



// let rec sheesh tripleCounter doubleCounter 