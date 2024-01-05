namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ARCTokenization.Terms
open Regex.ActivePatterns

module MetadataSheet =

    let (|Term|_|) (terms : CvTerm list) (key : string) : CvTerm Option =
        terms 
        |> List.tryFind (fun (term) -> term.Name = key)

    let (|UnMatchable|) (key : string) : string =
        key

    // we need to have separate functions here because matching is done based on term name, of which some are contained in multiple structural ontologies 
    // (e.g. the study metadata section is a copy of the resepctive section in an investigation file)

    let rec parseKeyWithTerms (terms: CvTerm list) (attributes : IParam list) (key : string) : ParamValue -> IParam = 

        match key with

        | Term terms term ->
            fun (pv) -> CvParam(term, pv, attributes)

        | Comment _ -> 
            fun pv -> CvParam(Terms.StructuralTerms.userComment, pv, attributes)

        | key when key.StartsWith("#") ->
            fun pv -> CvParam(Terms.StructuralTerms.ignoreLine, pv, attributes)

        | UnMatchable name -> 
            fun (pv) -> UserParam(name, pv, attributes) 

    let rec parseAssayKey = parseKeyWithTerms AssayMetadata.nonRootCvTerms

    let rec parseStudyKey = parseKeyWithTerms StudyMetadata.nonRootCvTerms

    let rec parseInvestigationKey = parseKeyWithTerms InvestigationMetadata.nonRootCvTerms