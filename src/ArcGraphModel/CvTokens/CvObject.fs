namespace ArcGraphModel

/// Represents a generic object, annotated with a controlled vocabulary term
type CvObject<'T>(cvAccession : string, cvName : string, cvRefUri : string, object : 'T) =
    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri

    new ((id,name,ref) : CvTerm,object : 'T) = CvObject (id,name,ref,object)

    member this.Object = object

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}"