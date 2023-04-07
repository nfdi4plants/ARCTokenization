namespace ArcGraphModel


/// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
/// ----------------------------------------------------------------------------------------
/// Controlled vocabulary (Cv) 
/// ----------------------------------------------------------------------------------------



module Param =
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

    type Address =
        /// Represents an address in the form of: sheetname * row * col
        | Table of string * int * int 

    /// Represents a term from a controlled vocabulary (Cv)
    /// in the form of: id|accession * name|value * refUri
    // ?Maybe [<Struct>]
    //[<Struct>]
    type CvTerm = string * string * string

    /// Represents a unit term from the unit ontology 
    /// in the form of: id|accession * name * refUri
    // ?Maybe [<Struct>]
    type CvUnit = string * string * string

    /// Represent the different cases of a parameter, which is either a simple value,
    /// a CvTerm or a simple value with CvUnit 
    // ?Maybe [<Struct>]
    type ParamValue<'T when 'T :> IConvertible> =
        | Value of v: 'T 
        // id|accession * name|value * ref
        | CvValue of cv: CvTerm
        // value * CvUnit
        | WithCvUnitAccession of cvu : 'T * CvUnit

        static member getValue (param:ParamValue<_>) =
            match param with
            | Value   v                 -> string v
            | CvValue (_,v,_)           -> v
            | WithCvUnitAccession (v,_) -> string v

        static member tryGetUnit  (param:ParamValue<_>) : CvUnit option =
            match param with
            | Value _                   -> None
            | CvValue _                 -> None
            | WithCvUnitAccession (_,u) -> Some u

    /// Interface ensures the propterties necessary for CvTerm 
    type ICvBase =
        abstract member ID       : string
        abstract member Name     : string
        abstract member RefUri   : string
        // here or as node/edge abstraction 
        // abstract member DataType   : ModelDataType
        // abstract member Adress     : Adress / Location / Position

        //override this.ToString() = 
        //    $"Name: {this.Name}\nID: {this.ID}\nRefUri: {this.RefUri}"

    /// Interface ensures the value as ParamValue<'T>  
    type IParamBase<'T when 'T :> IConvertible> =
        abstract member Value : ParamValue<'T>

        //override this.ToString() = 
        //    $"Value: {this.Value}"

    /// Represents controlled vocabulary term as key for a ParamValue as value
    type CvParam<'T when 'T :> IConvertible>(cvAccession : string, cvName : string, cvRefUri : string, paramValue : ParamValue<'T>) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri
        interface IParamBase<'T> with 
            member this.Value  = paramValue

        new ((id,name,ref) : CvTerm,pv : ParamValue<'T>) = CvParam (id,name,ref,pv)

        member this.CvAccession = cvAccession
        
        static member fromValue(category : CvTerm,v : 'T) =
            CvParam(category, ParamValue.Value (v :> IConvertible))

        static member fromCategory(category : CvTerm,term : CvTerm) =
            CvParam(category, ParamValue.CvValue term)

        static member fromValueWithUnit(category : CvTerm,v : 'T, unit : CvUnit) =
            CvParam(category, ParamValue.WithCvUnitAccession (v :> IConvertible,unit))

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\n\tID: {this.CvAccession}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tValue: {(this :> IParamBase<'T>).Value}"

    /// Represents user defined term as key for a ParamValue as value
    type UserParam<'T when 'T :> IConvertible>(name : string, paramValue : ParamValue<'T>) =
        interface ICvBase with 
            member this.ID     = name
            member this.Name   = name
            member this.RefUri = "UserTerm"
        interface IParamBase<'T> with
            member this.Value  = paramValue

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tValue: {(this :> IParamBase<'T>).Value}"

    type CvObject<'T>(cvAccession : string, cvName : string, cvRefUri : string, object : 'T) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri

        new ((id,name,ref) : CvTerm,object : 'T) = CvObject (id,name,ref,object)

        member this.Object = object

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}"

    type CvDoc<'T when 'T :> IParamBase<IConvertible> and 'T:> ICvBase>(cvAccession : string, cvName : string, cvRefUri : string, doc : 'T list) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri

        new ((id,name,ref) : CvTerm,doc : 'T list) = CvDoc (id,name,ref,doc)

        member this.Document = doc

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}"

    // Maybe a CvJDoc is necessary. Using SimpleJson as document type




    let getCvAccession (param : #ICvBase) =
        param.ID

    let getCvName (param : #ICvBase) =
        param.Name

    let getCvRef (param : #ICvBase) =
        param.RefUri

    let getValue (param : #IParamBase<_>) =
        match param.Value with
        | Value                    v    -> v
        | CvValue               (_,v,_) -> v
        | WithCvUnitAccession     (v,_) -> v

    let tryGetValueAccession (param : #IParamBase<_>) =
        match param.Value with
        | CvValue                   (a,_,_) -> Some a
        | Value                      _      -> None     // mere Value has no accession number
        | WithCvUnitAccession        _      -> None     // use tryGetCvUnitAccession instead
        //| WithCvUnitAccession (_,(a,_,_))   -> Some a

    let tryGetValueRef (param : #IParamBase<_>) =
        match param.Value with
        | CvValue               (_,_,r) -> Some r
        | Value                      _  -> None     // mere Value has no ref
        | WithCvUnitAccession        _  -> None     // use tryGetCvUnitRef instead

    let tryGetCvUnit (param : #IParamBase<_>) : CvUnit option =
        match param.Value with
        | Value                  _  -> None
        | CvValue                _  -> None
        | WithCvUnitAccession (_,u) -> Some u

    let tryGetCvUnitValue (param : #IParamBase<_>) : #IConvertible option =
        match param.Value with
        | Value                  _  -> None
        | CvValue                _  -> None
        | WithCvUnitAccession (v,_) -> Some v

    let tryGetCvUnitName (param : #IParamBase<_>) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,(_,n,_)) -> Some n

    let tryGetCvUnitAccession (param : #IParamBase<_>) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,(a,_,_)) -> Some a

    let tryGetCvUnitRef (param : #IParamBase<_>) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,(_,_,r)) -> Some r