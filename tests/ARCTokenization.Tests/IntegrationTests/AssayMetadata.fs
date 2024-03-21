namespace IntegrationTests

module AssayMetadata =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.ExcelIO
    open ARCTokenization
    open Xunit

    open TestUtils

    let allExpectedMetadataTermsEmpty = 
        Terms.AssayMetadata.nonObsoleteNonRootCvTerms
        |> List.map (fun p -> CvParam(p, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), []))

    open ARCTokenization.StructuralOntology

    let allExpectedMetadataTermsFull =
        ARCMock.AssayMetadataTokens(
            Assay_File_Name = [@"measurement1\isa.assay.xlsx"],
            Assay_Performer_First_Name = ["Oliver"; "Marius"],
            Assay_Performer_Last_Name = ["Maus"; "Katz"],
            Assay_Performer_Mid_Initials = [""; "G."],
            Assay_Performer_Email = ["maus@nfdi4plants.org"],
            Assay_Performer_Affiliation = ["RPTU University of Kaiserslautern"],
            Assay_Performer_Roles = ["research assistant"],
            Assay_Performer_Roles_Term_Accession_Number = ["http://purl.org/spar/scoro/research-assistant"],
            Assay_Performer_Roles_Term_Source_REF = ["scoro"]
        )
        |> List.concat // use flat list

    [<Fact>]
    let ``Simple study is parsed from filepath CvParam with all structural ontology terms in order`` () =
        let fakePath = CvParam(cvTerm = AFSO.Assay_File, v = "assays/measurement1/isa.assay.xlsx")
        let rootDir = (System.IO.Path.GetFullPath("Fixtures/arcStructure/"))
        let actual = 
            [fakePath]
            |> Assay.parseMetadataSheetsFromTokens(
            ) rootDir
            |> Seq.head
        Assert.All((List.zip allExpectedMetadataTermsFull actual), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))