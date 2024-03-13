namespace CodeGenerationTests

open OBO.NET
open ARCTokenization
open ARCTokenization.Terms
open Xunit
open ARCTokenization.StructuralOntology
open FSharpAux
open type System.Environment

open TestUtils


module toUnderscoredName =

    [<Fact>]
    let ``returns correct underscored name`` () =
        let expected = "Investigation_Metadata"
        let actual = List.head InvestigationMetadata.ontology.Terms |> CodeGeneration.toUnderscoredName
        Assert.Equal(expected, actual)


module toTermSourceRef =

    [<Fact>]
    let ``returns correct TermSourceRef`` () =
        let expected = "INVMSO"
        let actual = List.head InvestigationMetadata.ontology.Terms |> CodeGeneration.toTermSourceRef
        Assert.Equal(expected, actual)


module toCodeString =

    [<Fact>]
    let ``returns correct F# code`` () =
        let expected = $"        let Investigation_Metadata = CvTerm.create(\"INVMSO:00000001\", \"Investigation Metadata\", \"INVMSO\"){NewLine}{NewLine}"
        let actual = List.head InvestigationMetadata.ontology.Terms |> CodeGeneration.toCodeString
        Assert.Equal(expected, actual)


module toSourceCode =

    [<Fact>]
    let ``returns correct source code`` () =
        let expected = $"namespace ARCTokenization.StructuralOntology{NewLine}{NewLine}    open ControlledVocabulary{NewLine}{NewLine}    module Investigation ={NewLine}{NewLine}        let Investigation_Metadata = CvTerm.create(\"INVMSO:00000001\", \"Investigation Metadata\", \"INVMSO\"){NewLine}{NewLine}        let ONTOLOGY_SOURCE_REFERENCE = CvTerm.create(\"INVMSO:00000002\", \"ONTOLOGY SOURCE REFERENCE\", \"INVMSO\"){NewLine}{NewLine}        let Term_Source_Name = CvTerm.create(\"INVMSO:00000003\", \"Term Source Name\", \"INVMSO\")"
        let actual = 
            CodeGeneration.toSourceCode "Investigation" InvestigationMetadata.ontology 
            |> String.splitS NewLine 
            |> Array.take 11 
            |> String.concat NewLine
        Assert.Equal(expected, actual)