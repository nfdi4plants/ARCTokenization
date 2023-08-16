namespace ControlledVocabulary

open System.Collections.Generic
open FSharpAux


type IParam =

    inherit ICvBase
    inherit IParamBase

type Param =

    //---------------------- IParamBase implementations ----------------------//

    /// Returns the value of the Param as a ParamValue
    static member getParamValue (param: IParam) = ParamBase.getParamValue param

    /// Returns the value of the Param as IConvertible
    static member getValue (param: IParam) = ParamBase.getValue param

    /// Returns the value of the Param as string
    static member getValueAsString (param: IParam) = ParamBase.getValueAsString param
        
    /// Returns the value of the Param as int if possible, else fails
    static member getValueAsInt (param: IParam) = ParamBase.getValueAsInt param

    /// Returns the value of the Param as a term
    static member getValueAsTerm (param: IParam) = ParamBase.getValueAsTerm param

    static member tryGetValueAccession (param: IParam) = ParamBase.tryGetValueAccession param

    static member tryGetValueRef (param: IParam) = ParamBase.tryGetValueRef param

    static member tryGetCvUnit (param: IParam) : CvUnit option = ParamBase.tryGetCvUnit param

    static member tryGetCvUnitValue (param: IParam) = ParamBase.tryGetCvUnitValue param

    static member tryGetCvUnitTermName (param: IParam) = ParamBase.tryGetCvUnitTermName param

    static member tryGetCvUnitTermAccession (param: IParam) = ParamBase.tryGetCvUnitTermAccession param

    static member tryGetCvUnitTermRef (param: IParam) = ParamBase.tryGetCvUnitTermRef param

    static member mapValue (f : ParamValue -> ParamValue) (param : IParam) = ParamBase.mapValue f param :?> IParam

    static member tryMapValue (f : ParamValue -> ParamValue option) (param : IParamBase) = 
        ParamBase.tryMapValue f param 
        |> Option.map (fun v -> v :?> IParam)

    static member tryAddName (name : string) (param : IParamBase) = 
        ParamBase.tryAddName name param
        |> Option.map (fun v -> v :?> IParam)

    static member tryAddAccession (acc : string) (param : IParamBase) = 
        ParamBase.tryAddAccession acc param
        |> Option.map (fun v -> v :?> IParam)

    static member tryAddReference (ref : string) (param : IParamBase) = 
        ParamBase.tryAddReference ref param
        |> Option.map (fun v -> v :?> IParam)

    static member tryAddUnit (unit : CvUnit) (param : IParamBase) = 
        ParamBase.tryAddUnit unit param
        |> Option.map (fun v -> v :?> IParam)

    //------------------------ ICvBase implementations -----------------------//
    
    /// Returns the id of the cv item
    static member getCvAccession (param: IParam) = CvBase.getCvAccession param

    /// Returns the name of the cv item
    static member getCvName (param: IParam) = CvBase.getCvName param

    /// Returns the reference of the cv item
    static member getCvRef (param: IParam) = CvBase.getCvRef param

    /// Returns the full term of the cv item
    static member getTerm (param: IParam) = CvBase.getTerm param

    /// Returns true, if the given term matches the term of the cv item
    static member equalsTerm (term : CvTerm) (param: IParam) = CvBase.equalsTerm term param

    /// Returns true, if the terms of the given param items match
    static member equals (param1 : IParam) (param2 : IParam) = CvBase.equals param1 param2

    /// Returns true, if the names of the given param items match
    static member equalsName (param1 : IParam) (param2 : IParam) = CvBase.equalsName param1 param2

    /// Returns Some Value of type 'T, if the given param item can be downcast, else returns None
    static member inline tryAs<'T when 'T :> IParam> (param: IParam) = CvBase.tryAs<'T> param

    /// Returns true, if the given param item can be downcast
    static member inline is<'T when 'T :> IParam> (param : IParam) = CvBase.is<'T> param

    //-------------------- IParam specific implementations -------------------//


module IParamExtensions =
    type CvBase with
        /// Returns Some Param, if the given param item can be downcast, else returns None
        static member tryParam (cv : ICvBase) =
            match cv with
            | :? IParam as param -> Some param
            | _ -> None

    type ParamBase with
        /// Returns Some Param, if the given value item can be downcast, else returns None
        static member tryParam (cv : IParamBase) =
            match cv with
            | :? IParam as param -> Some param
            | _ -> None