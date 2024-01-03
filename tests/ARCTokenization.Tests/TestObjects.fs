module TestObjects

open ARCTokenization
open FsSpreadsheet

module Tokenization =
    
    module InvestigationMetadata =
        
        ()

module Integration =
    
    module AssayAnnotationTable =
    
        module Correct =
        
            let ``assay with only source and sample column`` =
                Assay.parseAnnotationTablesFromFile "Fixtures/correct/assay_with_only_source_and_sample_column.xlsx"

            let ``assay with single characteristics`` = 
                Assay.parseAnnotationTablesFromFile "Fixtures/correct/assay_with_single_characteristics.xlsx"

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