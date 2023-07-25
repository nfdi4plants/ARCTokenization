namespace ControlledVocabulary

open System.Collections.Generic
open FSharpAux

module internal Dictionary = 
    
    let addOrAppendInPlace (k : 'Key) (v : 'Value) (dict : IDictionary<'Key, 'Value seq>) =
        if dict.ContainsKey k then
            dict.[k] <- Seq.append dict.[k] (Seq.singleton v)
        else dict.[k] <- Seq.singleton v

/// Represents a collection of structured properties, annotated with a controlled vocabulary term.
type CvContainer (
    cvAccession : string, 
    cvName : string, 
    cvRefUri : string, 
    attributes : IDictionary<string,IParam>,
    properties : IDictionary<string,seq<ICvBase>>
    ) =

    inherit CvAttributeCollection(attributes)

    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri    
        member this.HasAttributes 
            with get() = this.Attributes |> Seq.isEmpty |> not

    new (cvAccession : string, cvName : string, cvRefUri : string, attributes : IDictionary<string,IParam>) =
        CvContainer(cvAccession, cvName, cvRefUri, attributes, Dictionary<string, ICvBase seq>())
    new (cvAccession : string, cvName : string, cvRefUri : string, attributes : seq<IParam>) =
        let dict = CvAttributeCollection(attributes)
        CvContainer(cvAccession, cvName, cvRefUri, dict)
    new (cvAccession : string, cvName : string, cvRefUri : string) =
        CvContainer(cvAccession, cvName, cvRefUri, Seq.empty)


    new ((id,name,ref) : CvTerm, attributes : IDictionary<string,IParam>) = 
        CvContainer(id,name,ref, attributes, Dictionary<string, ICvBase seq>())
    new (term : CvTerm,attributes : seq<IParam>) = 
        let dict = CvAttributeCollection(attributes)
        CvContainer(term, dict)
    new (term : CvTerm) = CvContainer (term, Seq.empty)  

    /// Returns Some CvContainer, if the given cv item can be downcast, else returns None
    static member tryCvContainer (cv : ICvBase) =
        match cv with
        | :? CvContainer as container -> Some container
        | _ -> None

    member this.Properties
        with get() = properties


    /// Retrieves children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    member this.GetMany propertyName =
        Dictionary.item propertyName this.Properties

    /// Retrieves children with the given name and which can be cast to the given type of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    member inline this.GetManyAs<'T when 'T :> ICvBase> propertyName =
        Dictionary.item propertyName this.Properties
        |> Seq.choose CvBase.tryAs<'T>

    /// Retrieves CvContainer children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    member this.GetManyContainers propertyName =
        Dictionary.item propertyName this.Properties
        |> Seq.choose CvContainer.tryCvContainer

    /// Retrieves Param children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    member this.GetManyParams propertyName =
        Dictionary.item propertyName this.Properties
        |> Seq.choose Param.tryParam

    /// Retrieves child with the given name of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    member this.GetSingle propertyName =
        Dictionary.item propertyName this.Properties
        |> Seq.exactlyOne

    /// Retrieves child with the given name and which can be cast to the given type of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    member inline this.GetSingleAs<'T when 'T :> ICvBase> propertyName =
        Dictionary.item propertyName this.Properties
        |> Seq.choose CvBase.tryAs<'T>
        |> Seq.exactlyOne

    /// Retrieves CvContainer child with the given name of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    member this.GetSingleContainer propertyName =
        Dictionary.item propertyName this.Properties
        |> Seq.choose CvContainer.tryCvContainer
        |> Seq.exactlyOne

    /// Retrieves Param child with the given name of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    member this.GetSingleParam propertyName =
        Dictionary.item propertyName this.Properties
        |> Seq.choose Param.tryParam
        |> Seq.exactlyOne
        

    /// Retrieves children with the given name of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    member this.TryGetMany propertyName =
        Dictionary.tryFind propertyName this.Properties

    /// Retrieves children with the given name and which can be cast to the given type of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    member inline this.TryGetManyAs<'T when 'T :> ICvBase> propertyName =
        Dictionary.tryFind propertyName this.Properties
        |> Option.bind (fun many ->
            Seq.choose CvBase.tryAs<'T> many
            |> fun items -> if Seq.isEmpty items then None else Some items
        )

    /// Retrieves CvContainer children with the given name of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    member this.TryGetManyContainers propertyName =
        Dictionary.tryFind propertyName this.Properties
        |> Option.bind (fun many ->
            Seq.choose CvContainer.tryCvContainer many
            |> fun items -> if Seq.isEmpty items then None else Some items
        )

    /// Retrieves Param children with the given name of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    member this.TryGetManyParams propertyName =
        Dictionary.tryFind propertyName this.Properties
        |> Option.bind (fun many ->
            many
            |> Seq.choose Param.tryParam
            |> fun items -> if Seq.isEmpty items then None else Some items
        )

    /// Retrieves child with the given name of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    member this.TryGetSingle propertyName =
        Dictionary.tryFind propertyName this.Properties
        |> Option.bind Seq.tryExactlyOne

    /// Retrieves child with the given name and which can be cast to the given type of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    member inline this.TryGetSingleAs<'T when 'T :> ICvBase> propertyName =
        Dictionary.tryFind propertyName this.Properties
        |> Option.bind (Seq.choose CvBase.tryAs<'T> >> Seq.tryExactlyOne)

    /// Retrieves CvContainer child with the given name of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    member this.TryGetSingleContainer propertyName =
        Dictionary.tryFind propertyName this.Properties
        |> Option.bind (Seq.choose CvContainer.tryCvContainer >> Seq.tryExactlyOne)

    /// Retrieves Param child with the given name of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    member this.TryGetSingleParam propertyName =
        Dictionary.tryFind propertyName this.Properties
        |> Option.bind (Seq.choose Param.tryParam >> Seq.tryExactlyOne)

    

    /// Retrieves children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    static member getMany propertyName (container : CvContainer) =
        container.GetMany propertyName

    /// Retrieves children with the given name and which can be cast to the given type of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    static member inline getManyAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
        container.GetManyAs<'T> propertyName

    /// Retrieves CvContainer children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    static member getManyContainers propertyName (container : CvContainer) =
        container.GetManyContainers propertyName

    /// Retrieves Param children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    static member getManyParams propertyName (container : CvContainer) =
        container.GetManyParams propertyName

    /// Retrieves child with the given name of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member getSingle propertyName (container : CvContainer) =
        container.GetSingle propertyName

    /// Retrieves child with the given name and which can be cast to the given type of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member inline getSingleAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
        container.GetSingleAs<'T> propertyName

    /// Retrieves CvContainer child with the given name of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member getSingleContainer propertyName (container : CvContainer) =
        container.GetSingleContainer propertyName

    /// Retrieves Param child with the given name of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member getSingleParam propertyName (container : CvContainer) =
        container.GetSingleParam propertyName
        

    /// Retrieves children with the given name of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    static member tryGetMany propertyName (container : CvContainer) =
        container.TryGetMany propertyName

    /// Retrieves children with the given name and which can be cast to the given type of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    static member inline tryGetManyAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
        container.TryGetManyAs<'T> propertyName

    /// Retrieves CvContainer children with the given name of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    static member tryGetManyContainers propertyName (container : CvContainer) =
        container.TryGetManyContainers propertyName

    /// Retrieves Param children with the given name of the CvContainer as sequence.
    ///
    /// Returns None if the propertyName cannot be found.
    static member tryGetManyParams propertyName (container : CvContainer) =
        container.TryGetManyParams propertyName

    /// Retrieves child with the given name of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member tryGetSingle propertyName (container : CvContainer) =
        container.TryGetSingle propertyName

    /// Retrieves child with the given name and which can be cast to the given type of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member inline tryGetSingleAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
        container.TryGetSingleAs<'T> propertyName

    /// Retrieves CvContainer child with the given name of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member tryGetSingleContainer propertyName (container : CvContainer) =
        container.TryGetSingleContainer propertyName

    /// Retrieves Param child with the given name of the CvContainer.
    ///
    /// Returns None if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member tryGetSingleParam propertyName (container : CvContainer) =
        container.TryGetSingleParam propertyName


    member this.ContainsProperty propertyName =
        this.Properties.ContainsKey propertyName

    static member containsProperty propertyName (container : CvContainer) =
        container.ContainsProperty propertyName

    member this.CountProperties() =
        this.Properties.Count

    static member countProperties (container : CvContainer) =
        container.CountProperties()

    member this.CountChildren() =
        this.Properties.Values |> Seq.concat |> Seq.length

    static member countChildren (container : CvContainer) =
        container.CountChildren()

    /// Adds children as a property of the CvContainer.
    ///
    /// If values with the same key already exist in the container, appends the new child
    static member addMany (values : seq<ICvBase>) (cvContainer : CvContainer) =
        values
        |> Seq.iter (fun v -> CvContainer.addSingle v cvContainer)

    /// Adds a child as a property of the CvContainer.
    ///
    /// If a value with the same key already exist in the container, appends the new child
    static member addSingle (value : ICvBase) (cvContainer : CvContainer) =
        Dictionary.addOrAppendInPlace value.Name value cvContainer.Properties
        

    /// Sets children as a property of the CvContainer.
    ///
    /// These children are supposed to all have the same name, as they will be grouped into one property of the container, accessible
    /// by this shared name.
    /// 
    /// Fails if values has elements with different names.
    static member setMany (values : seq<ICvBase>) (cvContainer : CvContainer) =
        let propertyName = 
            values
            |> Seq.countBy (fun v -> v.Name)
            |> Seq.exactlyOne
            |> fst
        Dictionary.addOrUpdateInPlace propertyName values cvContainer.Properties
        |> ignore

    /// Sets a single child as a property of the CvContainer, accessible by its name.
    static member setSingle (value : ICvBase) (cvContainer : CvContainer) =
        Dictionary.addOrUpdateInPlace value.Name (Seq.singleton value) cvContainer.Properties

    override this.ToString() = 
        $"CvContainer: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tProperties: {this.Properties.Keys |> Seq.toList}"