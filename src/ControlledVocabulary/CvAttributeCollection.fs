namespace ControlledVocabulary

open System.Collections.Generic
open FSharpAux


/// Contains attributes by which to qualify a cv object
type CvAttributeCollection(attributes : IDictionary<string,IParam>) =

    inherit Dictionary<string,IParam>(attributes)

    new(attributes) =
        let dict = 
            attributes
            |> Seq.map (fun cvp -> (cvp :> ICvBase).Value, cvp)
            |> Dictionary.ofSeq
        CvAttributeCollection(dict)

    /// Returns all attributes as a list
    member this.Attributes 
        with get() = this.Values |> Seq.toList

    /// Add an IParam as an attribute. Fails, if an attribute with the same key already exists
    member this.AddAttribute (param : IParam) =
        Dictionary.addInPlace (param |> CvBase.getCvValue) param this
        |> ignore

    /// Add an IParam as an attribute. Does not fail, if an attribute with the same key already exists
    member this.TryAddAttribute (param : IParam) =
        let key = param |> CvBase.getCvValue 
        if this.ContainsAttribute key then false
        else 
            this.AddAttribute param
            true

    /// Retrieves an IParam attribute by its name, if it exists, else returns None
    member this.TryGetAttribute (name : string) =
        Dictionary.tryFind name this

    /// Retrieves an IParam attribute by its term, if it exists, else returns None
    member this.TryGetAttribute (term : CvTerm) =
        Dictionary.tryFind term.Value this
        |> Option.bind (fun param -> 
            if CvBase.equalsTerm term param then 
                Some param 
            else None)

    /// Retrieves an IParam attribute by its name, if it exists, else fails
    member this.GetAttribute (name : string) =
        Dictionary.item name this

    /// Retrieves an IParam attribute by its term, if it exists, else fails
    member this.GetAttribute (term : CvTerm) =
        Dictionary.item term.Value this

    /// Returns true, if an attribute with the given name exists in the collection
    member this.ContainsAttribute (name : string) =
        Dictionary.containsKey name this

    /// Returns true, if an attribute with the given term exists in the collection
    member this.ContainsAttribute (term : CvTerm) =
        match Dictionary.tryFind term.Value this with
        | Some param when CvBase.equalsTerm term param -> true
        | _ -> false

    /// Returns true, if an attribute with the same term as the given parent exists in the collection
    member this.IsStructuralChildOf (parent : ICvBase) =
        this.ContainsAttribute (CvBase.getTerm parent)

    /// Returns true, if an attribute with the same term as the given parent exists in the collection
    static member isStructuralChildOf (parent : ICvBase) (child : IAttributeCollection) =
        match box child with
        | :? CvAttributeCollection as ac -> 
            ac.ContainsAttribute (CvBase.getTerm parent)
        | _ -> false

    /// Returns the attribute of the value, if it implements CvAttributeCollection and the attribute exists, else returns None
    static member tryAddAttribute (param : IParam) (v : IAttributeCollection) =
        match box v with
        | :? CvAttributeCollection as ac -> 
            ac.TryAddAttribute param
        | _ -> false

    /// Returns the attribute of the value, if it implements CvAttributeCollection and the attribute exists, else returns None
    static member tryGetAttribute (name : string) (v : IAttributeCollection) =
        match box v with
        | :? CvAttributeCollection as ac -> 
            ac.TryGetAttribute name
        | _ -> None

    /// Returns the attribute of the value, if it implements CvAttributeCollection and the attribute exists, else returns None
    static member tryGetAttributeByTerm (term : string) (v : IAttributeCollection) =
        match box v with
        | :? CvAttributeCollection as ac -> 
            ac.TryGetAttribute term
        | _ -> None

    /// Returns true, if it implements a CvAttributeCollection and the attribute exists
    static member containsAttribute (name : string) (v : IAttributeCollection) =
        match box v with
        | :? CvAttributeCollection as ac -> 
            ac.ContainsAttribute name
        | _ -> false

    /// Returns true, if it implements a CvAttributeCollection and the attribute exists
    static member containsAttributeTerm (term : CvTerm) (v : IAttributeCollection) =
        match box v with
        | :? CvAttributeCollection as ac -> 
            ac.ContainsAttribute term
        | _ -> false