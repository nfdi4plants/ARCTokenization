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

    /// Returns true, if the name of the given params match
    static member equalsName (param1 : IParam) (param2 : IParam) =
        CvBase.equalsName param1 param2

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

    /// Returns Some Value of type 'T, if the given param can be downcast, else returns None
    static member inline tryAs<'T when 'T :> IParam> (cv : IParam) =
        match cv with
        | :? 'T as cv -> Some cv
        | _ -> None

    /// Returns true, if the given param can be downcast
    static member inline is<'T when 'T :> IParam> (cv : IParam) =
        match cv with
        | :? 'T as cv -> true
        | _ -> false

    /// Returns the typed value of the param.
    static member getParamValue (param : IParam) =
        param |> ParamBase.getParamValue

    /// Returns the value of the Param as a IConvertiböe
    static member getValue (param:IParamBase) =
        param |> ParamBase.getValue


    /// Returns the value of the param as a string.
    static member getValueAsString (cvParam : IParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsString

    /// Returns the value of the param as an int if possible, else fails.
    static member getValueAsInt (cvParam : IParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsInt

    /// Returns the value of the param as a CvTerm.
    static member getValueAsTerm (cvParam : IParam) =
        (cvParam :> IParamBase).Value
        |> ParamValue.getValueAsTerm

    static member mapValue (f : ParamValue -> ParamValue) (param : IParam) = 
        ParamBase.mapValue f param :?> IParam

    static member tryMapValue (f : ParamValue -> ParamValue option) (param : IParam) = 
        ParamBase.tryMapValue f param |> Option.map (fun v -> v :?> IParam)

    static member tryAddName (name : string) (param : IParam) = 
        ParamBase.tryAddName name param |> Option.map (fun v -> v :?> IParam)

    static member tryAddAnnotationID (id : string) (param : IParam) = 
        ParamBase.tryAddAnnotationID id param |> Option.map (fun v -> v :?> IParam)

    static member tryAddReference (ref : string) (param : IParam) = 
        ParamBase.tryAddReference ref param |> Option.map (fun v -> v :?> IParam)

    static member tryAddUnit (unit : CvUnit) (param : IParam) = 
        ParamBase.tryAddUnit unit param |> Option.map (fun v -> v :?> IParam)
// TO DO: create-Funktionen in UserParam und CvParam sollen IParam zurückgeben statt ihren eigenen Typ
