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
        //|> List.skip 1 //(ignore root term)
        |> List.map (fun p -> CvParam(p, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), []))

    let parsedStudyMetadataEmpty = Study.parseMetadataSheetFromFile "Fixtures/incorrect/study_empty.xlsx"
    let parsedStudyMetadataSimple = Study.parseMetadataSheetFromFile "Fixtures/correct/study_simple.xlsx"

    [<Fact>]
    let ``First Param is CvParam`` () =
        Assert.True (parsedStudyMetadataEmpty.Head |> Param.tryCvParam).IsSome 

    [<Fact>]
    let ``First CvParam`` () = CvParam.structuralEquality (parsedStudyMetadataEmpty.Head :?> CvParam) allExpectedMetadataTermsEmpty[0]

    //[<Fact>]
    //let ``Empty study is parsed with all structural ontology terms in order`` () =
    //    Assert.All((List.zip allExpectedMetadataTermsEmpty parsedStudyMetadataEmpty), (fun (expected,actual) ->
    //        CvParam.structuralEquality (expected) (actual :?> CvParam)
    //    ))

    //let expectedTermValuesSimple = 
    //    [
    //        [""]
    //        [""; "sid"]
    //        [""; "stit"]
    //        [""; "sdesc"]
    //        [""; "ssubdat"]
    //        [""; "spubreldat"]
    //        [""; "sfilnam"]
    //        [""]
    //        [""; "sdestyp"]
    //        [""; "sdestan"]
    //        [""; "sdestyptsr"]
    //        [""]
    //        [""; "spubpubmedid"]
    //        [""]
    //        [""]
    //        [""; "spubtit"]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""; "samt"]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""; "Moliver"; "Schnevin"; "Oh"]
    //        [""; "Aus"; "Keider"; "Ah"]
    //        [""; "G."]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //        [""]
    //    ]

    //let allExpectedMetadataTermsFull =
    //    Terms.StudyMetadata.cvTerms
    //    |> List.skip 1 //(ignore root term)
    //    |> List.filter (fun t -> not (t.Name.StartsWith("Comment"))) // ignore orcids
    //    |> List.filter (fun t -> not (List.contains t Terms.InvestigationMetadata.obsoleteCvTerms)) // ignore obsolete terms
    //    |> List.zip expectedTermValuesSimple
    //    |> List.map (fun (values,term) ->
    //        values
    //        |> List.mapi (fun i v ->
    //            if i = 0 then
    //                CvParam(term, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), [])
    //            else
    //                CvParam(term, ParamValue.Value v, [])
    //        )
    //    )
    //    |> List.concat

    //[<Fact>]
    //let ``Simple study is parsed with all structural ontology terms in order`` () =
    //    Assert.All((List.zip allExpectedMetadataTermsFull parsedStudyMetadataSimple), (fun (expected,actual) ->
    //        CvParam.structuralEquality (expected) (actual :?> CvParam)
    //    ))