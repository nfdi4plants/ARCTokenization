namespace ARCTokenization.StructuralOntology

    open ControlledVocabulary

    module STDMSO =

        let Study_Metadata = CvTerm.create("STDMSO:00000001", "Study Metadata", "STDMSO")

        let STUDY = CvTerm.create("STDMSO:00000002", "STUDY", "STDMSO")

        let STUDY_METADATA = CvTerm.create("STDMSO:00000062", "STUDY METADATA", "STDMSO")

        let Study_Identifier = CvTerm.create("STDMSO:00000003", "Study Identifier", "STDMSO")

        let Study_Title = CvTerm.create("STDMSO:00000004", "Study Title", "STDMSO")

        let Study_Description = CvTerm.create("STDMSO:00000005", "Study Description", "STDMSO")

        let Study_Submission_Date = CvTerm.create("STDMSO:00000006", "Study Submission Date", "STDMSO")

        let Study_Public_Release_Date = CvTerm.create("STDMSO:00000007", "Study Public Release Date", "STDMSO")

        let Study_File_Name = CvTerm.create("STDMSO:00000008", "Study File Name", "STDMSO")

        let STUDY_DESIGN_DESCRIPTORS = CvTerm.create("STDMSO:00000009", "STUDY DESIGN DESCRIPTORS", "STDMSO")

        let Study_Design_Type = CvTerm.create("STDMSO:00000010", "Study Design Type", "STDMSO")

        let Study_Design_Type_Term_Accession_Number = CvTerm.create("STDMSO:00000011", "Study Design Type Term Accession Number", "STDMSO")

        let Study_Design_Type_Term_Source_REF = CvTerm.create("STDMSO:00000012", "Study Design Type Term Source REF", "STDMSO")

        let STUDY_PUBLICATIONS = CvTerm.create("STDMSO:00000013", "STUDY PUBLICATIONS", "STDMSO")

        let Study_Publication_PubMed_ID = CvTerm.create("STDMSO:00000014", "Study Publication PubMed ID", "STDMSO")

        let Study_Publication_DOI = CvTerm.create("STDMSO:00000015", "Study Publication DOI", "STDMSO")

        let Study_Publication_Author_List = CvTerm.create("STDMSO:00000016", "Study Publication Author List", "STDMSO")

        let Study_Publication_Title = CvTerm.create("STDMSO:00000017", "Study Publication Title", "STDMSO")

        let Study_Publication_Status = CvTerm.create("STDMSO:00000018", "Study Publication Status", "STDMSO")

        let Study_Publication_Status_Term_Accession_Number = CvTerm.create("STDMSO:00000019", "Study Publication Status Term Accession Number", "STDMSO")

        let Study_Publication_Status_Term_Source_REF = CvTerm.create("STDMSO:00000020", "Study Publication Status Term Source REF", "STDMSO")

        let STUDY_FACTORS = CvTerm.create("STDMSO:00000021", "STUDY FACTORS", "STDMSO")

        let Study_Factor_Name = CvTerm.create("STDMSO:00000022", "Study Factor Name", "STDMSO")

        let Study_Factor_Type = CvTerm.create("STDMSO:00000023", "Study Factor Type", "STDMSO")

        let Study_Factor_Type_Term_Accession_Number = CvTerm.create("STDMSO:00000024", "Study Factor Type Term Accession Number", "STDMSO")

        let Study_Factor_Type_Term_Source_REF = CvTerm.create("STDMSO:00000025", "Study Factor Type Term Source REF", "STDMSO")

        let STUDY_ASSAYS = CvTerm.create("STDMSO:00000026", "STUDY ASSAYS", "STDMSO")

        let Study_Assay_Measurement_Type = CvTerm.create("STDMSO:00000027", "Study Assay Measurement Type", "STDMSO")

        let Study_Assay_Measurement_Type_Term_Accession_Number = CvTerm.create("STDMSO:00000028", "Study Assay Measurement Type Term Accession Number", "STDMSO")

        let Study_Assay_Measurement_Type_Term_Source_REF = CvTerm.create("STDMSO:00000029", "Study Assay Measurement Type Term Source REF", "STDMSO")

        let Study_Assay_Technology_Type = CvTerm.create("STDMSO:00000030", "Study Assay Technology Type", "STDMSO")

        let Study_Assay_Technology_Type_Term_Accession_Number = CvTerm.create("STDMSO:00000031", "Study Assay Technology Type Term Accession Number", "STDMSO")

        let Study_Assay_Technology_Type_Term_Source_REF = CvTerm.create("STDMSO:00000032", "Study Assay Technology Type Term Source REF", "STDMSO")

        let Study_Assay_Technology_Platform = CvTerm.create("STDMSO:00000033", "Study Assay Technology Platform", "STDMSO")

        let Study_Assay_File_Name = CvTerm.create("STDMSO:00000034", "Study Assay File Name", "STDMSO")

        let STUDY_PROTOCOLS = CvTerm.create("STDMSO:00000035", "STUDY PROTOCOLS", "STDMSO")

        let Study_Protocol_Name = CvTerm.create("STDMSO:00000036", "Study Protocol Name", "STDMSO")

        let Study_Protocol_Type = CvTerm.create("STDMSO:00000037", "Study Protocol Type", "STDMSO")

        let Study_Protocol_Type_Term_Accession_Number = CvTerm.create("STDMSO:00000038", "Study Protocol Type Term Accession Number", "STDMSO")

        let Study_Protocol_Type_Term_Source_REF = CvTerm.create("STDMSO:00000039", "Study Protocol Type Term Source REF", "STDMSO")

        let Study_Protocol_Description = CvTerm.create("STDMSO:00000040", "Study Protocol Description", "STDMSO")

        let Study_Protocol_URI = CvTerm.create("STDMSO:00000041", "Study Protocol URI", "STDMSO")

        let Study_Protocol_Version = CvTerm.create("STDMSO:00000042", "Study Protocol Version", "STDMSO")

        let Study_Protocol_Parameters_Name = CvTerm.create("STDMSO:00000043", "Study Protocol Parameters Name", "STDMSO")

        let Study_Protocol_Parameters_Term_Accession_Number = CvTerm.create("STDMSO:00000044", "Study Protocol Parameters Term Accession Number", "STDMSO")

        let Study_Protocol_Parameters_Term_Source_REF = CvTerm.create("STDMSO:00000045", "Study Protocol Parameters Term Source REF", "STDMSO")

        let Study_Protocol_Components_Name = CvTerm.create("STDMSO:00000046", "Study Protocol Components Name", "STDMSO")

        let Study_Protocol_Components_Type = CvTerm.create("STDMSO:00000047", "Study Protocol Components Type", "STDMSO")

        let Study_Protocol_Components_Type_Term_Accession_Number = CvTerm.create("STDMSO:00000048", "Study Protocol Components Type Term Accession Number", "STDMSO")

        let Study_Protocol_Components_Type_Term_Source_REF = CvTerm.create("STDMSO:00000049", "Study Protocol Components Type Term Source REF", "STDMSO")

        let STUDY_CONTACTS = CvTerm.create("STDMSO:00000050", "STUDY CONTACTS", "STDMSO")

        let Study_Person_Last_Name = CvTerm.create("STDMSO:00000051", "Study Person Last Name", "STDMSO")

        let Study_Person_First_Name = CvTerm.create("STDMSO:00000052", "Study Person First Name", "STDMSO")

        let Study_Person_Mid_Initials = CvTerm.create("STDMSO:00000053", "Study Person Mid Initials", "STDMSO")

        let Study_Person_Email = CvTerm.create("STDMSO:00000054", "Study Person Email", "STDMSO")

        let Study_Person_Phone = CvTerm.create("STDMSO:00000055", "Study Person Phone", "STDMSO")

        let Study_Person_Fax = CvTerm.create("STDMSO:00000056", "Study Person Fax", "STDMSO")

        let Study_Person_Address = CvTerm.create("STDMSO:00000057", "Study Person Address", "STDMSO")

        let Study_Person_Affiliation = CvTerm.create("STDMSO:00000058", "Study Person Affiliation", "STDMSO")

        let Study_Person_Roles = CvTerm.create("STDMSO:00000059", "Study Person Roles", "STDMSO")

        let Study_Person_Roles_Term_Accession_Number = CvTerm.create("STDMSO:00000060", "Study Person Roles Term Accession Number", "STDMSO")

        let Study_Person_Roles_Term_Source_REF = CvTerm.create("STDMSO:00000061", "Study Person Roles Term Source REF", "STDMSO")

