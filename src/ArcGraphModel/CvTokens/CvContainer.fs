namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux

/// Represents a collection of structured properties, annotated with a controlled vocabulary term.
type internal CvContainer (
    cvAccession : string, 
    cvName : string, 
    cvRefUri : string, 
    attributes : IDictionary<string,IParam>, 
    parameters : IDictionary<string,IParam seq>, 
    cvContainers : Dictionary<string,CvContainer seq>
    ) =

    inherit CvAttributeCollection(attributes)

    let _cvContainers = cvContainers
    let _parameters = parameters

    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri    


    new (cvAccession : string, cvName : string, cvRefUri : string) =
        CvContainer(cvAccession, cvName, cvRefUri, Dictionary<string, IParam>(), Dictionary<string, IParam seq>(), Dictionary<string, CvContainer seq>())

    new ((id,name,ref) : CvTerm, attributes : IDictionary<string,IParam>) = 
        CvContainer(id, name, ref, attributes, Dictionary<string, IParam seq>(), Dictionary<string, CvContainer seq>())

    new (term : CvTerm,items : seq<IParam>) = 
        let parameters : IDictionary<string,IParam seq> = 
            items
            |> Seq.groupBy (fun v -> v.Name)
            |> Dictionary.ofSeq
        CvContainer(term, Dictionary<string,IParam>(), parameters, Dictionary<string,CvContainer seq>())

    new (term : CvTerm) = CvContainer (term, Seq.empty)  


    member internal this.CvContainers
        with get() = _cvContainers

    member internal this.Properties
        with get() = _parameters

    member this.lel v =
        this.CvContainers["lel"] <- v


    /// Retrieves children with the given name of the CvContainer as sequence.
    ///
    /// Fails if the propertyName cannot be found.
    static member getManyAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        Dictionary.item propertyName cvContainer
        |> Seq.map (fun cv -> cv :?> 'T)

    /// Retrieves children with the given name of the CvContainer as sequence if they exist. Else returns None.
    static member tryGetManyAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        Dictionary.tryFind propertyName cvContainer
        |> Option.map (Seq.map (fun cv -> cv :?> 'T))

    /// Retrieves the values of children with the given name of the CvContainer as sequence if they exist. Else returns None.
    static member tryGetManyAsValues<'T when 'T :> IParamBase and 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        CvContainer.tryGetManyAs<'T> propertyName cvContainer
        |> Option.map (Seq.map (fun cvb -> cvb :> IParamBase |> ParamBase.getValue))

    /// Retrieves child with the given name of the CvContainer.
    ///
    /// Fails if there is not exactly one child with the given name or if the propertyName cannot be found.
    static member getSingleAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        Dictionary.item propertyName cvContainer
        |> Seq.exactlyOne 
        |> fun cv -> cv :?> 'T

    /// Retrieves child with the given name of the CvContainer if it exists. Else returns None.
    static member tryGetSingleAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        try     // <- to catch the exception if `Seq.exactlyOne` throws
            Dictionary.tryFind propertyName cvContainer
            |> Option.map (
                Seq.exactlyOne 
                >> (fun cv -> cv :?> 'T)
            )
        with _ -> None

    /// Retrieves the value of the child with the given name of the CvContainer if it exists. Else returns None.
    static member tryGetSingleAsValue<'T when 'T :> ICvBase and 'T :> IParamBase> propertyName (cvContainer : CvContainer) =
        CvContainer.tryGetSingleAs<'T> propertyName cvContainer
        |> Option.map (fun cvb -> cvb :> IParamBase |> ParamBase.getValue)

    /// Sets children as a property of the CvContainer.
    ///
    /// These children are supposed to all have the same name, as they will be grouped into one property of the container, accessible
    /// by this shared name.
    /// 
    /// Fails if values has elements with different names.
    static member setMany (values : seq<#ICvBase>) (cvContainer : CvContainer) =
        let propertyName = 
            values
            |> Seq.countBy (fun v -> v.Name)
            |> Seq.exactlyOne
            |> fst
        Dictionary.addOrUpdateInPlace propertyName values cvContainer
        |> ignore

    /// Sets a single child as a property of the CvContainer, accessible by its name.
    static member setSingle (value : #ICvBase) (cvContainer : CvContainer) =
        Dictionary.addOrUpdateInPlace value.Name (Seq.singleton value) cvContainer
        |> ignore

    ///// Returns the values of the given key in the CvContainer if they exist. Else returns None.
    //static member tryGetValues key (cvContainer : CvContainer) =
    //    try 
    //        Dictionary.tryFind key cvContainer
    //        |> Option.map (Seq.map (fun cvb -> cvb :?> CvParam |> ParamBase.getValue))
    //    with _ -> None

    ///// Returns the head of the ICvBase sequence under the given key in the CvContainer if it exists. Else returns None.
    //static member tryGetHead key (cvContainer : CvContainer) =
    //    Dictionary.tryFind key cvContainer
    //    |> Option.map Seq.head

    override this.ToString() = 
        //$"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tQualifiers: {(this.Keys, this.Values) ||> Seq.zip |> Seq.truncate 4 |> Seq.fold (fun acc x -> acc + x.ToString() + System.Environment.NewLine) System.String.Empty}"
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}"