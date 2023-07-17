module KeyParserTests

open ArcGraphModel.IO
open ArcGraphModel
#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif





//module Keys =
    
//    let justToken = "Last Name"
//    let tokenWithSingleQualifier = "Person Last Name"
//    let tokenWithTwoIdentifiers = "Study Person Last Name"
//    let structuredName = "[temperature]"
//    let structuredNameWithQualifier = "Factor [temperature]"
//    let unparseableToken = "I'm just a token"


//let private simpleParsing = testList "SimpleParsing" [

//    testCase "JustToken" (fun _ ->
//        let f = KeyParser.parseKey [] Keys.justToken
//        let result = f (ParamValue.Value "Test")
//        Expect.isTrue (CvParam.isCvParam result) "Key was not correctly parsed to CvParam"
//        let param = result :?> CvParam
//        Expect.equal param.Attributes List.empty "Token should not have Attributes"
//        Expect.equal (CvBase.getTerm param) Terms.familyName "Token was parsed to wrong term"
//        Expect.equal (Param.getValue param) ("Test" :> System.IConvertible) "Value was stored incorrectly"
//    )
//    testCase "TokenWithSingleQualifier" (fun _ ->
//        let f = KeyParser.parseKey [] Keys.tokenWithSingleQualifier
//        let result = f (ParamValue.Value "Test")
//        Expect.isTrue (CvParam.isCvParam result) "Key was not correctly parsed to CvParam"
//        let param = result :?> CvParam
//        Expect.equal 1 param.Attributes.Length "Token should have one attribute"
//        Expect.equal (CvBase.getTerm param.Attributes.[0]) Terms.person "Incorrect attribute"
//        Expect.equal (CvBase.getTerm param) Terms.familyName "Token was parsed to wrong term"
//        Expect.equal (Param.getValue param) ("Test" :> System.IConvertible) "Value was stored incorrectly"
//    )
//    testCase "TokenWithTwoIdentifiers" (fun _ ->
//        let f = KeyParser.parseKey [] Keys.tokenWithTwoIdentifiers
//        let result = f (ParamValue.Value "Test")
//        Expect.isTrue (CvParam.isCvParam result) "Key was not correctly parsed to CvParam"
//        let param = result :?> CvParam
//        Expect.equal 2 param.Attributes.Length "Token should have two attributes"
//        Expect.equal (CvBase.getTerm param.Attributes.[0]) Terms.person "Incorrect attribute"
//        Expect.equal (CvBase.getTerm param.Attributes.[1]) Terms.study "Incorrect attribute"
//        Expect.equal (CvBase.getTerm param) Terms.familyName "Token was parsed to wrong term"
//        Expect.equal (Param.getValue param) ("Test" :> System.IConvertible) "Value was stored incorrectly"
//    )
//    testCase "StructuredName" (fun _ ->
//        let f = KeyParser.parseKey [] Keys.structuredName
//        let result = f (ParamValue.Value "Test")
//        Expect.isTrue (CvParam.isCvParam result) "Key was not correctly parsed to CvParam"
//        let param = result :?> CvParam
//        Expect.equal param.Attributes List.empty "Token should not have Attributes"
//        Expect.equal (CvBase.getTerm param) ("","temperature","") "Token was parsed to wrong term"
//        Expect.equal (Param.getValue param) ("Test" :> System.IConvertible) "Value was stored incorrectly"
//    )
//    testCase "StructuredNameWithQualifer" (fun _ ->
//        let f = KeyParser.parseKey [] Keys.structuredNameWithQualifier
//        let result = f (ParamValue.Value "Test")
//        Expect.isTrue (CvParam.isCvParam result) "Key was not correctly parsed to CvParam"
//        let param = result :?> CvParam
//        Expect.equal 1 param.Attributes.Length "Token should have one attribute"
//        Expect.equal (CvBase.getTerm param) ("","temperature","") "Token was parsed to wrong term"
//        Expect.equal (CvBase.getTerm param.Attributes.[0]) Terms.factor "Incorrect attribute"
//        Expect.equal (Param.getValue param) ("Test" :> System.IConvertible) "Value was stored incorrectly"
//    )
//    testCase "UnparseableToken" (fun _ ->
//        let f = KeyParser.parseKey [] Keys.unparseableToken
//        let result = f (ParamValue.Value "Test")
//        Expect.isTrue (CvBase.is<UserParam> result) "Key was not correctly parsed to UserParam"
//        let param = result :?> UserParam
//        Expect.equal param.Attributes List.empty "Token should not have Attributes"
//        Expect.equal (CvBase.getCvName param) ("I'm just a token") "Token was parsed to wrong term"
//        Expect.equal (Param.getValue param) ("Test" :> System.IConvertible) "Value was stored incorrectly"
//    )
//]

//let main =
//    testList "KeyParserTests" [
//        simpleParsing
//    ]
