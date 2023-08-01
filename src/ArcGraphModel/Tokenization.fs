namespace ArcGraphModel

open ControlledVocabulary
open FsSpreadsheet
open MetadataSheet
open ArcGraphModel.Terms

module Tokenization = 
    
    let convertTokens (keyParser: IParam list -> string -> (ParamValue -> IParam)) (line : FsCell seq) =
        match line |> Seq.toList with
        | [] -> failwith "Cannot convert nothin"
        | key :: [] -> 
            let f = keyParser [] key.Value
            [f (ParamValue.CvValue ("","MetadataSectionKey",""))]
        | key :: cells ->
            let f = keyParser [] key.Value
            cells
            |> List.map (fun c -> 
                let param = f (ParamValue.Value c.Value)
                CvAttributeCollection.tryAddAttribute (Address.createRowParam(c.RowNumber)) param       |> ignore
                CvAttributeCollection.tryAddAttribute (Address.createColumnParam(c.ColumnNumber)) param |> ignore
                param
            )