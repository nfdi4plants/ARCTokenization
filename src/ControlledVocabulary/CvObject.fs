namespace ControlledVocabulary

open System.Collections.Generic

/// Represents a generic object, annotated with a controlled vocabulary term
type CvObject<'T>(cvAccession : string, cvValue : string, cvRef : string, object : 'T, attributes : Dictionary<string,IParam>) =

    inherit CvAttributeCollection(attributes)

    interface ICvBase with 
        member this.Accession   = cvAccession
        member this.Value       = cvValue
        member this.RefUri         = cvRef
        member this.HasAttributes 
            with get() = this.Attributes |> Seq.isEmpty |> not

    new (term: CvTerm, object : 'T, attributes) = CvObject (term.Accession, term.Value, term.RefUri, object, attributes)

    member this.Object = object

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Value}\n\tID: {(this :> ICvBase).Accession}\n\tRefUri: {(this :> ICvBase).RefUri}"