namespace CvParamTests

open ControlledVocabulary
open ReferenceObjects
open Xunit

module InstanceMemberTests =

    [<Fact>]
    let ``Accession`` () =
        let expected = [testAccession1; testAccession1; testAccession2]
        let actual = testCvParams |> List.map (fun x -> x.Accession)
        Assert.Equal<string List>(expected, actual)

    [<Fact>]
    let ``Name`` () =
        let expected = [testName1; testName1; testName2]
        let actual = testCvParams |> List.map (fun x -> x.Name)
        Assert.Equal<string List>(expected, actual)

    [<Fact>]
    let ``RefUri`` () =
        let expected = [testRef1; testRef1; testRef2]
        let actual = testCvParams |> List.map (fun x -> x.RefUri)
        Assert.Equal<string List>(expected, actual)

    [<Fact>]
    let ``Value`` () =
        let expected = [ParamValue.Value 5; ParamValue.CvValue testTerm2; ParamValue.WithCvUnitAccession (5, testTerm1)]
        let actual = testCvParams |> List.map (fun x -> x.Value)
        Assert.Equal<ParamValue List>(expected, actual)


module StaticMemberTests =

    [<Fact>]
    let ``getParamValue`` () =
        let expected = [ParamValue.Value 5; ParamValue.CvValue testTerm2; ParamValue.WithCvUnitAccession (5, testTerm1)]
        let actual = testCvParams |> List.map CvParam.getParamValue
        Assert.Equal<ParamValue List>(expected, actual)

    [<Fact>]
    let ``getValue`` () =
        let expected : System.IConvertible list = [5; testTerm2.Name; 5]
        let actual = testCvParams |> List.map CvParam.getValue
        Assert.Equal<System.IConvertible List>(expected, actual)

    [<Fact>]
    let ``getValueAsString`` () =
        let expected = ["5"; testTerm2.Name; "5"]
        let actual = testCvParams |> List.map CvParam.getValueAsString
        Assert.Equal<string List>(expected, actual)

    [<Fact>]
    let ``getValueAsInt`` () =
        let expected = [5; 5; 5]
        let actual = testCvParams |> List.map CvParam.getValueAsInt
        Assert.Equal<int List>(expected, actual)

    [<Fact>]
    let ``getValueAsTerm`` () =
        let expected = [
            CvTerm.create(name = "5")
            testTerm2
            CvTerm.create(name = "5")
        ]
        let actual = testCvParams |> List.map CvParam.getValueAsTerm
        Assert.Equal<CvTerm List>(expected, actual)  

    [<Fact>]
    let ``tryGetValueAccession`` () =
        let expected = [None; Some testAccession2; None]
        let actual = testCvParams |> List.map CvParam.tryGetValueAccession
        Assert.Equal<Option<string> List>(expected, actual)

    [<Fact>]
    let ``tryGetValueRef`` () =
        let expected = [None; Some testRef2; None]
        let actual = testCvParams |> List.map CvParam.tryGetValueRef
        Assert.Equal<Option<string> List>(expected, actual)

    [<Fact>]
    let ``tryGetCvUnit`` () =
        let expected = [None; None; Some testTerm1]
        let actual = testCvParams |> List.map CvParam.tryGetCvUnit
        Assert.Equal<Option<CvUnit> List>(expected, actual)

    [<Fact>]
    let ``tryGetCvUnitValue`` () =
        let expected : (System.IConvertible option) list = [None; None; Some 5]
        let actual = testCvParams |> List.map CvParam.tryGetCvUnitValue
        Assert.Equal<Option<System.IConvertible> List>(expected, actual)

    [<Fact>]
    let ``tryGetCvUnitTermName`` () =
        let expected = [None; None; Some testName1]
        let actual = testCvParams |> List.map CvParam.tryGetCvUnitTermName
        Assert.Equal<Option<string> List>(expected, actual)

    [<Fact>]
    let ``tryGetCvUnitTermAccession`` () =
        let expected = [None; None; Some testAccession1]
        let actual = testCvParams |> List.map CvParam.tryGetCvUnitTermAccession
        Assert.Equal<Option<string> List>(expected, actual)

    [<Fact>]
    let ``tryGetCvUnitTermRef`` () =
        let expected = [None; None; Some testRef2]
        let actual = testCvParams |> List.map CvParam.tryGetCvUnitTermRef
        Assert.Equal<Option<string> List>(expected, actual)

