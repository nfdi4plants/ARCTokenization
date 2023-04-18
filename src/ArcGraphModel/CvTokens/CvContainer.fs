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

    /// Retrieve children with the given name of the CvContainer as sequence
    static member getManyAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        Dictionary.item propertyName cvContainer
        |> Seq.map (fun cv -> cv :?> 'T)

    /// Retrieve child with the given name of the CvContainer
    ///
    /// Fails, if there is not exactly one child with the given name
    static member getSingleAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        Dictionary.item propertyName cvContainer
        |> Seq.exactlyOne 
        |> (fun cv -> cv :?> 'T)

    /// Set children as a property of the CvContainer
    ///
    /// These children are supposed to all have the same name, as they will be grouped into one property of the container, accessible by this shared name
    static member setMany (values : seq<#ICvBase>) (cvContainer : CvContainer) =
        let propertyName = 
            values
            |> Seq.countBy (fun v -> v.Name)
            |> Seq.exactlyOne
            |> fst
        Dictionary.addOrUpdateInPlace propertyName values cvContainer
        |> ignore

    /// Set a single child as a property of the CvContainer, accessible by its name
    static member setSingle (value : #ICvBase) (cvContainer : CvContainer) =
        Dictionary.addOrUpdateInPlace value.Name (Seq.singleton value) cvContainer
        |> ignore