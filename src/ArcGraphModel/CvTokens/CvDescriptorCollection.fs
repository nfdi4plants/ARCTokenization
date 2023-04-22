namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux


/// 
type CvAttributeCollection(attributes : IDictionary<string,IParam>) =

    inherit Dictionary<string,IParam>(attributes)

    member this.AddQualifier (ip : IParam) =
        Dictionary.addInPlace (ip |> CvBase.getCvName) ip this
        |> ignore

    member this.TryGetQualifier (name : string) =
        Dictionary.tryFind name this

    member this.TryGetQualifier (term : CvTerm) =
        Dictionary.tryFind (CvTerm.getName term) this

    member this.GetQualifier (name : string) =
        Dictionary.item name this

    member this.GetQualifier (term : CvTerm) =
        Dictionary.item (CvTerm.getName term) this

    member this.ContainsQualifier (name : string) =
        Dictionary.containsKey name this

    member this.ContainsQualifier (term : CvTerm) =
        Dictionary.containsKey (CvTerm.getName term) this