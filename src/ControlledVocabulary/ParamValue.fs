namespace ControlledVocabulary

open System

/// Represent the different cases of a parameter, which is either a simple value,
/// a CvTerm or a simple value with CvUnit 
// ?Maybe [<Struct>]
type ParamValue =
    | Value of v: IConvertible
    // id|accession * name|value * ref
    | CvValue of cv: CvTerm
    // value * CvUnit
    | WithCvUnitAccession of cvu : IConvertible * CvUnit

    //static member getValueAsStringWithUnit (param:ParamValue) =
    //    match param with
    //    | Value   v                 -> string v
    //    | CvValue (_,v,_)           -> v
    //    | WithCvUnitAccession (v,_) -> string v

    /// Returns the value of the Param as IConvertible
    static member getValue (param:ParamValue) =
        match param with
        | Value   v                 -> v
        | CvValue (_,v,_)           -> v :> IConvertible
        | WithCvUnitAccession (v,_) -> v

    /// Returns the value of the Param as string
    static member getValueAsString (param:ParamValue) =
        match param with
        | Value   v                 -> string v
        | CvValue (_,v,_)           -> v
        | WithCvUnitAccession (v,_) -> string v
        
    /// Returns the value of the Param as int if possible, else fails
    static member getValueAsInt (param:ParamValue) =
        match param with
        | Value   v                 -> v :?> int
        | CvValue (_,v,_)           -> v |> int
        | WithCvUnitAccession (v,_) -> v :?> int

    /// Returns the value of the Param as a term
    static member getValueAsTerm (param:ParamValue) =
        match param with
        | Value   v              -> CvTerm.fromName (v |> string)
        | CvValue term           -> term
        | WithCvUnitAccession (v,_) -> CvTerm.fromName (v |> string)

    /// Returns the unit of the Param if it exists, else returns None
    static member tryGetUnit  (param:ParamValue) : CvUnit option =
        match param with
        | Value _                   -> None
        | CvValue _                 -> None
        | WithCvUnitAccession (_,u) -> Some u

    /// Returns a new paramValue with the given name if possible, else returns None
    static member tryAddName (name : string) (param : ParamValue) = 
        match param with
        | Value v                   -> Some (Value name)
        | CvValue (id,"",ref)       -> Some (CvValue (id,name,ref))
        | CvValue term              -> None
        | WithCvUnitAccession (_,u) -> None
        
    /// Returns a new paramValue with the given annotationID if possible, else returns None
    static member tryAddAnnotationID (id : string) (param : ParamValue) = 
        match param with
        | Value (:? string as v)    -> Some (CvValue (id,v,""))
        | Value v                   -> None
        | CvValue ("",name,ref)     -> Some (CvValue (id,name,ref))
        | CvValue term              -> None
        | WithCvUnitAccession (_,u) -> None

    /// Returns a new paramValue with the given reference if possible, else returns None
    static member tryAddReference (ref : string) (param : ParamValue) = 
        match param with
        | Value (:? string as v)    -> Some (CvValue ("",v,ref))
        | Value v                   -> None
        | CvValue (id,name,"")     -> Some (CvValue (id,name,ref))
        | CvValue term              -> None
        | WithCvUnitAccession (_,u) -> None

    /// Returns a new paramValue with the given unit if possible, else returns None
    static member tryAddUnit (unit : CvUnit) (param : ParamValue) = 
        match param with
        | Value v                   -> Some (WithCvUnitAccession (v,unit))
        | CvValue term              -> None
        | WithCvUnitAccession (_,u) -> None