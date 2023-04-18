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

    //static member getValueAsStringWithUnit (param:ParamValue) =
    //    match param with
    //    | Value   v                 -> string v
    //    | CvValue (_,v,_)           -> v
    //    | WithCvUnitAccession (v,_) -> string v

    static member getValueAsString (param:ParamValue) =
        match param with
        | Value   v                 -> string v
        | CvValue (_,v,_)           -> v
        | WithCvUnitAccession (v,_) -> string v

    static member getValueAsInt (param:ParamValue) =
        match param with
        | Value   v                 -> v :?> int
        | CvValue (_,v,_)           -> v |> int
        | WithCvUnitAccession (v,_) -> v :?> int

    static member getValueAsTerm (param:ParamValue) =
        match param with
        | Value   v              -> CvTerm.fromName (v |> string)
        | CvValue term           -> term
        | WithCvUnitAccession (v,_) -> CvTerm.fromName (v |> string)


    static member tryGetUnit  (param:ParamValue) : CvUnit option =
        match param with
        | Value _                   -> None
        | CvValue _                 -> None
        | WithCvUnitAccession (_,u) -> Some u