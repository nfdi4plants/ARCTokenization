namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ARCTokenization.Terms

module Workbook =

    let getInvestigationMetadataSheet (useLastSheetOnIncorrectName: bool) investigation =

        match FsWorkbook.tryGetWorksheetByName Globals.INVESTIGATION_METADATA_SHEET_NAME investigation with
        | Some ws -> ws
        | None ->
            if useLastSheetOnIncorrectName then
                match FsWorkbook.getWorksheets investigation |> Seq.tryLast with
                | Some ws -> ws
                | None -> failwith "No worksheets found in the workbook."
            else
                failwith $"No worksheet named {Globals.INVESTIGATION_METADATA_SHEET_NAME} found in the workbook."


    let getStudyMetadataSheet (useLastSheetOnIncorrectName: bool) study =

        match FsWorkbook.tryGetWorksheetByName Globals.STUDY_OBSOLETE_METADATA_SHEET_NAME study with
        | Some ws -> ws
        | None ->
            match FsWorkbook.tryGetWorksheetByName Globals.STUDY_METADATA_SHEET_NAME study with
            | Some ws -> ws
            | None ->
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets study |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith $"No worksheet named {Globals.STUDY_OBSOLETE_METADATA_SHEET_NAME} or {Globals.STUDY_METADATA_SHEET_NAME} found in the workbook."

    let getAssayMetadataSheet (useLastSheetOnIncorrectName: bool) assay =

        match FsWorkbook.tryGetWorksheetByName Globals.ASSAY_OBSOLETE_METADATA_SHEET_NAME assay with
        | Some ws -> ws
        | None ->
            match FsWorkbook.tryGetWorksheetByName Globals.ASSAY_METADATA_SHEET_NAME assay with
            | Some ws -> ws
            | None ->
                if useLastSheetOnIncorrectName then
                    match FsWorkbook.getWorksheets assay |> Seq.tryLast with
                    | Some ws -> ws
                    | None -> failwith "No worksheets found in the workbook."
                else
                    failwith $"No worksheet named {Globals.ASSAY_OBSOLETE_METADATA_SHEET_NAME} or {Globals.ASSAY_METADATA_SHEET_NAME} found in the workbook"
