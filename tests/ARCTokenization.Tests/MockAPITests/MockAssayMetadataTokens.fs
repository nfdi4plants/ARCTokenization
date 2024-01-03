namespace MockAPITests

open ARCTokenization
open Xunit
open TestUtils

module MockAssayMetadataTokens =
    
    [<Fact>]
    let ``Empty assay mock token list contains all metadata section keys`` () = 
        let expected = ReferenceObjects.MockAPI.AssayMetadataTokens.empty
        let actual = TestObjects.MockAPI.AssayMetadataTokens.empty
        Assert.All(
            List.zip expected actual,
            (fun (expected,actual) -> CvParam.structuralEquality (expected) (actual))
        )