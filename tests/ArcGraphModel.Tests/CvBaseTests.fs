module CvBaseTests

open ArcGraphModel
#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif


let private casting = testList "casting" [
    testCase "CanBeCastToCvParamGeneric" (fun _ ->
        let v = CvParam(Terms.assay,ParamValue.Value 5) :> ICvBase
        let result = CvBase.tryAs<CvParam> v
        Expect.isSome result "Could not downcast CvBase to CvParam, consider inlining the used function."
        let result2 = 
            result.Value
            |> Param.getValueAsInt
        Expect.equal result2 5 "Value could not be retrieved correctly"    
    )
    testCase "CanBeCastToCvParamGeneric" (fun _ ->
        let v = CvParam(Terms.assay,ParamValue.Value 5) :> ICvBase
        let result = CvParam.tryCvParam v
        Expect.isSome result "Could not downcast CvBase to CvParam, consider inlining the used function."
        let result2 = 
            result.Value
            |> Param.getValueAsInt
        Expect.equal result2 5 "Value could not be retrieved correctly"    
    )
    testCase "CanBeCastToUserParamGeneric" (fun _ ->
        let v = UserParam("MyParam",ParamValue.Value 5) :> ICvBase
        let result = CvBase.tryAs<UserParam> v
        Expect.isSome result "Could not downcast CvBase to CvParam, consider inlining the used function."
        let result2 = 
            result.Value
            |> Param.getValueAsInt
        Expect.equal result2 5 "Value could not be retrieved correctly"    
    )
    testCase "CanBeCastToUserParam" (fun _ ->
        let v = UserParam("MyParam",ParamValue.Value 5) :> ICvBase
        let result = UserParam.tryUserParam v
        Expect.isSome result "Could not downcast CvBase to CvParam, consider inlining the used function."
        let result2 = 
            result.Value
            |> Param.getValueAsInt
        Expect.equal result2 5 "Value could not be retrieved correctly"    
    )
    testCase "CanBeCastToCvContainerGeneric" (fun _ ->
        let v = CvContainer(Terms.assay) :> ICvBase
        let result = CvBase.tryAs<CvContainer> v
        Expect.isSome result "Could not downcast CvBase to CvParam, consider inlining the used function."
    )
    testCase "CanBeCastToCvContainer" (fun _ ->
        let v = CvContainer(Terms.assay) :> ICvBase
        let result = CvContainer.tryCvContainer v
        Expect.isSome result "Could not downcast CvBase to CvParam, consider inlining the used function."
    )
    
]

let main =
    testList "CvBaseTests" [
        casting
    ]