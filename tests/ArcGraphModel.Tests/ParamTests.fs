module ParamTests

open ArcGraphModel
#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif

[<AutoOpenAttribute>]
module Parameter =

    let testParamValueValue = ParamValue.Value "ParamValue.Value"
    let testCvParam1 = CvParam("CvParam_TAN_1", "CvParam_Name_1", "CvParam_TSR_1", testParamValueValue)
    let testCvTerm = CvTerm("CvTerm_TAN", "CvTerm_Name", "CvTerm_TSR")
    let testParamValueCvValue = ParamValue.CvValue testCvTerm
    let testCvParam2 : CvParam = CvParam("CvParam_TAN_2", "CvParam_Name_2", "CvParam_TSR_3", testParamValueCvValue)
    let testCvUnit = CvUnit("CvUnit_TAN", "CvUnit_Name", "CvUnit_TSR")
    let testParamValueCvUnit = ParamValue.WithCvUnitAccession ("CvUnit_Value", testCvUnit)
    let testCvParam3 = CvParam("CvParam_TAN_3", "CvParam_Name_3", "CvParam_TSR_3", testParamValueCvUnit)

let private getCvAccession = testList "getCvAccession" [
    testCase "returns correct TAN" (fun _ ->
        let result = CvBase.getCvAccession testCvParam1
        Expect.equal "CvParam_TAN_1" result ""
    )
]

let private getCvName = testList "getCvName" [
    testCase "returns correct Name" (fun _ ->
        let result = CvBase.getCvName testCvParam1
        Expect.equal "CvParam_Name_1" result ""
    )
]

let private getCvRef = testList "getCvRef" [
    testCase "returns correct TSR" (fun _ ->
        let result = CvBase.getCvRef testCvParam1
        Expect.equal "CvParam_TSR_1" result ""
    )
]

let private getValue = testList "getValue" [
    testCase "returns correct Value" (fun _ ->
        let result = ParamBase.getValue testCvParam1 :?> string
        Expect.equal "ParamValue.Value" result ""
    )
]

let private tryGetValueAccession = testList "tryGetValueAccession" [

    let retrievedValueTan = ParamBase.tryGetValueAccession testCvParam2

    testCase "isSome" (fun _ ->
        Expect.isTrue retrievedValueTan.IsSome ""
    )
    testCase "returns correct ParamValue TAN" (fun _ ->
        Expect.equal "CvTerm_TAN" retrievedValueTan.Value ""
    )
]

let private tryGetValueRef = testList "tryGetValueRef" [

    let retreivedValueTsr = ParamBase.tryGetValueRef testCvParam2

    testCase "isSome" (fun _ ->
        Expect.isTrue retreivedValueTsr.IsSome ""
    )
    testCase "returns correct ParamValue TSR" (fun _ ->
        Expect.equal "CvTerm_TSR" retreivedValueTsr.Value ""
    )
]

let private tryGetCvUnit = testList "tryGetCvUnit" [

    let retrievedCvUnit = ParamBase.tryGetCvUnit testCvParam3

    testCase "isSome" (fun _ ->
        Expect.isTrue retrievedCvUnit.IsSome ""
    )
    testCase "returns correct ParamValue CvUnit Name" (fun _ ->
        let result = retrievedCvUnit.Value |> fun (_,n,_) -> n
        Expect.equal "CvUnit_Name" result ""
    )
    testCase "returns correct ParamValue CvUnit TAN" (fun _ ->
        let result = retrievedCvUnit.Value |> fun (a,_,_) -> a
        Expect.equal "CvUnit_TAN" result ""
    )
    testCase "returns correct ParamValue CvUnit TSR" (fun _ ->
        let result = retrievedCvUnit.Value |> fun (_,_,r) -> r
        Expect.equal "CvUnit_TSR" result ""
    )
]

let private tryGetCvUnitValue = testList "tryGetCvUnitValue" [

    let retrievedCvUnitValue = ParamBase.tryGetCvUnitValue testCvParam3

    testCase "isSome" (fun _ ->
        Expect.isTrue retrievedCvUnitValue.IsSome ""
    )
    testCase "returns correct CvUnit Value" (fun _ ->
        let result = retrievedCvUnitValue.Value :?> string
        Expect.equal "CvUnit_Value" result ""
    )
]

let private tryGetCvUnitName = testList "tryGetCvUnitName" [

    let retrievedCvUnitName = ParamBase.tryGetCvUnitName testCvParam3

    testCase "isSome" (fun _ ->
        Expect.isTrue retrievedCvUnitName.IsSome ""
    )
    testCase "returns correct CvUnit Name" (fun _ ->
        let result = retrievedCvUnitName.Value
        Expect.equal "CvUnit_Name" result ""
    )
]

let private tryGetCvUnitAccession = testList "tryGetCvUnitAccession" [

    let retrievedCvUnitTan = ParamBase.tryGetCvUnitAccession testCvParam3

    testCase "isSome" (fun _ ->
        Expect.isTrue retrievedCvUnitTan.IsSome ""
    )
    testCase "returns correct CvUnit TAN" (fun _ ->
        let result = retrievedCvUnitTan.Value
        Expect.equal "CvUnit_TAN" result ""
    )
]

let private tryGetCvUnitRef = testList "tryGetCvUnitRef" [

    let retrievedCvUnitTsr = ParamBase.tryGetCvUnitRef testCvParam3

    testCase "isSome" (fun _ ->
        Expect.isTrue retrievedCvUnitTsr.IsSome ""
    )
    testCase "returns correct CvUnit TSR" (fun _ ->
        let result = retrievedCvUnitTsr.Value
        Expect.equal "CvUnit_TSR" result ""
    )
]

let main =
    testList "ParamTests" [
        getCvAccession
        getCvName
        getCvRef
        getValue
        tryGetValueAccession
        tryGetValueRef
        tryGetCvUnit
        tryGetCvUnitValue
        tryGetCvUnitName
        tryGetCvUnitAccession
        tryGetCvUnitRef
    ]