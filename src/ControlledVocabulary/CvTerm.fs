namespace ControlledVocabulary

/// Represents a term from a controlled vocabulary (Cv)
/// in the form of: id|accession ; name|value ; refUri
// ?Maybe [<Struct>]
//[<Struct>]
type CvTerm = {
    Accession: string
    Name: string
    RefUri: string
} with
    static member create(
        accession: string,
        name: string,
        ref : string
    ) = 
        {Accession = accession; Name = name; RefUri = ref}

    static member create(
        name: string
    ) = 
        CvTerm.create(
            name = name,
            accession = "",
            ref = ""
        )

/// Represents a unit term from the unit ontology 
/// in the form of: id|accession * name * refUri
// ?Maybe [<Struct>]
type CvUnit = CvTerm