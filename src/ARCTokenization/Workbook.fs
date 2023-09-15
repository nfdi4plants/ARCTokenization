namespace ARCTokenization


open FSharpAux
open FsSpreadsheet


/// Functions to parse and tokenize FsWorkbooks.
module Workbook =

    /// Returns the metadata worksheet from an Investigation. If `useLastSheetOnIncorrectName` is true, returns the last sheet if no worksheet with the name `Investigation` or `isa_investigation` can be found.
    let getInvestigationMetadataSheet (useLastSheetOnIncorrectName : bool) investigation =
        match FsWorkbook.tryGetWorksheetByName "Investigation" investigation with
        | Some ws -> ws
        | None ->
            match FsWorkbook.tryGetWorksheetByName "isa_investigation" investigation with
            | Some ws -> ws
            | None ->
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets investigation |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith "No worksheet named 'Investigation' or 'isa_investigation' found in the workbook."

    /// Returns the metadata worksheet from a Study. If `useLastSheetOnIncorrectName` is true, returns the last sheet if no worksheet with the name `Study` or `isa_study` can be found.
    let getStudyMetadataSheet (useLastSheetOnIncorrectName : bool) study =
        match FsWorkbook.tryGetWorksheetByName "Study" study with
        | Some ws -> ws
        | None ->
            match FsWorkbook.tryGetWorksheetByName "isa_study" study with
            | Some ws -> ws
            | None ->
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets study |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith "No worksheet named 'Study' or 'isa_study' found in the workbook."

    /// Returns the metadata worksheet from an Assay. If `useLastSheetOnIncorrectName` is true, returns the last sheet if no worksheet with the name `Assay` or `isa_assay` can be found.
    let getAssayMetadataSheet (useLastSheetOnIncorrectName : bool) assay =
        match FsWorkbook.tryGetWorksheetByName "Assay" assay with
        | Some ws -> ws
        | None ->
            match FsWorkbook.tryGetWorksheetByName "isa_assay" assay with
            | Some ws -> ws
            | None ->
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets assay |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith "No worksheet named 'Assay' or 'isa_assay' found in the workbook."