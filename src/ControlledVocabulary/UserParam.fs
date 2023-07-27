namespace ControlledVocabulary

open System.Collections.Generic
open FSharpAux

/// Represents a structured value, annotated by a user defined name
[<StructuredFormatDisplay("{DisplayText}")>]
type UserParam(name : string, paramValue : ParamValue, attributes : IDictionary<string,IParam>) =

    inherit CvAttributeCollection(attributes)        

    interface IParam with 
        member this.ID     = name
        member this.Name   = name
        member this.RefUri = "UserTerm"
        member this.Value  = paramValue
        member this.WithValue(v : ParamValue) = UserParam(name,v,attributes)
        member this.HasAttributes 
            with get() = this.Attributes |> Seq.isEmpty |> not

    new (name,pv,attributes : seq<IParam>) = 
        let dict = CvAttributeCollection(attributes)
        UserParam (name,pv,dict)
    new (name,pv) = 
        UserParam (name,pv,Seq.empty)

    /// Returns Some UserParam, if the given value item can be downcast, else returns None
    static member tryUserParam (cv : ICvBase) =
        match cv with
        | :? UserParam as param -> Some param
        | _ -> None

    /// Returns Some Param if the given value item can be downcast, else returns None
    static member tryUserParam (cv : IParamBase) =
        match cv with
        | :? UserParam as param -> Some param
        | _ -> None

    /// Returns Some Param if the given param item can be downcast, else returns None
    static member tryUserParam (cv : IParam) =
        match cv with
        | :? UserParam as param -> Some param
        | _ -> None

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tValue: {(this :> IParamBase).Value}\n\tQualifiers: {this.Keys |> Seq.toList}"

    member this.DisplayText = 
        this.ToString()