namespace ControlledVocabulary

open System 

/// Interface ensures the value as ParamValue<'T>  
type IParamBase =
    abstract member Value : ParamValue
    abstract member WithValue : ParamValue -> IParamBase

type ParamBase = 

    /// Returns the value of the Param as a ParamValue
    static member getParamValue (param:IParamBase) =
        param.Value

    /// Returns the value of the Param as IConvertible
    static member getValue (param:IParamBase) =
        ParamValue.getValue param.Value

    /// Returns the value of the Param as string
    static member getValueAsString (param:IParamBase) =
        ParamValue.getValueAsString param.Value
        
    /// Returns the value of the Param as int if possible, else fails
    static member getValueAsInt (param:IParamBase) =
        ParamValue.getValueAsInt param.Value

    /// Returns the value of the Param as a term
    static member getValueAsTerm (param:IParamBase) =
        ParamValue.getValueAsTerm param.Value

    static member tryGetValueAccession (param : #IParamBase) =
        match param.Value with
        | CvValue                   cv      -> Some cv.Accession
        | Value                      _      -> None     // mere Value has no accession number
        | WithCvUnitAccession        _      -> None     // use tryGetCvUnitAccession instead
        //| WithCvUnitAccession (_,(a,_,_))   -> Some a

    static member tryGetValueRef (param : #IParamBase) =
        match param.Value with
        | CvValue                    cv -> Some cv.RefUri
        | Value                      _  -> None     // mere Value has no ref
        | WithCvUnitAccession        _  -> None     // use tryGetCvUnitRef instead

    static member tryGetCvUnit (param : #IParamBase) : CvUnit option =
        match param.Value with
        | Value                  _  -> None
        | CvValue                _  -> None
        | WithCvUnitAccession (_,u) -> Some u

    static member tryGetCvUnitValue (param : #IParamBase) : #IConvertible option =
        match param.Value with
        | Value                  _  -> None
        | CvValue                _  -> None
        | WithCvUnitAccession (v,_) -> Some v

    static member tryGetCvUnitTermName (param : #IParamBase) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,cvu)     -> Some cvu.Name

    static member tryGetCvUnitTermAccession (param : #IParamBase) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,cvu)     -> Some cvu.Accession

    static member tryGetCvUnitTermRef (param : #IParamBase) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,cvu)     -> Some cvu.RefUri

    static member mapValue (f : ParamValue -> ParamValue) (param : IParamBase) = 
        param.WithValue(f param.Value)

    static member tryMapValue (f : ParamValue -> ParamValue option) (param : IParamBase) = 
        match f param.Value with
        | Some value -> 
            Some (param.WithValue(value))
        | None -> None

    static member tryAddName (value : string) (param : IParamBase) = 
        ParamBase.tryMapValue (ParamValue.tryAddName value) param

    static member tryAddAccession (acc : string) (param : IParamBase) = 
        ParamBase.tryMapValue (ParamValue.tryAddAccession acc) param

    static member tryAddReference (ref : string) (param : IParamBase) = 
        ParamBase.tryMapValue (ParamValue.tryAddReference ref) param

    static member tryAddUnit (unit : CvUnit) (param : IParamBase) = 
        ParamBase.tryMapValue (ParamValue.tryAddUnit unit) param