module Globals


open ARCtrl


let [<Literal>] INVESTIGATION_FILE_NAME = Path.InvestigationFileName
let [<Literal>] INVESTIGATION_METADATA_SHEET_NAME = ARCtrl.Spreadsheet.ArcInvestigation.metadataSheetName

let [<Literal>] STUDY_FILE_NAME = Path.StudyFileName
let [<Literal>] STUDY_METADATA_SHEET_NAME = ARCtrl.Spreadsheet.ArcStudy.metadataSheetName
let [<Literal>] STUDY_OBSOLETE_METADATA_SHEET_NAME= ARCtrl.Spreadsheet.ArcStudy.obsoleteMetadataSheetName

let [<Literal>] ASSAY_FILE_NAME = Path.AssayFileName
let [<Literal>] ASSAY_METADATA_SHEET_NAME = ARCtrl.Spreadsheet.ArcAssay.metadataSheetName
let [<Literal>] ASSAY_OBSOLETE_METADATA_SHEET_NAME = ARCtrl.Spreadsheet.ArcAssay.obsoleteMetadataSheetName
