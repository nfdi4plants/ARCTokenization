namespace ARCTokenization

open ControlledVocabulary
open FsSpreadsheet
open MetadataSheet
open ARCTokenization.Terms
open ARCtrl
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

    module ARCtrl =

        module OntologyAnnotation =

            let asCvTerm (oa: OntologyAnnotation) =
                CvTerm.create(
                    accession = oa.TermAccessionString,
                    name = oa.NameText,
                    ref = oa.TermSourceREFString
                )

        module IOType =

            let asCvTerm (io: IOType) = 
                match io with
                | IOType.Source            -> StructuralOntology.APGSO.Source
                | IOType.Sample            -> StructuralOntology.APGSO.Sample
                | IOType.RawDataFile       -> StructuralOntology.APGSO.RawDataFile
                | IOType.DerivedDataFile   -> StructuralOntology.APGSO.DerivedDataFile
                | IOType.ImageFile         -> StructuralOntology.APGSO.ImageFile
                | IOType.Material          -> StructuralOntology.APGSO.Material
                | IOType.FreeText s        -> CvTerm.create (accession = "", name = s, ref = "")

        module CompositeHeader =

            let toCvTerm(ch: CompositeHeader) =
                match ch with
                | CompositeHeader.Characteristic _      -> StructuralOntology.APGSO.Characteristic
                | CompositeHeader.Factor _              -> StructuralOntology.APGSO.Factor
                | CompositeHeader.Parameter _           -> StructuralOntology.APGSO.Parameter
                | CompositeHeader.Component _           -> StructuralOntology.APGSO.Component
                | CompositeHeader.ProtocolType          -> StructuralOntology.APGSO.ProtocolType
                | CompositeHeader.ProtocolDescription   -> StructuralOntology.APGSO.ProtocolDescription
                | CompositeHeader.ProtocolUri           -> StructuralOntology.APGSO.ProtocolUri
                | CompositeHeader.ProtocolVersion       -> StructuralOntology.APGSO.ProtocolVersion
                | CompositeHeader.ProtocolREF           -> StructuralOntology.APGSO.ProtocolREF
                | CompositeHeader.Performer             -> StructuralOntology.APGSO.Performer
                | CompositeHeader.Date                  -> StructuralOntology.APGSO.Date
                | CompositeHeader.Input _               -> StructuralOntology.APGSO.Input
                | CompositeHeader.Output _              -> StructuralOntology.APGSO.Output
                | CompositeHeader.FreeText _            -> StructuralOntology.APGSO.FreeText

            let toHeaderParam (ch: CompositeHeader) : IParam = 
                match ch with
                | CompositeHeader.Characteristic term -> 
                    CvParam(StructuralOntology.APGSO.Characteristic, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.Factor term -> 
                    CvParam(StructuralOntology.APGSO.Factor, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.Parameter term ->
                    CvParam(StructuralOntology.APGSO.Parameter, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.Component term -> 
                    CvParam(StructuralOntology.APGSO.Component, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.ProtocolType -> 
                    CvParam(StructuralOntology.APGSO.ProtocolType, ParamValue.Value "")

                | CompositeHeader.ProtocolDescription ->
                    CvParam(StructuralOntology.APGSO.ProtocolDescription, ParamValue.Value "")

                | CompositeHeader.ProtocolUri ->
                    CvParam(StructuralOntology.APGSO.ProtocolUri, ParamValue.Value "")

                | CompositeHeader.ProtocolVersion ->
                    CvParam(StructuralOntology.APGSO.ProtocolVersion, ParamValue.Value "")

                | CompositeHeader.ProtocolREF ->
                    CvParam(StructuralOntology.APGSO.ProtocolREF, ParamValue.Value "")

                | CompositeHeader.Performer ->
                    CvParam(StructuralOntology.APGSO.Performer, ParamValue.Value "")

                | CompositeHeader.Date ->
                    CvParam(StructuralOntology.APGSO.Date, ParamValue.Value "")

                | CompositeHeader.Input io ->
                    CvParam(StructuralOntology.APGSO.Input, ParamValue.CvValue (IOType.asCvTerm io))

                | CompositeHeader.Output io -> 
                    CvParam(StructuralOntology.APGSO.Output, ParamValue.CvValue (IOType.asCvTerm io))

                | CompositeHeader.FreeText f -> 
                    UserParam(f, ParamValue.CvValue StructuralOntology.APGSO.FreeText)

        module CompositeCell =
            
            let toCvParam (ch: CompositeHeader) (cc: CompositeCell) : IParam =

                let headerTerm = CompositeHeader.toCvTerm ch

                match cc with
                | CompositeCell.FreeText t -> 
                    CvParam(headerTerm, ParamValue.Value t)

                | CompositeCell.Term term ->
                    CvParam(headerTerm, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeCell.Unitized (v, unit) ->
                    CvParam(headerTerm, ParamValue.WithCvUnitAccession (v, OntologyAnnotation.asCvTerm unit))

        module CompositeColumn =
            
            let tokenize (cc: CompositeColumn) : IParam list= 
                [
                    CompositeHeader.toHeaderParam cc.Header
                    yield! (Array.map (CompositeCell.toCvParam cc.Header) cc.Cells)
                ]

        module ARCTable =
            
            let tokenizeColumns (at: ArcTable) = 
                at.Columns
                |> Array.map CompositeColumn.tokenize
                |> List.ofArray

    module ArcFileSystem =
        
        /// Represents the type of file system entity (Directory or File)
        type PType =
            | File
            | Directory
    
        /// Matches a CvParam based on the relative path and file system type
        let convertRelativePath (pType:PType) (relativePath: string) = 
                match pType with
                | PType.Directory ->
                    match (relativePath.Split '/') with
                    | [|Path.StudiesFolderName|]        ->  StructuralOntology.AFSO.Studies_Directory   |> fun t -> CvParam(t,relativePath)
                    | [|Path.StudiesFolderName; _|]     ->  StructuralOntology.AFSO.Study_Directory     |> fun t -> CvParam(t,relativePath)
                    | [|Path.AssaysFolderName|]         ->  StructuralOntology.AFSO.Assays_Directory    |> fun t -> CvParam(t,relativePath)
                    | [|Path.AssaysFolderName; _|]      ->  StructuralOntology.AFSO.Assay_Directory     |> fun t -> CvParam(t,relativePath)
                    | [|Path.RunsFolderName|]           ->  StructuralOntology.AFSO.Runs_Directory      |> fun t -> CvParam(t,relativePath)
                    | [|Path.RunsFolderName; _|]        ->  StructuralOntology.AFSO.Run_Directory       |> fun t -> CvParam(t,relativePath)
                    | [|Path.WorkflowsFolderName|]      ->  StructuralOntology.AFSO.Workflows_Directory |> fun t -> CvParam(t,relativePath)
                    | [|Path.WorkflowsFolderName; _|]   ->  StructuralOntology.AFSO.Workflow_Directory  |> fun t -> CvParam(t,relativePath)
                    | _                                 ->  StructuralOntology.AFSO.Directory_Path      |> fun t -> CvParam(t,relativePath)
                | PType.File ->
                    match relativePath with
                    | _ when relativePath.EndsWith "isa.investigation.xlsx" -> StructuralOntology.AFSO.Investigation_File   |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith "isa.assay.xlsx"         -> StructuralOntology.AFSO.Assay_File           |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith "isa.dataset.xlsx"       -> StructuralOntology.AFSO.Dataset_File         |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith "isa.study.xlsx"         -> StructuralOntology.AFSO.Study_File           |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith ".yml"                   -> StructuralOntology.AFSO.YML_File             |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith ".cwl"                   -> StructuralOntology.AFSO.CWL_File             |> fun t -> CvParam(t,relativePath)
                    | _                                                     -> StructuralOntology.AFSO.File_Path            |> fun t -> CvParam(t,relativePath)
        
        /// Gets CvParams based on the root path, file system type, and full path
        let getArcFileSystemTokens (rootPath:string) (pType:PType) (path:string) =
            let relativePath = path.Replace(rootPath,"").TrimStart('/')
            convertRelativePath pType relativePath
            