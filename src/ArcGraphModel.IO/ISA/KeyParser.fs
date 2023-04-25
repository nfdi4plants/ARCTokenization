namespace ArcGraphModel.IO

open ArcGraphModel
open FSharpAux
open FsSpreadsheet

type ContainerBase =
    
    {
        Term : CvTerm
        Key  : string
    }

    static member investigation : ContainerBase = 
        {Term = Terms.investigation; Key = "INVESTIGATION"}

    static member investigationContacts : ContainerBase = 
        {Term = Terms.person; Key = "INVESTIGATION CONTACTS"}

    static member investigationPublication : ContainerBase = 
        {Term = Terms.publication; Key = "INVESTIGATION PUBLICATIONS"}

    static member study : ContainerBase = 
        {Term = Terms.study; Key = "STUDY"}

    static member studyAssays : ContainerBase = 
        {Term = Terms.assay; Key = "STUDY ASSAYS"}

    static member studyContacts : ContainerBase = 
        {Term = Terms.person; Key = "STUDY CONTACTS"}

    static member studyFactors : ContainerBase = 
        {Term = Terms.factor; Key = "STUDY FACTORS"}

    static member studyDesignDescriptors : ContainerBase = 
        {Term = Terms.design; Key = "STUDY DESIGN DESCRIPTORS"}

type TokenBase =
    
    {
        Term    : CvTerm
        Key     : string
    }

    static member title : TokenBase = {Term = Terms.title; Key = "Title"}

    static member identifier : TokenBase = {Term = Terms.identifier; Key = "Identifier"}

    static member name : TokenBase = {Term = Terms.name; Key = "Name"}

    static member fileName : TokenBase = {Term = Terms.filepath; Key = "File Name"}

    static member description : TokenBase = {Term = Terms.descriptor; Key = "Description"}

    static member familyName : TokenBase = {Term = Terms.familyName; Key = "Last Name"}

    static member givenName : TokenBase = {Term = Terms.givenName; Key = "First Name"}

    static member midInitials : TokenBase = {Term = Terms.midInitials; Key = "Mid Initials"}

    static member email : TokenBase = {Term = Terms.email; Key = "Email"}

    static member phone : TokenBase = {Term = Terms.phone; Key = "Phone"}

    static member factorType : TokenBase = {Term = Terms.factor; Key = "Factor Type"}

    static member designType : TokenBase = {Term = Terms.design; Key = "Design Type"}

    static member sample : TokenBase = {Term = Terms.sample; Key = "Sample Name"}

    static member source : TokenBase = {Term = Terms.source; Key = "Source Name"}

    static member data : TokenBase = {Term = Terms.data; Key = "Data File"}

    static member termSourceRef : TokenBase = {Term = Terms.termSourceRef; Key = "Term Source REF"}

    static member annotationID : TokenBase = {Term = Terms.annotationID; Key = "Term Accession Number"}

type AttributeBase =   
    {
        Term    : CvTerm
        Key     : string
    }

    static member person : AttributeBase = {Term = Terms.person; Key = "Person"}

    static member study : AttributeBase = {Term = Terms.study; Key = "Study"}

    static member assay : AttributeBase = {Term = Terms.assay; Key = "Assay"}

    static member publication : AttributeBase = {Term = Terms.publication; Key = "Publication"}

    static member investigation : AttributeBase = {Term = Terms.investigation; Key = "Investigation"}

    static member factor : AttributeBase = {Term = Terms.factor; Key = "Factor"}

    static member parameter : AttributeBase = {Term = Terms.parameter; Key = "Parameter"}

    static member characteristic : AttributeBase = {Term = Terms.characteristic; Key = "Characteristic"}

    static member factorType : AttributeBase = {Term = Terms.factor; Key = "Factor Type"}

    static member designType : AttributeBase = {Term = Terms.design; Key = "Design Type"}
    
    static member rawData : AttributeBase = {Term = Terms.rawData; Key = "Raw"}

    static member processedData : AttributeBase = {Term = Terms.processedData; Key = "Derived"}

    //static member person : AttributeBase = {Term = Terms.person; Key = "Person"}

module KeyParser =

    let (|Container|_|) (container : ContainerBase) (key : FsCell) =
        if key.Value.Trim() = container.Key then
            Some (CvContainer(container.Term))
        else 
            None

    let (|Attribute|_|) (token : AttributeBase) (key : string) =
    
        if key.Contains token.Key then 
            let newKey = key.Replace(token.Key,"").Trim()
            let Attribute = CvParam(token.Term,ParamValue.Value "")
            Some (Attribute,newKey)
        else None

    let (|Token|_|) (token : TokenBase) (key : string) : CvTerm Option =
        if key =  token.Key then 
            Some token.Term
        else None

    let (|StructuredName|_|) (key : string) : CvTerm Option =
        let namePattern = @"(?<=\[).*(?=[\]])"
        Regex.tryParseValue namePattern key
        |> Option.map (fun n ->
            "",n,""
        )

    let (|AnnotationID|_|) (key : string) : CvTerm Option =
        let namePattern = @"(?<=\[).*(?=[\]])"
        Regex.tryParseValue namePattern key
        |> Option.map (fun n ->
            "",n,""
        )

    let (|UnMatchable|) (key : string) : string =
        key


    let rec parseKey (attributes : IParam list) (key : string) : ParamValue -> IParam = 
        match key with
        | Token TokenBase.identifier term
        | Token TokenBase.name term
        | Token TokenBase.title term
        | Token TokenBase.description term
        | Token TokenBase.familyName term
        | Token TokenBase.givenName term
        | Token TokenBase.midInitials term
        | Token TokenBase.email term
        | Token TokenBase.factorType term
        | Token TokenBase.designType term
        | Token TokenBase.data term
        | Token TokenBase.source term
        | Token TokenBase.sample term
        | Token TokenBase.annotationID term
        | Token TokenBase.termSourceRef term
        | Token TokenBase.phone term -> 
            fun (pv) -> CvParam(term,pv,attributes)

        | Attribute AttributeBase.person (attribute,key) 
        | Attribute AttributeBase.investigation (attribute,key) 
        | Attribute AttributeBase.study (attribute,key) 
        | Attribute AttributeBase.assay (attribute,key) 
        | Attribute AttributeBase.publication (attribute,key) 
        | Attribute AttributeBase.factorType (attribute,key) 
        | Attribute AttributeBase.designType (attribute,key)
        | Attribute AttributeBase.parameter (attribute,key) 
        | Attribute AttributeBase.characteristic (attribute,key) 
        | Attribute AttributeBase.rawData (attribute,key) 
        | Attribute AttributeBase.processedData (attribute,key)
        | Attribute AttributeBase.factor (attribute,key) -> 
            parseKey (attribute :: attributes) key

        | StructuredName term ->
            fun (pv) -> CvParam(term,pv,attributes)

        | UnMatchable name -> 
            fun (pv) -> UserParam(name,pv,attributes) // UserParam(name,pv,Attributes)