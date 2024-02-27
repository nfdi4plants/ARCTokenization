namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open FsSpreadsheet.ExcelIO

module internal ISA =

    open System.IO

    let tryParseMetadataSheetFromToken (isaFileName: string) (isaMdsParsingF: string -> IParam list) (absFileToken: IParam) =

        let cvpStr = Param.getValueAsString absFileToken
        //printfn $"cvpStr: {cvpStr}"
        //if String.contains isaFileName cvpStr then
        if Path.GetFileName cvpStr = isaFileName then
            try 
                Some (isaMdsParsingF cvpStr)
            with _ -> 
                None
        else None

    let parseMetadataSheetsFromTokens (isaFileName: string) (isaMdsParsingF: string -> IParam list) (absFileTokens: #IParam seq) =
        absFileTokens
        |> Seq.choose (fun token ->  tryParseMetadataSheetFromToken isaFileName isaMdsParsingF token)

    //type löl =
        

    //    static member parseStudyMetadataSheetFromCvp absFileTokens =
    //        parseMetadataSheetsFromCvps "isa.study.xlsx" ARCTokenization.Study.parseMetadataSheetfromFile absFileTokens

    //    static member parseAssayMetadataSheetFromCvp absFileTokens =
    //        parseMetadataSheetsFromCvps "isa.assay.xlsx" ARCTokenization.Assay.parseMetadataSheetFromFile absFileTokens

    //    static member tryParseIsaMetadataSheetFromCvp (isaFileName : string) isaMdsParsingF absFileTokens =
    //        absFileTokens
    //        |> Seq.choose (
    //            fun cvp ->
    //                let cvpStr = Param.getValueAsString cvp
    //                //printfn $"cvpStr: {cvpStr}"
    //                //if String.contains isaFileName cvpStr then
    //                if isaFileName = Path.GetFileName cvpStr then
    //                    try Some (isaMdsParsingF cvpStr)
    //                    with _ -> None
    //                else None
    //        )

    //    static member tryParseInvestigationMetadataSheetFromCvp (absFileTokens : #IParam seq) =
    //        try ParamBasedParsers.tryParseIsaMetadataSheetFromCvp "isa.investigation.xlsx" ARCTokenization.Investigation.parseMetadataSheetFromFile absFileTokens 
    //            |> Seq.concat
    //        with _ -> Seq.empty

    //    static member tryParseStudyMetadataSheetFromCvp (absFileTokens : #IParam seq) =
    //        ParamBasedParsers.tryParseIsaMetadataSheetFromCvp "isa.study.xlsx" ARCTokenization.Study.parseMetadataSheetfromFile absFileTokens

    //    static member tryParseAssayMetadataSheetFromCvp (absFileTokens : #IParam seq) =
    //        ParamBasedParsers.tryParseIsaMetadataSheetFromCvp "isa.assay.xlsx" ARCTokenization.Assay.parseMetadataSheetFromFile absFileTokens

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

    /// <summary>
    /// Returns all files in the given rootPath as a list of CvParams containing the annotated relative file paths.
    ///
    /// Note that rootPath must be an absolute path ending with a trailing slash.
    /// </summary>
    /// <param name="rootPath">absolute path ending with a trailing slash</param>
    static member parseARCFileSystem(
        rootPath:string
    ) =
        FS.tokenizeARCFileSystem rootPath

type Investigation =

    /// <summary>
    /// Returns a function that parses the metadata sheet from an ISA Investigation XLSX file as a row-based 2D list of `IParam`s.
    /// </summary>
    /// <param name="path">The path to the investigation xlsx file</param>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_investigation") in the workbook</param>
    static member parseMetadataRowsFromFile(
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        fun (path: string) -> 
            let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
            FsWorkbook.fromXlsxFile path
            |> Workbook.getInvestigationMetadataSheet useLastSheetOnIncorrectName
            |> Worksheet.parseRowsWith (Tokenization.convertMetadataTokens MetadataSheet.parseInvestigationKey)

    /// <summary>
    /// Returns a function that parses the metadata sheet from an ISA Investigation XLSX file at a given path as a flat list of `IParam`s.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_investigation") in the workbook</param>
    static member parseMetadataSheetFromFile(
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        fun (path: string) -> 
            path
            |> Investigation.parseMetadataRowsFromFile(
                ?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName
            )
            |> List.concat

    /// <summary>
    /// Returns a function that returns Some flat IParam list representing the investigation metadata if the given token contains a filepath with the standard investigation file name ("isa.investigation.xlsx") or None otherwise.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_investigation") in the workbook</param>
    /// <param name="FileName">The name of the investigation file, note that this should not be set if the file follows spec (as "isa.investigation.xlsx" is the default)</param>
    static member tryParseMetadataSheetFromToken(
        ?UseLastSheetOnIncorrectName: bool,
        ?FileName: string
    ) =
        let fileName = defaultArg FileName Globals.INVESTIGATION_FILE_NAME

        fun (token: #IParam) ->
            ISA.tryParseMetadataSheetFromToken
                fileName
                (Investigation.parseMetadataSheetFromFile(?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName))
                token


    /// <summary>
    /// Returns a function that parses all metadata sheets from all the tokens containing a filepath with the standard investigation file name ("isa.investigation.xlsx")
    /// in a given collection of tokens as a 2D list containing the individual Investigation metadata as a flat list of `IParam`s.
    ///
    /// if no tokens contain such a file path, the result will be an empty list.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_investigation") in the workbook</param>
    /// <param name="FileName">The name of the investigation file, note that this should not be set if the file follows spec (as "isa.investigation.xlsx" is the default)</param>
    static member parseMetadataSheetsFromTokens(
        ?UseLastSheetOnIncorrectName: bool,
        ?FileName: string
    ) =
        let fileName = defaultArg FileName Globals.INVESTIGATION_FILE_NAME

        fun (tokens: #seq<#IParam>) ->
            ISA.parseMetadataSheetsFromTokens
                fileName
                (Investigation.parseMetadataSheetFromFile(?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName))
                tokens
            |> List.ofSeq

type Study =

    /// <summary>
    /// Returns a function that parses the metadata sheet from an ISA Study XLSX file as a row-based 2D list of `IParam`s.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_study") in the workbook</param>
    static member parseMetadataRowsFromFile(
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        fun (path: string) ->

            let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
            FsWorkbook.fromXlsxFile path
            |> Workbook.getStudyMetadataSheet useLastSheetOnIncorrectName
            |> Worksheet.parseRowsWith (Tokenization.convertMetadataTokens MetadataSheet.parseStudyKey)

    /// <summary>
    /// Returns a function that parses the metadata sheet from an ISA Study XLSX file at a given path as a flat list of `IParam`s.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_study") in the workbook</param>
    static member parseMetadataSheetFromFile(
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        fun (path: string) ->
            path
            |> Study.parseMetadataRowsFromFile(
                ?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName
            )
            |> List.concat

    /// <summary>
    /// Returns a function that returns Some flat IParam list representing the study metadata if the given token contains a filepath with the standard study file name ("isa.study.xlsx") or None otherwise.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_study") in the workbook</param>
    /// <param name="FileName">The name of the study file, note that this should not be set if the file follows spec (as "isa.study.xlsx" is the default)</param>
    static member tryParseMetadataSheetFromToken(
        ?UseLastSheetOnIncorrectName: bool,
        ?FileName: string
    ) =
        let fileName = defaultArg FileName Globals.STUDY_FILE_NAME

        fun (token: #IParam) ->
            ISA.tryParseMetadataSheetFromToken
                fileName
                (Study.parseMetadataSheetFromFile(?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName))
                token


    /// <summary>
    /// Returns a function that parses all metadata sheets from all the tokens containing a filepath with the standard study file name ("isa.study.xlsx")
    /// in a given collection of tokens as a 2D list containing the individual study metadata as a flat list of `IParam`s.
    ///
    /// if no tokens contain such a file path, the result will be an empty list.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_study") in the workbook</param>
    /// <param name="FileName">The name of the study file, note that this should not be set if the file follows spec (as "isa.study.xlsx" is the default)</param>
    static member parseMetadataSheetsFromTokens(
        ?UseLastSheetOnIncorrectName: bool,
        ?FileName: string
    ) =
        let fileName = defaultArg FileName Globals.STUDY_FILE_NAME

        fun (tokens: #seq<#IParam>) ->
            ISA.parseMetadataSheetsFromTokens
                fileName
                (Study.parseMetadataSheetFromFile(?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName))
                tokens
            |> List.ofSeq


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
    /// Returns a function that parses the metadata sheet from an ISA Assay XLSX file as a row-based 2D list of `IParam`s.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_assay") in the workbook</param>
    static member parseMetadataRowsFromFile(
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        fun (path: string) ->

            let useLastSheetOnIncorrectName = defaultArg UseLastSheetOnIncorrectName false
        
            FsWorkbook.fromXlsxFile path
            |> Workbook.getAssayMetadataSheet useLastSheetOnIncorrectName
            |> Worksheet.parseRowsWith (Tokenization.convertMetadataTokens MetadataSheet.parseAssayKey)

    /// <summary>
    /// Returns a function that parses the metadata sheet from an ISA Assay XLSX file at a given path as a flat list of `IParam`s.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_assay") in the workbook</param>
    static member parseMetadataSheetFromFile(
        ?UseLastSheetOnIncorrectName: bool
    ) = 
        fun (path: string) ->
            path
            |> Assay.parseMetadataRowsFromFile(
                ?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName
            )
            |> List.concat

    /// <summary>
    /// Returns a function that returns Some flat IParam list representing the assay metadata if the given token contains a filepath with the standard assay file name ("isa.assay.xlsx") or None otherwise.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_assay") in the workbook</param>
    /// <param name="FileName">The name of the assay file, note that this should not be set if the file follows spec (as "isa.assay.xlsx" is the default)</param>
    static member tryParseMetadataSheetFromToken(
        ?UseLastSheetOnIncorrectName: bool,
        ?FileName: string
    ) =
        let fileName = defaultArg FileName Globals.ASSAY_FILE_NAME

        fun (token: #IParam) ->
            ISA.tryParseMetadataSheetFromToken
                fileName
                (Assay.parseMetadataSheetFromFile(?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName))
                token


    /// <summary>
    /// Returns a function that parses all metadata sheets from all the tokens containing a filepath with the standard assay file name ("isa.assay.xlsx")
    /// in a given collection of tokens as a 2D list containing the individual assay metadata as a flat list of `IParam`s.
    ///
    /// if no tokens contain such a file path, the result will be an empty list.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_assay") in the workbook</param>
    /// <param name="FileName">The name of the assay file, note that this should not be set if the file follows spec (as "isa.assay.xlsx" is the default)</param>
    static member parseMetadataSheetsFromTokens(
        ?UseLastSheetOnIncorrectName: bool,
        ?FileName: string
    ) =
        let fileName = defaultArg FileName Globals.ASSAY_FILE_NAME

        fun (tokens: #seq<#IParam>) ->
            ISA.parseMetadataSheetsFromTokens
                fileName
                (Assay.parseMetadataSheetFromFile(?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName))
                tokens
            |> List.ofSeq

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