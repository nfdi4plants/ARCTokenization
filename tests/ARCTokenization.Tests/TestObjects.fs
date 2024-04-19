﻿module TestObjects

open ARCTokenization
open FsSpreadsheet
open ARCtrl
open ARCtrl.ISA

module MockAPI =
    
    module InvestigationMetadataTokens = 

        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty = 
            ARCMock.InvestigationMetadataTokens(false)
            |> List.concat // use flat list

    module StudyMetadataTokens = 

        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty = 
            ARCMock.StudyMetadataTokens(false)
            |> List.concat // use flat list

    module AssayMetadataTokens = 
        
        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty = 
            ARCMock.AssayMetadataTokens(false)
            |> List.concat // use flat list

    module ProcessGraphTokens =
        
        let inputColumn = 
            ARCMock.ProcessGraphColumn(
                header = CompositeHeader.Input IOType.Source,
                cells = [
                    CompositeCell.FreeText "Source_1"
                    CompositeCell.FreeText "Source_1"
                ]
            )

        let characteristicsColumn =
            ARCMock.ProcessGraphColumn(
                header = CompositeHeader.Characteristic (OntologyAnnotation.create(TermAccessionNumber = "OBI:0100026", Name = AnnotationValue.Text "organism", TermSourceREF = "OBI")),
                cells = [
                    CompositeCell.Term (OntologyAnnotation.create(TermAccessionNumber = "http://purl.obolibrary.org/obo/NCBITaxon_3702", Name = AnnotationValue.Text "Arabidopsis thaliana", TermSourceREF = "NCBITaxon"))
                    CompositeCell.Term (OntologyAnnotation.create(TermAccessionNumber = "http://purl.obolibrary.org/obo/NCBITaxon_3702", Name = AnnotationValue.Text "Arabidopsis thaliana", TermSourceREF = "NCBITaxon"))
                ]
            )
                    
        let outputColumn = 
            ARCMock.ProcessGraphColumn(
                header = CompositeHeader.Output IOType.Sample,
                cells = [
                    CompositeCell.FreeText "Sample_1"
                    CompositeCell.FreeText "Sample_2"
                ]
            )

        let simpleStudy = 
            ARCMock.ProcessGraph(
                [
                    CompositeHeader.Input IOType.Source, [
                        CompositeCell.FreeText "Source_1"
                        CompositeCell.FreeText "Source_1"
                    ]

                    CompositeHeader.Characteristic (OntologyAnnotation.create(TermAccessionNumber = "OBI:0100026", Name = AnnotationValue.Text "organism", TermSourceREF = "OBI")), [
                        CompositeCell.Term (OntologyAnnotation.create(TermAccessionNumber = "http://purl.obolibrary.org/obo/NCBITaxon_3702", Name = AnnotationValue.Text "Arabidopsis thaliana", TermSourceREF = "NCBITaxon"))
                        CompositeCell.Term (OntologyAnnotation.create(TermAccessionNumber = "http://purl.obolibrary.org/obo/NCBITaxon_3702", Name = AnnotationValue.Text "Arabidopsis thaliana", TermSourceREF = "NCBITaxon"))
                    ]

                    CompositeHeader.Output IOType.Sample, [
                        CompositeCell.FreeText "Sample_1"
                        CompositeCell.FreeText "Sample_2"
                    ]
                ]
            )