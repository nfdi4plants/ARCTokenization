namespace ArcGraphModel

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet


module Worksheet =

    /// Parses a given list of FsCells of a given FsWorksheet via a given tokenization function and returns the resulting IAttributeCollection list.
    let parseCells cellsList tokenizationFunction (worksheet : FsWorksheet) =
        let sheetName = Address.createWorksheetParam worksheet.Name
        cellsList
        |> List.choose (fun r -> 
            match tokenizationFunction r |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat
        |> List.map (fun token ->
            CvAttributeCollection.tryAddAttribute sheetName token |> ignore
            token
        )

    /// Parses rows of a given FsWorksheet via a given tokenization function and returns the resulting IAttributeCollection list.
    let parseRows tokenizationFunction (worksheet : FsWorksheet) = 
        parseCells (worksheet.Rows) tokenizationFunction worksheet

    /// Parses rows of a given FsWorksheet and returns the resulting aggregated ICvBase list.
    let parseRowsAggregated (worksheet : FsWorksheet) = 
        parseRows Tokenization.parseLine worksheet

    /// Parses rows of a given FsWorksheet and returns the resulting flat IParam list.
    let parseRowsFlat (worksheet : FsWorksheet) =
        parseRows Tokenization.convertTokens worksheet

    /// Parses columns of a given FsWorksheet via a given tokenization function and returns the resulting IAttributeCollection list.
    let parseColumns tokenizationFunction (worksheet : FsWorksheet) = 
        parseCells (Seq.toList worksheet.Columns) tokenizationFunction worksheet

    /// Parses columns of a given FsWorksheet and returns the resulting aggregated ICvBase list.
    let parseColumnsAggregated (worksheet : FsWorksheet) =
        parseColumns Tokenization.parseLine worksheet

    /// Parses columns of a given FsWorksheet and returns the resulting flat IParam list.
    let parseColumnsFlat (worksheet : FsWorksheet) =
        parseColumns Tokenization.convertTokens worksheet

    /// Parses the columns of the first FsTable in a given FsWorksheet via a given tokenization function and returns the resulting IAttributeCollection list.
    let parseTableColumns tokenizationFunction (worksheet : FsWorksheet) = 
        parseCells (worksheet.Tables.Head.GetColumns(worksheet.CellCollection) |> Seq.toList) tokenizationFunction worksheet

    /// Parses the columns of the first FsTable in a given FsWorksheet and returns the resulting aggregated ICvBase list.
    let parseTableColumnsAggregated (worksheet : FsWorksheet) =
        parseTableColumns Tokenization.parseLine worksheet

    /// Parses the columns of the first FsTable in a a given FsWorksheet and returns the resulting flat IParam list.
    let parseTableColumnsFlat (worksheet : FsWorksheet) =
        parseTableColumns Tokenization.convertTokens worksheet