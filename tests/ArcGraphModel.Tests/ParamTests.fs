namespace ArcGraphModel.Tests

open Xunit
open ArcGraphModel.Param


module Param =

    let testParamValueValue = ParamValue.Value "ParamValue.Value"
    let testCvParam1 = CvParam("CvParam_TAN_1", "CvParam_Name_1", "CvParam_TSR_1", testParamValueValue)
    let testCvTerm = CvTerm("CvTerm_TAN", "CvTerm_Name", "CvTerm_TSR")
    let testParamValueCvValue = ParamValue.CvValue testCvTerm
    let testCvParam2 : CvParam<string> = CvParam("CvParam_TAN_2", "CvParam_Name_2", "CvParam_TSR_3", testParamValueCvValue)
    let testCvUnit = CvUnit("CvUnit_TAN", "CvUnit_Name", "CvUnit_TSR")
    let testParamValueCvUnit = ParamValue.WithCvUnitAccession ("CvUnit_Value", testCvUnit)
    let testCvParam3 = CvParam("CvParam_TAN_3", "CvParam_Name_3", "CvParam_TSR_3", testParamValueCvUnit)


    module getCvAccession =

        [<Fact>]
        let ``returns correct TAN`` () =
            let retrievedTan = getCvAccession testCvParam1
            Assert.Equal("CvParam_TAN_1", retrievedTan)


    module getCvName =

        [<Fact>]
        let ``returns correct Name`` () =
            let retrievedName = getCvName testCvParam1
            Assert.Equal("CvParam_Name_1", retrievedName)


    module getCvRef =

        [<Fact>]
        let ``returns correct TSR`` () =
            let retrievedTsr = getCvRef testCvParam1
            Assert.Equal("CvParam_TSR_1", retrievedTsr)


    module getValue =

        [<Fact>]
        let ``returns correct Value`` () =
            let retrievedValue = getValue testCvParam1
            Assert.Equal("ParamValue.Value", retrievedValue)


    module tryGetValueAccession =

        let retrievedValueTan = tryGetValueAccession testCvParam2

        [<Fact>]
        let ``is Some`` () =
            Assert.True retrievedValueTan.IsSome

        [<Fact>]
        let ``returns correct ParamValue TAN`` () =
            Assert.Equal("CvTerm_TAN", retrievedValueTan.Value)


    module tryGetValueRef =

        let retreivedValueTsr = tryGetValueRef testCvParam2

        [<Fact>]
        let ``is Some`` () =
            Assert.True retreivedValueTsr.IsSome

        [<Fact>]
        let ``returns correct ParamValue TSR`` () =
            Assert.Equal("CvTerm_TSR", retreivedValueTsr.Value)


    module tryGetCvUnit =

        let retrievedCvUnit = tryGetCvUnit testCvParam3

        [<Fact>]
        let ``is Some`` () =
            Assert.True retrievedCvUnit.IsSome

        //let ``returns correct ParamValue CvUnit Value`` () =
        //    Assert.Equal("CvUnit_Value", retrievedCvUnit.Value)