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

    static member getManyAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        Dictionary.item propertyName cvContainer
        |> Seq.map (fun cv -> cv :?> 'T)

    static member getSingleAs<'T when 'T :> ICvBase> propertyName (cvContainer : CvContainer) =
        Dictionary.item propertyName cvContainer
        |> Seq.exactlyOne 
        |> (fun cv -> cv :?> 'T)

    static member setMany propertyName (values : seq<#ICvBase>) (cvContainer : CvContainer) =
        Dictionary.addOrUpdateInPlace propertyName values cvContainer
        |> ignore

    static member setSingle propertyName (value : #ICvBase) (cvContainer : CvContainer) =
        Dictionary.addOrUpdateInPlace propertyName (Seq.singleton value) cvContainer
        |> ignore