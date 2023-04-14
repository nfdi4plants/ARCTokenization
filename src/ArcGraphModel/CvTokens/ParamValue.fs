namespace ArcGraphModel

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

    static member getValue (param:ParamValue) =
        match param with
        | Value   v                 -> string v
        | CvValue (_,v,_)           -> v
        | WithCvUnitAccession (v,_) -> string v

    static member tryGetUnit  (param:ParamValue) : CvUnit option =
        match param with
        | Value _                   -> None
        | CvValue _                 -> None
        | WithCvUnitAccession (_,u) -> Some u