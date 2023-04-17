namespace ArcGraphModel


/// Interface ensures the propterties necessary for CvTerm 
type ICvBase =
    abstract member ID       : string
    abstract member Name     : string
    abstract member RefUri   : string
    // here or as node/edge abstraction 
    // abstract member DataType   : ModelDataType
    // abstract member Adress     : Adress / Location / Position

    //override this.ToString() = 
    //    $"Name: {this.Name}\nID: {this.ID}\nRefUri: {this.RefUri}"

module CvBase = 

    let getCvAccession (param : #ICvBase) =
        param.ID

    let getCvName (param : #ICvBase) =
        param.Name

    let getCvRef (param : #ICvBase) =
        param.RefUri