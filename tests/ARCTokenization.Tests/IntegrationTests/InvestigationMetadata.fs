namespace IntegrationTests

module InvestigationMetadata =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.ExcelIO
    open ARCTokenization
    open Xunit

    open TestUtils

    let parsedInvestigationMetadataEmpty = Investigation.parseMetadataSheetFromFile "Fixtures/incorrect/investigation_empty.xlsx"
    let parsedInvestigationMetadataSimple = Investigation.parseMetadataSheetFromFile "Fixtures/correct/investigation_simple.xlsx"

    let allExpectedMetadataTermsEmpty = 
        Terms.InvestigationMetadata.cvTerms
        |> List.skip 1 //(ignore root term)
        |> List.filter (fun t -> not (t.Name.StartsWith("Comment"))) // ignore orcids
        |> List.filter (fun t -> not (List.contains t Terms.InvestigationMetadata.obsoleteCvTerms)) // ignore obsolete terms
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

    let expectedTermValuesSimple = 
        [
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""; "iid"]
            [""; "ititle"]
            [""; "idesc"]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""; "Maus"; "Keider"; "müller"; ""; "oih"]
            [""; "Oliver"; ""; "andreas";]
            [""; "L. I."; "C."]
            [""; "maus@nfdi4plants.org"]
            [""]
            [""]
            [""]
            [""; ""; "Affe"]
            [""]
            [""]
            [""]
            [""]
            [""; "sid"]
            [""; "stitle"]
            [""; "sdesc"]
            [""]
            [""]
            [""; @"sid\isa.study.xlsx"]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""; @"aid\isa.assay.xlsx"; @"aid2\isa.assay.xlsx"]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""; "weil"]
            [""; ""; "lukas"]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
            [""]
        ]

    let allExpectedMetadataTermsFull =
        Terms.InvestigationMetadata.cvTerms
        |> List.skip 1 //(ignore root term)
        |> List.filter (fun t -> not (t.Name.StartsWith("Comment"))) // ignore orcids
        |> List.filter (fun t -> not (List.contains t Terms.InvestigationMetadata.obsoleteCvTerms)) // ignore obsolete terms
        |> List.zip expectedTermValuesSimple
        |> List.map (fun (values,term) ->
            values
            |> List.mapi (fun i v ->
                if i = 0 then
                    CvParam(term, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), [])
                else
                    CvParam(term, ParamValue.Value v, [])
            )
        )
        |> List.concat

    [<Fact>]
    let ``Simple investigation is parsed with all structural ontology terms in order`` () =
        Assert.All((List.zip allExpectedMetadataTermsFull parsedInvestigationMetadataSimple), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))