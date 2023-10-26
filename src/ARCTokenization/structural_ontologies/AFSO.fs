
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