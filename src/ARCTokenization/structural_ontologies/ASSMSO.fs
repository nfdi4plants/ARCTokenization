
// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collection of terms needed for baseline WIP validation

namespace ARCTokenization.StructuralOntology

open ControlledVocabulary

module ASSMSO = 
 
    module ``Assay Metadata`` =
        let key =  CvTerm.create("ASSMSO:00000001","Assay Metadata","ASSMSO")

        module ``ASSAY`` =
            let key =  CvTerm.create("ASSMSO:00000002","ASSAY","ASSMSO")
        
            let ``Assay File Name`` =  CvTerm.create("ASSMSO:00000019","Assay File Name","ASSMSO")

        module ``ASSAY PERFORMERS`` =
            let key =  CvTerm.create("ASSMSO:00000020","ASSAY PERFORMERS","ASSMSO")

            let ``Assay Performer Last Name`` =  CvTerm.create("ASSMSO:00000021","Assay Performer Last Name","ASSMSO")
            let ``Assay Performer First Name`` =  CvTerm.create("ASSMSO:00000023","Assay Performer First Name","ASSMSO")
            let ``Assay Performer Mid Initials`` =  CvTerm.create("ASSMSO:00000025","Assay Performer Mid Initials","ASSMSO")
            let ``Assay Performer Email`` =  CvTerm.create("ASSMSO:00000027","Assay Performer Email","ASSMSO")
            let ``Assay Performer Phone`` =  CvTerm.create("ASSMSO:00000029","Assay Performer Phone","ASSMSO")
            let ``Assay Performer Fax`` =  CvTerm.create("ASSMSO:00000031","Assay Performer Fax","ASSMSO")
            let ``Assay Performer Address`` =  CvTerm.create("ASSMSO:00000033","Assay Performer Address","ASSMSO")
            let ``Assay Performer Affiliation`` =  CvTerm.create("ASSMSO:00000035","Assay Performer Affiliation","ASSMSO")
            let ``Assay Performer Roles`` =  CvTerm.create("ASSMSO:00000037","Assay Performer Roles","ASSMSO")
            let ``Assay Performer Roles Term Accession Number`` =  CvTerm.create("ASSMSO:00000039","Assay Performer Roles Term Accession Number","ASSMSO")
            let ``Assay Performer Roles Term Source REF`` =  CvTerm.create("ASSMSO:00000041","Assay Performer Roles Term Source REF","ASSMSO")
