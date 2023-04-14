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