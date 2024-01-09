namespace MockAPITests

open ARCTokenization
open Xunit
open TestUtils

module MockProcessGraphTokens =
    
    module ProcessGraphColumn =
    
        [<Fact>]
        let ``Input column mock tokens are correct`` () = 
            let expected = ReferenceObjects.MockAPI.ProcessGraphTokens.referenceInputColumn
            let actual = TestObjects.MockAPI.ProcessGraphTokens.inputColumn
            Assert.All(
            
                List.zip expected actual,
                (fun (expected,actual) -> Param.typedStructuralEquality (expected) (actual))
            )

        [<Fact>]
        let ``Characteristics column mock tokens are correct`` () = 
            let expected = ReferenceObjects.MockAPI.ProcessGraphTokens.referenceCharacteristicsColumn
            let actual = TestObjects.MockAPI.ProcessGraphTokens.characteristicsColumn
            Assert.All(
            
                List.zip expected actual,
                (fun (expected,actual) -> Param.typedStructuralEquality (expected) (actual))
            )

        [<Fact>]
        let ``Output column mock tokens are correct`` () = 
            let expected = ReferenceObjects.MockAPI.ProcessGraphTokens.referenceOutputColumn
            let actual = TestObjects.MockAPI.ProcessGraphTokens.outputColumn
            Assert.All(
            
                List.zip expected actual,
                (fun (expected,actual) -> Param.typedStructuralEquality (expected) (actual))
            )

    module ProcessGraph =

        [<Fact>]
        let ``Simple study process graph mock tokens are correct`` () = 
            let expected = ReferenceObjects.MockAPI.ProcessGraph.referenceStudyProcessGraphTable
            let actual = TestObjects.MockAPI.ProcessGraphTokens.simpleStudy
            Assert.All(
                List.zip expected actual,
                (fun (expected,actual) -> 
                    Assert.All(
                        List.zip expected actual,
                        (fun (expected,actual) -> Param.typedStructuralEquality (expected) (actual)))
                )
            )
