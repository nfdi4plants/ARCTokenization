namespace ArcGraphModel

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet
open ArcGraphModel.Terms

module MetadataSheet =

    let (|Term|_|) (terms : CvTerm list) (key : string) : CvTerm Option =
        terms 
        |> List.tryFind (fun (term) -> CvTerm.getName term = key)

    let (|UnMatchable|) (key : string) : string =
        key

    // we need to have separate functions here because matching is done based on term name, of which some are contained in multiple structural ontologies 
    // (e.g. the study metadata section is a copy of the resepctive section in an investigation file)

    let rec parseAssayKey (attributes : IParam list) (key : string) : ParamValue -> IParam = 
        match key with

        | Term AssayMetadata.cvTerms term ->
            fun (pv) -> CvParam(term,pv,attributes)

        | UnMatchable name -> 
            fun (pv) -> UserParam(name,pv,attributes) // UserParam(name,pv,Attributes)

    let rec parseStudyKey (attributes : IParam list) (key : string) : ParamValue -> IParam = 
        match key with

        | Term StudyMetadata.cvTerms term ->
            fun (pv) -> CvParam(term,pv,attributes)

        | UnMatchable name -> 
            fun (pv) -> UserParam(name,pv,attributes) // UserParam(name,pv,Attributes)

    let rec parseInvestigationKey (attributes : IParam list) (key : string) : ParamValue -> IParam = 
        match key with

        | Term InvestigationMetadata.cvTerms term ->
            fun (pv) -> CvParam(term,pv,attributes)

        | UnMatchable name -> 
            fun (pv) -> UserParam(name,pv,attributes) // UserParam(name,pv,Attributes)