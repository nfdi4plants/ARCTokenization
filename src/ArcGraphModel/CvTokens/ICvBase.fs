﻿namespace ArcGraphModel


/// Interface ensures the propterties necessary for CvTerm 
type ICvBase =
    abstract member ID       : string
    abstract member Name     : string
    abstract member RefUri   : string
    // here or as node/edge abstraction 
    // abstract member DataType   : ModelDataType
    // abstract member Adress     : Adress / Location / Position

    //override this.ToString() = 
    //    $"Name: {this.Name}\nID: {this.ID}\nRefUri: {this.RefUri}"

module CvBase = 
    
    /// Returns the id of the cv item
    let getCvAccession (cv : #ICvBase) =
        cv.ID

    /// Returns the name of the cv item
    let getCvName (cv : #ICvBase) =
        cv.Name

    /// Returns the reference of the cv item
    let getCvRef (cv : #ICvBase) =
        cv.RefUri

    /// Returns the full term of the cv item
    let getTerm (cv : #ICvBase) : CvTerm =
        CvTerm.create cv.ID cv.Name cv.RefUri

    /// Returns true, if the given term matches the term of the cv item
    let equalsTerm (term : CvTerm) (cv : #ICvBase) =
        getTerm cv = term

    /// Returns true, if the given terms of the given cv items match
    let equals (cv1 : #ICvBase) (cv2 : #ICvBase) =
        getTerm cv1 = getTerm cv2


    /// Returns Some Value of type 'T, if the given cv item can be downcast, else returns None
    let tryAs<'T when 'T :> ICvBase> (cv : ICvBase) =
        match cv with
        | :? 'T as cv -> Some cv
        | _ -> None
        