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
                | IOType.Source            -> StructuralOntology.APGSO.IOType.Source
                | IOType.Sample            -> StructuralOntology.APGSO.IOType.Sample
                | IOType.RawDataFile       -> StructuralOntology.APGSO.IOType.RawDataFile
                | IOType.DerivedDataFile   -> StructuralOntology.APGSO.IOType.DerivedDataFile
                | IOType.ImageFile         -> StructuralOntology.APGSO.IOType.ImageFile
                | IOType.Material          -> StructuralOntology.APGSO.IOType.Material
                | IOType.FreeText s        -> CvTerm.create (accession = "", name = s, ref = "")

        module CompositeHeader =

            let toCvTerm(ch: CompositeHeader) =
                match ch with
                | CompositeHeader.Characteristic _      -> StructuralOntology.APGSO.``Process Graph Header``.Characteristic
                | CompositeHeader.Factor _              -> StructuralOntology.APGSO.``Process Graph Header``.Factor
                | CompositeHeader.Parameter _           -> StructuralOntology.APGSO.``Process Graph Header``.Parameter
                | CompositeHeader.Component _           -> StructuralOntology.APGSO.``Process Graph Header``.Component
                | CompositeHeader.ProtocolType          -> StructuralOntology.APGSO.``Process Graph Header``.ProtocolType
                | CompositeHeader.ProtocolDescription   -> StructuralOntology.APGSO.``Process Graph Header``.ProtocolDescription
                | CompositeHeader.ProtocolUri           -> StructuralOntology.APGSO.``Process Graph Header``.ProtocolUri
                | CompositeHeader.ProtocolVersion       -> StructuralOntology.APGSO.``Process Graph Header``.ProtocolVersion
                | CompositeHeader.ProtocolREF           -> StructuralOntology.APGSO.``Process Graph Header``.ProtocolREF
                | CompositeHeader.Performer             -> StructuralOntology.APGSO.``Process Graph Header``.Performer
                | CompositeHeader.Date                  -> StructuralOntology.APGSO.``Process Graph Header``.Date
                | CompositeHeader.Input _               -> StructuralOntology.APGSO.``Process Graph Header``.Input
                | CompositeHeader.Output _              -> StructuralOntology.APGSO.``Process Graph Header``.Output
                | CompositeHeader.FreeText _            -> StructuralOntology.APGSO.FreeText

            let toHeaderParam (ch: CompositeHeader) : IParam = 
                match ch with
                | CompositeHeader.Characteristic term -> 
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Characteristic, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.Factor term -> 
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Factor, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.Parameter term ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Parameter, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.Component term -> 
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Component, ParamValue.CvValue (OntologyAnnotation.asCvTerm term))

                | CompositeHeader.ProtocolType -> 
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.ProtocolType, ParamValue.Value "")

                | CompositeHeader.ProtocolDescription ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.ProtocolDescription, ParamValue.Value "")

                | CompositeHeader.ProtocolUri ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.ProtocolUri, ParamValue.Value "")

                | CompositeHeader.ProtocolVersion ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.ProtocolVersion, ParamValue.Value "")

                | CompositeHeader.ProtocolREF ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.ProtocolREF, ParamValue.Value "")

                | CompositeHeader.Performer ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Performer, ParamValue.Value "")

                | CompositeHeader.Date ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Date, ParamValue.Value "")

                | CompositeHeader.Input io ->
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Input, ParamValue.CvValue (IOType.asCvTerm io))

                | CompositeHeader.Output io -> 
                    CvParam(StructuralOntology.APGSO.``Process Graph Header``.Output, ParamValue.CvValue (IOType.asCvTerm io))

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

    module SpecificTokens =
        
        /// Represents the type of file system entity (Directory or File)
        type PType =
            | File
            | Directory
    
        /// Matches a CvParam based on the relative path and file system type
        let matchFileSystemTokenByRelativePath (pType:PType) (relativePath: string) = 
                match pType with
                | PType.Directory ->
                    match (relativePath.Split '/') with
                    | [|Path.StudiesFolderName|]        ->  StructuralOntology.AFSO.``Studies Directory``   |> fun t -> CvParam(t,relativePath)
                    | [|Path.StudiesFolderName; _|]     ->  StructuralOntology.AFSO.``Study Directory``     |> fun t -> CvParam(t,relativePath)
                    | [|Path.AssaysFolderName|]         ->  StructuralOntology.AFSO.``Assays Directory``    |> fun t -> CvParam(t,relativePath)
                    | [|Path.AssaysFolderName; _|]      ->  StructuralOntology.AFSO.``Assay Directory``     |> fun t -> CvParam(t,relativePath)
                    | [|Path.RunsFolderName|]           ->  StructuralOntology.AFSO.``Runs Directory``      |> fun t -> CvParam(t,relativePath)
                    | [|Path.RunsFolderName; _|]        ->  StructuralOntology.AFSO.``Run Directory``       |> fun t -> CvParam(t,relativePath)
                    | [|Path.WorkflowsFolderName|]      ->  StructuralOntology.AFSO.``Workflows Directory`` |> fun t -> CvParam(t,relativePath)
                    | [|Path.WorkflowsFolderName; _|]   ->  StructuralOntology.AFSO.``Study Directory``     |> fun t -> CvParam(t,relativePath)
                    | _                                 ->  StructuralOntology.AFSO.``Directory Path``      |> fun t -> CvParam(t,relativePath)
                | PType.File ->
                    match relativePath with
                    | _ when relativePath.EndsWith "isa.investigation.xlsx" -> StructuralOntology.AFSO.``Investigation File``   |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith "isa.assay.xlsx"         -> StructuralOntology.AFSO.``Assay File``           |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith "isa.dataset.xlsx"       -> StructuralOntology.AFSO.``Dataset File``         |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith "isa.study.xlsx"         -> StructuralOntology.AFSO.``Study File``           |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith ".yml"                   -> StructuralOntology.AFSO.``YML File``             |> fun t -> CvParam(t,relativePath)
                    | _ when relativePath.EndsWith ".cwl"                   -> StructuralOntology.AFSO.``CWL File``             |> fun t -> CvParam(t,relativePath)
                    | _                                                     -> StructuralOntology.AFSO.``File Path``            |> fun t -> CvParam(t,relativePath)
        
        /// Gets CvParams based on the root path, file system type, and full path
        let getSpecificTokens (rootPath:string) (pType:PType) (path:string) =
            let relativePath = path.Replace(rootPath,"")
            matchFileSystemTokenByRelativePath pType relativePath
            