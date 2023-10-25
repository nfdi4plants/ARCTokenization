
// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collection of terms needed for baseline WIP validation

namespace ARCTokenization.StructuralOntology

open ControlledVocabulary

module INVMSO =

    module ``Investigation Metadata`` =
        let key =  CvTerm.create("INVMSO:00000001","Investigation Metadata","INVMSO")

        module ``ONTOLOGY SOURCE REFERENCE`` =
            let key =  CvTerm.create("INVMSO:00000002","ONTOLOGY SOURCE REFERENCE","INVMSO")

        module ``INVESTIGATION`` =
            let key =  CvTerm.create("INVMSO:00000007","INVESTIGATION","INVMSO")
            
            let ``Investigation Identifier`` = CvTerm.create("INVMSO:00000008","Investigation Identifier","INVMSO")
            let ``Investigation Title`` = CvTerm.create("INVMSO:00000009","Investigation Title","INVMSO")
            let ``Investigation Description`` = CvTerm.create("INVMSO:00000010","Investigation Description","INVMSO")
            let ``Investigation Submission Date`` = CvTerm.create("INVMSO:00000011","Investigation Submission Date","INVMSO")
            let ``Investigation Public Release Date`` = CvTerm.create("INVMSO:00000012","Investigation Public Release Date","INVMSO")

        module ``INVESTIGATION PUBLICATIONS`` =
            let key =  CvTerm.create("INVMSO:00000013","INVESTIGATION PUBLICATIONS","INVMSO")

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
            let ``Investigation Person Roles Term Source REF`` = CvTerm.create("INVMSO:000000","Investigation Person Roles Term Source REF","INVMSO")
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

        module ``STUDY PUBLICATIONS`` =
            let key = CvTerm.create("INVMSO:00000044","STUDY PUBLICATIONS","INVMSO")

        module ``STUDY FACTORS`` =
            let key = CvTerm.create("INVMSO:00000052","STUDY FACTORS","INVMSO")

        module ``STUDY ASSAYS`` =
            let key = CvTerm.create("INVMSO:00000057","STUDY ASSAYS","INVMSO")

            let ``Study Assay File Name`` = CvTerm.create("INVMSO:00000065","Study Assay File Namee","INVMSO")

        module ``STUDY PROTOCOLS`` =
            let key = CvTerm.create("INVMSO:00000066","STUDY PROTOCOLS","INVMSO")

        module ``STUDY CONTACTS`` =
            let key = CvTerm.create("INVMSO:00000081","STUDY CONTACTS","INVMSO")
