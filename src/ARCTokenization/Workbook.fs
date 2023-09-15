namespace ARCTokenization


open FSharpAux
open FsSpreadsheet


/// Functions to parse and tokenize FsWorkbooks.
module Workbook =

    /// Returns the metadata worksheet from an Investigation. If `useLastSheetOnIncorrectName` is true, returns the last sheet if no worksheet with the name `Investigation` or `isa_investigation` can be found.
    let getInvestigationMetadataSheet (useLastSheetOnIncorrectName : bool) investigation =
        FsWorkbook.tryGetWorksheetByName "Investigation" investigation
        |> Option.defaultValue (
            FsWorkbook.tryGetWorksheetByName "isa_investigation" investigation
            |> Option.defaultValue (
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets investigation |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith "No worksheet named 'Investigation' or 'isa_investigation' found in the workbook."
            )
        )

    /// Returns the metadata worksheet from a Study. If `useLastSheetOnIncorrectName` is true, returns the last sheet if no worksheet with the name `Study` or `isa_study` can be found.
    let getStudyMetadataSheet (useLastSheetOnIncorrectName : bool) study =
        FsWorkbook.tryGetWorksheetByName "Study" study
        |> Option.defaultValue (
            FsWorkbook.tryGetWorksheetByName "isa_study" study
            |> Option.defaultValue (
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets study |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith "No worksheet named 'Study' or 'isa_study' found in the workbook."
            )
        )

    /// Returns the metadata worksheet from an Assay. If `useLastSheetOnIncorrectName` is true, returns the last sheet if no worksheet with the name `Assay` or `isa_assay` can be found.
    let getAssayMetadataSheet (useLastSheetOnIncorrectName : bool) assay =
        FsWorkbook.tryGetWorksheetByName "Assay" assay
        |> Option.defaultValue (
            FsWorkbook.tryGetWorksheetByName "isa_assay" assay
            |> Option.defaultValue (
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets assay |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith "No worksheet named 'Assay' or 'isa_assay' found in the workbook."
            )
        )