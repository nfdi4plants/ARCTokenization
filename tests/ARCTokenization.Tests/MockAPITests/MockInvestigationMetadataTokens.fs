namespace MockAPITests

open ARCTokenization
open Xunit
open TestUtils

module MockInvestigationMetadataTokens =
    
    [<Fact>]
    let ``Empty investigation mock token list contains all metadata section keys`` () = 
        let expected = ReferenceObjects.MockAPI.InvestigationMetadataTokens.empty
        let actual = TestObjects.MockAPI.InvestigationMetadataTokens.empty
        Assert.All(
            List.zip expected actual,
            (fun (expected,actual) -> CvParam.structuralEquality (expected) (actual))
        )
