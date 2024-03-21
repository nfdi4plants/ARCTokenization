namespace ARCTokenization.StructuralOntology

    open ControlledVocabulary

    module INVMSO =

        let Investigation_Metadata = CvTerm.create("INVMSO:00000001", "Investigation Metadata", "INVMSO")

        let ONTOLOGY_SOURCE_REFERENCE = CvTerm.create("INVMSO:00000002", "ONTOLOGY SOURCE REFERENCE", "INVMSO")

        let Term_Source_Name = CvTerm.create("INVMSO:00000003", "Term Source Name", "INVMSO")

        let Term_Source_File = CvTerm.create("INVMSO:00000004", "Term Source File", "INVMSO")

        let Term_Source_Version = CvTerm.create("INVMSO:00000005", "Term Source Version", "INVMSO")

        let Term_Source_Description = CvTerm.create("INVMSO:00000006", "Term Source Description", "INVMSO")

        let INVESTIGATION = CvTerm.create("INVMSO:00000007", "INVESTIGATION", "INVMSO")

        let Investigation_Identifier = CvTerm.create("INVMSO:00000008", "Investigation Identifier", "INVMSO")

        let Investigation_Title = CvTerm.create("INVMSO:00000009", "Investigation Title", "INVMSO")

        let Investigation_Description = CvTerm.create("INVMSO:00000010", "Investigation Description", "INVMSO")

        let Investigation_Submission_Date = CvTerm.create("INVMSO:00000011", "Investigation Submission Date", "INVMSO")

        let Investigation_Public_Release_Date = CvTerm.create("INVMSO:00000012", "Investigation Public Release Date", "INVMSO")

        let INVESTIGATION_PUBLICATIONS = CvTerm.create("INVMSO:00000013", "INVESTIGATION PUBLICATIONS", "INVMSO")

        let Investigation_Publication_PubMed_ID = CvTerm.create("INVMSO:00000014", "Investigation Publication PubMed ID", "INVMSO")

        let Investigation_Publication_DOI = CvTerm.create("INVMSO:00000015", "Investigation Publication DOI", "INVMSO")

        let Investigation_Publication_Author_List = CvTerm.create("INVMSO:00000016", "Investigation Publication Author List", "INVMSO")

        let Investigation_Publication_Title = CvTerm.create("INVMSO:00000017", "Investigation Publication Title", "INVMSO")

        let Investigation_Publication_Status = CvTerm.create("INVMSO:00000018", "Investigation Publication Status", "INVMSO")

        let Investigation_Publication_Status_Term_Accession_Number = CvTerm.create("INVMSO:00000019", "Investigation Publication Status Term Accession Number", "INVMSO")

        let Investigation_Publication_Status_Term_Source_REF = CvTerm.create("INVMSO:00000020", "Investigation Publication Status Term Source REF", "INVMSO")

        let INVESTIGATION_CONTACTS = CvTerm.create("INVMSO:00000021", "INVESTIGATION CONTACTS", "INVMSO")

        let Investigation_Person_Last_Name = CvTerm.create("INVMSO:00000022", "Investigation Person Last Name", "INVMSO")

        let Investigation_Person_First_Name = CvTerm.create("INVMSO:00000023", "Investigation Person First Name", "INVMSO")

        let Investigation_Person_Mid_Initials = CvTerm.create("INVMSO:00000024", "Investigation Person Mid Initials", "INVMSO")

        let Investigation_Person_Email = CvTerm.create("INVMSO:00000025", "Investigation Person Email", "INVMSO")

        let Investigation_Person_Phone = CvTerm.create("INVMSO:00000026", "Investigation Person Phone", "INVMSO")

        let Investigation_Person_Fax = CvTerm.create("INVMSO:00000027", "Investigation Person Fax", "INVMSO")

        let Investigation_Person_Address = CvTerm.create("INVMSO:00000028", "Investigation Person Address", "INVMSO")

        let Investigation_Person_Affiliation = CvTerm.create("INVMSO:00000029", "Investigation Person Affiliation", "INVMSO")

        let Investigation_Person_Roles = CvTerm.create("INVMSO:00000030", "Investigation Person Roles", "INVMSO")

        let Investigation_Person_Roles_Term_Accession_Number = CvTerm.create("INVMSO:00000031", "Investigation Person Roles Term Accession Number", "INVMSO")

        let Investigation_Person_Roles_Term_Source_REF = CvTerm.create("INVMSO:00000032", "Investigation Person Roles Term Source REF", "INVMSO")

        let ``Comment[<Investigation_Person_ORCID>]`` = CvTerm.create("INVMSO:00000093", "Comment[<Investigation Person ORCID>]", "INVMSO")

        let ``Comment[Investigation_Person_ORCID]`` = CvTerm.create("INVMSO:00000094", "Comment[Investigation Person ORCID]", "INVMSO")

        let ``Comment[ORCID]`` = CvTerm.create("INVMSO:00000095", "Comment[ORCID]", "INVMSO")

        let STUDY = CvTerm.create("INVMSO:00000033", "STUDY", "INVMSO")

        let Study_Identifier = CvTerm.create("INVMSO:00000034", "Study Identifier", "INVMSO")

        let Study_Title = CvTerm.create("INVMSO:00000035", "Study Title", "INVMSO")

        let Study_Description = CvTerm.create("INVMSO:00000036", "Study Description", "INVMSO")

        let Study_Submission_Date = CvTerm.create("INVMSO:00000037", "Study Submission Date", "INVMSO")

        let Study_Public_Release_Date = CvTerm.create("INVMSO:00000038", "Study Public Release Date", "INVMSO")

        let Study_File_Name = CvTerm.create("INVMSO:00000039", "Study File Name", "INVMSO")

        let STUDY_DESIGN_DESCRIPTORS = CvTerm.create("INVMSO:00000040", "STUDY DESIGN DESCRIPTORS", "INVMSO")

        let Study_Design_Type = CvTerm.create("INVMSO:00000041", "Study Design Type", "INVMSO")

        let Study_Design_Type_Term_Accession_Number = CvTerm.create("INVMSO:00000042", "Study Design Type Term Accession Number", "INVMSO")

        let Study_Design_Type_Term_Source_REF = CvTerm.create("INVMSO:00000043", "Study Design Type Term Source REF", "INVMSO")

        let STUDY_PUBLICATIONS = CvTerm.create("INVMSO:00000044", "STUDY PUBLICATIONS", "INVMSO")

        let Study_Publication_PubMed_ID = CvTerm.create("INVMSO:00000045", "Study Publication PubMed ID", "INVMSO")

        let Study_Publication_DOI = CvTerm.create("INVMSO:00000046", "Study Publication DOI", "INVMSO")

        let Study_Publication_Author_List = CvTerm.create("INVMSO:00000047", "Study Publication Author List", "INVMSO")

        let Study_Publication_Title = CvTerm.create("INVMSO:00000048", "Study Publication Title", "INVMSO")

        let Study_Publication_Status = CvTerm.create("INVMSO:00000049", "Study Publication Status", "INVMSO")

        let Study_Publication_Status_Term_Accession_Number = CvTerm.create("INVMSO:00000050", "Study Publication Status Term Accession Number", "INVMSO")

        let Study_Publication_Status_Term_Source_REF = CvTerm.create("INVMSO:00000051", "Study Publication Status Term Source REF", "INVMSO")

        let STUDY_FACTORS = CvTerm.create("INVMSO:00000052", "STUDY FACTORS", "INVMSO")

        let Study_Factor_Name = CvTerm.create("INVMSO:00000053", "Study Factor Name", "INVMSO")

        let Study_Factor_Type = CvTerm.create("INVMSO:00000054", "Study Factor Type", "INVMSO")

        let Study_Factor_Type_Term_Accession_Number = CvTerm.create("INVMSO:00000055", "Study Factor Type Term Accession Number", "INVMSO")

        let Study_Factor_Type_Term_Source_REF = CvTerm.create("INVMSO:00000056", "Study Factor Type Term Source REF", "INVMSO")

        let STUDY_ASSAYS = CvTerm.create("INVMSO:00000057", "STUDY ASSAYS", "INVMSO")

        let Study_Assay_Measurement_Type = CvTerm.create("INVMSO:00000058", "Study Assay Measurement Type", "INVMSO")

        let Study_Assay_Measurement_Type_Term_Accession_Number = CvTerm.create("INVMSO:00000059", "Study Assay Measurement Type Term Accession Number", "INVMSO")

        let Study_Assay_Measurement_Type_Term_Source_REF = CvTerm.create("INVMSO:00000060", "Study Assay Measurement Type Term Source REF", "INVMSO")

        let Study_Assay_Technology_Type = CvTerm.create("INVMSO:00000061", "Study Assay Technology Type", "INVMSO")

        let Study_Assay_Technology_Type_Term_Accession_Number = CvTerm.create("INVMSO:00000062", "Study Assay Technology Type Term Accession Number", "INVMSO")

        let Study_Assay_Technology_Type_Term_Source_REF = CvTerm.create("INVMSO:00000063", "Study Assay Technology Type Term Source REF", "INVMSO")

        let Study_Assay_Technology_Platform = CvTerm.create("INVMSO:00000064", "Study Assay Technology Platform", "INVMSO")

        let Study_Assay_File_Name = CvTerm.create("INVMSO:00000065", "Study Assay File Name", "INVMSO")

        let STUDY_PROTOCOLS = CvTerm.create("INVMSO:00000066", "STUDY PROTOCOLS", "INVMSO")

        let Study_Protocol_Name = CvTerm.create("INVMSO:00000067", "Study Protocol Name", "INVMSO")

        let Study_Protocol_Type = CvTerm.create("INVMSO:00000068", "Study Protocol Type", "INVMSO")

        let Study_Protocol_Type_Term_Accession_Number = CvTerm.create("INVMSO:00000069", "Study Protocol Type Term Accession Number", "INVMSO")

        let Study_Protocol_Type_Term_Source_REF = CvTerm.create("INVMSO:00000070", "Study Protocol Type Term Source REF", "INVMSO")

        let Study_Protocol_Description = CvTerm.create("INVMSO:00000071", "Study Protocol Description", "INVMSO")

        let Study_Protocol_URI = CvTerm.create("INVMSO:00000072", "Study Protocol URI", "INVMSO")

        let Study_Protocol_Version = CvTerm.create("INVMSO:00000073", "Study Protocol Version", "INVMSO")

        let Study_Protocol_Parameters_Name = CvTerm.create("INVMSO:00000074", "Study Protocol Parameters Name", "INVMSO")

        let Study_Protocol_Parameters_Term_Accession_Number = CvTerm.create("INVMSO:00000075", "Study Protocol Parameters Term Accession Number", "INVMSO")

        let Study_Protocol_Parameters_Term_Source_REF = CvTerm.create("INVMSO:00000076", "Study Protocol Parameters Term Source REF", "INVMSO")

        let Study_Protocol_Components_Name = CvTerm.create("INVMSO:00000077", "Study Protocol Components Name", "INVMSO")

        let Study_Protocol_Components_Type = CvTerm.create("INVMSO:00000078", "Study Protocol Components Type", "INVMSO")

        let Study_Protocol_Components_Type_Term_Accession_Number = CvTerm.create("INVMSO:00000079", "Study Protocol Components Type Term Accession Number", "INVMSO")

        let Study_Protocol_Components_Type_Term_Source_REF = CvTerm.create("INVMSO:00000080", "Study Protocol Components Type Term Source REF", "INVMSO")

        let STUDY_CONTACTS = CvTerm.create("INVMSO:00000081", "STUDY CONTACTS", "INVMSO")

        let Study_Person_Last_Name = CvTerm.create("INVMSO:00000082", "Study Person Last Name", "INVMSO")

        let Study_Person_First_Name = CvTerm.create("INVMSO:00000083", "Study Person First Name", "INVMSO")

        let Study_Person_Mid_Initials = CvTerm.create("INVMSO:00000084", "Study Person Mid Initials", "INVMSO")

        let Study_Person_Email = CvTerm.create("INVMSO:00000085", "Study Person Email", "INVMSO")

        let Study_Person_Phone = CvTerm.create("INVMSO:00000086", "Study Person Phone", "INVMSO")

        let Study_Person_Fax = CvTerm.create("INVMSO:00000087", "Study Person Fax", "INVMSO")

        let Study_Person_Address = CvTerm.create("INVMSO:00000088", "Study Person Address", "INVMSO")

        let Study_Person_Affiliation = CvTerm.create("INVMSO:00000089", "Study Person Affiliation", "INVMSO")

        let Study_Person_Roles = CvTerm.create("INVMSO:00000090", "Study Person Roles", "INVMSO")

        let Study_Person_Roles_Term_Accession_Number = CvTerm.create("INVMSO:00000091", "Study Person Roles Term Accession Number", "INVMSO")

        let Study_Person_Roles_Term_Source_REF = CvTerm.create("INVMSO:00000092", "Study Person Roles Term Source REF", "INVMSO")

