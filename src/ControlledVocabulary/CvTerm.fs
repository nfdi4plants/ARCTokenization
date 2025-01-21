namespace ControlledVocabulary


open FSharpAux


/// Represents a term from a controlled vocabulary (Cv)
/// in the form of: id|accession ; name|value ; refUri
// ?Maybe [<Struct>]
//[<Struct>]
type CvTerm = {
    Accession: string
    Name: string
    RefUri: string
} with

    /// <summary>
    /// Checks if the given accession is an URI.
    /// </summary>
    /// <param name="accession">The input accession.</param>
    static member checkForUri accession =
        let rx = System.Text.RegularExpressions.Regex("^https?:\/\/[a-zA-Z0-9\/]+\/[a-zA-Z]+_[0-9]+\/?$")
        rx.Match(accession).Success

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