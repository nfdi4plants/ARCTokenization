namespace ControlledVocabulary

open System
open System.Collections.Generic
open FSharpAux



/// Represents a structured value, annotated with a controlled vocabulary term
///
/// Attributes can be used to further describe the CvParam
[<StructuredFormatDisplay("{DisplayText}")>]
type CvParam(cvAccession : string, cvName : string, cvRef : string, paramValue : ParamValue, attributes : IDictionary<string,IParam>) =

    inherit CvAttributeCollection(attributes)

    interface IParam with 
        member this.Accession   = cvAccession
        member this.Name        = cvName
        member this.RefUri      = cvRef
        member this.Value       = paramValue
        member this.WithValue(v : ParamValue) = CvParam(cvAccession,cvName,cvRef,v,attributes)
        member this.HasAttributes 
            with get() = this.Attributes |> Seq.isEmpty |> not

    new (id,name,ref,pv,attributes : seq<IParam>) =  
        let dict = CvAttributeCollection(attributes)
        CvParam (id,name,ref,pv,dict)
    new (id,name,ref,pv) = 
        CvParam (id,name,ref,pv,Seq.empty)
    new (id,name,ref,v : IConvertible) = 
        CvParam (id,name,ref,ParamValue.Value v)

    new (term : CvTerm, pv, attributes : seq<IParam>) = 
        CvParam (term.Accession, term.Name, term.RefUri, pv, attributes)
    new (cvTerm,pv : ParamValue) = 
        CvParam (cvTerm,pv,Seq.empty)
    new (cvTerm,v : IConvertible) = 
        CvParam (cvTerm,ParamValue.Value v)

    member this.Equals (term : CvTerm) = 
        CvBase.equalsTerm term this

    member this.Equals (cv : ICvBase) = 
        CvBase.equals cv this

    member this.Equals (cvp : CvParam) =
        this.Equals(cvp :> ICvBase)

    /// Create a CvParam from a category and a simple value
    static member fromValue (category : CvTerm) (v : 'T) =
        CvParam(category, ParamValue.Value (v :> IConvertible))

    /// Creates a CvParam from a category and a value coming from a controlled vocabulary.
    static member fromCategory (category : CvTerm) (term : CvTerm) =
        CvParam(category, ParamValue.CvValue term)

    /// Creates a CvParam from a category, a simple value and a unit coming from a controlled vocabulary.
    static member fromValueWithUnit (category : CvTerm) (v : 'T) (unit : CvUnit) =
        CvParam(category, ParamValue.WithCvUnitAccession (v :> IConvertible,unit))

    static member mapValue (f : ParamValue -> ParamValue) (param : CvParam) = 
        Param.mapValue f param

    static member tryMapValue (f : ParamValue -> ParamValue option) (param : CvParam) = 
        Param.tryMapValue f param

    static member tryAddName (value : string) (param : CvParam) = 
        Param.tryAddName value param

    static member tryAddAccession (id : string) (param : CvParam) = 
        Param.tryAddAccession id param

    static member tryAddReference (ref : string) (param : CvParam) = 
        Param.tryAddReference ref param

    static member tryAddUnit (unit : CvUnit) (param : CvParam) = 
        Param.tryAddUnit unit param

    static member getAttributes (param : CvParam) =
        param.Values |> Seq.cast


    override this.ToString() = 
        $"CvParam: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).Accession}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tValue: {(this :> IParamBase).Value}\n\tAttributes: {this.Keys |> Seq.toList}"

    member this.DisplayText = 
        this.ToString()

[<AutoOpen>]
module CvParamExtensions = 

    type ParamBase with
        /// Returns Some Param if the given value item can be downcast, else returns None
        static member tryCvParam (cv : IParamBase) =
            match cv with
            | :? CvParam as param -> Some param
            | _ -> None

    type Param with
        /// Returns Some Param if the given param item can be downcast, else returns None
        static member tryCvParam (cv : IParam) =
            match cv with
            | :? CvParam as param -> Some param
            | _ -> None

    type CvBase with
        /// Returns Some Param if the given cv item can be downcast, else returns None
        static member tryCvParam (cv : ICvBase) =
            match cv with
            | :? CvParam as param -> Some param
            | _ -> None

        /// Returns true, if the given value item can be downcast to CvParam
        static member isCvParam (cv : ICvBase) = 
            CvBase.is<CvParam> cv
