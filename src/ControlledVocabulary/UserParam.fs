namespace ControlledVocabulary

open System.Collections.Generic
open FSharpAux

/// Represents a structured value, annotated by a user defined name
[<StructuredFormatDisplay("{DisplayText}")>]
type UserParam(name : string, paramValue : ParamValue, attributes : IDictionary<string,IParam>) =

    inherit CvAttributeCollection(attributes)        

    member this.Accession   = name
    member this.Name        = name
    member this.RefUri      = "UserTerm"
    member this.Value       = paramValue
    member this.WithValue(v : ParamValue) = UserParam(name,v,attributes)
    member this.HasAttributes 
        with get() = this.Attributes |> Seq.isEmpty |> not

    interface IParam with 
        member this.Accession                   = this.Accession
        member this.Name                        = this.Name     
        member this.RefUri                      = this.RefUri   
        member this.Value                       = this.Value    
        member this.WithValue(v : ParamValue)   = this.WithValue(v)
        member this.HasAttributes               = this.HasAttributes

    new (name,pv,attributes : seq<IParam>) = 
        let dict = CvAttributeCollection(attributes)
        UserParam (name,pv,dict)
    new (name,pv) = 
        UserParam (name,pv,Seq.empty)

    //---------------------- IParam implementations ----------------------//

    /// Returns the value of the Param as a ParamValue
    static member getParamValue (up: UserParam) = Param.getParamValue up

    /// Returns the value of the Param as IConvertible
    static member getValue (up: UserParam) = Param.getValue up

    /// Returns the value of the Param as string
    static member getValueAsString (up: UserParam) = Param.getValueAsString up
        
    /// Returns the value of the Param as int if possible, else fails
    static member getValueAsInt (up: UserParam) = Param.getValueAsInt up

    /// Returns the value of the Param as a term
    static member getValueAsTerm (up: UserParam) = Param.getValueAsTerm up

    static member tryGetValueAccession (up: UserParam) = Param.tryGetValueAccession up

    static member tryGetValueRef (up: UserParam) = Param.tryGetValueRef up

    static member tryGetCvUnit (up: UserParam) : CvUnit option = Param.tryGetCvUnit up

    static member tryGetCvUnitValue (up: UserParam) = Param.tryGetCvUnitValue up

    static member tryGetCvUnitTermName (up: UserParam) = Param.tryGetCvUnitTermName up

    static member tryGetCvUnitTermAccession (up: UserParam) = Param.tryGetCvUnitTermAccession up

    static member tryGetCvUnitTermRef (up: UserParam) = Param.tryGetCvUnitTermRef up

    static member mapValue (f : ParamValue -> ParamValue) (up: UserParam) = Param.mapValue f up :?> CvParam

    static member tryMapValue (f : ParamValue -> ParamValue option) (up: UserParam) = 
        Param.tryMapValue f up 
        |> Option.map (fun v -> v :?> UserParam)

    static member tryAddName (name : string) (up: UserParam) = 
        Param.tryAddName name up
        |> Option.map (fun v -> v :?> UserParam)

    static member tryAddAccession (acc : string) (up: UserParam) = 
        Param.tryAddAccession acc up
        |> Option.map (fun v -> v :?> UserParam)

    static member tryAddReference (ref : string) (up: UserParam) = 
        Param.tryAddReference ref up
        |> Option.map (fun v -> v :?> UserParam)

    static member tryAddUnit (unit : CvUnit) (up: UserParam) = 
        Param.tryAddUnit unit up
        |> Option.map (fun v -> v :?> UserParam)

    /// Returns the id of the cv item
    static member getCvAccession (up: UserParam) = Param.getCvAccession up

    /// Returns the name of the cv item
    static member getCvName (up: UserParam) = Param.getCvName up

    /// Returns the reference of the cv item
    static member getCvRef (up: UserParam) = Param.getCvRef up

    /// Returns the full term of the cv item
    static member getTerm (up: UserParam) = Param.getTerm up

    /// Returns true, if the given term matches the term of the cv item
    static member equalsTerm (term : CvTerm) (up: UserParam) = Param.equalsTerm term up

    /// Returns true, if the terms of the given param items match
    static member equals (up1: UserParam) (up2: UserParam) = Param.equals up1 up2

    /// Returns true, if the names of the given param items match
    static member equalsName (up1: UserParam) (up2: UserParam) = Param.equalsName up1 up2

    //---------------------- UserParam specific implementations ----------------------//

    static member toCvParam (up: UserParam) = CvParam(CvTerm.create(up.Accession, up.Name, up.RefUri), up.Value, up.Attributes)

    override this.ToString() = 
        $"Name: {this.Name}\n\tValue: {this.Value}\n\tQualifiers: {this.Keys |> Seq.toList}"

    member this.DisplayText = 
        this.ToString()

[<AutoOpen>]
module UserParamExtensions = 

    type CvParam with
        static member toUserParam (cvp: CvParam) =
            UserParam(cvp.Name, cvp.Value, cvp.Attributes)

    type ParamBase with
        /// Returns Some Param if the given value item can be downcast, else returns None
        static member tryUserParam (cv : IParamBase) =
            match cv with
            | :? UserParam as param -> Some param
            | _ -> None

    type Param with
        /// Returns Some Param if the given param item can be downcast, else returns None
        static member tryUserParam (cv : IParam) =
            match cv with
            | :? UserParam as param -> Some param
            | _ -> None

        static member toCvParam (cv : IParam) =
            match cv with
            | :? UserParam as up -> up |> UserParam.toCvParam
            | :? CvParam as cvp -> cvp
            | _ -> failwith "no conversion to CvParam available for this type"
            
        static member toUserParam (cv : IParam) =
            match cv with
            | :? UserParam as up -> up 
            | :? CvParam as cvp -> cvp |> CvParam.toUserParam
            | _ -> failwith "no conversion to CvParam available for this type"

    type CvBase with
        /// Returns Some UserParam, if the given value item can be downcast, else returns None
        static member tryUserParam (cv : ICvBase) =
            match cv with
            | :? UserParam as param -> Some param
            | _ -> None