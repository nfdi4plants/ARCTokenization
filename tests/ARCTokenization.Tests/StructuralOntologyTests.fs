namespace StructuralOntologyTests

open FsOboParser
open ARCTokenization
open ARCTokenization.Terms
open Xunit

open TestUtils

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

    [<Fact>] 
    let ``no root term in non root terms`` () =
        Assert.All(
            InvestigationMetadata.nonRootCvTerms,
            (fun t -> 
                Assert.True(
                    t.Name <> ReferenceObjects.Terms.referenceInvestigationMetadataOntologyRootTerm.Name
                    && t.Accession <> ReferenceObjects.Terms.referenceInvestigationMetadataOntologyRootTerm.Accession
                )
            )
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

    [<Fact>] 
    let ``no root term in non root terms`` () =
        Assert.All(
            StudyMetadata.nonRootCvTerms,
            (fun t -> 
                Assert.True(
                    t.Name <> ReferenceObjects.Terms.referenceStudyMetadataOntologyRootTerm.Name
                    && t.Accession <> ReferenceObjects.Terms.referenceStudyMetadataOntologyRootTerm.Accession
                )
            )
        )

    [<Fact>]
    let ``"STUDY METADATA" has correct ID`` () =
        let smTerm = 
            StudyMetadata.ontology.Terms 
            |> List.tryFind (fun t -> t.Name = "STUDY METADATA")
        let actual =
            match smTerm with
            | Some t -> t.Id = "STDMSO:00000062"
            | None -> false
        Assert.True actual

    [<Fact>]
    let ``Terms have correct relations to "STUDY METADATA"`` () =
        let childrenNames = [
            "Study Identifier"
            "Study Title"
            "Study Description"
            "Study Submission Date"
            "Study Public Release Date"
            "Study File Name"
            "STUDY DESIGN DESCRIPTORS"
            "STUDY PUBLICATIONS"
            "STUDY FACTORS"
            "STUDY ASSAYS"
            "STUDY PROTOCOLS"
            "STUDY CONTACTS"
        ]
        let actual =
        //let childrenRelations =
            childrenNames
            |> List.choose (
                fun n -> 
                    let respectiveTerm = StudyMetadata.ontology.Terms |> List.tryFind (fun t -> n = t.Name)
                    Option.map (fun t -> OboOntology.getRelatedTerms t StudyMetadata.ontology) respectiveTerm
            )
        //let actual =
        //    childrenRelations
        //    |> List.map (
        //        List.exists (fun (it,rel,ot : OboTerm option) -> rel = "part_of" && ot.IsSome && ot.Value.Name = "STUDY METADATA")
        //    )
        Assert.All(
            actual, 
            fun l -> 
                List.map (
                    List.exists (fun (it,rel,ot : OboTerm option) -> rel = "part_of" && ot.IsSome && ot.Value.Name = "STUDY METADATA")
                )
                |> ignore
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

    [<Fact>] 
    let ``no root term in non root terms`` () =
        Assert.All(
            AssayMetadata.nonRootCvTerms,
            (fun t -> 
                Assert.True(
                    t.Name <> ReferenceObjects.Terms.referenceAssayMetadataOntologyRootTerm.Name
                    && t.Accession <> ReferenceObjects.Terms.referenceAssayMetadataOntologyRootTerm.Accession
                )
            )
        )