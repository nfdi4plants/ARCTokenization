namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux


/// Contains attributes by which to qualify a cv object
type CvAttributeCollection(attributes : IDictionary<string,IParam>) =

    inherit Dictionary<string,IParam>(attributes)

    new(attributes) =
        let dict = 
            attributes
            |> Seq.map (fun cvp -> (cvp :> ICvBase).Name, cvp)
            |> Dictionary.ofSeq
        CvAttributeCollection(dict)

    /// Returns all attributes as a list
    member this.Attributes 
        with get() = this.Values |> Seq.toList

    /// Add an IParam as an attribute, fails, if an attribute with the same key already exists
    member this.AddAttribute (param : IParam) =
        Dictionary.addInPlace (param |> CvBase.getCvName) param this
        |> ignore

    /// Retrieves an IParam attribute by its name, if it exists, else returns None
    member this.TryGetAttribute (name : string) =
        Dictionary.tryFind name this

    /// Retrieves an IParam attribute by its term, if it exists, else returns None
    member this.TryGetAttribute (term : CvTerm) =
        Dictionary.tryFind (CvTerm.getName term) this
        |> Option.bind (fun param -> 
            if CvBase.equalsTerm term param then 
                Some param 
            else None)

    /// Retrieves an IParam attribute by its name, if it exists, else fails
    member this.GetAttribute (name : string) =
        Dictionary.item name this

    /// Retrieves an IParam attribute by its term, if it exists, else fails
    member this.GetAttribute (term : CvTerm) =
        Dictionary.item (CvTerm.getName term) this

    /// Returns true, if an attribute with the given name exists in the collection
    member this.ContainsAttribute (name : string) =
        Dictionary.containsKey name this

    /// Returns true, if an attribute with the given term exists in the collection
    member this.ContainsAttribute (term : CvTerm) =
        match Dictionary.tryFind (CvTerm.getName term) this with
        | Some param when CvBase.equalsTerm term param -> true
        | _ -> false

    /// Returns true, if an attribute with the same term as the given parent exists in the collection
    member this.IsStructuralChildOf (parent : ICvBase) =
        this.ContainsAttribute (CvBase.getTerm parent)

    /// Returns true, if an attribute with the same term as the given parent exists in the collection
    static member isStructuralChildOf (parent : ICvBase) (child : #CvAttributeCollection) =
        child.IsStructuralChildOf(parent)

    /// Returns the attribute of the value, if it implements CvAttributeCollection and the attribute exists, else returns None
    static member tryGetAttribute (name : string) (v : 'T) =
        match box v with
        | :? CvAttributeCollection as ac -> 
            ac.TryGetAttribute name
        | _ -> None

    /// Returns true, if it implements a CvAttributeCollection and the attribute exists, else returns None
    static member containsAttribute (name : string) (v : 'T) =
        match box v with
        | :? CvAttributeCollection as ac -> 
            ac.ContainsAttribute name
        | _ -> false