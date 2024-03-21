namespace IntegrationTests

module InvestigationMetadata =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.ExcelIO
    open ARCTokenization
    open Xunit

    open TestUtils

    let parsedInvestigationMetadataEmpty = Investigation.parseMetadataSheetFromFile() "Fixtures/incorrect/investigation_empty.xlsx"
    let parsedInvestigationMetadataSimple = Investigation.parseMetadataSheetFromFile() "Fixtures/correct/isa.investigation.xlsx"

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

    let allExpectedMetadataTermsFull =
        ARCMock.InvestigationMetadataTokens(
            Investigation_Identifier = ["iid"],
            Investigation_Title = ["ititle"],
            Investigation_Description = ["idesc"],
            Investigation_Person_Last_Name = ["Maus"; "Keider"; "müller"; ""; "oih"],
            Investigation_Person_First_Name = ["Oliver"; ""; "andreas";],
            Investigation_Person_Mid_Initials = ["L. I."; "C."],
            Investigation_Person_Email = ["maus@nfdi4plants.org"],
            Investigation_Person_Affiliation = [""; "Affe"],
            Study_Identifier = ["sid"],
            Study_Title = ["stitle"],
            Study_Description = ["sdesc"],
            Study_File_Name = [@"sid\isa.study.xlsx"],
            Study_Assay_File_Name = [@"aid\isa.assay.xlsx"; @"aid2\isa.assay.xlsx"],
            Study_Person_Last_Name = ["weil"],
            Study_Person_First_Name = [""; "lukas"]
        )
        |> List.concat // use flat list

    [<Fact>]
    let ``Simple investigation is parsed with all structural ontology terms in order`` () =
        Assert.All((List.zip allExpectedMetadataTermsFull parsedInvestigationMetadataSimple), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))

    open ARCTokenization.StructuralOntology

    [<Fact>]
    let ``Simple investigation is parsed from filepath CvParam with all structural ontology terms in order`` () =
        let fakePath = CvParam(cvTerm = AFSO.Investigation_File, v = "isa.investigation.xlsx")
        let rootDir = (System.IO.Path.GetFullPath("Fixtures/arcStructure/"))
        let actual = 
            Investigation.parseMetadataSheetsFromTokens(
            )  rootDir [fakePath]
            |> Seq.head

        Assert.All((List.zip allExpectedMetadataTermsFull actual), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))