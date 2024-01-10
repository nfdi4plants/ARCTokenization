module Globals

open ARCtrl
open ARCtrl.ISA

let [<Literal>] INVESTIGATION_FILE_NAME = Path.InvestigationFileName
let [<Literal>] INVESTIGATION_METADATA_SHEET_NAME = ARCtrl.ISA.Spreadsheet.ArcInvestigation.metaDataSheetName

let [<Literal>] STUDY_FILE_NAME = Path.StudyFileName
let [<Literal>] STUDY_METADATA_SHEET_NAME = ARCtrl.ISA.Spreadsheet.ArcStudy.metaDataSheetName
let [<Literal>] STUDY_OBSOLETE_METADATA_SHEET_NAME= ARCtrl.ISA.Spreadsheet.ArcStudy.obsoleteMetaDataSheetName

let [<Literal>] ASSAY_FILE_NAME = Path.AssayFileName
let [<Literal>] ASSAY_METADATA_SHEET_NAME = ARCtrl.ISA.Spreadsheet.ArcAssay.metaDataSheetName
let [<Literal>] ASSAY_OBSOLETE_METADATA_SHEET_NAME = ARCtrl.ISA.Spreadsheet.ArcAssay.obsoleteMetaDataSheetName
