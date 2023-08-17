namespace TokenizationTests

open ControlledVocabulary
open ARCTokenization
open Xunit

module ParseKeyWithTerms =
    
    open ReferenceObjects.Tokenization.KeyParser

    let tokenizer = MetadataSheet.parseKeyWithTerms referenceTerms []
    
    let parsedCvParams = 
         List.zip referenceKeys referenceParamValues
         |> List.map (fun (key, pv) -> tokenizer key pv)

    [<Fact>]
    let ``CvTerms are matched and parsed as CvParams`` () =
        let expected = referenceCvParams
        let actual = parsedCvParams
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    let parsedComments = 
        List.zip referenceCommentKeys referenceParamValues
        |> List.map (fun (key, pv) -> tokenizer key pv)

    [<Fact>]
    let ``Comments are matched and parsed as CvParams`` () =
        let expected = referenceCommentCvParams
        let actual = parsedComments
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    let parsedIgnoreLines =
        List.zip referenceIgnoreLineKeys referenceParamValues
        |> List.map (fun (key, pv) -> tokenizer key pv)

    [<Fact>]
    let ``IgnoreLines are matched and parsed as CvParams`` () =
        let expected = referenceIgnoreLineCvParams
        let actual = parsedIgnoreLines
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    let parsedUserParams = 
        List.zip referenceUserParamKeys referenceParamValues
        |> List.map (fun (key, pv) -> tokenizer key pv)

    [<Fact>]
    let ``UserParams are matched and parsed as UserParams`` () =
        let expected = referenceUserParams
        let actual = parsedUserParams
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    let parsedMixedParams = 
        List.zip referenceMixedKeys referenceMixedParamValues
        |> List.map (fun (key, pv) -> tokenizer key pv)

    [<Fact>]
    let ``Mixed keys are matched and parsed to correct Params`` () =
        let expected = referenceMixedParams
        let actual = parsedMixedParams
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

module ConvertTokens = 
    
    [<Fact>]
    let ``?`` () =
        let actual = Tokenization.convertTokens
        ()
