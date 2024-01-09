module ReferenceObjects

open ControlledVocabulary
open ARCTokenization
open FsSpreadsheet

module Terms =

    module InvestigationMetadata =

        let referenceOntologyName = "INVMSO"

        let referenceOntologyRootTerm = CvTerm.create(accession = "INVMSO:00000001", name = "Investigation Metadata", ref = "INVMSO")

        let epectedNonObsoleteNonRootTerms = [
            CvTerm.create("INVMSO:00000002", "ONTOLOGY SOURCE REFERENCE", "INVMSO")
            CvTerm.create("INVMSO:00000003", "Term Source Name", "INVMSO")
            CvTerm.create("INVMSO:00000004", "Term Source File", "INVMSO")
            CvTerm.create("INVMSO:00000005", "Term Source Version", "INVMSO")
            CvTerm.create("INVMSO:00000006", "Term Source Description", "INVMSO")
            CvTerm.create("INVMSO:00000007", "INVESTIGATION", "INVMSO")
            CvTerm.create("INVMSO:00000008", "Investigation Identifier", "INVMSO")
            CvTerm.create("INVMSO:00000009", "Investigation Title", "INVMSO")
            CvTerm.create("INVMSO:00000010", "Investigation Description", "INVMSO")
            CvTerm.create("INVMSO:00000011", "Investigation Submission Date", "INVMSO")
            CvTerm.create("INVMSO:00000012", "Investigation Public Release Date", "INVMSO")
            CvTerm.create("INVMSO:00000013", "INVESTIGATION PUBLICATIONS", "INVMSO")
            CvTerm.create("INVMSO:00000014", "Investigation Publication PubMed ID", "INVMSO")
            CvTerm.create("INVMSO:00000015", "Investigation Publication DOI", "INVMSO")
            CvTerm.create("INVMSO:00000016", "Investigation Publication Author List", "INVMSO")
            CvTerm.create("INVMSO:00000017", "Investigation Publication Title", "INVMSO")
            CvTerm.create("INVMSO:00000018", "Investigation Publication Status", "INVMSO")
            CvTerm.create("INVMSO:00000019", "Investigation Publication Status Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000020", "Investigation Publication Status Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000021", "INVESTIGATION CONTACTS", "INVMSO")
            CvTerm.create("INVMSO:00000022", "Investigation Person Last Name", "INVMSO")
            CvTerm.create("INVMSO:00000023", "Investigation Person First Name", "INVMSO")
            CvTerm.create("INVMSO:00000024", "Investigation Person Mid Initials", "INVMSO")
            CvTerm.create("INVMSO:00000025", "Investigation Person Email", "INVMSO")
            CvTerm.create("INVMSO:00000026", "Investigation Person Phone", "INVMSO")
            CvTerm.create("INVMSO:00000027", "Investigation Person Fax", "INVMSO")
            CvTerm.create("INVMSO:00000028", "Investigation Person Address", "INVMSO")
            CvTerm.create("INVMSO:00000029", "Investigation Person Affiliation", "INVMSO")
            CvTerm.create("INVMSO:00000030", "Investigation Person Roles", "INVMSO")
            CvTerm.create("INVMSO:00000031", "Investigation Person Roles Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000032", "Investigation Person Roles Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000095", "Comment[ORCID]", "INVMSO")
            CvTerm.create("INVMSO:00000033", "STUDY", "INVMSO")
            CvTerm.create("INVMSO:00000034", "Study Identifier", "INVMSO")
            CvTerm.create("INVMSO:00000035", "Study Title", "INVMSO")
            CvTerm.create("INVMSO:00000036", "Study Description", "INVMSO")
            CvTerm.create("INVMSO:00000037", "Study Submission Date", "INVMSO")
            CvTerm.create("INVMSO:00000038", "Study Public Release Date", "INVMSO")
            CvTerm.create("INVMSO:00000039", "Study File Name", "INVMSO")
            CvTerm.create("INVMSO:00000040", "STUDY DESIGN DESCRIPTORS", "INVMSO")
            CvTerm.create("INVMSO:00000041", "Study Design Type", "INVMSO")
            CvTerm.create("INVMSO:00000042", "Study Design Type Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000043", "Study Design Type Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000044", "STUDY PUBLICATIONS", "INVMSO")
            CvTerm.create("INVMSO:00000045", "Study Publication PubMed ID", "INVMSO")
            CvTerm.create("INVMSO:00000046", "Study Publication DOI", "INVMSO")
            CvTerm.create("INVMSO:00000047", "Study Publication Author List", "INVMSO")
            CvTerm.create("INVMSO:00000048", "Study Publication Title", "INVMSO")
            CvTerm.create("INVMSO:00000049", "Study Publication Status", "INVMSO")
            CvTerm.create("INVMSO:00000050", "Study Publication Status Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000051", "Study Publication Status Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000052", "STUDY FACTORS", "INVMSO")
            CvTerm.create("INVMSO:00000053", "Study Factor Name", "INVMSO")
            CvTerm.create("INVMSO:00000054", "Study Factor Type", "INVMSO")
            CvTerm.create("INVMSO:00000055", "Study Factor Type Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000056", "Study Factor Type Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000057", "STUDY ASSAYS", "INVMSO")
            CvTerm.create("INVMSO:00000058", "Study Assay Measurement Type", "INVMSO")
            CvTerm.create("INVMSO:00000059", "Study Assay Measurement Type Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000060", "Study Assay Measurement Type Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000061", "Study Assay Technology Type", "INVMSO")
            CvTerm.create("INVMSO:00000062", "Study Assay Technology Type Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000063", "Study Assay Technology Type Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000064", "Study Assay Technology Platform", "INVMSO")
            CvTerm.create("INVMSO:00000065", "Study Assay File Name", "INVMSO")
            CvTerm.create("INVMSO:00000066", "STUDY PROTOCOLS", "INVMSO")
            CvTerm.create("INVMSO:00000067", "Study Protocol Name", "INVMSO")
            CvTerm.create("INVMSO:00000068", "Study Protocol Type", "INVMSO")
            CvTerm.create("INVMSO:00000069", "Study Protocol Type Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000070", "Study Protocol Type Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000071", "Study Protocol Description", "INVMSO")
            CvTerm.create("INVMSO:00000072", "Study Protocol URI", "INVMSO")
            CvTerm.create("INVMSO:00000073", "Study Protocol Version", "INVMSO")
            CvTerm.create("INVMSO:00000074", "Study Protocol Parameters Name", "INVMSO")
            CvTerm.create("INVMSO:00000075", "Study Protocol Parameters Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000076", "Study Protocol Parameters Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000077", "Study Protocol Components Name", "INVMSO")
            CvTerm.create("INVMSO:00000078", "Study Protocol Components Type", "INVMSO")
            CvTerm.create("INVMSO:00000079", "Study Protocol Components Type Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000080", "Study Protocol Components Type Term Source REF", "INVMSO")
            CvTerm.create("INVMSO:00000081", "STUDY CONTACTS", "INVMSO")
            CvTerm.create("INVMSO:00000082", "Study Person Last Name", "INVMSO")
            CvTerm.create("INVMSO:00000083", "Study Person First Name", "INVMSO")
            CvTerm.create("INVMSO:00000084", "Study Person Mid Initials", "INVMSO")
            CvTerm.create("INVMSO:00000085", "Study Person Email", "INVMSO")
            CvTerm.create("INVMSO:00000086", "Study Person Phone", "INVMSO")
            CvTerm.create("INVMSO:00000087", "Study Person Fax", "INVMSO")
            CvTerm.create("INVMSO:00000088", "Study Person Address", "INVMSO")
            CvTerm.create("INVMSO:00000089", "Study Person Affiliation", "INVMSO")
            CvTerm.create("INVMSO:00000090", "Study Person Roles", "INVMSO")
            CvTerm.create("INVMSO:00000091", "Study Person Roles Term Accession Number", "INVMSO")
            CvTerm.create("INVMSO:00000092", "Study Person Roles Term Source REF", "INVMSO")
        ]
    
    module StudyMetadata = 

        let referenceOntologyName = "STDMSO"

        let referenceOntologyRootTerm = CvTerm.create(accession = "STDMSO:00000001", name = "Study Metadata", ref = "STDMSO")
    
        let epectedNonObsoleteNonRootTerms = [
            CvTerm.create("STDMSO:00000002", "STUDY", "STDMSO")
            CvTerm.create("STDMSO:00000003", "Study Identifier", "STDMSO")
            CvTerm.create("STDMSO:00000004", "Study Title", "STDMSO")
            CvTerm.create("STDMSO:00000005", "Study Description", "STDMSO")
            CvTerm.create("STDMSO:00000006", "Study Submission Date", "STDMSO")
            CvTerm.create("STDMSO:00000007", "Study Public Release Date", "STDMSO")
            CvTerm.create("STDMSO:00000008", "Study File Name", "STDMSO")
            CvTerm.create("STDMSO:00000009", "STUDY DESIGN DESCRIPTORS", "STDMSO")
            CvTerm.create("STDMSO:00000010", "Study Design Type", "STDMSO")
            CvTerm.create("STDMSO:00000011", "Study Design Type Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000012", "Study Design Type Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000013", "STUDY PUBLICATIONS", "STDMSO")
            CvTerm.create("STDMSO:00000014", "Study Publication PubMed ID", "STDMSO")
            CvTerm.create("STDMSO:00000015", "Study Publication DOI", "STDMSO")
            CvTerm.create("STDMSO:00000016", "Study Publication Author List", "STDMSO")
            CvTerm.create("STDMSO:00000017", "Study Publication Title", "STDMSO")
            CvTerm.create("STDMSO:00000018", "Study Publication Status", "STDMSO")
            CvTerm.create("STDMSO:00000019", "Study Publication Status Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000020", "Study Publication Status Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000021", "STUDY FACTORS", "STDMSO")
            CvTerm.create("STDMSO:00000022", "Study Factor Name", "STDMSO")
            CvTerm.create("STDMSO:00000023", "Study Factor Type", "STDMSO")
            CvTerm.create("STDMSO:00000024", "Study Factor Type Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000025", "Study Factor Type Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000026", "STUDY ASSAYS", "STDMSO")
            CvTerm.create("STDMSO:00000027", "Study Assay Measurement Type", "STDMSO")
            CvTerm.create("STDMSO:00000028", "Study Assay Measurement Type Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000029", "Study Assay Measurement Type Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000030", "Study Assay Technology Type", "STDMSO")
            CvTerm.create("STDMSO:00000031", "Study Assay Technology Type Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000032", "Study Assay Technology Type Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000033", "Study Assay Technology Platform", "STDMSO")
            CvTerm.create("STDMSO:00000034", "Study Assay File Name", "STDMSO")
            CvTerm.create("STDMSO:00000035", "STUDY PROTOCOLS", "STDMSO")
            CvTerm.create("STDMSO:00000036", "Study Protocol Name", "STDMSO")
            CvTerm.create("STDMSO:00000037", "Study Protocol Type", "STDMSO")
            CvTerm.create("STDMSO:00000038", "Study Protocol Type Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000039", "Study Protocol Type Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000040", "Study Protocol Description", "STDMSO")
            CvTerm.create("STDMSO:00000041", "Study Protocol URI", "STDMSO")
            CvTerm.create("STDMSO:00000042", "Study Protocol Version", "STDMSO")
            CvTerm.create("STDMSO:00000043", "Study Protocol Parameters Name", "STDMSO")
            CvTerm.create("STDMSO:00000044", "Study Protocol Parameters Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000045", "Study Protocol Parameters Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000046", "Study Protocol Components Name", "STDMSO")
            CvTerm.create("STDMSO:00000047", "Study Protocol Components Type", "STDMSO")
            CvTerm.create("STDMSO:00000048", "Study Protocol Components Type Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000049", "Study Protocol Components Type Term Source REF", "STDMSO")
            CvTerm.create("STDMSO:00000050", "STUDY CONTACTS", "STDMSO")
            CvTerm.create("STDMSO:00000051", "Study Person Last Name", "STDMSO")
            CvTerm.create("STDMSO:00000052", "Study Person First Name", "STDMSO")
            CvTerm.create("STDMSO:00000053", "Study Person Mid Initials", "STDMSO")
            CvTerm.create("STDMSO:00000054", "Study Person Email", "STDMSO")
            CvTerm.create("STDMSO:00000055", "Study Person Phone", "STDMSO")
            CvTerm.create("STDMSO:00000056", "Study Person Fax", "STDMSO")
            CvTerm.create("STDMSO:00000057", "Study Person Address", "STDMSO")
            CvTerm.create("STDMSO:00000058", "Study Person Affiliation", "STDMSO")
            CvTerm.create("STDMSO:00000059", "Study Person Roles", "STDMSO")
            CvTerm.create("STDMSO:00000060", "Study Person Roles Term Accession Number", "STDMSO")
            CvTerm.create("STDMSO:00000061", "Study Person Roles Term Source REF", "STDMSO")
        ]
    

    module AssayMetadata = 

        let referenceOntologyName = "ASSMSO"
        
        let referenceOntologyRootTerm = CvTerm.create(accession = "ASSMSO:00000001", name = "Assay Metadata", ref = "ASSMSO")
        
        let epectedNonObsoleteNonRootTerms = [
            CvTerm.create("ASSMSO:00000002", "ASSAY", "ASSMSO")
            CvTerm.create("ASSMSO:00000004", "Assay Measurement Type", "ASSMSO")
            CvTerm.create("ASSMSO:00000006", "Assay Measurement Type Term Accession Number", "ASSMSO")
            CvTerm.create("ASSMSO:00000008", "Assay Measurement Type Term Source REF", "ASSMSO")
            CvTerm.create("ASSMSO:00000011", "Assay Technology Type", "ASSMSO")
            CvTerm.create("ASSMSO:00000013", "Assay Technology Type Term Accession Number", "ASSMSO")
            CvTerm.create("ASSMSO:00000015", "Assay Technology Type Term Source REF", "ASSMSO")
            CvTerm.create("ASSMSO:00000017", "Assay Technology Platform", "ASSMSO")
            CvTerm.create("ASSMSO:00000019", "Assay File Name", "ASSMSO")
            CvTerm.create("ASSMSO:00000020", "ASSAY PERFORMERS", "ASSMSO")
            CvTerm.create("ASSMSO:00000021", "Assay Performer Last Name", "ASSMSO")
            CvTerm.create("ASSMSO:00000023", "Assay Performer First Name", "ASSMSO")
            CvTerm.create("ASSMSO:00000025", "Assay Performer Mid Initials", "ASSMSO")
            CvTerm.create("ASSMSO:00000027", "Assay Performer Email", "ASSMSO")
            CvTerm.create("ASSMSO:00000029", "Assay Performer Phone", "ASSMSO")
            CvTerm.create("ASSMSO:00000031", "Assay Performer Fax", "ASSMSO")
            CvTerm.create("ASSMSO:00000033", "Assay Performer Address", "ASSMSO")
            CvTerm.create("ASSMSO:00000035", "Assay Performer Affiliation", "ASSMSO")
            CvTerm.create("ASSMSO:00000037", "Assay Performer Roles", "ASSMSO")
            CvTerm.create("ASSMSO:00000039", "Assay Performer Roles Term Accession Number", "ASSMSO")
            CvTerm.create("ASSMSO:00000041", "Assay Performer Roles Term Source REF", "ASSMSO")

        ]

    module ProcessGraph =
        
        let referenceOntologyName = "APGSO"

        let expectedNonObsoleteNonRootTerms = [
            CvTerm.create(accession = "APGSO:00000002", name = "Characteristic", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000003", name = "Factor", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000004", name = "Parameter", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000005", name = "Component", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000006", name = "ProtocolType", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000007", name = "ProtocolDescription", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000008", name = "ProtocolUri", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000009", name = "ProtocolVersion", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000010", name = "ProtocolREF", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000011", name = "Performer", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000012", name = "Date", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000013", name = "Input", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000014", name = "Output", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000016", name = "Source", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000017", name = "Sample", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000018", name = "RawDataFile", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000019", name = "DerivedDataFile", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000020", name = "ImageFile", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000021", name = "Material", ref = "APGSO")
            CvTerm.create(accession = "APGSO:00000022", name = "FreeText", ref = "APGSO")
        ]



