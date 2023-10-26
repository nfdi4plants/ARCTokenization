namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ARCTokenization.Terms
open ARCTokenization.StructuralOntology

open System.IO
open System
open ControlledVocabulary

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