namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux

/// Represents a structured value, annotated by a user defined name
type UserParam(name : string, paramValue : ParamValue, attributes : IDictionary<string,IParam>) =

    inherit CvAttributeCollection(attributes)        

    interface IParam with 
        member this.ID     = name
        member this.Name   = name
        member this.RefUri = "UserTerm"
        member this.Value  = paramValue

    new (name,pv,attributes : seq<IParam>) = 
        let dict = CvAttributeCollection(attributes)
        UserParam (name,pv,dict)
    new (name,pv) = 
        UserParam (name,pv,Seq.empty)

    /// Returns Some UserParam, if the given value item can be downcast, else returns None
    static member tryCvParam (cv : IParamBase) =
        match cv with
        | :? UserParam as param -> Some param
        | _ -> None

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tValue: {(this :> IParamBase).Value}\n\tQualifiers: {this.Keys |> Seq.toList}"