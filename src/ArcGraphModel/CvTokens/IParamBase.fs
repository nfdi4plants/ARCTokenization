namespace ArcGraphModel

open System 

/// Interface ensures the value as ParamValue<'T>  
type IParamBase =
    abstract member Value : ParamValue


module ParamBase = 

    let getValue (param : #IParamBase) =
        match param.Value with
        | Value                    v    -> v
        | CvValue               (_,v,_) -> v
        | WithCvUnitAccession     (v,_) -> v

    let tryGetValueAccession (param : #IParamBase) =
        match param.Value with
        | CvValue                   (a,_,_) -> Some a
        | Value                      _      -> None     // mere Value has no accession number
        | WithCvUnitAccession        _      -> None     // use tryGetCvUnitAccession instead
        //| WithCvUnitAccession (_,(a,_,_))   -> Some a

    let tryGetValueRef (param : #IParamBase) =
        match param.Value with
        | CvValue               (_,_,r) -> Some r
        | Value                      _  -> None     // mere Value has no ref
        | WithCvUnitAccession        _  -> None     // use tryGetCvUnitRef instead

    let tryGetCvUnit (param : #IParamBase) : CvUnit option =
        match param.Value with
        | Value                  _  -> None
        | CvValue                _  -> None
        | WithCvUnitAccession (_,u) -> Some u

    let tryGetCvUnitValue (param : #IParamBase) : #IConvertible option =
        match param.Value with
        | Value                  _  -> None
        | CvValue                _  -> None
        | WithCvUnitAccession (v,_) -> Some v

    let tryGetCvUnitName (param : #IParamBase) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,(_,n,_)) -> Some n

    let tryGetCvUnitAccession (param : #IParamBase) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,(a,_,_)) -> Some a

    let tryGetCvUnitRef (param : #IParamBase) =
        match param.Value with
        | Value                  _          -> None
        | CvValue                _          -> None
        | WithCvUnitAccession   (_,(_,_,r)) -> Some r

