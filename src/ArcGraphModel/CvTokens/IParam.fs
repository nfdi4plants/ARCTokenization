namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux


type IParam =

    inherit ICvBase
    inherit IParamBase


type Param =

    /// Returns true, if the given term matches the term of the param
    static member equalsTerm (term : CvTerm) (param : IParam) =
        CvBase.equalsTerm term param

    /// Returns true, if the terms of the given params match
    static member equals (param1 : IParam) (param2 : IParam) =
        CvBase.equals param1 param2

    /// Returns Some Param, if the given cv item can be downcast, else returns None
    static member tryParam (cv : ICvBase) =
        match cv with
        | :? IParam as param -> Some param
        | _ -> None

    /// Returns Some Param, if the given value item can be downcast, else returns None
    static member tryParam (cv : IParamBase) =
        match cv with
        | :? IParam as param -> Some param
        | _ -> None

    /// Returns the typed value of the CvParam.
    static member getValue (cvParam : IParam) =
        (cvParam :> IParamBase).Value

    /// Returns the value of the CvParam as a string.
    static member getValueAsString (cvParam : IParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsString

    /// Returns the value of the CvParam as an int if possible, else fails.
    static member getValueAsInt (cvParam : IParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsInt

    /// Returns the value of the CvParam as a CvTerm.
    static member getValueAsTerm (cvParam : IParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsTerm
// TO DO: create-Funktionen in UserParam und CvParam sollen IParam zurückgeben statt ihren eigenen Typ
