namespace ArcGraphMode.Tests

open Xunit
open ArcGraphModel.Param


module Param =

    let testParamValueValue = ParamValue.Value "ParamValue.Value"
    let testCvParam = CvParam("TAN", "Name", "TSR", testParamValueValue)


    module getCvAccession =

        [<Fact>]
        let ``returns correct TAN`` () =
            let retrievedTan = getCvAccession testCvParam
            Assert.Equal("TAN", retrievedTan)