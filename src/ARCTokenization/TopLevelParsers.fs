namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open FsSpreadsheet.ExcelIO

module internal ISA =

    open System.IO

    let tryParseMetadataSheetFromToken (rootPath:string) (isaCvTerm: CvTerm) (isaMdsParsingF: string -> IParam list) (refFileToken: IParam) =

        let cvpStr = Param.getValueAsString refFileToken
        let path = Path.Combine(rootPath, cvpStr)
        let containsToken = refFileToken|> (fun x -> x.Name = isaCvTerm.Name) 

        if containsToken then
            try 
                Some (isaMdsParsingF path)
            with _ -> 
                None
        else None

    let parseMetadataSheetsFromTokens (rootPath:string) (isaCvTerm: CvTerm) (isaMdsParsingF: string -> IParam list) (refFileTokens: #IParam seq) =
        refFileTokens
        |> Seq.choose (fun token ->  tryParseMetadataSheetFromToken rootPath isaCvTerm isaMdsParsingF token)
    
    let parseProcessGraphColumnsFromToken (rootPath:string) (refFileToken: IParam) =
        let cvpStr = Param.getValueAsString refFileToken
        let path = System.IO.Path.Combine(rootPath, cvpStr)
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

    let parseProcessGraphColumnsFromTokens (rootPath:string) (isaCvTerm: CvTerm) (refFileTokens: #IParam seq) =
        refFileTokens
        |> Seq.choose (fun token ->  
            match token.Name = isaCvTerm.Name with 
            | true -> Some (parseProcessGraphColumnsFromToken rootPath token)
            | false -> None
        )
        |> fun x -> 
            match Seq.length x with
            | 0 -> failwith "No token found"
            | _ -> x 


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
        ?UseLastSheetOnIncorrectName: bool
    ) =
        let fileToken = StructuralOntology.AFSO.``Investigation File``

        fun (rootPath:string) (token: #IParam) ->
            ISA.tryParseMetadataSheetFromToken
                rootPath
                fileToken
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
        ?UseLastSheetOnIncorrectName: bool
    ) =
        let fileToken = StructuralOntology.AFSO.``Investigation File``

        fun (rootPath:string) (tokens: #seq<#IParam>) ->
            ISA.parseMetadataSheetsFromTokens
                rootPath
                fileToken
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
        ?UseLastSheetOnIncorrectName: bool
    ) =

        let fileToken = StructuralOntology.AFSO.``Study File``

        fun (rootPath:string) (token: #IParam) ->
            ISA.tryParseMetadataSheetFromToken
                rootPath
                fileToken
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
        ?UseLastSheetOnIncorrectName: bool
    ) =
        let fileToken = StructuralOntology.AFSO.``Study File``

        fun (rootPath:string) (tokens: #seq<#IParam>) ->
            ISA.parseMetadataSheetsFromTokens
                rootPath
                fileToken
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

    /// <summary>
    /// Returns an annotation tables from an IParam if the given token contains a filepath with the standard study file name ("isa.study.xlsx").
    /// Map of string * `IParam` 2D List representing the individual parts parts of the Process graph, 
    /// where the string is the name of the worksheet that contained the table, 
    /// and the 2D lists represent a single table in which the inner 1D lists represent a single column.
    /// </summary>
    /// <param name="rootPath">ARC root path</param>
    /// <param name="refFileToken">IParam of the ARC Tokens</param>
    static member parseProcessGraphColumnsFromToken (rootPath:string) (refFileToken: IParam) =
        ISA.parseProcessGraphColumnsFromToken rootPath refFileToken


    /// <summary>
    /// Returns a seq of annotation tables from an IParam seq if the given tokens contains a filepath with the standard study file name ("isa.study.xlsx").
    /// Map of string * `IParam` 2D List representing the individual parts parts of the Process graph, 
    /// where the string is the name of the worksheet that contained the table, 
    /// and the 2D lists represent a single table in which the inner 1D lists represent a single column.
    /// </summary>
    /// <param name="rootPath">ARC root path</param>
    /// <param name="refFileToken">IParam seq of the ARC Tokens</param>
    static member parseProcessGraphColumnsFromTokens (rootPath:string) (refFileTokens: #IParam seq) =
        ISA.parseProcessGraphColumnsFromTokens rootPath (StructuralOntology.AFSO.``Study File``) refFileTokens

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
    static member tryParseMetadataSheetFromToken(
        ?UseLastSheetOnIncorrectName: bool
    ) =
        let fileToken = StructuralOntology.AFSO.``Assay File``

        fun (rootPath:string) (token: #IParam) ->
            ISA.tryParseMetadataSheetFromToken
                rootPath
                fileToken
                (Assay.parseMetadataSheetFromFile(?UseLastSheetOnIncorrectName = UseLastSheetOnIncorrectName))
                token


    /// <summary>
    /// Returns a function that parses all metadata sheets from all the tokens containing a filepath with the standard assay file name ("isa.assay.xlsx")
    /// in a given collection of tokens as a 2D list containing the individual assay metadata as a flat list of `IParam`s.
    ///
    /// if no tokens contain such a file path, the result will be an empty list.
    /// </summary>
    /// <param name="UseLastSheetOnIncorrectName">Wether or not to try parse the last sheet as metadata sheet when there is no sheet with the correct name ("isa_assay") in the workbook</param>
    static member parseMetadataSheetsFromTokens(
        ?UseLastSheetOnIncorrectName: bool
    ) =
        let fileToken = StructuralOntology.AFSO.``Assay File``

        fun (rootPath:string) (tokens: #seq<#IParam>) ->
            ISA.parseMetadataSheetsFromTokens
                rootPath
                fileToken
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

    /// <summary>
    /// Returns an annotation tables from an IParam if the given token contains a filepath with the standard assay file name ("isa.assay.xlsx").
    /// Map of string * `IParam` 2D List representing the individual parts parts of the Process graph, 
    /// where the string is the name of the worksheet that contained the table, 
    /// and the 2D lists represent a single table in which the inner 1D lists represent a single column.
    /// </summary>
    /// <param name="rootPath">ARC root path</param>
    /// <param name="refFileToken">IParam of the ARC Tokens</param>
    static member parseProcessGraphColumnsFromToken (rootPath:string) (refFileToken: IParam) =
        ISA.parseProcessGraphColumnsFromToken rootPath refFileToken

    /// <summary>
    /// Returns a seq of annotation tables from an IParam seq if the given tokens contains a filepath with the standard assay file name ("isa.assay.xlsx").
    /// Map of string * `IParam` 2D List representing the individual parts parts of the Process graph, 
    /// where the string is the name of the worksheet that contained the table, 
    /// and the 2D lists represent a single table in which the inner 1D lists represent a single column.
    /// </summary>
    /// <param name="rootPath">ARC root path</param>
    /// <param name="refFileToken">IParam seq of the ARC Tokens</param>
    static member parseProcessGraphColumnsFromTokens (rootPath:string) (refFileTokens: #IParam seq) =
        ISA.parseProcessGraphColumnsFromTokens rootPath (StructuralOntology.AFSO.``Assay File``) refFileTokens
