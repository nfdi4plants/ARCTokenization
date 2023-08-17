namespace StructuralOntologyTests

open FsOboParser
open ARCTokenization
open ARCTokenization.Terms
open Xunit

module InvestigationMetadata =
    
    [<Fact>]
    let ``no duplicate term ids`` () =
        let expected = [1 .. InvestigationMetadata.ontology.Terms.Length]
        let actual = 
            InvestigationMetadata.ontology.Terms
            |> List.map (fun t ->
                t.Id.Replace("INVMSO:","") |> int
            )
            |> List.sort
        Assert.All(
            List.zip expected actual,
            (fun (e,a) -> Assert.Equal(e,a))
        )

module StudyMetadata =
    
    [<Fact>]
    let ``no duplicate term ids`` () =
        let expected = [1 .. StudyMetadata.ontology.Terms.Length]
        let actual = 
            StudyMetadata.ontology.Terms
            |> List.map (fun t ->
                t.Id.Replace("STDMSO:","") |> int
            )
            |> List.sort
        Assert.All(
            List.zip expected actual,
            (fun (e,a) -> Assert.Equal(e,a))
        )

module AssayMetadata =
    
    [<Fact>]
    let ``no duplicate term ids`` () =
        let expected = [1 .. AssayMetadata.ontology.Terms.Length]
        let actual = 
            AssayMetadata.ontology.Terms
            |> List.map (fun t ->
                t.Id.Replace("ASSMSO:","") |> int
            )
            |> List.sort
        Assert.All(
            List.zip expected actual,
            (fun (e,a) -> Assert.Equal(e,a))
        )