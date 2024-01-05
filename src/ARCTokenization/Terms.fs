namespace ARCTokenization.Terms

open FsOboParser
open System.IO
open ControlledVocabulary

module internal EmbeddedResource = 

    open System.Reflection
    open System.IO

    let assembly = Assembly.GetExecutingAssembly()

    let load file = 
        use str = assembly.GetManifestResourceStream($"ARCTokenization.{file}")
        use r = new StreamReader(str)
        r.ReadToEnd()

module InvestigationMetadata =
    
    let internal obo = (EmbeddedResource.load "structural_ontologies.investigation_metadata_structural_ontology.obo").Replace("\r\n", "\n").Split('\n')

    let ontology = OboOntology.fromLines true obo

    let nonRootOboTerms = 
        ontology.Terms
        |> List.skip 1

    let nonObsoleteOboTerms = 
        ontology.Terms
        |> List.filter (fun t -> not t.IsObsolete)

    let nonObsoleteNonRootOboTerms = 
        nonRootOboTerms
        |> List.filter (fun t -> not t.IsObsolete)

    let obsoleteOboTerms = 
        nonRootOboTerms
        |> List.filter (fun t -> t.IsObsolete)

    let cvTerms = 
        ontology.Terms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "INVMSO"))

    let nonRootCvTerms = 
        nonRootOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "INVMSO"))

    let nonObsoleteCvTerms =
        nonObsoleteOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "INVMSO"))

    let nonObsoleteNonRootCvTerms =
        nonObsoleteNonRootOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "INVMSO"))

    let obsoleteCvTerms =
        obsoleteOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "INVMSO"))

module StudyMetadata =
    
    let internal obo = (EmbeddedResource.load "structural_ontologies.study_metadata_structural_ontology.obo").Replace("\r\n", "\n").Split('\n')

    let ontology = OboOntology.fromLines true obo

    let nonRootOboTerms = 
        ontology.Terms
        |> List.skip 1

    let nonObsoleteOboTerms = 
        ontology.Terms
        |> List.filter (fun t -> not t.IsObsolete)

    let nonObsoleteNonRootOboTerms = 
        nonRootOboTerms
        |> List.filter (fun t -> not t.IsObsolete)

    let obsoleteOboTerms = 
        nonRootOboTerms
        |> List.filter (fun t -> t.IsObsolete)

    let cvTerms = 
        ontology.Terms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "STDMSO"))

    let nonRootCvTerms = 
        nonRootOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "STDMSO"))

    let nonObsoleteCvTerms =
        nonObsoleteOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "STDMSO"))

    let nonObsoleteNonRootCvTerms =
        nonObsoleteNonRootOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "STDMSO"))

    let obsoleteCvTerms =
        obsoleteOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "STDMSO"))
module AssayMetadata = 
    
    let internal obo = (EmbeddedResource.load "structural_ontologies.assay_metadata_structural_ontology.obo").Replace("\r\n", "\n").Split('\n')

    let ontology = OboOntology.fromLines true obo

    let nonRootOboTerms = 
        ontology.Terms
        |> List.skip 1

    let nonObsoleteOboTerms = 
        ontology.Terms
        |> List.filter (fun t -> not t.IsObsolete)

    let nonObsoleteNonRootOboTerms = 
        nonRootOboTerms
        |> List.filter (fun t -> not t.IsObsolete)

    let obsoleteOboTerms = 
        nonRootOboTerms
        |> List.filter (fun t -> t.IsObsolete)

    let cvTerms = 
        ontology.Terms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "ASSMSO"))

    let nonRootCvTerms = 
        nonRootOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "ASSMSO"))

    let nonObsoleteCvTerms =
        nonObsoleteOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "ASSMSO"))

    let nonObsoleteNonRootCvTerms =
        nonObsoleteNonRootOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "ASSMSO"))

    let obsoleteCvTerms =
        obsoleteOboTerms
        |> List.map (fun t -> CvTerm.create(accession = t.Id, name = t.Name, ref = "ASSMSO"))


module StructuralTerms =
    
    let metadataSectionKey = CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")

    let userComment = CvTerm.create(accession = "AGMO:00000002", name = "User Comment", ref = "AGMO")

    let ignoreLine = CvTerm.create(accession = "AGMO:00000003", name = "Ignore Line", ref = "AGMO")