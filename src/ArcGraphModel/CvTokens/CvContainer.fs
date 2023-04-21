namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux

/// Represents a collection of structured properties, annotated with a controlled vocabulary term
type CvContainer (cvAccession:string,cvName:string,cvRefUri:string,properties : IDictionary<string,ICvBase seq>) =

    inherit Dictionary<string,ICvBase seq>(properties)

    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri    

    new ((id,name,ref) : CvTerm,properties : IDictionary<string,ICvBase seq>) = 
        CvContainer(id,name,ref,properties)

    new (term : CvTerm,items : seq<ICvBase>) = 
        let properties : IDictionary<string,ICvBase seq> = 
            items
            |> Seq.groupBy (fun v -> v.Name)
            |> Dictionary.ofSeq
        CvContainer(term,properties)

    new (term : CvTerm) = CvContainer (term,Seq.empty)  

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