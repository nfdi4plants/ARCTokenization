namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ARCTokenization.Terms

module Workbook =

    let getInvestigationMetadataSheet (useLastSheetOnIncorrectName: bool) investigation =
        try
            FsWorkbook.getWorksheetByName "isa_investigation" investigation
        with _ ->
            if useLastSheetOnIncorrectName then
                FsWorkbook.getWorksheets investigation
                |> List.last
            else
                failwith "No worksheet named 'isa_investigation' found in the workbook"

    let getStudyMetadataSheet (useLastSheetOnIncorrectName: bool) study =
        try
            FsWorkbook.tryGetWorksheetByName "Study" study
            |> Option.defaultValue (FsWorkbook.getWorksheetByName "isa_study" study)
        with _ ->
            if useLastSheetOnIncorrectName then
                FsWorkbook.getWorksheets study
                |> List.last
            else
                failwith "No worksheet named 'Study' or 'isa_study' found in the workbook"

    let getAssayMetadataSheet (useLastSheetOnIncorrectName: bool) assay =
        try
            FsWorkbook.tryGetWorksheetByName "Assay" assay
            |> Option.defaultValue (FsWorkbook.getWorksheetByName "isa_assay" assay)
        with _ ->
            if useLastSheetOnIncorrectName then
                FsWorkbook.getWorksheets assay
                |> List.last
            else
                failwith "No worksheet named 'Assay' or 'isa_assay' found in the workbook"