namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux

/// Represents a collection of structured properties, annotated with a controlled vocabulary term.
type internal CvContainer (
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

    new (cvAccession : string, cvName : string, cvRefUri : string, attributes : IDictionary<string,IParam>) =
        CvContainer(cvAccession, cvName, cvRefUri, attributes, Dictionary<string, ICvBase seq>())
    new (cvAccession : string, cvName : string, cvRefUri : string, attributes : seq<IParam>) =
        let dict = CvAttributeCollection(attributes)
        CvContainer(cvAccession, cvName, cvRefUri, dict)
    new (cvAccession : string, cvName : string, cvRefUri : string) =
        CvContainer(cvAccession, cvName, cvRefUri, Seq.empty)
    

    new ((id,name,ref) : CvTerm, attributes : IDictionary<string,IParam>) = 
        CvContainer(id, name, ref, attributes, Dictionary<string, ICvBase seq>())
    new (term : CvTerm,attributes : seq<IParam>) = 
        let dict = CvAttributeCollection(attributes)
        CvContainer(term, dict)
    new (term : CvTerm) = CvContainer (term, Seq.empty)  

    /// Returns Some CvContainer, if the given cv item can be downcast, else returns None
    static member tryCvContainer (cv : ICvBase) =
        match cv with
        | :? CvContainer as container -> Some container
        | _ -> None

    member internal this.Properties
        with get() = properties


    /// Retrieves children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    member this.GetMany propertyName =
        Dictionary.item propertyName this.Properties

    /// Retrieves children with the given name and which can be cast to the given type of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    member this.GetManyAs<'T when 'T :> ICvBase> propertyName =
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
    member this.GetSingleAs<'T when 'T :> ICvBase> propertyName =
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
    member this.TryGetManyAs<'T when 'T :> ICvBase> propertyName =
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
    member this.TryGetSingleAs<'T when 'T :> ICvBase> propertyName =
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
    static member getManyAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
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
    static member getSingleAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
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
    static member tryGetManyAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
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
    static member tryGetSingleAs<'T when 'T :> ICvBase> propertyName (container : CvContainer) =
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



    ///// Sets children as a property of the CvContainer.
    /////
    ///// These children are supposed to all have the same name, as they will be grouped into one property of the container, accessible
    ///// by this shared name.
    ///// 
    ///// Fails if values has elements with different names.
    //static member setMany (values : seq<#ICvBase>) (cvContainer : CvContainer) =
    //    let propertyName = 
    //        values
    //        |> Seq.countBy (fun v -> v.Name)
    //        |> Seq.exactlyOne
    //        |> fst
    //    Dictionary.addOrUpdateInPlace propertyName values cvContainer
    //    |> ignore

    ///// Sets a single child as a property of the CvContainer, accessible by its name.
    //static member setSingle (value : #ICvBase) (cvContainer : CvContainer) =
    //    Dictionary.addOrUpdateInPlace value.Name (Seq.singleton value) cvContainer

    override this.ToString() = 
        $"CvContainer: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tProperties: {this.Keys |> Seq.toList}"