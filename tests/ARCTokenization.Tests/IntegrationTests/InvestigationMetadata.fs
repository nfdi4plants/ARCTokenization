module IntegrationTests.InvestigationMetadata

open ControlledVocabulary
open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open ARCTokenization
open Xunit

open TestUtils

let parsedInvestigationMetadataEmpty = Investigation.parseMetadataSheetFromFile "Fixtures/incorrect/investigation_empty.xlsx"
let parsedInvestigationMetadataSimple = Investigation.parseMetadataSheetFromFile "Fixtures/correct/investigation_simple.xlsx"

let allExpectedMetadataTermsEmpty = 
    // maybe we want to not rely on parsed obo? i think we can.
    //[
    //    CvParam(("INVMSO:00000002","ONTOLOGY SOURCE REFERENCE", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000003","Term Source Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000004","Term Source File", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000005","Term Source Version", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000006","Term Source Description", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000007","INVESTIGATION", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000008","Investigation Identifier", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000009","Investigation Title", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000010","Investigation Description", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000011","Investigation Submission Date", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000012","Investigation Public Release Date", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000013","INVESTIGATION PUBLICATIONS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000014","Investigation Publication PubMed ID", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000015","Investigation Publication DOI", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000016","Investigation Publication Author List", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000017","Investigation Publication Title", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000018","Investigation Publication Status", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000019","Investigation Publication Status Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000020","Investigation Publication Status Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000021","INVESTIGATION CONTACTS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000022","Investigation Person Last Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000023","Investigation Person First Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000024","Investigation Person Mid Initials", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000025","Investigation Person Email", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000026","Investigation Person Phone", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000027","Investigation Person Fax", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000028","Investigation Person Address", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000029","Investigation Person Affiliation", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000030","Investigation Person Roles", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000031","Investigation Person Roles Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000032","Investigation Person Roles Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000033","STUDY", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000034","Study Identifier", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000035","Study Title", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000036","Study Description", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000037","Study Submission Date", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000038","Study Public Release Date", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000039","Study File Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000040","STUDY DESIGN DESCRIPTORS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000041","Study Design Type", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000042","Study Design Type Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000043","Study Design Type Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000044","STUDY PUBLICATIONS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000045","Study Publication PubMed ID", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000046","Study Publication DOI", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000047","Study Publication Author List", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000048","Study Publication Title", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000049","Study Publication Status", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000050","Study Publication Status Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000051","Study Publication Status Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000052","STUDY FACTORS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000053","Study Factor Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000054","Study Factor Type", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000055","Study Factor Type Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000056","Study Factor Type Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000057","STUDY ASSAYS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000058","Study Assay Measurement Type", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000059","Study Assay Measurement Type Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000060","Study Assay Measurement Type Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000061","Study Assay Technology Type", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000062","Study Assay Technology Type Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000063","Study Assay Technology Type Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000064","Study Assay Technology Platform", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000065","Study Assay File Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000066","STUDY PROTOCOLS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000067","Study Protocol Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000068","Study Protocol Type", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000069","Study Protocol Type Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000070","Study Protocol Type Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000071","Study Protocol Description", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000072","Study Protocol URI", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000073","Study Protocol Version", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000074","Study Protocol Parameters Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000075","Study Protocol Parameters Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000076","Study Protocol Parameters Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000077","Study Protocol Components Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000078","Study Protocol Components Type", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000079","Study Protocol Components Type Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000080","Study Protocol Components Type Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000081","STUDY CONTACTS", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000082","Study Person Last Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000083","Study Person First Name", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000084","Study Person Mid Initials", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000085","Study Person Email", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000086","Study Person Phone", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000087","Study Person Fax", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000088","Study Person Address", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000089","Study Person Affiliation", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000090","Study Person Roles", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000091","Study Person Roles Term Accession Number", "INVMSO"), ParamValue.Value "", [])
    //    CvParam(("INVMSO:00000092","Study Person Roles Term Source REF", "INVMSO"), ParamValue.Value "", [])
    //]
    Terms.InvestigationMetadata.cvTerms
    |> List.skip 1 //(ignore root term)
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