module Tokenization =
    
    module KeyParser = 

        let referenceKeys = [
            "1";"2";"3"
        ]

        let referenceTerms = [
            CvTerm.create(accession = "1", name = "1", ref = "1")
            CvTerm.create(accession = "2", name = "2", ref = "2")
            CvTerm.create(accession = "3", name = "3", ref = "3")
        ]

        let referenceParamValues = [
            ParamValue.Value 1
            ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2"))
            ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3"))
        ]

        let referenceCvParams = [
            CvParam(CvTerm.create(accession = "1", name = "1", ref = "1"), ParamValue.Value 1)
            CvParam(CvTerm.create(accession = "2", name = "2", ref = "2"), ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
            CvParam(CvTerm.create(accession = "3", name = "3", ref = "3"), ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
        ]

        let referenceCommentKeys = [
            "Comment[1]"
            "Comment[<2>]"
            "Comment[<3 even has a newline. 
            >]"
        ]

        let referenceCommentCvParams =
            [
                CvParam(Terms.StructuralTerms.userComment, ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.userComment, ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
                CvParam(Terms.StructuralTerms.userComment, ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
            ]

        let referenceIgnoreLineKeys = [
            "#Oh jeez"
            "#  i hope they"
            "#ignore
            this"
        ]

        let referenceIgnoreLineCvParams =
            [
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
            ]

        let referenceUserParamKeys =
            [
                "hahaha"
                "this follows no structure"
                "fk you 
                lmaoooo"
            ]

        let referenceUserParams =
            [
                UserParam("hahaha", ParamValue.Value 1)
                UserParam("this follows no structure", ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
                UserParam("fk you 
                lmaoooo", ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
            ]

        let referenceMixedKeys = 
            [
                "1"
                "Comment[<2>]"
                "#Oh jeez"
                "fk you 
                lmaoooo"
            ]

        let referenceMixedParamValues = [
            for i in 0 .. 3 -> ParamValue.Value 1
        ]

        let referenceMixedParams: IParam list =
            [
                CvParam(CvTerm.create(accession = "1", name = "1", ref = "1"), ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.userComment, ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value 1)
                UserParam("fk you 
                lmaoooo", ParamValue.Value 1)
            ]

    module ConvertMetadataTokens =
        
        let referenceTerms = [
            CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1")
            CvTerm.create(accession = "2", name = "ReferenceTerm2", ref = "2")
            CvTerm.create(accession = "3", name = "ReferenceTerm3", ref = "3")
        ]

        let referenceRow = [
            FsCell("ReferenceTerm1", address = FsAddress(rowNumber = 0, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 0, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 0, columnNumber = 2))
        ]

        let referenceCvParams = [
            CvParam(CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1"), ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            CvParam(CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1"), ParamValue.Value "some value")
            CvParam(CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1"), ParamValue.Value "another value")
        ]

        let referenceCommentRow = [
            FsCell("Comment[xD]", address = FsAddress(rowNumber = 1, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 1, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 1, columnNumber = 2))
        ]

        let referenceCommentCvParams = [
            CvParam(Terms.StructuralTerms.userComment, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            CvParam(Terms.StructuralTerms.userComment, ParamValue.Value "some value")
            CvParam(Terms.StructuralTerms.userComment, ParamValue.Value "another value")
        ]

        let referenceIgnoreLineRow = [
            FsCell("# why am i even here?", address = FsAddress(rowNumber = 0, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 0, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 0, columnNumber = 2))
        ]

        let referenceIgnoreLineCvParams = [
            CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value "some value")
            CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value "another value")
        ]

        let referenceUserParamRow = [
            FsCell("fk u lmaooooo", address = FsAddress(rowNumber = 0, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 0, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 0, columnNumber = 2))
        ]

        let referenceUserParams = [
            UserParam("fk u lmaooooo", ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            UserParam("fk u lmaooooo", ParamValue.Value "some value")
            UserParam("fk u lmaooooo", ParamValue.Value "another value")
        ]

    module FileSystem =
        
        let referenceRelativeDirectoryPaths = 
            [
                @"1"
                @"2"
                @"1/1_1"
                @"2/2_1"
                @"2/2_2"
                @"2/2_2/2_2_1"
            ]
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000010","Directory Path","AFSO"),
                    v = v
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

        let referenceAbsoluteDirectoryPaths(root) =
            [
                @"1"
                @"2"
                @"1/1_1"
                @"2/2_1"
                @"2/2_2"
                @"2/2_2/2_2_1"
            ]
            |> List.map (fun f -> System.IO.Path.Combine(root, f))
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000010","Directory Path","AFSO"),
                    v = v.Replace("\\", "/")
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

        let referenceRelativeFilePaths = 
            [
                @"1/1_1/.gitkeep"
                @"2/2_1/.gitkeep"
                @"2/2_2/2_2_1/.gitkeep"
            ]
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000009","File Path","AFSO"),
                    v = v
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

        let referenceAbsoluteFilePaths(root) =
            [
                @"1/1_1/.gitkeep"
                @"2/2_1/.gitkeep"
                @"2/2_2/2_2_1/.gitkeep"
            ]
            |> List.map (fun f -> System.IO.Path.Combine(root, f))
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000009","File Path","AFSO"),
                    v = v.Replace("\\", "/")
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

module MockAPI =
    
    module InvestigationMetadataTokens = 

        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty =
            Terms.InvestigationMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
            |> List.map (fun cvTerm -> CvParam(cvTerm, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, []))

    module StudyMetadataTokens = 

        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty =
            Terms.StudyMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> not (t.Name.StartsWith("Comment")) ) // ignore all comments
            |> List.map (fun cvTerm -> CvParam(cvTerm, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, []))

    module AssayMetadataTokens = 

        // equivalent to a metadatasheet with only the first column that contains metadata section keys
        let empty =
            Terms.AssayMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> not (t.Name.StartsWith("Comment")) ) // ignore all comments
            |> List.map (fun cvTerm -> CvParam(cvTerm, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, []))