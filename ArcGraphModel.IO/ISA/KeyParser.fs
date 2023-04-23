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

    static member investigationPublication : ContainerBase = 
        {Term = Terms.publication; Key = "INVESTIGATION PUBLICATIONS"}

    static member study : ContainerBase = 
        {Term = Terms.study; Key = "STUDY"}

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

    static member name : TokenBase = {Term = Terms.name; Key = "Name"}

    static member description : TokenBase = {Term = Terms.descriptor; Key = "Description"}

    static member familyName : TokenBase = {Term = Terms.familyName; Key = "Last Name"}

    static member givenName : TokenBase = {Term = Terms.givenName; Key = "First Name"}

    static member midInitials : TokenBase = {Term = Terms.midInitials; Key = "Mid Initials"}

    static member email : TokenBase = {Term = Terms.email; Key = "Email"}

    static member phone : TokenBase = {Term = Terms.phone; Key = "Phone"}

    static member factorType : TokenBase = {Term = Terms.factor; Key = "Factor Type"}

    static member designType : TokenBase = {Term = Terms.design; Key = "Design Type"}

    static member termSourceRef : TokenBase = {Term = Terms.termSourceRef; Key = "Term Source REF"}

    static member annotationID : TokenBase = {Term = Terms.annotationID; Key = "Term Accession Number"}

type QualifierBase =   
    {
        Term    : CvTerm
        Key     : string
    }

    static member person : QualifierBase = {Term = Terms.person; Key = "Person"}

    static member study : QualifierBase = {Term = Terms.study; Key = "Study"}

    static member assay : QualifierBase = {Term = Terms.assay; Key = "Assay"}

    static member publication : QualifierBase = {Term = Terms.publication; Key = "Publication"}

    static member investigation : QualifierBase = {Term = Terms.investigation; Key = "Investigation"}

    static member factor : QualifierBase = {Term = Terms.factor; Key = "Factor"}

    static member factorType : QualifierBase = {Term = Terms.factor; Key = "Factor Type"}

    static member designType : QualifierBase = {Term = Terms.design; Key = "Design Type"}

    //static member person : QualifierBase = {Term = Terms.person; Key = "Person"}

module KeyParser =

    let (|Container|_|) (container : ContainerBase) (key : FsCell) =
        if key.Value.Trim() = container.Key then
            Some (CvContainer(container.Term))
        else 
            None

    let (|Qualifier|_|) (token : QualifierBase) (key : string) =
    
        if key.Contains token.Key then 
            let newKey = key.Replace(token.Key,"").Trim()
            let qualifier = CvParam(token.Term,ParamValue.Value "")
            Some (qualifier,newKey)
        else None

    let (|Token|_|) (token : TokenBase) (key : string) : CvTerm Option =
        if key =  token.Key then 
            Some token.Term
        else None

    let (|StructuredName|_|) (key : string) : CvTerm Option =
        let namePattern = @"(?<= \[).*(?=[\]])"
        Regex.tryParseValue namePattern key
        |> Option.map (fun n ->
            "",n,""
        )

    let (|UnMatchable|) (key : string) : string =
        key


    let rec parseKey (attributes : IParam list) (key : string) : ParamValue -> IParam = 
        match key with
        | Token TokenBase.name term
        | Token TokenBase.description term
        | Token TokenBase.familyName term
        | Token TokenBase.givenName term
        | Token TokenBase.midInitials term
        | Token TokenBase.email term
        | Token TokenBase.factorType term
        | Token TokenBase.designType term
        | Token TokenBase.annotationID term
        | Token TokenBase.termSourceRef term
        | Token TokenBase.phone term -> 
            fun (pv) -> CvParam(term,pv,attributes)

        | Qualifier QualifierBase.person (attribute,key) 
        | Qualifier QualifierBase.investigation (attribute,key) 
        | Qualifier QualifierBase.study (attribute,key) 
        | Qualifier QualifierBase.assay (attribute,key) 
        | Qualifier QualifierBase.publication (attribute,key) 
        | Qualifier QualifierBase.factorType (attribute,key) 
        | Qualifier QualifierBase.designType (attribute,key)
        | Qualifier QualifierBase.factor (attribute,key) -> 
            parseKey (attribute :: attributes) key

        | StructuredName term ->
            fun (pv) -> CvParam(term,pv,attributes)

        | UnMatchable name -> 
            fun (pv) -> UserParam(name,pv,attributes) // UserParam(name,pv,qualifiers)