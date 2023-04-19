namespace ArcGraphModel

/// Represents a structured value, annotated by a user defined name
type UserParam(name : string, paramValue : ParamValue) =
    interface ICvBase with 
        member this.ID     = name
        member this.Name   = name
        member this.RefUri = "UserTerm"
    interface IParamBase with
        member this.Value  = paramValue

    override this.ToString() = 
        $"Name: {(this :> ICvBase).Name}\n\tID: {(this :> ICvBase).ID}\n\tRefUri: {(this :> ICvBase).RefUri}\n\tValue: {(this :> IParamBase).Value}"