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

    /// Creates a CvParam from a category and a simple value.
    static member fromValue (category : CvTerm) (v : 'T) =
        CvParam(category, ParamValue.Value (v :> IConvertible))

    /// Creates a CvParam from a category and a value coming from a controlled vocabulary.
    static member fromCategory (category : CvTerm) (term : CvTerm) =
        CvParam(category, ParamValue.CvValue term)

    /// Creates a CvParam from a category, a simple value and a unit coming from a controlled vocabulary.
    static member fromValueWithUnit (category : CvTerm) (v : 'T) (unit : CvUnit) =
        CvParam(category, ParamValue.WithCvUnitAccession (v :> IConvertible,unit))

    /// Returns the typed value of the CvParam.
    static member getValue (cvParam : CvParam) =
        (cvParam :> IParamBase).Value

    /// Returns the value of the CvParam as a string.
    static member getValueAsString (cvParam : CvParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsString

    /// Returns the value of the CvParam as an int if possible, else fails.
    static member getValueAsInt (cvParam : CvParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsInt

    /// Returns the value of the CvParam as a CvTerm.
    static member getValueAsTerm (cvParam : CvParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsTerm

    /// Returns the qualifier with the given name if present in the CvParam. Else returns None.
    static member tryGetQualifier qualifierName (cvParam : CvParam) =
        Dictionary.tryFind qualifierName cvParam

    /// Returns the ParamValue of the qualifier with the given name if present in the CvParam. Else returns None.
    static member tryGetQualifierValue qualifierName (cvParam : CvParam) =
        CvParam.tryGetQualifier qualifierName cvParam
        |> Option.map CvParam.getValue

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tValue: {(this :> IParamBase).Value}"