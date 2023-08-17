namespace IntegrationTests

module StudyMetadata =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.ExcelIO
    open ARCTokenization
    open Xunit

    open TestUtils

    let allExpectedMetadataTermsEmpty = 
        Terms.StudyMetadata.cvTerms
        |> List.skip 1 //(ignore root term)
        |> List.map (fun p -> CvParam(p, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), []))