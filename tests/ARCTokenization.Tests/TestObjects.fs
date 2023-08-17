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