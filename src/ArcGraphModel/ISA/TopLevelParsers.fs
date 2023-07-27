namespace ArcGraphModel

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open FsSpreadsheet.ExcelIO

type Investigation() =

    /// <summary>
    /// Parses the metadata sheet from an ISA investigation XLSX file as a flat list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the investigation xlsx file</param>
    /// <param name="UseFirstSheetOnIncorrectName">Wether or not to try parse the first sheet as metadata sheet when there is no sheet with the correct name ("isa_investigation") in the workbook</param>
    static member parseMetadataSheetfromXlsxFile(
        path: string,
        ?UseFirstSheetOnIncorrectName: bool
    ) = 
        
        let useFirstSheetOnIncorrectName = defaultArg UseFirstSheetOnIncorrectName false

        let investigation = FsWorkbook.fromXlsxFile path

        let metadataSheet = 
            try
                FsWorkbook.getWorksheetByName "isa_investigation" investigation
            with _ ->
                if useFirstSheetOnIncorrectName then
                    FsWorkbook.getWorksheets investigation
                    |> List.head
                else
                    failwith "No worksheet named 'isa_investigation' found in the workbook"

        ArcGraphModel.Worksheet.parseRowsFlat metadataSheet

type Study() =

    /// <summary>
    /// Parses the metadata sheet from an ISA Study XLSX file as a flat list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the study xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Study") in the workbook</param>
    static member parseMetadataSheetFromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        
        let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
        let study = FsWorkbook.fromXlsxFile path

        let metadataSheet = 
            try
                FsWorkbook.getWorksheetByName "Study" study
            with _ ->
                if useLastSheetOnIncorrectName then
                    FsWorkbook.getWorksheets study
                    |> List.last
                else
                    failwith "No worksheet named 'Study' found in the workbook"

        ArcGraphModel.Worksheet.parseRowsFlat metadataSheet

    /// <summary>
    /// Parses all annotation tables from an ISA Study XLSX file as a list of `TokenizedAnnotationTable`s, a type that contains IO columns separated from the other columns.
    /// </summary>
    /// <param name="path">he path to the study xlsx file</param>
    static member parseAnnotationTablesFromFile (path: string) =
        FsWorkbook.fromXlsxFile path
        |> AnnotationTable.parseWorkbook

type Assay() =

    /// <summary>
    /// Parses the metadata sheet from an ISA Assay XLSX file as a flat list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the assay xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Assay") in the workbook</param>
    static member parseMetadataSheetFromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 

        let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
        let assay = FsWorkbook.fromXlsxFile path

        let metadataSheet = 
            try
                FsWorkbook.getWorksheetByName "Assay" assay
            with _ ->
                if useLastSheetOnIncorrectName then
                    FsWorkbook.getWorksheets assay
                    |> List.last
                else
                    failwith "No worksheet named 'Assay' found in the workbook"

        ArcGraphModel.Worksheet.parseRowsFlat metadataSheet

    /// <summary>
    /// Parses all annotation tables from an ISA Assay XLSX file as a list of `TokenizedAnnotationTable`s, a type that contains IO columns separated from the other columns.
    /// </summary>
    /// <param name="path">he path to the assay xlsx file</param>
    static member parseAnnotationTablesFromFile (path: string) =
        FsWorkbook.fromXlsxFile path
        |> AnnotationTable.parseWorkbook