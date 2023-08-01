module IntegrationTests.AssayMetadata

open ControlledVocabulary
open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open ArcGraphModel
open Xunit

open TestUtils



let allExpectedMetadataTerms = 
    
    Terms.AssayMetadata.cvTerms
    |> List.skip 1 //(ignore root term)
    |> List.map (fun p -> CvParam(p, ParamValue.Value "", []))