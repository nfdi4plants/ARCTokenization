
// This file should eventually be auto-generated from the respective obo files, to have a safe way of updating it from the same source.
// For now, it is manually created and updated. It is not complete, just a collectAFSOn of terms needed for baseline WIP validatAFSOn

namespace ARCTokenization.StructuralOntology

open ControlledVocabulary

module AFSO =

    let ``File``      = CvTerm.create("AFSO:00000001","File","AFSO")
    let ``Directory`` = CvTerm.create("AFSO:00000002","Directory","AFSO")
    let ``File Type`` = CvTerm.create("AFSO:00000003","File Type","AFSO")
    let ``Extension`` = CvTerm.create("AFSO:00000004","Extension","AFSO")

    /// The name of the file.
    let ``File Name``      = CvTerm.create("AFSO:00000005","File Name","AFSO")
    let ``Directory Name`` = CvTerm.create("AFSO:00000006","Directory Name","AFSO")
    
    /// The full path of the directory or file.
    let ``Full Name``      = CvTerm.create("AFSO:00000007","Full Name","AFSO")

    /// The full path, relative path or a Universal Naming ConventAFSOn (UNC) path
    let ``Path``           = CvTerm.create("AFSO:00000008","Path","AFSO")
    let ``File Path``      = CvTerm.create("AFSO:00000009","File Path","AFSO")
    let ``Directory Path`` = CvTerm.create("AFSO:00000010","Directory Path","AFSO")

    // Top level Directories
    let ``Studies Directory``   = CvTerm.create("AFSO:00000011","Studies Directory","AFSO")
    let ``Assays Directory``    = CvTerm.create("AFSO:00000012","Assays Directory","AFSO")
    let ``Runs Directory``      = CvTerm.create("AFSO:00000013","Runs Directory","AFSO")
    let ``Workflows Directory`` = CvTerm.create("AFSO:00000014","Workflows Directory","AFSO")

    // Sub level folders
    let ``Study``   = CvTerm.create("AFSO:00000015","Study","AFSO")
    let ``Assay``    = CvTerm.create("AFSO:00000016","Assay","AFSO")
    let ``Run``      = CvTerm.create("AFSO:00000017","Run","AFSO")
    let ``Workflow`` = CvTerm.create("AFSO:00000018","Workflow","AFSO")


    // Isa FileTypes 
    let ``Investigation ISA``   = CvTerm.create("AFSO:00000019","Investigation ISA","AFSO")
    let ``Study ISA``           = CvTerm.create("AFSO:00000020","Study ISA","AFSO")
    let ``Assay ISA``           = CvTerm.create("AFSO:00000021","Assay ISA","AFSO")
    let ``Dataset ISA``         = CvTerm.create("AFSO:00000022","Dataset ISA","AFSO")

    // Additional FileTypes
    let ``CWL File``         = CvTerm.create("AFSO:00000023","CWL File","AFSO")
    let ``YML File``         = CvTerm.create("AFSO:00000024","YML File","AFSO")
 