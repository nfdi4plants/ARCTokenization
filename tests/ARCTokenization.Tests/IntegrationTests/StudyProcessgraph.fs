namespace IntegrationTests

module StudyProcessGraph =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.ExcelIO
    open ARCTokenization
    open Xunit

    open TestUtils

    let parsedStudyProcessGraphSimple = Study.parseProcessGraphColumnsFromFile "Fixtures/correct/study_with_source_characteristics_sample.xlsx"

    let allExpectedProcessGraphTerms = 
        Map ([
            "process_sheet_1",
            [
                // Input [Source Name]
                [
                    CvParam(CvTerm.create("APGSO:00000013", "Input", "APGSO"), ParamValue.CvValue(CvTerm.create("APGSO:00000016", "Source", "APGSO")))
                    CvParam(CvTerm.create("APGSO:00000013", "Input", "APGSO"), ParamValue.Value "Source_1")
                    CvParam(CvTerm.create("APGSO:00000013", "Input", "APGSO"), ParamValue.Value "Source_1")
                ]
                // Characteristic [organism] | Term Source REF (OBI:0100026) | Term Accession Number (OBI:0100026)
                [
                    CvParam(CvTerm.create("APGSO:00000002", "Characteristic", "APGSO"), ParamValue.CvValue(CvTerm.create("OBI:0100026","organism","OBI")))
                    CvParam(CvTerm.create("APGSO:00000002", "Characteristic", "APGSO"), ParamValue.CvValue(CvTerm.create("http://purl.obolibrary.org/obo/NCBITaxon_3702","Arabidopsis thaliana","NCBITaxon")))
                    CvParam(CvTerm.create("APGSO:00000002", "Characteristic", "APGSO"), ParamValue.CvValue(CvTerm.create("http://purl.obolibrary.org/obo/NCBITaxon_3702","Arabidopsis thaliana","NCBITaxon")))
                ]
                // Output [Sample Name]
                [
                    CvParam(CvTerm.create("APGSO:00000014", "Output", "APGSO"), ParamValue.CvValue(CvTerm.create("APGSO:00000017", "Sample", "APGSO")))
                    CvParam(CvTerm.create("APGSO:00000014", "Output", "APGSO"), ParamValue.Value "Sample_1")
                    CvParam(CvTerm.create("APGSO:00000014", "Output", "APGSO"), ParamValue.Value "Sample_2")
                ]
            ]
        ])

    [<Fact>]
    let ``Simple study process graph has corrrect sheet keys`` () =
        Assert.Equal<seq<string>>(
            (allExpectedProcessGraphTerms  |> Map.keys), 
            (parsedStudyProcessGraphSimple |> Map.keys)
        )

    [<Fact>]
    let ``Simple study process graph is tokenized correctly`` () =
        Assert.All(
            List.zip
                allExpectedProcessGraphTerms["process_sheet_1"]
                parsedStudyProcessGraphSimple["process_sheet_1"]
            ,
            (fun (expected,actual) -> 
                Assert.All(
                    List.zip expected actual,
                    (fun (expected,actual) -> Param.typedStructuralEquality expected actual)
                )
            )
        )

