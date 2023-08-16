module CvBaseTests.Casting

open ControlledVocabulary
open Xunit

let assayTerm = CvTerm.create(accession = "ARCO:1234", name = "Assay",ref = "ARCO")

[<Fact>]
let ``ICvBase can be cast to CvParam using generic tryAs`` () = 
    let v = CvParam(assayTerm,ParamValue.Value 5) :> ICvBase
    let result = CvBase.tryAs<CvParam> v
    Assert.True(result.IsSome)

    let result2 = 
        result.Value
        |> Param.getValueAsInt
    Assert.Equal(5, result2)

[<Fact>]
let ``ICvBase can be cast to CvParam using tryCvParam`` () =
    let v = CvParam(assayTerm,ParamValue.Value 5) :> ICvBase
    let result = CvParam.tryCvParam v
    Assert.True(result.IsSome)

    let result2 = 
        result.Value
        |> Param.getValueAsInt
    Assert.Equal(5, result2)

[<Fact>]
let ``ICvBase can be cast to UserParam using generic tryAs`` () =
    let v = UserParam("MyParam",ParamValue.Value 5) :> ICvBase
    let result = CvBase.tryAs<UserParam> v
    Assert.True(result.IsSome)

    let result2 = 
        result.Value
        |> Param.getValueAsInt
    Assert.Equal(5, result2)

[<Fact>]
let ``ICvBase can be cast to UserParam using tryUserParam`` () =
    let v = UserParam("MyParam",ParamValue.Value 5) :> ICvBase
    let result = UserParam.tryUserParam v
    Assert.True(result.IsSome)

    let result2 = 
        result.Value
        |> Param.getValueAsInt
    Assert.Equal(5, result2)

[<Fact>]
let ``ICvBase can be cast to CvContainer using generic tryAs`` () =
    let v = CvContainer(assayTerm) :> ICvBase
    let result = CvBase.tryAs<CvContainer> v
    Assert.True(result.IsSome)

[<Fact>]
let ``ICvBase can be cast to UserParam using tryCvContainer`` () =
    let v = CvContainer(assayTerm) :> ICvBase
    let result = CvContainer.tryCvContainer v
    Assert.True(result.IsSome)
