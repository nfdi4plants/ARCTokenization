namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ARCTokenization.Terms
open ARCTokenization.StructuralOntology

open System.IO
open System
open ControlledVocabulary
open Tokenization

module internal FS =

    let tokenizeRelativeDirectoryPaths (rootPath:string) =
        let root = System.Uri(rootPath)
        let tokens = Tokenization.SpecificTokens.matchPathToCVTerms rootPath
        tokens
        |>Seq.map(fun (path,param) ->
            let currentUri =  System.Uri(path)
            CvParam(
                cvTerm = param,
                v = root.MakeRelativeUri(currentUri).ToString()
            )   
        )

    let tokenizeAbsoluteDirectoryPaths  (rootPath:string) =
        let tokens = Tokenization.SpecificTokens.matchPathToCVTerms rootPath
        tokens
        |>Seq.map(fun (path,param) ->
            CvParam(
                cvTerm = param,
                v = path.Replace("\\","/")
            )   
        )

    let tokenizeRelativeFilePaths (rootPath:string) =
        let root = System.Uri(rootPath)
        let tokens = Tokenization.SpecificTokens.matchFilePathToCVTerms rootPath
        tokens
        |>Seq.map(fun (path,param) ->
            let currentUri =  System.Uri(path)
            CvParam(
                cvTerm = param,
                v = root.MakeRelativeUri(currentUri).ToString()
            )   
        )

    let tokenizeAbsoluteFilePaths (rootPath:string) =
        let tokens = Tokenization.SpecificTokens.matchFilePathToCVTerms rootPath
        tokens
        |>Seq.map(fun (path,param) ->
            CvParam(
                cvTerm = param,
                v = path.Replace("\\","/")
            )   
        )
