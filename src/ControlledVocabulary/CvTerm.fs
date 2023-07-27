namespace ControlledVocabulary

/// Represents a term from a controlled vocabulary (Cv)
/// in the form of: id|accession * name|value * refUri
// ?Maybe [<Struct>]
//[<Struct>]
type CvTerm = string * string * string

module CvTerm =
    
    /// gets the name of the CvTerm
    let getName (cvTerm : CvTerm) = 
        match cvTerm with
        | id, name, refUri -> name
       
    /// gets the name of the CvTerm
    let getId (cvTerm : CvTerm) = 
        match cvTerm with
        | id, name, refUri -> id

    /// gets the source reference of the CvTerm
    let getRef (cvTerm : CvTerm) = 
        match cvTerm with
        | id, name, refUri -> refUri

    /// creates a CvTerm from name
    let fromName (name : string) : CvTerm= 
        "", name, ""

    /// creates a CvTerm from a term triplet
    let create id name ref : CvTerm = 
        id,name,ref
        
/// Represents a unit term from the unit ontology 
/// in the form of: id|accession * name * refUri
// ?Maybe [<Struct>]
type CvUnit = string * string * string