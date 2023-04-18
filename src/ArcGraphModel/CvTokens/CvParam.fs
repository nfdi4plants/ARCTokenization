namespace ArcGraphModel

open System
open System.Collections.Generic
open FSharpAux

/// Represents a structured value, annotated with a controlled vocabulary term
///
/// Qualifiers can be used to further describe the CvParam
type CvParam(cvAccession : string, cvName : string, cvRefUri : string, paramValue : ParamValue, qualifiers : IDictionary<string,CvParam>) =

    inherit Dictionary<string,CvParam>(qualifiers)        

    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri
    interface IParamBase with 
        member this.Value  = paramValue

    new (id,name,ref,pv,qualifiers : seq<CvParam>) = 
        let dict = 
            qualifiers
            |> Seq.map (fun cvp -> (cvp :> ICvBase).Name, cvp)
            |> Dictionary.ofSeq
        CvParam (id,name,ref,pv,dict)
    new (id,name,ref,pv) = 
        CvParam (id,name,ref,pv,Seq.empty)

    new ((id,name,ref) : CvTerm,pv,qualifiers : seq<CvParam>) = 
        CvParam (id,name,ref,pv,qualifiers)
    new (cvTerm,pv : ParamValue) = 
        CvParam (cvTerm,pv,Seq.empty)

    /// Create a CvParam from a category and a simple value
    static member fromValue (category : CvTerm) (v : 'T) =
        CvParam(category, ParamValue.Value (v :> IConvertible))

    /// Create a CvParam from a category and a value coming from a controlled vocabulary
    static member fromCategory (category : CvTerm) (term : CvTerm) =
        CvParam(category, ParamValue.CvValue term)

    /// Create a CvParam from a category, a simple value and a unit coming from a controlled vocabulary
    static member fromValueWithUnit (category : CvTerm) (v : 'T) (unit : CvUnit) =
        CvParam(category, ParamValue.WithCvUnitAccession (v :> IConvertible,unit))

    /// Returns the typed value of the CvParam
    static member getValue (cvp : CvParam) =
        (cvp :> IParamBase).Value

    /// Returns the value of the CvParam as a string
    static member getValueAsString (cvp : CvParam) =
        (cvp :> IParamBase).Value
        |> ParamValue.getValueAsString

    /// Returns the value of the CvParam as an int if possible, else fails
    static member getValueAsInt (cvp : CvParam) =
        (cvp :> IParamBase).Value
        |> ParamValue.getValueAsInt

    /// Returns the value of the CvParam as a term
    static member getValueAsTerm (cvp : CvParam) =
        (cvp :> IParamBase).Value
        |> ParamValue.getValueAsTerm

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tValue: {(this :> IParamBase).Value}"