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
        seq {
            for dir in Directory.EnumerateDirectories(rootPath, "*", SearchOption.AllDirectories) do
                let currentUri =  System.Uri(dir)
                yield CvParam(
                    cvTerm = AFSO.``Directory Path``,
                    v = root.MakeRelativeUri(currentUri).ToString()
                )
        }

    let tokenizeAbsoluteDirectoryPaths  (rootPath:string) =
        seq {
                for dir in Directory.EnumerateDirectories(rootPath, "*", SearchOption.AllDirectories) do
                    yield CvParam(
                        cvTerm = AFSO.``Directory Path``,
                        v = dir.Replace("\\","/")
                    )
            }    


    let tokenizeRelativeFilePaths (rootPath:string) =
        let root = System.Uri(rootPath)
        seq {
            for file in Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories) do
                let currentFileUri =  System.Uri(file)
                yield CvParam(
                    cvTerm = AFSO.``File Path``, 
                    v = root.MakeRelativeUri(currentFileUri).ToString()
                )
        }

    let tokenizeAbsoluteFilePaths (rootPath:string) =
        seq {
            for file in Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories) do
            yield CvParam(
                cvTerm = AFSO.``File Path``, 
                v = file.Replace("\\","/")
            )
        }


    let internal normalisePath (path:string) =
        path.Replace("\\","/")

    let tokenizeARCFileSystem (rootPath:string) =
        let rootPathNormalised = rootPath|>normalisePath
        
        let directories =
            Directory.EnumerateDirectories(rootPath, "*", SearchOption.AllDirectories)
            |> Seq.map(fun p -> 
                Tokenization.ArcFileSystem.PType.Directory,
                p|>normalisePath
            )

        let files = 
            Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories)
            |> Seq.map(fun p -> 
                Tokenization.ArcFileSystem.PType.File,
                p|>normalisePath
            )
        let collection: (Tokenization.ArcFileSystem.PType * string) seq = Seq.concat (seq{directories;files})

        collection
        |>Seq.map(fun (pType,p) ->  ArcFileSystem.getArcFileSystemTokens rootPathNormalised pType p)
        
        