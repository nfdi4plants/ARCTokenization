namespace ArcGraphModel


/// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
/// ----------------------------------------------------------------------------------------
/// Controlled vocabulary (Cv) 
/// ----------------------------------------------------------------------------------------



module ParamTwo =
    open System


    type ModelDataType =
        | Investigation
        | Study
        | Assay
        | Person
        | IsaCharacteristic  
        | IsaFactor
        | IsaParamter
        | IsaSample
        | IsaProtocolRef
        | File
        | Directory
        | Custom of string 

    type Adress =
        // sheetname * row * col
        | Table of string * int * int 

    /// Represents a term from a controlled vocabulary (Cv)
    /// in the form of: id|accession * name|value * refUri
    // ?Maybe [<Struct>]
    type CvTerm = string * string * string

    ///// Represents a unit term from the unit ontology 
    ///// in the form of: id|accession * name * refUri
    //// ?Maybe [<Struct>]
    //type CvUnit = string * string * string

    //type BaseValue = string * ValueType
    //| Float of float
    //| Int of int
    //| String of string

    type BaseValue = 
        | Float of float
        | Int of int
        | String of string

        member this.AsString() = 
            match this with
            | Float f  -> string f
            | Int i    -> string i
            | String s -> s

    /// Represent the different cases of a parameter, which is either a simple value,
    /// a CvTerm or a simple value with CvUnit 
    // ?Maybe [<Struct>]
    type ParamValue =
    | Value of v: BaseValue
    // id|accession * name|value * ref
    | CvValue of cv: CvTerm
    // value * CvUnit
    | WithCvUnitAccession of cvu : BaseValue * CvTerm

    /// Interface ensures the propterties necessary for CvTerm 
    type ICvBase =        
        abstract member ID       : string    
        abstract member Name     : string
        abstract member RefUri   : string
        // here or as node/edge abstraction 
        // abstract member DataType   : ModelDataType
        // abstract member Adress     : Adress / Location / Position

    /// Interface ensures the value as ParamValue<'T>  
    type IParamBase =        
        abstract member Value    : ParamValue 

    /// Represents controlled vocabulary term as key for a ParamValue as value
    type CvParam(cvAccession:string,cvName:string,cvRefUri:string,paramValue:ParamValue) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri
        interface IParamBase with 
            member this.Value  = paramValue

        member this.CvAccession = cvAccession

    /// Represents user defined term as key for a ParamValue as value
    type UserParam(name:string,paramValue:ParamValue) =
        interface ICvBase with 
            member this.ID     = name
            member this.Name   = name
            member this.RefUri = "UserTerm"
        interface IParamBase with         
            member this.Value  = paramValue


    type CvObject<'T>(cvAccession:string,cvName:string,cvRefUri:string,object:'T) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri    

        member this.Object = object

    type CvDoc<'T when 'T :> IParamBase and 'T:> ICvBase>(cvAccession:string,cvName:string,cvRefUri:string,doc:'T list) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri    

        member this.Document = doc

    // Maybe a CvJDoc is necessary. Using SimpleJson as document type




    let getCvAccessionOrName (param:#ICvBase) =
        param.ID

    let getValue (param:IParamBase) =
        match param.Value with
        | Value   v                 -> v.AsString()
        | CvValue (_,v,_)           -> v
        | WithCvUnitAccession (v,_) -> v.AsString()

    let tryGetCvUnit  (param:IParamBase) : CvTerm option =
        match param.Value with
        | Value _                   -> None
        | CvValue _                 -> None
        | WithCvUnitAccession (_,u) -> Some u



