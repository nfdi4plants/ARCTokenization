namespace ArcGraphModel

open ControlledVocabulary
open FsSpreadsheet
open KeyParser

module Tokenization = 
    
    let convertTokens (line : FsCell seq) =
        match line |> Seq.toList with
        | [] -> failwith "Cannot convert nothin"
        | key :: [] -> 
            let f = parseKey [] key.Value
            [f (ParamValue.Value "")]
        | key :: cells ->
            let f = parseKey [] key.Value
            cells
            |> List.map (fun c -> 
                let param = f (ParamValue.Value c.Value)
                CvAttributeCollection.tryAddAttribute (Address.createRowParam(c.RowNumber)) param       |> ignore
                CvAttributeCollection.tryAddAttribute (Address.createColumnParam(c.ColumnNumber)) param |> ignore
                param
            )
    
    let parseLine (line : FsCell seq) =
        match line |> Seq.toList |> List.filter (fun c -> c.Value <> "") with
        | [] -> seq []
        | [Container (ContainerBase.investigationContacts) container]
        | [Container (ContainerBase.investigationPublication) container] ->
            container 
            |> CvAttributeCollection.tryAddAttribute (CvParam(Terms.investigation,"")) 
            |> ignore
            [container] |> Seq.cast<ICvBase>
        | [Container (ContainerBase.studyContacts) container]
        | [Container (ContainerBase.studyAssays) container]
        | [Container (ContainerBase.studyDesignDescriptors) container]
        | [Container (ContainerBase.studyFactors) container] ->
            container 
            |> CvAttributeCollection.tryAddAttribute (CvParam(Terms.study,"")) 
            |> ignore
            [container] |> Seq.cast<ICvBase>
        | [Container (ContainerBase.investigation) container]
        | [Container (ContainerBase.study) container] -> 
            [container] |> Seq.cast<ICvBase>
        | line -> 
            convertTokens line
            |> Seq.cast<ICvBase>