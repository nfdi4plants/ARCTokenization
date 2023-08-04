namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ARCTokenization.Terms

module Worksheet =

    /// <summary>
    /// Parses a given list of FsCells of a given FsWorksheet via a given tokenization function and returns the resulting IParam list.
    /// </summary>
    /// <param name="cellCollection">a 2D collection of cells, can for example be a list of FsRows or FsColumns</param>
    /// <param name="tokenizationFunction">The tokenization function to use on the inner cell collections (e.g. rows or columns)</param>
    /// <param name="worksheet">The FsWorksheet the FsCells belong to</param>
    let parseCellsWith (cellCollection: #seq<FsCell> list) (tokenizationFunction: (#seq<FsCell> -> IParam list)) (worksheet : FsWorksheet) =
        let sheetName = Address.createWorksheetParam worksheet.Name
        cellCollection
        |> List.map tokenizationFunction 
        |> List.map (
            List.map (fun token ->
                CvAttributeCollection.tryAddAttribute sheetName token |> ignore
                token
            )
        )

    // <summary>
    /// Parses a given list of FsCells of a given FsWorksheet via a given tokenization function and returns the resulting IParam list.
    /// Concatenates the resulting lists of IParams into a single list.
    /// </summary>
    /// <param name="cellCollection">a 2D collection of cells, can for example be a list of FsRows or FsColumns</param>
    /// <param name="tokenizationFunction">The tokenization function to use on the inner cell collections (e.g. rows or columns)</param>
    /// <param name="worksheet">The FsWorksheet the FsCells belong to</param>
    let parseCellsFlatWith (cellCollection: #seq<FsCell> list) (tokenizationFunction: (#seq<FsCell> -> IParam list)) (worksheet : FsWorksheet) =
        parseCellsWith cellCollection tokenizationFunction worksheet
        |> List.concat

    /// Parses rows of a given FsWorksheet via a given tokenization function and returns the resulting IParam list.
    let parseRowsWith tokenizationFunction (worksheet : FsWorksheet) = 
        parseCellsWith (worksheet.Rows) tokenizationFunction worksheet

    /// Parses rows of a given FsWorksheet via a given tokenization function and returns the resulting IParam list.
    /// Concatenates the resulting lists of IParams into a single list.
    let parseRowsFlatWith tokenizationFunction (worksheet : FsWorksheet) = 
        parseCellsFlatWith (worksheet.Rows) tokenizationFunction worksheet

    /// Parses columns of a given FsWorksheet via a given tokenization function and returns the resulting IParam list.
    let parseColumnsWith tokenizationFunction (worksheet : FsWorksheet) = 
        parseCellsWith (Seq.toList worksheet.Columns) tokenizationFunction worksheet

    /// Parses columns of a given FsWorksheet via a given tokenization function and returns the resulting IAttributeCollection list.
    /// Concatenates the resulting lists of IParams into a single list.
    let parseColumnsFlatWith tokenizationFunction (worksheet : FsWorksheet) = 
        parseCellsFlatWith (Seq.toList worksheet.Columns) tokenizationFunction worksheet