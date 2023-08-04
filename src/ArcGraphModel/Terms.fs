namespace ArcGraphModel.Terms

open FsOboParser
open System.IO
open ControlledVocabulary

module internal EmbeddedResource = 

    open System.Reflection
    open System.IO

    let assembly = Assembly.GetExecutingAssembly()

    let load file = 
        use str = assembly.GetManifestResourceStream($"ArcGraphModel.{file}")
        use r = new StreamReader(str)
        r.ReadToEnd()

module InvestigationMetadata =
    
    let internal obo = (EmbeddedResource.load "structural_ontologies.investigation_metadata_structural_ontology.obo").Replace("\r\n", "\n").Split('\n')

    let ontology = OboOntology.fromLines true obo

    let cvTerms = 
        ontology.Terms
        |> List.map (fun t -> CvTerm(t.Id,t.Name,"INVMSO"))

module StudyMetadata =
    
    let internal obo = (EmbeddedResource.load "structural_ontologies.study_metadata_structural_ontology.obo").Replace("\r\n", "\n").Split('\n')

    let ontology = OboOntology.fromLines true obo

    let cvTerms = 
        ontology.Terms
        |> List.map (fun t -> CvTerm(t.Id,t.Name,"STDMSO"))    

module AssayMetadata = 
    
    let internal obo = (EmbeddedResource.load "structural_ontologies.assay_metadata_structural_ontology.obo").Replace("\r\n", "\n").Split('\n')

    let ontology = OboOntology.fromLines true obo

    let cvTerms = 
        ontology.Terms
        |> List.map (fun t -> CvTerm(t.Id,t.Name,"ASSMSO"))

module StructuralTerms =
    
    let metadataSectionKey = CvTerm("AGMO:00000001","Metadata Section Key","AGMO")