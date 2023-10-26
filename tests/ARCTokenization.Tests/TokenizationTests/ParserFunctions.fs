namespace TokenizationTests

open ControlledVocabulary
open ARCTokenization
open Xunit

module FileSystem =
    
    open ReferenceObjects.Tokenization.FileSystem
    open System.IO

    let parsedRelativeDirectoryPaths = FS.tokenizeRelativeDirectoryPaths (Path.GetFullPath("Fixtures/testPaths/")) |> List.ofSeq |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

    [<Fact>]
    let ``Relative directory paths are tokenized correctly`` () =
        let actual = parsedRelativeDirectoryPaths
        let expected = referenceRelativeDirectoryPaths
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(e.Equals(a))
        )

    let parsedRelativeFilePaths = FS.tokenizeRelativeFilePaths (Path.GetFullPath("Fixtures/testPaths/")) |> List.ofSeq |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

    [<Fact>]
    let ``Relative file paths are tokenized correctly`` () =
        let actual = parsedRelativeFilePaths
        let expected = referenceRelativeFilePaths
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(e.Equals(a))
        )

    let parsedAbsoluteDirectoryPaths = FS.tokenizeAbsoluteDirectoryPaths (Path.GetFullPath("Fixtures/testPaths/")) |> List.ofSeq |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

    [<Fact>]
    let ``Absolute directory paths are tokenized correctly`` () =
        let actual = parsedAbsoluteDirectoryPaths
        let expected = referenceAbsoluteDirectoryPaths(Path.Combine(System.Environment.CurrentDirectory, "Fixtures/testPaths/"))
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(e.Equals(a))
        )

    let parsedAbsoluteFilePaths = FS.tokenizeAbsoluteFilePaths (Path.GetFullPath("Fixtures/testPaths/")) |> List.ofSeq |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

    [<Fact>]
    let ``Absolute file paths are tokenized correctly`` () =
        let actual = parsedAbsoluteFilePaths
        let expected = referenceAbsoluteFilePaths(Path.Combine(System.Environment.CurrentDirectory, "Fixtures/testPaths/"))
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(e.Equals(a))
        )

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

module ConvertMetadataTokens = 
    
    open ReferenceObjects.Tokenization.ConvertMetadataTokens
    open FsSpreadsheet

    let tokenizer : FsCell seq -> IParam list = Tokenization.convertMetadataTokens (MetadataSheet.parseKeyWithTerms referenceTerms)

    let parsedCvParams = tokenizer referenceRow

    [<Fact>]
    let ``Row with CvTerm as section key is tokenized as CvParams`` () =
        let actual = parsedCvParams
        let expected = referenceCvParams
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    [<Fact>]
    let ``CvTerm row has metadata section key as value of first token`` () =
        let actual = parsedCvParams.[0] |> Param.getValueAsTerm
        let expected = Terms.StructuralTerms.metadataSectionKey
        Assert.Equal(expected, actual)

    let parsedComments = tokenizer referenceCommentRow

    [<Fact>]
    let ``Row with Comment as section key is tokenized as CvParams`` () =
        let actual = parsedComments
        let expected = referenceCommentCvParams
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    [<Fact>]
    let ``Comment row has metadata section key as value of first token`` () =
        let actual = parsedComments.[0] |> Param.getValueAsTerm
        let expected = Terms.StructuralTerms.metadataSectionKey
        Assert.Equal(expected, actual)

    let parsedIgnoreLines = tokenizer referenceIgnoreLineRow

    [<Fact>]
    let ``Row with IgnoreLine as section key is tokenized as CvParams`` () =
        let actual = parsedIgnoreLines
        let expected = referenceIgnoreLineCvParams
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    [<Fact>]
    let ``IgnoreLine row has metadata section key as value of first token`` () =
        let actual = parsedIgnoreLines.[0] |> Param.getValueAsTerm
        let expected = Terms.StructuralTerms.metadataSectionKey
        Assert.Equal(expected, actual)

    let parsedUserParams = tokenizer referenceUserParamRow

    [<Fact>]
    let ``Row with UserParam as section key is tokenized as UserParams`` () =
        let actual = parsedUserParams
        let expected = referenceUserParams
        Assert.All(
            List.zip expected actual,
            fun (e, a) -> Assert.True(Param.equals e a)
        )

    [<Fact>]
    let ``UserParam row has metadata section key as value of first token`` () =
        let actual = parsedUserParams.[0] |> Param.getValueAsTerm
        let expected = Terms.StructuralTerms.metadataSectionKey
        Assert.Equal(expected, actual)
