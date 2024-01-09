module TestObjects

open ARCTokenization
open FsSpreadsheet
open ARCtrl
open ARCtrl.ISA

module MockAPI =
    
    module InvestigationMetadataTokens = 

        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty = 
            ARCMock.InvestigationMetadataTokens()
            |> List.concat // use flat list

    module StudyMetadataTokens = 

        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty = 
            ARCMock.StudyMetadataTokens()
            |> List.concat // use flat list

    module AssayMetadataTokens = 
        
        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty = 
            ARCMock.AssayMetadataTokens()
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