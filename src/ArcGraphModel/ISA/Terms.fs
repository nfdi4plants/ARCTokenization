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

module Terms = 

    let filepath = "http://purl.obolibrary.org/obo/NCIT_C47922","Pathname","NCIT"

    let person = "http://purl.obolibrary.org/obo/NCIT_C25190","Person","NCIT"

    let name = "http://semanticscience.org/resource/SIO_000116","name","SIO"

    let identifier = "http://semanticscience.org/resource/SIO_000115","identifier","SIO"

    let title = "http://semanticscience.org/resource/SIO_000185","title","SIO"

    let descriptor = "http://semanticscience.org/resource/SIO_000133","descriptor","SIO"

    let familyName = "http://purl.obolibrary.org/obo/IAO_0020017","family name","IAO"

    let givenName = "http://purl.obolibrary.org/obo/IAO_0020016","given name","IAO"

    let midInitials = "http://purl.obolibrary.org/obo/NCIT_C25536","Initials","NCIT"

    let email = "http://purl.obolibrary.org/obo/NCIT_C42775","E-mail Address","NCIT"

    let phone = "http://purl.obolibrary.org/obo/NCIT_C40978","Telephone Number","NCIT"
    
    let design = "http://semanticscience.org/resource/SIO_000705","Design","SIO"

    let investigation = "ARCO:1234","Investigation","ARCO"

    let study = "ARCO:1234","Study","ARCO"

    let assay = "ARCO:1234","Assay","ARCO"

    let publication = "ARCO:1234","Publication","ARCO"

    let status = "ARCO:1234","Status","ARCO"

    let factor = "ARCO:12345","Factor","ARCO"

    let parameter = "ARCO:12346","Parameter","ARCO"

    let characteristic = "ARCO:12347","Characteristic","ARCO"

    let rawData = "http://purl.obolibrary.org/obo/NCIT_C142663","Raw Data","NCIT"

    let processedData = "http://purl.obolibrary.org/obo/NCIT_C84340","Processed Data","NCIT"

    let data = "http://purl.obolibrary.org/obo/NCIT_C25474","Data","NCIT"

    let source = "ARCO:12349","Source Material","ARCO"

    let sample = "ARCO:12350","Sample","ARCO"

    let annotationID = "ARCO:1","AnnotationID","ARCO"

    let termSourceRef = "ARCO:2","termSourceRef","ARCO"