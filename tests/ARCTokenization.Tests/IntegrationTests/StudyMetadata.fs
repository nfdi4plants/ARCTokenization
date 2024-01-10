namespace IntegrationTests

module StudyMetadata =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.ExcelIO
    open ARCTokenization
    open Xunit

    open TestUtils

    let allExpectedMetadataTermsEmpty = 
        Terms.StudyMetadata.nonObsoleteNonRootCvTerms
        |> List.map (fun p -> CvParam(p, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), []))

    let allExpectedMetadataTermsFull =
        ARCMock.StudyMetadataTokens(
            Study_Identifier = ["experiment1_material"],
            Study_Title = ["Prototype for experimental data"],
            Study_Description = ["In this a devised study to have an exemplary experimental material description."],
            Study_File_Name = [@"experiment1_material\isa.study.xlsx"]
        )
        |> List.concat // use flat list

    open ARCTokenization.StructuralOntology

    [<Fact>]
    let ``Simple study is parsed from filepath CvParam with all structural ontology terms in order`` () =
        let fakePath = CvParam(cvTerm = AFSO.``File Path``, v = "Fixtures/correct/study_simple.xlsx")
        let actual = 
            [fakePath]
            |> Study.parseMetadataSheetsFromTokens(
                FileName = "study_simple.xlsx"
            ) 
            |> Seq.head
        Assert.All((List.zip allExpectedMetadataTermsFull actual), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))