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

    member this.Accession   = cvAccession
    member this.Name        = cvName
    member this.RefUri      = cvRef
    member this.Value       = paramValue
    member this.WithValue(v : ParamValue) = CvParam(cvAccession,cvName,cvRef,v,attributes)
    member this.HasAttributes 
        with get() = this.Attributes |> Seq.isEmpty |> not

    interface IParam with 
        member this.Accession                   = this.Accession
        member this.Name                        = this.Name     
        member this.RefUri                      = this.RefUri   
        member this.Value                       = this.Value    
        member this.WithValue(v : ParamValue)   = this.WithValue(v)
        member this.HasAttributes               = this.HasAttributes

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

    /// Serves as the default hash function.
    override this.GetHashCode() =
        hash (cvAccession, cvName, cvRef, paramValue, attributes)

    /// Determines whether the specified object is equals to the current object.
    override this.Equals(o) =
        match o with
        | :? CvTerm as cvt -> Param.equalsTerm cvt this
        | :? CvParam as cvp -> 
            cvp.Name        = this.Name &&
            cvp.Accession   = this.Accession &&
            cvp.RefUri      = this.RefUri &&
            cvp.Value       = this.Value &&
            cvp.Attributes  = this.Attributes   // careful bc of Dictionary! Comment out if necessary!
        | :? IParam as p ->
            p.Name      = this.Name &&
            p.Accession = this.Accession &&
            p.RefUri    = this.Name &&
            p.Value     = this.Value
        | :? ICvBase as cvb -> CvBase.equals cvb this
        | :? IParamBase as pb -> pb.Value = this.Value
        | _ -> false

    //---------------------- IParam implementations ----------------------//

    /// Returns the value of the Param as a ParamValue
    static member getParamValue (cvp: CvParam) = Param.getParamValue cvp

    /// Returns the value of the Param as IConvertible
    static member getValue (cvp: CvParam) = Param.getValue cvp

    /// Returns the value of the Param as string
    static member getValueAsString (cvp: CvParam) = Param.getValueAsString cvp
        
    /// Returns the value of the Param as int if possible, else fails
    static member getValueAsInt (cvp: CvParam) = Param.getValueAsInt cvp

    /// Returns the value of the Param as a term
    static member getValueAsTerm (cvp: CvParam) = Param.getValueAsTerm cvp

    static member tryGetValueAccession (cvp: CvParam) = Param.tryGetValueAccession cvp

    static member tryGetValueRef (cvp: CvParam) = Param.tryGetValueRef cvp

    static member tryGetCvUnit (cvp: CvParam) : CvUnit option = Param.tryGetCvUnit cvp

    static member tryGetCvUnitValue (cvp: CvParam) = Param.tryGetCvUnitValue cvp

    static member tryGetCvUnitTermName (cvp: CvParam) = Param.tryGetCvUnitTermName cvp

    static member tryGetCvUnitTermAccession (cvp: CvParam) = Param.tryGetCvUnitTermAccession cvp

    static member tryGetCvUnitTermRef (cvp: CvParam) = Param.tryGetCvUnitTermRef cvp

    static member mapValue (f : ParamValue -> ParamValue) (cvp: CvParam) = Param.mapValue f cvp :?> CvParam

    static member tryMapValue (f : ParamValue -> ParamValue option) (cvp: CvParam) = 
        Param.tryMapValue f cvp 
        |> Option.map (fun v -> v :?> CvParam)

    static member tryAddName (name : string) (cvp: CvParam) = 
        Param.tryAddName name cvp
        |> Option.map (fun v -> v :?> CvParam)

    static member tryAddAccession (acc : string) (cvp: CvParam) = 
        Param.tryAddAccession acc cvp
        |> Option.map (fun v -> v :?> CvParam)

    static member tryAddReference (ref : string) (cvp: CvParam) = 
        Param.tryAddReference ref cvp
        |> Option.map (fun v -> v :?> CvParam)

    static member tryAddUnit (unit : CvUnit) (cvp: CvParam) = 
        Param.tryAddUnit unit cvp
        |> Option.map (fun v -> v :?> CvParam)

    /// Returns the id of the cv item
    static member getCvAccession (cvp: CvParam) = Param.getCvAccession cvp

    /// Returns the name of the cv item
    static member getCvName (cvp: CvParam) = Param.getCvName cvp

    /// Returns the reference of the cv item
    static member getCvRef (cvp: CvParam) = Param.getCvRef cvp

    /// Returns the full term of the cv item
    static member getTerm (cvp: CvParam) = Param.getTerm cvp

    /// Returns true, if the given term matches the term of the cv item
    static member equalsTerm (term : CvTerm) (cvp: CvParam) = Param.equalsTerm term cvp

    /// Returns true, if the terms of the given param items match
    static member equals (cvp1 : CvParam) (cvp2 : CvParam) = Param.equals cvp1 cvp2

    /// Returns true, if the names of the given param items match
    static member equalsName (cvp1 : CvParam) (cvp2 : CvParam) = Param.equalsName cvp1 cvp1

    //---------------------- CvParam specific implementations ----------------------//

    /// Create a CvParam from a category and a simple value
    static member fromValue (category : CvTerm) (v : 'T) =
        CvParam(category, ParamValue.Value (v :> IConvertible))

    /// Creates a CvParam from a category and a value coming from a controlled vocabulary.
    static member fromCategory (category : CvTerm) (term : CvTerm) =
        CvParam(category, ParamValue.CvValue term)

    /// Creates a CvParam from a category, a simple value and a unit coming from a controlled vocabulary.
    static member fromValueWithUnit (category : CvTerm) (v : 'T) (unit : CvUnit) =
        CvParam(category, ParamValue.WithCvUnitAccession (v :> IConvertible,unit))

    static member getAttributes (param : CvParam) =
        param.Values |> Seq.cast

    override this.ToString() = 
        $"CvParam: {this.Name}\n\tID: {this.Accession}\n\tRefUri: {this.RefUri}\n\tValue: {this.Value}\n\tAttributes: {this.Keys |> Seq.toList}"

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
