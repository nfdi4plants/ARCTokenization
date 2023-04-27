module TokenAggregationTests

open ArcGraphModel.IO
open ArcGraphModel
#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

let valueTerm = "ABC:123","Best Characteristic","ABC"

let baseToken = CvParam(Terms.characteristic,CvTerm.getName valueTerm)
let annotationToken = 
    let cv = CvParam(Terms.annotationID,CvTerm.getId valueTerm)
    //cv.AddAttribute(CvParam(Terms.characteristic),"")
    cv
let sourceRefToken =    
    let cv = CvParam(Terms.termSourceRef,CvTerm.getRef valueTerm)
    cv
let baseToken2 = CvParam(Terms.characteristic,CvTerm.getName valueTerm)
//let baseToken3
//let strayAnnotationToken


let private mergeParams = testList "MergeParams" [

    testCase "MergeAnnotationID" (fun _ ->
        let result = TokenAggregation.mergeParams annotationToken baseToken 
        Expect.isTrue (CvParam.isCvParam result) "Key was not correctly merged to CvParam"
        let param = result :?> CvParam
        Expect.equal (CvBase.getTerm param) Terms.characteristic "Term was falsely modified"
        Expect.equal (Param.getValueAsTerm param) (CvTerm.getId valueTerm,CvTerm.getName valueTerm,"") "annotationValue was transferred incorrectly"
    ) 
    testCase "MergeSourceRef" (fun _ ->
        let result = TokenAggregation.mergeParams sourceRefToken baseToken 
        Expect.isTrue (CvParam.isCvParam result) "Key was not correctly merged to CvParam"
        let param = result :?> CvParam
        Expect.equal (CvBase.getTerm param) Terms.characteristic "Term was falsely modified"
        Expect.equal (Param.getValueAsTerm param) ("",CvTerm.getName valueTerm,CvTerm.getRef valueTerm) "ref was transferred incorrectly"
    ) 
    ]


let main =
    testList "TokenAggregationTests" [
        mergeParams
    ]