namespace ARCTokenization

open ControlledVocabulary
open FsSpreadsheet
open MetadataSheet
open ARCTokenization.Terms
open ARCtrl.ISA

module Tokenization = 
    
    let convertMetadataTokens (keyParser: IParam list -> string -> (ParamValue -> IParam)) (line : FsCell seq) =
        match line |> Seq.toList with
        | [] -> failwith "Cannot convert nothin"
        | key :: [] -> 
            let f = keyParser [] (key.ValueAsString())

            let keyTerm = 
                let tmp = f (ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
                CvAttributeCollection.tryAddAttribute (Address.createRowParam(key.RowNumber)) tmp       |> ignore
                CvAttributeCollection.tryAddAttribute (Address.createColumnParam(key.ColumnNumber)) tmp |> ignore
                tmp
            
            [keyTerm]

        | key :: cells ->
            let f = keyParser [] (key.ValueAsString())

            let keyTerm = 
                let tmp = f (ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
                CvAttributeCollection.tryAddAttribute (Address.createRowParam(key.RowNumber)) tmp       |> ignore
                CvAttributeCollection.tryAddAttribute (Address.createColumnParam(key.ColumnNumber)) tmp |> ignore
                tmp

            let cellTerms =
                cells
                |> List.map (fun c -> 
                    let param = f (ParamValue.Value (c.ValueAsString()))
                    CvAttributeCollection.tryAddAttribute (Address.createRowParam(c.RowNumber)) param       |> ignore
                    CvAttributeCollection.tryAddAttribute (Address.createColumnParam(c.ColumnNumber)) param |> ignore
                    param
            
                )
            keyTerm :: cellTerms

    let convertAnnotationValue (av: AnnotationValue) =
        match av with
        | Text t -> t :> System.IConvertible
        | Float f -> f :> System.IConvertible
        | Int i -> i :> System.IConvertible

    module OntologyAnnotation =

        let tryAsCvTerm (oa: OntologyAnnotation) =
            match (oa.TermAccessionNumber, oa.Name, oa.TermSourceREF) with
            | (Some accession, Some name, Some ref) -> 
                Some (
                    CvTerm.create(
                        accession = accession,
                        name = name.ToString(),
                        ref = ref 
                    )
                )

            | _ -> None