﻿namespace ControlledVocabulary

open System.Collections.Generic

type IAttributeCollection = 

    abstract member HasAttributes : bool // Dictionary<string,ICvBase seq>

/// Interface ensures the propterties necessary for CvTerm 
and ICvBase =
    abstract member Accession   : string
    abstract member Value       : string
    abstract member RefUri         : string
    
    inherit IAttributeCollection

    //override this.ToString() = 
    //    $"Name: {this.Name}\nID: {this.ID}\nRefUri: {this.RefUri}"

module CvBase = 
    
    /// Returns the id of the cv item
    let getCvAccession (cv : #ICvBase) =
        cv.Accession

    /// Returns the name of the cv item
    let getCvValue (cv : #ICvBase) =
        cv.Value

    /// Returns the reference of the cv item
    let getCvRef (cv : #ICvBase) =
        cv.RefUri

    /// Returns the full term of the cv item
    let getTerm (cv : #ICvBase) : CvTerm =
        CvTerm.create(accession = cv.Accession, value = cv.Value, ref = cv.RefUri)

    /// Returns true, if the given term matches the term of the cv item
    let equalsTerm (term : CvTerm) (cv : #ICvBase) =
        getTerm cv = term

    /// Returns true, if the terms of the given cv items match
    let equals (cv1 : #ICvBase) (cv2 : #ICvBase) =
        getTerm cv1 = getTerm cv2

    /// Returns true, if the names of the given cv items match
    let equalsValue (cv1 : #ICvBase) (cv2 : #ICvBase) =
        getCvValue cv1 = getCvValue cv2

    /// Returns Some Value of type 'T, if the given cv item can be downcast, else returns None
    let inline tryAs<'T when 'T :> ICvBase> (cv : ICvBase) =
        match cv with
        | :? 'T as cv -> Some cv
        | _ -> None

    /// Returns true, if the given cv item can be downcast
    let inline is<'T when 'T :> ICvBase> (cv : ICvBase) =
        match cv with
        | :? 'T as cv -> true
        | _ -> false
        