namespace IntegrationTests

module StudyMetadata =

    open ControlledVocabulary
    open FsSpreadsheet
    open FsSpreadsheet.Net
    open ARCTokenization
    open Xunit

    open TestUtils

    let allExpectedMetadataTermsEmpty = 
        Terms.StudyMetadata.nonObsoleteNonRootCvTerms
        |> List.map (fun p -> CvParam(p, ParamValue.CvValue (CvTerm.create(accession = "AGMO:00000001", name = "Metadata Section Key", ref = "AGMO")), []))

    let allExpectedMetadataTermsFull =
        ARCMock.StudyMetadataTokens(
            true,
            STUDY= Seq.empty,
            Study_Identifier = ["experiment1_material"],
            Study_Title = ["Prototype for experimental data"],
            Study_Description = ["In this a devised study to have an exemplary experimental material description."],
            Study_Submission_Date= Seq.empty,
            Study_Public_Release_Date=Seq.empty,
            Study_File_Name = ["studies/experiment1_material/isa.study.xlsx"],
            STUDY_DESIGN_DESCRIPTORS= Seq.empty,
            Study_Design_Type = Seq.empty,
            Study_Design_Type_Term_Accession_Number= Seq.empty,
            Study_Design_Type_Term_Source_REF= Seq.empty,
            STUDY_PUBLICATIONS= Seq.empty,
            Study_Publication_PubMed_ID= Seq.empty,
            Study_Publication_DOI= Seq.empty,
            Study_Publication_Author_List= Seq.empty,
            Study_Publication_Title= Seq.empty,
            Study_Publication_Status= Seq.empty,
            Study_Publication_Status_Term_Accession_Number= Seq.empty,
            Study_Publication_Status_Term_Source_REF= Seq.empty,
            STUDY_FACTORS= Seq.empty,
            Study_Factor_Name= Seq.empty,
            Study_Factor_Type= Seq.empty,
            Study_Factor_Type_Term_Accession_Number= Seq.empty,
            Study_Factor_Type_Term_Source_REF= Seq.empty,
            STUDY_ASSAYS= Seq.empty,
            Study_Assay_Measurement_Type= Seq.empty,
            Study_Assay_Measurement_Type_Term_Accession_Number= Seq.empty,
            Study_Assay_Measurement_Type_Term_Source_REF= Seq.empty,
            Study_Assay_Technology_Type= Seq.empty,
            Study_Assay_Technology_Type_Term_Accession_Number= Seq.empty,
            Study_Assay_Technology_Type_Term_Source_REF= Seq.empty,
            Study_Assay_Technology_Platform= Seq.empty,
            Study_Assay_File_Name= Seq.empty,
            STUDY_PROTOCOLS= Seq.empty,
            Study_Protocol_Name = ["process_1";"process_2";"Cell Cultivation"],
            Study_Protocol_Type = ["";"";"growth protocol"],
            Study_Protocol_Type_Term_Accession_Number= ["";"";"http://purl.obolibrary.org/obo/EFO_0003789"],
            Study_Protocol_Type_Term_Source_REF = ["";"";"EFO"],
            Study_Protocol_Description= Seq.empty,
            Study_Protocol_URI= Seq.empty,
            Study_Protocol_Version= Seq.empty,
            Study_Protocol_Parameters_Name= Seq.empty,
            Study_Protocol_Parameters_Term_Accession_Number= Seq.empty,
            Study_Protocol_Parameters_Term_Source_REF= Seq.empty,
            Study_Protocol_Components_Name= Seq.empty,
            Study_Protocol_Components_Type= Seq.empty,
            Study_Protocol_Components_Type_Term_Accession_Number= Seq.empty,
            Study_Protocol_Components_Type_Term_Source_REF= Seq.empty,
            STUDY_CONTACTS= Seq.empty,
            Study_Person_Last_Name= Seq.empty,
            Study_Person_First_Name= Seq.empty,
            Study_Person_Mid_Initials= Seq.empty,
            Study_Person_Email= Seq.empty,
            Study_Person_Phone= Seq.empty,
            Study_Person_Fax= Seq.empty,
            Study_Person_Address= Seq.empty,
            Study_Person_Affiliation= Seq.empty,
            Study_Person_Roles= Seq.empty,
            Study_Person_Roles_Term_Accession_Number= Seq.empty,
            Study_Person_Roles_Term_Source_REF= Seq.empty
        )
        |> List.concat // use flat list

    open ARCTokenization.StructuralOntology

    [<Fact>]
    let ``Simple study is parsed from filepath CvParam with all structural ontology terms in order`` () =
        let fakePath = CvParam(cvTerm = AFSO.``Study File``, v = "studies/experiment1_material/isa.study.xlsx")
        let rootDir = (System.IO.Path.GetFullPath("Fixtures/arcStructure/"))
        let actual = 
            [fakePath]
            |> Study.parseMetadataSheetsFromTokens(
            ) rootDir
            |> Seq.head
        // let x1  = 
        //     allExpectedMetadataTermsFull
        //     |>List.map(fun x -> x.Name)
        //     |>String.concat "\n"
        // let x2  = 
        //     actual
        //     |>List.map(fun x -> x.Name)
        //     |>String.concat "\n"
        // let x = String.concat "--------------/n /n" [x1; x2]
        // Assert.True(1 = 3 , x)
        Assert.All((List.zip allExpectedMetadataTermsFull actual), (fun (expected,actual) ->
            CvParam.structuralEquality (expected) (actual :?> CvParam)
        ))