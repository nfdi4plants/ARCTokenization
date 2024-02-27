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


    let parsedFilePathsArc         = FS.tokenizeARCFileSystem (Path.Combine(System.Environment.CurrentDirectory, "Fixtures/arcStructure/"))

    [<Fact>]
    let ``Test ARC Tokenisation``() =
        let actual = parsedFilePathsArc |> List.ofSeq |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)
        let expected =
            [
                CvParam(StructuralOntology.AFSO.``Assays Directory`` ,  "assays")
                CvParam(StructuralOntology.AFSO.``Assay Directory`` ,   "assays/measurement1")
                CvParam(StructuralOntology.AFSO.``Assay File`` ,        "assays/measurement1/isa.assay.xlsx")
                CvParam(StructuralOntology.AFSO.``Dataset File``,       "assays/measurement1/isa.dataset.xlsx")
                CvParam(StructuralOntology.AFSO.``Investigation File``, "isa.investigation.xlsx")
                CvParam(StructuralOntology.AFSO.``Runs Directory`` ,    "runs")
                CvParam(StructuralOntology.AFSO.``YML File`` ,          "runs/FSharpArcCapsule.yml")
                CvParam(StructuralOntology.AFSO.``Studies Directory`` , "studies")
                CvParam(StructuralOntology.AFSO.``Study Directory`` ,   "studies/experiment1_material")
                CvParam(StructuralOntology.AFSO.``Study File`` ,        "studies/experiment1_material/isa.study.xlsx")
                CvParam(StructuralOntology.AFSO.``Directory Path``,     "studies/experiment1_material/resources")
                CvParam(StructuralOntology.AFSO.``File Path``,     "studies/experiment1_material/resources/.gitkeep")
                CvParam(StructuralOntology.AFSO.``Workflows Directory`` ,"workflows")
                CvParam(StructuralOntology.AFSO.``Workflow Directory``, "workflows/FixedScript")
                CvParam(StructuralOntology.AFSO.``File Path``,          "workflows/FixedScript/script.fsx")
            ]
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)
        
        Assert.All(   
            List.zip actual expected,
            fun (a,e) -> 
                Assert.True(a.Equals(e))
        )
