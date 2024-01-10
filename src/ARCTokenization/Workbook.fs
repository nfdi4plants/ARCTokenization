namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ARCTokenization.Terms

module Workbook =

    let getInvestigationMetadataSheet (useLastSheetOnIncorrectName: bool) investigation =
        try
            FsWorkbook.getWorksheetByName Globals.INVESTIGATION_METADATA_SHEET_NAME investigation
        with _ ->
            if useLastSheetOnIncorrectName then
                FsWorkbook.getWorksheets investigation
                |> Seq.last
            else
                failwith "No worksheet named 'isa_investigation' found in the workbook"

    let getStudyMetadataSheet (useLastSheetOnIncorrectName: bool) study =
        try
            FsWorkbook.tryGetWorksheetByName Globals.STUDY_OBSOLETE_METADATA_SHEET_NAME study
            |> Option.defaultValue (FsWorkbook.getWorksheetByName Globals.STUDY_METADATA_SHEET_NAME study)
        with _ ->
            if useLastSheetOnIncorrectName then
                FsWorkbook.getWorksheets study
                |> Seq.last
            else
                failwith $"No worksheet named {Globals.STUDY_OBSOLETE_METADATA_SHEET_NAME} or {Globals.STUDY_METADATA_SHEET_NAME} found in the workbook"

    let getAssayMetadataSheet (useLastSheetOnIncorrectName: bool) assay =
        try
            FsWorkbook.tryGetWorksheetByName Globals.ASSAY_OBSOLETE_METADATA_SHEET_NAME assay
            |> Option.defaultValue (FsWorkbook.getWorksheetByName Globals.ASSAY_METADATA_SHEET_NAME assay)
        with _ ->
            if useLastSheetOnIncorrectName then
                FsWorkbook.getWorksheets assay
                |> Seq.last
            else
                failwith $"No worksheet named {Globals.ASSAY_OBSOLETE_METADATA_SHEET_NAME} or {Globals.ASSAY_METADATA_SHEET_NAME} found in the workbook"