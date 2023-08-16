namespace ControlledVocabulary

/// Represents a term from a controlled vocabulary (Cv)
/// in the form of: id|accession ; name|value ; refUri
// ?Maybe [<Struct>]
//[<Struct>]
type CvTerm = {
    Accession: string
    Value: string
    RefUri: string
} with
    static member create(
        accession: string,
        value: string,
        ref : string
    ) = 
        {Accession = accession; Value = value; RefUri = ref}

    static member create(
        value: string
    ) = 
        CvTerm.create(
            value = value,
            accession = "",
            ref = ""
        )

/// Represents a unit term from the unit ontology 
/// in the form of: id|accession * name * refUri
// ?Maybe [<Struct>]
type CvUnit = CvTerm