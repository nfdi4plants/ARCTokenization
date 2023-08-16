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

    /// Returns the value of the Param as IConvertible
    static member getValue (param:ParamValue) =
        match param with
        | Value   v                 -> v
        | CvValue cv                -> cv.Name :> IConvertible
        | WithCvUnitAccession (v,_) -> v

    /// Returns the value of the Param as string
    static member getValueAsString (param:ParamValue) =
        match param with
        | Value   v                 -> string v
        | CvValue cv                -> cv.Name
        | WithCvUnitAccession (v,_) -> string v
        
    /// Returns the value of the Param as int if possible, else fails
    static member getValueAsInt (param:ParamValue) =
        match param with
        | Value   v                 -> v :?> int
        | CvValue cv                -> cv.Name |> int
        | WithCvUnitAccession (v,_) -> v :?> int

    /// Returns the value of the Param as a term
    static member getValueAsTerm (param:ParamValue) =
        match param with
        | Value   v                 -> CvTerm.create(name = (v |> string))
        | CvValue term              -> term
        | WithCvUnitAccession (v,_) -> CvTerm.create(name = (v |> string))

    /// Returns the unit of the Param if it exists, else returns None
    static member tryGetUnit  (param:ParamValue) : CvUnit option =
        match param with
        | Value _                   -> None
        | CvValue _                 -> None
        | WithCvUnitAccession (_,u) -> Some u

    /// Returns a new paramValue with the given name if possible, else returns None
    static member tryAddName (name : string) (param : ParamValue) = 
        match param with
        | Value v                       -> Some (Value name)
        | CvValue cv when cv.Name = ""  -> Some (CvValue {cv with Name = name})
        | CvValue term                  -> None
        | WithCvUnitAccession (_,u)     -> None
        
    /// Returns a new paramValue with the given annotationID if possible, else returns None
    static member tryAddAccession (accession : string) (param : ParamValue) = 
        match param with
        | Value (:? string as v)            -> Some (CvValue (CvTerm.create(accession = accession, name = v, ref = "")))
        | Value v                           -> None
        | CvValue cv when cv.Accession = "" -> Some (CvValue {cv with Accession = accession})
        | CvValue term                      -> None
        | WithCvUnitAccession (_,u)         -> None

    /// Returns a new paramValue with the given reference if possible, else returns None
    static member tryAddReference (ref : string) (param : ParamValue) = 
        match param with
        | Value (:? string as v)        -> Some (CvValue (CvTerm.create(accession = "", name = v, ref = ref)))
        | Value v                       -> None
        | CvValue cv when cv.RefUri = ""   -> Some (CvValue {cv with RefUri = ref})
        | CvValue term                  -> None
        | WithCvUnitAccession (_,u)     -> None

    /// Returns a new paramValue with the given unit if possible, else returns None
    static member tryAddUnit (unit : CvUnit) (param : ParamValue) = 
        match param with
        | Value v                   -> Some (WithCvUnitAccession (v,unit))
        | CvValue term              -> None
        | WithCvUnitAccession (_,u) -> None