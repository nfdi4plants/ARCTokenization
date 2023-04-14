namespace ArcGraphModel

/// Represents a term from a controlled vocabulary (Cv)
/// in the form of: id|accession * name|value * refUri
// ?Maybe [<Struct>]
//[<Struct>]
type CvTerm = string * string * string

/// Represents a unit term from the unit ontology 
/// in the form of: id|accession * name * refUri
// ?Maybe [<Struct>]
type CvUnit = string * string * string