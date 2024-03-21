
// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collection of terms needed for baseline WIP validation

namespace ARCTokenization.StructuralOntology

open ControlledVocabulary

module INVMSO =

    module ``Investigation Metadata`` =
        let key =  CvTerm.create("INVMSO:00000001","Investigation Metadata","INVMSO")

        module ``ONTOLOGY SOURCE REFERENCE`` =
        
            let key = CvTerm.create("INVMSO:00000002","ONTOLOGY SOURCE REFERENCE","INVMSO")
            let ``Term Source Name`` = CvTerm.create("INVMSO:00000003","Term Source Name","INVMSO")
            let ``Term Source File`` = CvTerm.create("INVMSO:00000004","Term Source File","INVMSO")
            let ``Term Source Version`` = CvTerm.create("INVMSO:00000005","Term Source Version","INVMSO")
            let ``Term Source Description`` = CvTerm.create("INVMSO:00000006","Term Source Description","INVMSO")

        module ``INVESTIGATION`` =
            let key =  CvTerm.create("INVMSO:00000007","INVESTIGATION","INVMSO")
            let ``Investigation Identifier`` = CvTerm.create("INVMSO:00000008","Investigation Identifier","INVMSO")
            let ``Investigation Title`` = CvTerm.create("INVMSO:00000009","Investigation Title","INVMSO")
            let ``Investigation Description`` = CvTerm.create("INVMSO:00000010","Investigation Description","INVMSO")
            let ``Investigation Submission Date`` = CvTerm.create("INVMSO:00000011","Investigation Submission Date","INVMSO")
            let ``Investigation Public Release Date`` = CvTerm.create("INVMSO:00000012","Investigation Public Release Date","INVMSO")

        module ``INVESTIGATION PUBLICATIONS`` = 
            let key = CvTerm.create("INVMSO:00000013","INVESTIGATION PUBLICATIONS","INVMSO")
            let ``Investigation Publication PubMed ID`` = CvTerm.create("INVMSO:00000014","Investigation Publication PubMed ID","INVMSO")
            let ``Investigation Publication DOI`` = CvTerm.create("INVMSO:00000015","Investigation Publication DOI","INVMSO")
            let ``Investigation Publication Author List`` = CvTerm.create("INVMSO:00000016","Investigation Publication Author List","INVMSO")
            let ``Investigation Publication Title`` = CvTerm.create("INVMSO:00000017","Investigation Publication Title","INVMSO")
            let ``Investigation Publication Status`` = CvTerm.create("INVMSO:00000018","Investigation Publication Status","INVMSO")
            let ``Investigation Publication Status Term Accession Number`` = CvTerm.create("INVMSO:00000019","Investigation Publication Status Term Accession Number","INVMSO")
            let ``Investigation Publication Status Term Source REF`` = CvTerm.create("INVMSO:00000020","Investigation Publication Status Term Source REF","INVMSO")

        module ``INVESTIGATION CONTACTS`` =
            let key =  CvTerm.create("INVMSO:00000021","INVESTIGATION CONTACTS","INVMSO")
            let ``Investigation Person Last Name`` = CvTerm.create("INVMSO:00000022","Investigation Person Last Name","INVMSO")
            let ``Investigation Person First Name`` = CvTerm.create("INVMSO:00000023","Investigation Person First Name","INVMSO")
            let ``Investigation Person Mid Initials`` = CvTerm.create("INVMSO:00000024","Investigation Person Mid Initials","INVMSO")
            let ``Investigation Person Email`` = CvTerm.create("INVMSO:00000025","Investigation Person Email","INVMSO")
            let ``Investigation Person Phone`` = CvTerm.create("INVMSO:00000026","Investigation Person Phone","INVMSO")
            let ``Investigation Person Fax`` = CvTerm.create("INVMSO:00000027","Investigation Person Fax","INVMSO")
            let ``Investigation Person Address`` = CvTerm.create("INVMSO:00000028","Investigation Person Address","INVMSO")
            let ``Investigation Person Affiliation`` = CvTerm.create("INVMSO:00000029","Investigation Person Affiliation","INVMSO")
            let ``Investigation Person Roles`` = CvTerm.create("INVMSO:00000030","Investigation Person Roles","INVMSO")
            let ``Investigation Person Roles Term Accession Number`` = CvTerm.create("INVMSO:00000031","Investigation Person Roles Term Accession Number","INVMSO")
            let ``Investigation Person Roles Term Source REF`` = CvTerm.create("INVMSO:00000032","Investigation Person Roles Term Source REF","INVMSO")
            let ``Comment[<Investigation Person ORCID>]`` = CvTerm.create("INVMSO:00000093","Comment[<Investigation Person ORCID>]","INVMSO")
            let ``Comment[Investigation Person ORCID]`` = CvTerm.create("INVMSO:00000094","Comment[Investigation Person ORCID]","INVMSO")
            let ``Comment[ORCID]`` = CvTerm.create("INVMSO:00000095","Comment[ORCID]","INVMSO")

        module ``STUDY`` =
            let key = CvTerm.create("INVMSO:00000033","STUDY","INVMSO")
            let ``Study Identifier`` = CvTerm.create("INVMSO:00000034","Study Identifier","INVMSO")
            let ``Study Title`` = CvTerm.create("INVMSO:00000035","Study Title","INVMSO")
            let ``Study Description`` = CvTerm.create("INVMSO:00000036","Study Description","INVMSO")
            let ``Study Submission Date`` = CvTerm.create("INVMSO:00000037","Study Submission Date","INVMSO")
            let ``Study Public Release Date`` = CvTerm.create("INVMSO:00000038","Study Public Release Date","INVMSO")
            let ``Study File Name`` = CvTerm.create("INVMSO:00000039","Study File Name","INVMSO")

        module ``STUDY DESIGN DESCRIPTORS`` = 
            let key = CvTerm.create("INVMSO:00000040","STUDY DESIGN DESCRIPTORS","INVMSO")
            let ``Study Design Type`` = CvTerm.create("INVMSO:00000041","Study Design Type","INVMSO")
            let ``Study Design Type Term Accession Number`` = CvTerm.create("INVMSO:00000042","Study Design Type Term Accession Number","INVMSO")
            let ``Study Design Type Term Source REF`` = CvTerm.create("INVMSO:00000043","Study Design Type Term Source REF","INVMSO")
        
        module ``STUDY PUBLICATIONS`` = 
            let key = CvTerm.create("INVMSO:00000044","STUDY PUBLICATIONS","INVMSO")
            let ``Study Publication PubMed ID`` = CvTerm.create("INVMSO:00000045","Study Publication PubMed ID","INVMSO")
            let ``Study Publication DOI`` = CvTerm.create("INVMSO:00000046","Study Publication DOI","INVMSO")
            let ``Study Publication Author List`` = CvTerm.create("INVMSO:00000047","Study Publication Author List","INVMSO")
            let ``Study Publication Title`` = CvTerm.create("INVMSO:00000048","Study Publication Title","INVMSO")
            let ``Study Publication Status`` = CvTerm.create("INVMSO:00000049","Study Publication Status","INVMSO")
            let ``Study Publication Status Term Accession Number`` = CvTerm.create("INVMSO:00000050","Study Publication Status Term Accession Number","INVMSO")
            let ``Study Publication Status Term Source REF`` = CvTerm.create("INVMSO:00000051","Study Publication Status Term Source REF","INVMSO")
        
        module ``STUDY FACTORS`` =  
            let key = CvTerm.create("INVMSO:00000052","STUDY FACTORS","INVMSO")
            let ``Study Factor Name`` = CvTerm.create("INVMSO:00000053","Study Factor Name","INVMSO")
            let ``Study Factor Type`` = CvTerm.create("INVMSO:00000054","Study Factor Type","INVMSO")
            let ``Study Factor Type Term Accession Number`` = CvTerm.create("INVMSO:00000055","Study Factor Type Term Accession Number","INVMSO")
            let ``Study Factor Type Term Source REF`` = CvTerm.create("INVMSO:00000056","Study Factor Type Term Source REF","INVMSO")

        module ``STUDY ASSAYS`` =
            let key = CvTerm.create("INVMSO:00000057","STUDY ASSAYS","INVMSO")
            let ``Study Assay Measurement Type`` = CvTerm.create("INVMSO:00000058","Study Assay Measurement Type","INVMSO")
            let ``Study Assay Measurement Type Term Accession Number`` = CvTerm.create("INVMSO:00000059","Study Assay Measurement Type Term Accession Number","INVMSO")
            let ``Study Assay Measurement Type Term Source REF`` = CvTerm.create("INVMSO:00000060","Study Assay Measurement Type Term Source REF","INVMSO")
            let ``Study Assay Technology Type`` = CvTerm.create("INVMSO:00000061","Study Assay Technology Type","INVMSO")
            let ``Study Assay Technology Type Term Accession Number`` = CvTerm.create("INVMSO:00000062","Study Assay Technology Type Term Accession Number","INVMSO")
            let ``Study Assay Technology Type Term Source REF`` = CvTerm.create("INVMSO:00000063","Study Assay Technology Type Term Source REF","INVMSO")
            let ``Study Assay Technology Platform`` = CvTerm.create("INVMSO:00000064","Study Assay Technology Platform","INVMSO")
            let ``Study Assay File Name`` = CvTerm.create("INVMSO:00000065","Study Assay File Name","INVMSO")

        module ``STUDY PROTOCOLS`` = 
            let key = CvTerm.create("INVMSO:00000066","STUDY PROTOCOLS","INVMSO")
            let ``Study Protocol Name`` = CvTerm.create("INVMSO:00000067","Study Protocol Name","INVMSO")
            let ``Study Protocol Type`` = CvTerm.create("INVMSO:00000068","Study Protocol Type","INVMSO")
            let ``Study Protocol Type Term Accession Number`` = CvTerm.create("INVMSO:00000069","Study Protocol Type Term Accession Number","INVMSO")
            let ``Study Protocol Type Term Source REF`` = CvTerm.create("INVMSO:00000070","Study Protocol Type Term Source REF","INVMSO")
            let ``Study Protocol Description`` = CvTerm.create("INVMSO:00000071","Study Protocol Description","INVMSO")
            let ``Study Protocol URI`` = CvTerm.create("INVMSO:00000072","Study Protocol URI","INVMSO")
            let ``Study Protocol Version`` = CvTerm.create("INVMSO:00000073","Study Protocol Version","INVMSO")
            let ``Study Protocol Parameters Name`` = CvTerm.create("INVMSO:00000074","Study Protocol Parameters Name","INVMSO")
            let ``Study Protocol Parameters Term Accession Number`` = CvTerm.create("INVMSO:00000075","Study Protocol Parameters Term Accession Number","INVMSO")
            let ``Study Protocol Parameters Term Source REF`` = CvTerm.create("INVMSO:00000076","Study Protocol Parameters Term Source REF","INVMSO")
            let ``Study Protocol Components Name`` = CvTerm.create("INVMSO:00000077","Study Protocol Components Name","INVMSO")
            let ``Study Protocol Components Type`` = CvTerm.create("INVMSO:00000078","Study Protocol Components Type","INVMSO")
            let ``Study Protocol Components Type Term Accession Number`` = CvTerm.create("INVMSO:00000079","Study Protocol Components Type Term Accession Number","INVMSO")
            let ``Study Protocol Components Type Term Source REF`` = CvTerm.create("INVMSO:00000080","Study Protocol Components Type Term Source REF","INVMSO")

        module ``STUDY CONTACTS`` = 
            let key = CvTerm.create("INVMSO:00000081","STUDY CONTACTS","INVMSO")
            let ``Study Person Last Name`` = CvTerm.create("INVMSO:00000082","Study Person Last Name","INVMSO")
            let ``Study Person First Name`` = CvTerm.create("INVMSO:00000083","Study Person First Name","INVMSO")
            let ``Study Person Mid Initials`` = CvTerm.create("INVMSO:00000084","Study Person Mid Initials","INVMSO")
            let ``Study Person Email`` = CvTerm.create("INVMSO:00000085","Study Person Email","INVMSO")
            let ``Study Person Phone`` = CvTerm.create("INVMSO:00000086","Study Person Phone","INVMSO")
            let ``Study Person Fax`` = CvTerm.create("INVMSO:00000087","Study Person Fax","INVMSO")
            let ``Study Person Address`` = CvTerm.create("INVMSO:00000088","Study Person Address","INVMSO")
            let ``Study Person Affiliation`` = CvTerm.create("INVMSO:00000089","Study Person Affiliation","INVMSO")
            let ``Study Person Roles`` = CvTerm.create("INVMSO:00000090","Study Person Roles","INVMSO")
            let ``Study Person Roles Term Accession Number`` = CvTerm.create("INVMSO:00000091","Study Person Roles Term Accession Number","INVMSO")
            let ``Study Person Roles Term Source REF`` = CvTerm.create("INVMSO:00000092","Study Person Roles Term Source REF","INVMSO")

            