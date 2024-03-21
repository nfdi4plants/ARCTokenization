
// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collection of terms needed for baseline WIP validation

namespace ARCTokenization.StructuralOntology

open ControlledVocabulary

module ASSMSO = 
 
    module ``Assay Metadata`` =
        let key =  CvTerm.create("ASSMSO:00000001","Assay Metadata","ASSMSO")

        module ``ASSAY`` =
            let key =  CvTerm.create("ASSMSO:00000002","ASSAY","ASSMSO")
            let ``Assay Measurement Type`` =  CvTerm.create("ASSMSO:00000004","Assay Measurement Type","ASSMSO")
            let ``Assay Measurement Type Term Accession Number`` =  CvTerm.create("ASSMSO:00000006","Assay Measurement Type Term Accession Number","ASSMSO")
            let ``Assay Measurement Type Term Source REF`` =  CvTerm.create("ASSMSO:00000008","Assay Measurement Type Term Source REF","ASSMSO")  
            let ``Assay Technology Type`` =  CvTerm.create("ASSMSO:00000011","Assay Technology Type","ASSMSO")
            let ``Assay Technology Type Term Accession Number`` = CvTerm.create("ASSMSO:00000013","Assay Technology Type Term Accession Number","ASSMSO")   
            let ``Assay Technology Type Term Source REF`` =CvTerm.create("ASSMSO:00000015","Assay Technology Type Term Source REF","ASSMSO")
            let ``Assay Technology Platform`` =  CvTerm.create("ASSMSO:00000017","Assay Technology Platform","ASSMSO")
            let ``Assay File Name`` =  CvTerm.create("ASSMSO:00000019","Assay File Name","ASSMSO")

        module ``ASSAY PERFORMERS`` =
            let key =  CvTerm.create("ASSMSO:00000020","ASSAY PERFORMERS","ASSMSO")

            let ``Assay Person Last Name`` =  CvTerm.create("ASSMSO:00000021","Assay Person Last Name","ASSMSO")
            let ``Assay Person First Name`` =  CvTerm.create("ASSMSO:00000023","Assay Person First Name","ASSMSO")
            let ``Assay Person Mid Initials`` =  CvTerm.create("ASSMSO:00000025","Assay Person Mid Initials","ASSMSO")
            let ``Assay Person Email`` =  CvTerm.create("ASSMSO:00000027","Assay Person Email","ASSMSO")
            let ``Assay Person Phone`` =  CvTerm.create("ASSMSO:00000029","Assay Person Phone","ASSMSO")
            let ``Assay Person Fax`` =  CvTerm.create("ASSMSO:00000031","Assay Person Fax","ASSMSO")
            let ``Assay Person Address`` =  CvTerm.create("ASSMSO:00000033","Assay Person Address","ASSMSO")
            let ``Assay Person Affiliation`` =  CvTerm.create("ASSMSO:00000035","Assay Person Affiliation","ASSMSO")
            let ``Assay Person Roles`` =  CvTerm.create("ASSMSO:00000037","Assay Person Roles","ASSMSO")
            let ``Assay Person Roles Term Accession Number`` =  CvTerm.create("ASSMSO:00000039","Assay Person Roles Term Accession Number","ASSMSO")
            let ``Assay Person Roles Term Source REF`` =  CvTerm.create("ASSMSO:00000041","Assay Person Roles Term Source REF","ASSMSO")
