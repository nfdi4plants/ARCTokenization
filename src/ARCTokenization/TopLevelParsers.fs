namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open FsSpreadsheet.ExcelIO

type FileSystem =
    
    /// <summary>
    /// Returns all directories in the given rootPath as a list of CvParams containing the annotated absolute directory paths.
    ///
    /// Note that rootPath must be an absolute path ending with a trailing slash.
    /// </summary>
    /// <param name="rootPath">absolute path ending with a trailing slash</param>
    static member parseAbsoluteDirectoryPaths(
        rootPath:string
    ) =
        FS.tokenizeAbsoluteDirectoryPaths rootPath

    /// <summary>
    /// Returns all files in the given rootPath as a list of CvParams containing the annotated absolute file paths.
    ///
    /// Note that rootPath must be an absolute path ending with a trailing slash.
    /// </summary>
    /// <param name="rootPath">absolute path ending with a trailing slash</param>
    static member parseAbsoluteFilePaths(
        rootPath:string
    ) =
        FS.tokenizeAbsoluteFilePaths rootPath

    /// <summary>
    /// Returns all directories in the given rootPath as a list of CvParams containing the annotated relative directory paths.
    ///
    /// Note that rootPath must be an absolute path ending with a trailing slash.
    /// </summary>
    /// <param name="rootPath">absolute path ending with a trailing slash</param>
    static member parseRelativeDirectoryPaths(
        rootPath:string
    ) =
        FS.tokenizeRelativeDirectoryPaths rootPath

    /// <summary>
    /// Returns all files in the given rootPath as a list of CvParams containing the annotated relative file paths.
    ///
    /// Note that rootPath must be an absolute path ending with a trailing slash.
    /// </summary>
    /// <param name="rootPath">absolute path ending with a trailing slash</param>
    static member parseRelativeFilePaths(
        rootPath:string
    ) =
        FS.tokenizeRelativeFilePaths rootPath

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
        |> Worksheet.parseRowsWith (Tokenization.convertMetadataTokens MetadataSheet.parseInvestigationKey)

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
        |> Worksheet.parseRowsWith (Tokenization.convertMetadataTokens MetadataSheet.parseStudyKey)

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
    /// Parses all annotation tables from an ISA Study XLSX file as a 
    /// Map of string * `IParam` 2D List representing the individual parts parts of the Process graph, 
    /// where the string is the name of the worksheet that contained the table, 
    /// and the 2D lists represent a single table in which the inner 1D lists represent a single column.
    /// </summary>
    /// <param name="path">he path to the study xlsx file</param>
    static member parseProcessGraphColumnsFromFile (path: string) =
        (FsWorkbook.fromXlsxFile path)
            .GetWorksheets()
            |> Seq.choose (fun ws ->
                ws
                |> ARCtrl.ISA.Spreadsheet.ArcTable.tryFromFsWorksheet
                |> Option.map (fun t -> 
                    ws.Name, 
                    t 
                    |> Tokenization.ARCtrl.ARCTable.tokenizeColumns
                )
            )
            |> Map.ofSeq

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
        |> Worksheet.parseRowsWith (Tokenization.convertMetadataTokens MetadataSheet.parseAssayKey)

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
    /// Parses all annotation tables from an ISA Assay XLSX file as a 
    /// Map of string * `IParam` 2D List representing the individual parts parts of the Process graph, 
    /// where the string is the name of the worksheet that contained the table, 
    /// and the 2D lists represent a single table in which the inner 1D lists represent a single column.
    /// </summary>
    /// <param name="path">he path to the study xlsx file</param>
    static member parseProcessGraphColumnsFromFile (path: string) =
        (FsWorkbook.fromXlsxFile path)
            .GetWorksheets()
            |> Seq.choose (fun ws ->
                ws
                |> ARCtrl.ISA.Spreadsheet.ArcTable.tryFromFsWorksheet
                |> Option.map (fun t -> 
                    ws.Name, 
                    t 
                    |> Tokenization.ARCtrl.ARCTable.tokenizeColumns
                )
            )
            |> Map.ofSeq