
// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collection of terms needed for baseline WIP validation

namespace ARCTokenization.StructuralOntology

open ControlledVocabulary

module STDMSO = 
    
    module ``Study Metadata`` =
        let key =  CvTerm.create("STDMSO:00000001","Study Metadata","STDMSO")

        module ``STUDY`` =
            let key =  CvTerm.create("STDMSO:00000002","STUDY","STDMSO")

            let ``Study Identifier`` = CvTerm.create("STDMSO:00000003","STUDY","STDMSO")
            let ``Study Title`` = CvTerm.create("STDMSO:00000004","Study Title","STDMSO")
            let ``Study Description`` = CvTerm.create("STDMSO:00000005","Study Description","STDMSO")
            let ``Study Submission Date`` = CvTerm.create("STDMSO:00000006","Study Submission Date","STDMSO")
            let ``Study Public Release Date`` = CvTerm.create("STDMSO:00000007","Study Public Release Date","STDMSO")
            let ``Study File Name`` = CvTerm.create("STDMSO:00000008","Study File Name","STDMSO")

        module ``STUDY DESIGN DESCRIPTORS`` =
            let key =  CvTerm.create("STDMSO:00000009","STUDY DESIGN DESCRIPTORS","STDMSO")

        module ``STUDY PUBLICATIONS`` =
            let key =  CvTerm.create("STDMSO:00000013","STUDY PUBLICATIONS","STDMSO")

        module ``STUDY FACTORS`` =
            let key =  CvTerm.create("STDMSO:00000021","STUDY FACTORS","STDMSO")

        module ``STUDY ASSAYS`` =
            let key =  CvTerm.create("STDMSO:00000026","STUDY ASSAYS","STDMSO")

            let ``Study Assay File Name`` = CvTerm.create("STDMSO:00000034","Study Assay File Name","STDMSO")

        module ``STUDY PROTOCOLS`` =
            let key =  CvTerm.create("STDMSO:00000035","STUDY PROTOCOLS","STDMSO")

        module ``STUDY CONTACTS`` =
            let key =  CvTerm.create("STDMSO:00000050","STUDY CONTACTS","STDMSO")

            let ``Study Person Last Name`` = CvTerm.create("STDMSO:000000051","Study Person Last Name","STDMSO")
            let ``Study Person First Name`` = CvTerm.create("STDMSO:000000052","Study Person First Name","STDMSO")
            let ``Study Person Mid Initials`` = CvTerm.create("STDMSO:000000053","Study Person Mid Initials","STDMSO")
            let ``Study Person Email`` = CvTerm.create("STDMSO:000000054","Study Person Email","STDMSO")
            let ``Study Person Phone`` = CvTerm.create("STDMSO:000000055","Study Person Phone","STDMSO")
            let ``Study Person Fax`` = CvTerm.create("STDMSO:000000056","Study Person Fax","STDMSO")
            let ``Study Person Address`` = CvTerm.create("STDMSO:000000057","Study Person Address","STDMSO")
            let ``Study Person Affiliation`` = CvTerm.create("STDMSO:000000058","Study Person Affiliation","STDMSO")
            let ``Study Person Roles`` = CvTerm.create("STDMSO:000000059","Study Person Roles","STDMSO")
            let ``Study Person Roles Term Accession Number`` = CvTerm.create("STDMSO:000000060","Study Person Roles Term Accession Number","STDMSO")
            let ``Study Person Roles Term Source REF`` = CvTerm.create("STDMSO:000000061","Study Person Roles Term Source REF","STDMSO")
