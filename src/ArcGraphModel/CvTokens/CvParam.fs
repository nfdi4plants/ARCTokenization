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

    static member fromValue(category : CvTerm,v : 'T) =
        CvParam(category, ParamValue.Value (v :> IConvertible))

    static member fromCategory(category : CvTerm,term : CvTerm) =
        CvParam(category, ParamValue.CvValue term)

    static member fromValueWithUnit(category : CvTerm,v : 'T, unit : CvUnit) =
        CvParam(category, ParamValue.WithCvUnitAccession (v :> IConvertible,unit))

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tValue: {(this :> IParamBase).Value}"