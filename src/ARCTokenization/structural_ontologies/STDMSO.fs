
// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collection of terms needed for baseline WIP validation

namespace ARCTokenization.StructuralOntology

open ControlledVocabulary

module STDMSO = 
    
    module ``Study Metadata`` =
        let key =  CvTerm.create("STDMSO:00000001","Study Metadata","STDMSO")

        module ``STUDY`` =
            let key =  CvTerm.create("STDMSO:00000002","STUDY","STDMSO")

            let ``Study Identifier`` = CvTerm.create("STDMSO:00000003","Study Identifier","STDMSO")
            let ``Study Title`` = CvTerm.create("STDMSO:00000004","Study Title","STDMSO")
            let ``Study Description`` = CvTerm.create("STDMSO:00000005","Study Description","STDMSO")
            let ``Study Submission Date`` = CvTerm.create("STDMSO:00000006","Study Submission Date","STDMSO")
            let ``Study Public Release Date`` = CvTerm.create("STDMSO:00000007","Study Public Release Date","STDMSO")
            let ``Study File Name`` = CvTerm.create("STDMSO:00000008","Study File Name","STDMSO")

        module ``STUDY DESIGN DESCRIPTORS`` =
            let key =  CvTerm.create("STDMSO:00000009","STUDY DESIGN DESCRIPTORS","STDMSO")
            let ``Study Design Type`` = CvTerm.create("STDMSO:00000010","Study Design Type","STDMSO")
            let ``Study Design Type Term Accession Number`` = CvTerm.create("STDMSO:00000011","Study Design Type Term Accession Number","STDMSO")
            let ``Study Design Type Term Source REF`` = CvTerm.create("STDMSO:00000012","Study Design Type Term Source REF","STDMSO")

        module ``STUDY PUBLICATIONS`` =
            let key =  CvTerm.create("STDMSO:00000013","STUDY PUBLICATIONS","STDMSO")
            let ``Study Publication PubMed ID`` = CvTerm.create("STDMSO:00000014","Study Publication PubMed ID","STDMSO")
            let ``Study Publication DOI`` = CvTerm.create("STDMSO:00000015","Study Publication DOI","STDMSO")
            let ``Study Publication Author List`` = CvTerm.create("STDMSO:00000016","Study Publication Author List","STDMSO")
            let ``Study Publication Title`` = CvTerm.create("STDMSO:00000017","Study Publication Title","STDMSO")
            let ``Study Publication Status`` = CvTerm.create("STDMSO:00000018","Study Publication Status","STDMSO")
            let ``Study Publication Status Term Accession Number`` = CvTerm.create("STDMSO:00000019","Study Publication Status Term Accession Number","STDMSO")
            let ``Study Publication Status Term Source REF`` = CvTerm.create("STDMSO:00000020","Study Publication Status Term Source REF","STDMSO")

        module ``STUDY FACTORS`` =
            let key =  CvTerm.create("STDMSO:00000021","STUDY FACTORS","STDMSO")
            let ``Study Factor Name`` = CvTerm.create("STDMSO:00000022","Study Factor Name","STDMSO")
            let ``Study Factor Type`` = CvTerm.create("STDMSO:00000023","Study Factor Type","STDMSO")
            let ``Study Factor Type Term Accession Number`` = CvTerm.create("STDMSO:00000024","Study Factor Type Term Accession Number","STDMSO")
            let ``Study Factor Type Term Source REF`` = CvTerm.create("STDMSO:00000025","Study Factor Type Term Source REF","STDMSO")

        module ``STUDY ASSAYS`` =
            let key =  CvTerm.create("STDMSO:00000026","STUDY ASSAYS","STDMSO")
            let ``Study Assay Measurement Type`` = CvTerm.create("STDMSO:00000027","Study Assay Measurement Type","STDMSO")
            let ``Study Assay Measurement Type Term Accession Number`` = CvTerm.create("STDMSO:00000028","Study Assay Measurement Type Term Accession Number","STDMSO")
            let ``Study Assay Measurement Type Term Source REF`` = CvTerm.create("STDMSO:00000029","Study Assay Measurement Type Term Source REF","STDMSO")
            let ``Study Assay Technology Type`` = CvTerm.create("STDMSO:00000030","Study Assay Technology Type","STDMSO")
            let ``Study Assay Technology Type Term Accession Number`` = CvTerm.create("STDMSO:00000031","Study Assay Technology Type Term Accession Number","STDMSO")
            let ``Study Assay Technology Type Term Source REF`` = CvTerm.create("STDMSO:00000032","Study Assay Technology Type Term Source REF","STDMSO")
            let ``Study Assay Technology Platform`` = CvTerm.create("STDMSO:00000033","Study Assay Technology Platform","STDMSO")
            let ``Study Assay File Name`` = CvTerm.create("STDMSO:00000034","Study Assay File Name","STDMSO")

        module ``STUDY PROTOCOLS`` =
            let key =  CvTerm.create("STDMSO:00000035","STUDY PROTOCOLS","STDMSO")
            let ``Study Protocol Name`` = CvTerm.create("STDMSO:00000036","Study Protocol Name","STDMSO")
            let ``Study Protocol Type`` = CvTerm.create("STDMSO:00000037","Study Protocol Type","STDMSO")
            let ``Study Protocol Type Term Accession Number`` = CvTerm.create("STDMSO:00000038","Study Protocol Type Term Accession Number","STDMSO")
            let ``Study Protocol Type Term Source REF`` = CvTerm.create("STDMSO:00000039","Study Protocol Type Term Source REF","STDMSO")
            let ``Study Protocol Description`` = CvTerm.create("STDMSO:00000040","Study Protocol Description","STDMSO")
            let ``Study Protocol URI`` = CvTerm.create("STDMSO:00000041","Study Protocol URI","STDMSO")
            let ``Study Protocol Version`` = CvTerm.create("STDMSO:00000042","Study Protocol Version","STDMSO")
            let ``Study Protocol Parameters Name`` = CvTerm.create("STDMSO:00000043","Study Protocol Parameters Name","STDMSO")
            let ``Study Protocol Parameters Term Accession Number`` = CvTerm.create("STDMSO:00000044","Study Protocol Parameters Term Accession Number","STDMSO")
            let ``Study Protocol Parameters Term Source REF`` = CvTerm.create("STDMSO:00000045","Study Protocol Parameters Term Source REF","STDMSO")
            let ``Study Protocol Components Name`` = CvTerm.create("STDMSO:00000046","Study Protocol Components Name","STDMSO")
            let ``Study Protocol Components Type`` = CvTerm.create("STDMSO:00000047","Study Protocol Components Type","STDMSO")
            let ``Study Protocol Components Type Term Accession Number`` = CvTerm.create("STDMSO:00000048","Study Protocol Components Type Term Accession Number","STDMSO")
            let ``Study Protocol Components Type Term Source REF`` = CvTerm.create("STDMSO:00000049","Study Protocol Components Type Term Source REF","STDMSO")

        module ``STUDY CONTACTS`` =
            let key =  CvTerm.create("STDMSO:00000050","STUDY CONTACTS","STDMSO")
            let ``Study Person Last Name`` = CvTerm.create("STDMSO:00000051","Study Person Last Name","STDMSO")
            let ``Study Person First Name`` = CvTerm.create("STDMSO:00000052","Study Person First Name","STDMSO")
            let ``Study Person Mid Initials`` = CvTerm.create("STDMSO:00000053","Study Person Mid Initials","STDMSO")
            let ``Study Person Email`` = CvTerm.create("STDMSO:00000054","Study Person Email","STDMSO")
            let ``Study Person Phone`` = CvTerm.create("STDMSO:00000055","Study Person Phone","STDMSO")
            let ``Study Person Fax`` = CvTerm.create("STDMSO:00000056","Study Person Fax","STDMSO")
            let ``Study Person Address`` = CvTerm.create("STDMSO:00000057","Study Person Address","STDMSO")
            let ``Study Person Affiliation`` = CvTerm.create("STDMSO:00000058","Study Person Affiliation","STDMSO")
            let ``Study Person Roles`` = CvTerm.create("STDMSO:00000059","Study Person Roles","STDMSO")
            let ``Study Person Roles Term Accession Number`` = CvTerm.create("STDMSO:00000060","Study Person Roles Term Accession Number","STDMSO")
            let ``Study Person Roles Term Source REF`` = CvTerm.create("STDMSO:00000061","Study Person Roles Term Source REF","STDMSO")
