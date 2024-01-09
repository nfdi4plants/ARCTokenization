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
