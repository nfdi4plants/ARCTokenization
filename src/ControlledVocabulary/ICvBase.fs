namespace ControlledVocabulary

open System.Collections.Generic

type IAttributeCollection = 

    abstract member HasAttributes : bool // Dictionary<string,ICvBase seq>

/// Interface ensures the propterties necessary for CvTerm 
and ICvBase =
    abstract member Accession   : string
    abstract member Name        : string
    abstract member RefUri      : string
    
    inherit IAttributeCollection

    //override this.ToString() = 
    //    $"Name: {this.Name}\nID: {this.ID}\nRefUri: {this.RefUri}"

type CvBase = 
    
    /// Returns the id of the cv item
    static member getCvAccession (cv : #ICvBase) =
        cv.Accession

    /// Returns the name of the cv item
    static member getCvName (cv : #ICvBase) =
        cv.Name

    /// Returns the reference of the cv item
    static member getCvRef (cv : #ICvBase) =
        cv.RefUri

    /// Returns the full term of the cv item
    static member getTerm (cv : #ICvBase) : CvTerm =
        CvTerm.create(accession = cv.Accession, name = cv.Name, ref = cv.RefUri)

    /// Returns true, if the given term matches the term of the cv item
    static member equalsTerm (term : CvTerm) (cv : #ICvBase) =
        CvBase.getTerm cv = term

    /// Returns true, if the terms of the given cv items match
    static member equals (cv1 : #ICvBase) (cv2 : #ICvBase) =
        CvBase.getTerm cv1 = CvBase.getTerm cv2

    /// Returns true, if the names of the given cv items match
    static member equalsName (cv1 : #ICvBase) (cv2 : #ICvBase) =
        CvBase.getCvName cv1 = CvBase.getCvName cv2

    /// Returns Some Value of type 'T, if the given cv item can be downcast, else returns None
    static member inline tryAs<'T when 'T :> ICvBase> (cv : ICvBase) =
        match cv with
        | :? 'T as cv -> Some cv
        | _ -> None

    /// Returns true, if the given cv item can be downcast
    static member inline is<'T when 'T :> ICvBase> (cv : ICvBase) =
        match cv with
        | :? 'T as cv -> true
        | _ -> false
        