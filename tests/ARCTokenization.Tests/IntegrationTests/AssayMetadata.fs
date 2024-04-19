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
            true,
            ASSAY= Seq.empty,
            Assay_Measurement_Type = Seq.empty,
            Assay_Measurement_Type_Term_Accession_Number= Seq.empty,
            Assay_Measurement_Type_Term_Source_REF= Seq.empty,
            Assay_Technology_Type= Seq.empty,
            Assay_Technology_Type_Term_Accession_Number= Seq.empty,
            Assay_Technology_Type_Term_Source_REF= Seq.empty,
            Assay_Technology_Platform= Seq.empty,
            Assay_File_Name = [@"measurement1\isa.assay.xlsx"],
            ASSAY_PERFORMERS= Seq.empty,
            Assay_Person_First_Name = ["Oliver"; "Marius"],
            Assay_Person_Last_Name = ["Maus"; "Katz"],
            Assay_Person_Mid_Initials = [""; "G."],
            Assay_Person_Email = ["maus@nfdi4plants.org"],
            Assay_Person_Phone= Seq.empty,
            Assay_Person_Fax= Seq.empty,
            Assay_Person_Address= Seq.empty,
            Assay_Person_Affiliation = ["RPTU University of Kaiserslautern"],
            Assay_Person_Roles = ["research assistant"],
            Assay_Person_Roles_Term_Accession_Number = ["http://purl.org/spar/scoro/research-assistant"],
            Assay_Person_Roles_Term_Source_REF = ["scoro"]
        )
        |> List.concat // use flat list

    [<Fact>]
    let ``Simple study is parsed from filepath CvParam with all structural ontology terms in order`` () =
        let fakePath = CvParam(cvTerm = AFSO.``Assay File``, v = "assays/measurement1/isa.assay.xlsx")
        let rootDir = (System.IO.Path.GetFullPath("Fixtures/arcStructure/"))
        let actual = 
            [fakePath]
            |> Assay.parseMetadataSheetsFromTokens(
            ) rootDir
            |> Seq.head
        Assert.All((List.zip allExpectedMetadataTermsFull actual), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))