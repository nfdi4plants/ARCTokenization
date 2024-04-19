namespace IntegrationTests

module InvestigationMetadata =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.ExcelIO
    open ARCTokenization
    open Xunit

    open TestUtils

    let parsedInvestigationMetadataEmpty = Investigation.parseMetadataSheetFromFile() "Fixtures/incorrect/investigation_empty.xlsx"
    let parsedInvestigationMetadataSimple = Investigation.parseMetadataSheetFromFile() "Fixtures/correct/isa.investigation_simple.xlsx"

    let allExpectedMetadataTermsEmpty = 
        Terms.InvestigationMetadata.nonObsoleteNonRootCvTerms
        |> List.filter (fun t -> not (t.Name.StartsWith("Comment"))) // ignore orcids
        |> List.map (fun p -> CvParam(p, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), []))

    [<Fact>]
    let ``First Param is CvParam`` () =
        Assert.True (parsedInvestigationMetadataEmpty.Head |> Param.tryCvParam).IsSome 

    [<Fact>]
    let ``First CvParam`` () = CvParam.structuralEquality (parsedInvestigationMetadataEmpty.Head :?> CvParam) allExpectedMetadataTermsEmpty[0]

    [<Fact>]
    let ``Empty investigation is parsed with all structural ontology terms in order`` () =
        Assert.All((List.zip allExpectedMetadataTermsEmpty parsedInvestigationMetadataEmpty), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))

    let allExpectedMetadataTerms =
        ARCMock.InvestigationMetadataTokens(
            true,
            ONTOLOGY_SOURCE_REFERENCE = Seq.empty,
            Term_Source_Name= Seq.empty,
            Term_Source_File= Seq.empty,
            Term_Source_Version= Seq.empty,
            Term_Source_Description= Seq.empty,
            INVESTIGATION= Seq.empty,
            Investigation_Submission_Date= Seq.empty,
            Investigation_Public_Release_Date= Seq.empty,
            INVESTIGATION_PUBLICATIONS= Seq.empty,
            Investigation_Publication_PubMed_ID= Seq.empty,
            Investigation_Publication_DOI= Seq.empty,
            Investigation_Publication_Author_List= Seq.empty,
            Investigation_Publication_Title= Seq.empty,
            Investigation_Publication_Status= Seq.empty,
            Investigation_Publication_Status_Term_Accession_Number= Seq.empty,
            Investigation_Publication_Status_Term_Source_REF= Seq.empty,
            Investigation_Identifier = ["iid"],
            Investigation_Title = ["ititle"],
            Investigation_Description = ["idesc"],
            INVESTIGATION_CONTACTS= Seq.empty,
            Investigation_Person_Last_Name = ["Maus"; "Keider"; "müller"; ""; "oih"],
            Investigation_Person_First_Name = ["Oliver"; ""; "andreas";],
            Investigation_Person_Mid_Initials = ["L. I."; "C."],
            Investigation_Person_Email = ["maus@nfdi4plants.org"],
            Investigation_Person_Phone= Seq.empty,
            Investigation_Person_Fax= Seq.empty,
            Investigation_Person_Address= Seq.empty,
            Investigation_Person_Affiliation = [""; "Affe"],
            Investigation_Person_Roles= Seq.empty,
            Investigation_Person_Roles_Term_Accession_Number= Seq.empty,
            Investigation_Person_Roles_Term_Source_REF= Seq.empty
        )
        |> List.concat // use flat list

    [<Fact>]
    let ``Simple investigation is parsed with all structural ontology terms in order`` () =
        let x = 
            allExpectedMetadataTerms
            |>List.map(fun x -> x.Name)
            |>String.concat "\n"
        let y =     
            parsedInvestigationMetadataSimple
            |>List.map(fun x -> x.Name)
            |>String.concat "\n"

        let z = String.concat "--------------\n\n" [x; y]
        // Assert.True(1=3,z)
        Assert.All((List.zip allExpectedMetadataTerms parsedInvestigationMetadataSimple), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam) 
        ))

    open ARCTokenization.StructuralOntology

    [<Fact>]
    let ``Simple investigation is parsed from filepath CvParam with all structural ontology terms in order`` () =
        // let fakePath = CvParam(cvTerm = AFSO.``Investigation File``, v = "isa.investigation.xlsx")
        let rootDir = (System.IO.Path.GetFullPath("Fixtures/arcStructure/"))
        let absoluteDirectoryPaths = FileSystem.parseARCFileSystem rootDir
        let actual = 
            Investigation.parseMetadataSheetsFromTokens(
            )  rootDir absoluteDirectoryPaths
            |> Seq.head

        Assert.All((List.zip allExpectedMetadataTerms actual), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))