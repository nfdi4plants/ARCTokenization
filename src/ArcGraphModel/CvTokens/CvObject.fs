namespace ArcGraphModel

open System.Collections.Generic

/// Represents a generic object, annotated with a controlled vocabulary term
type CvObject<'T>(cvAccession : string, cvName : string, cvRefUri : string, object : 'T, attributes : Dictionary<string,IParam>) =

    inherit CvAttributeCollection(attributes)

    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri

    new ((id,name,ref) : CvTerm,object : 'T, attributes) = CvObject (id,name,ref,object,attributes)

    member this.Object = object

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}"