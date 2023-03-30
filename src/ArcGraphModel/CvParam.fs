namespace ArcGraphModel

open System.Collections.Generic


/// ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
/// ----------------------------------------------------------------------------------------
/// Controlled vocabulary (Cv) 
/// ----------------------------------------------------------------------------------------



module Param =
    open System


    type ModelDataType =
        | Investigation
        | Study
        | Assay
        | Person
        | IsaCharacteristic  
        | IsaFactor
        | IsaParamter
        | IsaSample
        | IsaProtocolRef
        | File
        | Directory
        | Custom of string 

    type Address =
        /// Represents an address in the form of: sheetname * row * col
        | Table of string * int * int 

    /// Represents a term from a controlled vocabulary (Cv)
    /// in the form of: id|accession * name|value * refUri
    // ?Maybe [<Struct>]
    //[<Struct>]
    type CvTerm = string * string * string

    /// Represents a unit term from the unit ontology 
    /// in the form of: id|accession * name * refUri
    // ?Maybe [<Struct>]
    type CvUnit = string * string * string

    /// Represent the different cases of a parameter, which is either a simple value,
    /// a CvTerm or a simple value with CvUnit 
    // ?Maybe [<Struct>]
    type ParamValue<'T when 'T :> IConvertible> =
        | Value of v: 'T 
        // id|accession * name|value * ref
        | CvValue of cv: CvTerm
        // value * CvUnit
        | WithCvUnitAccession of cvu : 'T * CvUnit

    /// Interface ensures the propterties necessary for CvTerm 
    type ICvBase =
        abstract member ID       : string
        abstract member Name     : string
        abstract member RefUri   : string
        // here or as node/edge abstraction 
        // abstract member DataType   : ModelDataType
        // abstract member Adress     : Adress / Location / Position

        //override this.ToString() = 
        //    $"Name: {this.Name}\nID: {this.ID}\nRefUri: {this.RefUri}"

    /// Interface ensures the value as ParamValue<'T>  
    type IParamBase<'T when 'T :> IConvertible> =
        abstract member Value : ParamValue<'T>

        //override this.ToString() = 
        //    $"Value: {this.Value}"

    /// Represents controlled vocabulary term as key for a ParamValue as value
    type CvParam<'T when 'T :> IConvertible>(cvAccession : string, cvName : string, cvRefUri : string, paramValue : ParamValue<'T>) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri
        interface IParamBase<'T> with 
            member this.Value  = paramValue

        member this.CvAccession = cvAccession

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\nID: {this.CvAccession}\nRefUri: {(this :> ICvBase).RefUri}\nValue: {(this :> IParamBase<'T>).Value}"

    /// Represents user defined term as key for a ParamValue as value
    type UserParam<'T when 'T :> IConvertible>(name : string, paramValue : ParamValue<'T>) =
        interface ICvBase with 
            member this.ID     = name
            member this.Name   = name
            member this.RefUri = "UserTerm"
        interface IParamBase<'T> with
            member this.Value  = paramValue

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\nID: {(this :> ICvBase).ID}\nRefUri: {(this :> ICvBase).RefUri}\nValue: {(this :> IParamBase<'T>).Value}"

    type CvObject<'T>(cvAccession : string, cvName : string, cvRefUri : string, object : 'T) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri

        member this.Object = object

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\nID: {(this :> ICvBase).ID}\nRefUri: {(this :> ICvBase).RefUri}"

    type CvDoc<'T when 'T :> IParamBase<IConvertible> and 'T:> ICvBase>(cvAccession : string, cvName : string, cvRefUri : string, doc : 'T list) =
        interface ICvBase with 
            member this.ID     = cvAccession
            member this.Name   = cvName
            member this.RefUri = cvRefUri

        member this.Document = doc

        override this.ToString() = 
            $"Name: {(this :> ICvBase).Name}\nID: {(this :> ICvBase).ID}\nRefUri: {(this :> ICvBase).RefUri}"

    // Maybe a CvJDoc is necessary. Using SimpleJson as document type


    let getCvName (param:#ICvBase) =
        param.Name

    let getCvAccession (param : #ICvBase) =
        param.ID

    let getCvName (param : #ICvBase) =
        param.Name

    let getCvRef (param : #ICvBase) =
        param.RefUri

    let getValue (param : #IParamBase<_>) =
        match param.Value with
        | Value                    v    -> v
        | CvValue               (_,v,_) -> v
        | WithCvUnitAccession     (v,_) -> v

    let tryGetValueAccession (param : #IParamBase<_>) =
        match param.Value with
        | CvValue                   (a,_,_) -> Some a
        | Value                      _      -> None     // mere Value has no accession number
        | WithCvUnitAccession        _      -> None     // use tryGetCvUnitAccession instead
        //| WithCvUnitAccession (_,(a,_,_))   -> Some a

    let tryGetValueRef (param : #IParamBase<_>) =
        match param.Value with
        | CvValue               (_,_,r) -> Some r
        | Value                      _  -> None     // mere Value has no ref
        | WithCvUnitAccession        _  -> None     // use tryGetCvUnitRef instead

    let tryGetCvUnit (param : #IParamBase<_>) : CvUnit option =
        match param.Value with
        | Value                  _  -> None
        | CvValue                _  -> None
        | WithCvUnitAccession (_,u) -> Some u

    // TO DO: try get for the parts of CvUnit




//**************************************************************************************************************************************
//**************************************************************************************************************************************
//**************************************************************************************************************************************






    module ReflectionHelper =
    
        open System.Reflection
    
        // Gets public properties including interface propterties
        let getPublicProperties (t:Type) =
            [|
                for propInfo in t.GetProperties() -> propInfo
                for i in t.GetInterfaces() do yield! i.GetProperties()
            |]

        /// Creates an instance of the Object according to applyStyle and applies the function..
        let buildApply (applyStyle:'a -> 'a) =
            let instance =
                System.Activator.CreateInstance<'a>()
            applyStyle instance

        /// Applies 'applyStyle' to item option. If None it creates a new instance.
        let optBuildApply (applyStyle:'a -> 'a) (item:'a option) =
            match item with
            | Some item' -> applyStyle item'
            | None       -> buildApply applyStyle

        /// Applies Some 'applyStyle' to item. If None it returns 'item' unchanged.
        let optApply (applyStyle:('a -> 'a)  option) (item:'a ) =
            match applyStyle with
            | Some apply -> apply item
            | None       -> item

        /// Returns the proptery name from quotation expression
        let tryGetPropertyName (expr : Microsoft.FSharp.Quotations.Expr) =
            match expr with
            | Microsoft.FSharp.Quotations.Patterns.PropertyGet (_,pInfo,_) -> Some pInfo.Name
            | _ -> None

        /// Try to get the PropertyInfo by name using reflection
        let tryGetPropertyInfo (o:obj) (propName:string) =
            getPublicProperties (o.GetType())
            |> Array.tryFind (fun n -> n.Name = propName)        

        /// Sets property value using reflection
        let trySetPropertyValue (o:obj) (propName:string) (value:obj) =
            match tryGetPropertyInfo o propName with 
            | Some property ->
                try 
                    property.SetValue(o, value, null)
                    Some o
                with
                | :? System.ArgumentException -> None
                | :? System.NullReferenceException -> None
            | None -> None

        /// Gets property value as option using reflection
        let tryGetPropertyValue (o:obj) (propName:string) =
            try 
                match tryGetPropertyInfo o propName with 
                | Some v -> Some (v.GetValue(o, null))
                | None -> None
            with 
            | :? System.Reflection.TargetInvocationException -> None
            | :? System.NullReferenceException -> None
    
        /// Gets property value as 'a option using reflection. Cast to 'a
        let tryGetPropertyValueAs<'a> (o:obj) (propName:string) =
            try 
                match tryGetPropertyInfo o propName with 
                | Some v -> Some (v.GetValue(o, null) :?> 'a)
                | None -> None
            with 
            | :? System.Reflection.TargetInvocationException -> None
            | :? System.NullReferenceException -> None

        /// Updates property value by given function
        let tryUpdatePropertyValueFromName (o:obj) (propName:string) (f: 'a -> 'a) =
            let v = optBuildApply f (tryGetPropertyValueAs<'a> o propName)
            trySetPropertyValue o propName v 
            //o

        /// Updates property value by given function
        let tryUpdatePropertyValue (o:obj) (expr : Microsoft.FSharp.Quotations.Expr) (f: 'a -> 'a) =
            let propName = tryGetPropertyName expr
            let g = (tryGetPropertyValueAs<'a> o propName.Value)
            let v = optBuildApply f g
            trySetPropertyValue o propName.Value v 
            //o

        let updatePropertyValueAndIgnore (o:obj) (expr : Microsoft.FSharp.Quotations.Expr) (f: 'a -> 'a) = 
            tryUpdatePropertyValue o expr f |> ignore


        /// Removes property 
        let removeProperty (o:obj) (propName:string) =        
            match tryGetPropertyInfo o propName with         
            | Some property ->
                try 
                    property.SetValue(o, null, null)
                    true
                with
                | :? System.ArgumentException -> false
                | :? System.NullReferenceException -> false
            | None -> false












    type CvParamContainer internal (dict:Dictionary<string, IParamBase<_>>) = 
        
        let properties = dict//new Dictionary<string, obj>()

        /// 
        new () = CvParamContainer(new Dictionary<string,  IParamBase<_>>())


        member this.SetCvParam (param:#IParamBase<_>) =
            let key = getCvName param
            match properties.ContainsKey(key) with
            | true -> properties.[key] <- param
            | false -> properties.Add(key,param)
        
        member this.TryGetCvParamBy (name) =
            match properties.ContainsKey(name) with
            | true -> properties.[name] |> Some
            | false -> None
            
            
