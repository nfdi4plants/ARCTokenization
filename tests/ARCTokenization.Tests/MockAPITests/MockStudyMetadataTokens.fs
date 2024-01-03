namespace MockAPITests

open ARCTokenization
open Xunit
open TestUtils

module MockStudyMetadataTokens =

    [<Fact>]
    let ``Empty study mock token list contains all metadata section keys`` () = 
        let expected = ReferenceObjects.MockAPI.StudyMetadataTokens.empty
        let actual = TestObjects.MockAPI.StudyMetadataTokens.empty
        Assert.All(
            List.zip expected actual,
            (fun (expected,actual) -> CvParam.structuralEquality (expected) (actual))
        )