namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open FsSpreadsheet.ExcelIO

type Investigation =

    /// <summary>
    /// Parses the metadata sheet from an ISA Investigation  XLSX file as a row-based 2D list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the investigation xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Assay") in the workbook</param>
    static member parseMetadataRowsFromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 

        let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
        FsWorkbook.fromXlsxFile path
        |> Workbook.getInvestigationMetadataSheet useLastSheetOnIncorrectName
        |> Worksheet.parseRowsWith (Tokenization.convertTokens MetadataSheet.parseInvestigationKey)

    /// <summary>
    /// Parses the metadata sheet from an ISA Study XLSX file as a flat list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the study xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Assay") in the workbook</param>
    static member parseMetadataSheetFromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        Investigation.parseMetadataRowsFromFile(
            path = path, 
            ?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName
        )
        |> List.concat

type Study =

    /// <summary>
    /// Parses the metadata sheet from an ISA Study XLSX file as a row-based 2D list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the study xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Assay") in the workbook</param>
    static member parseMetadataRowsFromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 

        let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
        FsWorkbook.fromXlsxFile path
        |> Workbook.getStudyMetadataSheet useLastSheetOnIncorrectName
        |> Worksheet.parseRowsWith (Tokenization.convertTokens MetadataSheet.parseStudyKey)

    /// <summary>
    /// Parses the metadata sheet from an ISA Study XLSX file as a flat list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the study xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Assay") in the workbook</param>
    static member parseMetadataSheetfromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        Study.parseMetadataRowsFromFile(
            path = path, 
            ?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName
        )
        |> List.concat

    /// <summary>
    /// Parses all annotation tables from an ISA Study XLSX file as a list of `TokenizedAnnotationTable`s, a type that contains IO columns separated from the other columns.
    /// </summary>
    /// <param name="path">he path to the study xlsx file</param>
    static member parseAnnotationTablesFromFile (path: string) =
        FsWorkbook.fromXlsxFile path
        |> AnnotationTable.parseWorkbook

type Assay =

    /// <summary>
    /// Parses the metadata sheet from an ISA Assay XLSX file as a row-based 2D list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the assay xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Assay") in the workbook</param>
    static member parseMetadataRowsFromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 

        let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
        FsWorkbook.fromXlsxFile path
        |> Workbook.getAssayMetadataSheet useLastSheetOnIncorrectName
        |> Worksheet.parseRowsWith (Tokenization.convertTokens MetadataSheet.parseAssayKey)

    /// <summary>
    /// Parses the metadata sheet from an ISA Assay XLSX file as a flat list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the assay xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("Assay") in the workbook</param>
    static member parseMetadataSheetFromFile(
        path: string,
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        Assay.parseMetadataRowsFromFile(
            path = path, 
            ?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName
        )
        |> List.concat

    /// <summary>
    /// Parses all annotation tables from an ISA Assay XLSX file as a list of `TokenizedAnnotationTable`s, a type that contains IO columns separated from the other columns.
    /// </summary>
    /// <param name="path">he path to the assay xlsx file</param>
    static member parseAnnotationTablesFromFile (path: string) =
        FsWorkbook.fromXlsxFile path
        |> AnnotationTable.parseWorkbook