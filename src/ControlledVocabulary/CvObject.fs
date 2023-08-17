namespace ControlledVocabulary

open System.Collections.Generic

/// Represents a generic object, annotated with a controlled vocabulary term
type CvObject<'T>(cvAccession : string, cvName : string, cvRef : string, object : 'T, attributes : Dictionary<string,IParam>) =

    inherit CvAttributeCollection(attributes)

    interface ICvBase with 
        member this.Accession   = cvAccession
        member this.Name        = cvName
        member this.RefUri         = cvRef
        member this.HasAttributes 
            with get() = this.Attributes |> Seq.isEmpty |> not

    new (term: CvTerm, object : 'T, attributes) = CvObject (term.Accession, term.Name, term.RefUri, object, attributes)

    member this.Object = object

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).Accession}\n\tRefUri: {(this :> ICvBase).RefUri}"