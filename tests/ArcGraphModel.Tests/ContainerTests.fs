module ContainerTests

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif
open System
open FSharpAux
open ArcGraphModel
open ParamTests

module Terms = 
    
    let protocol = "Test:123", "Protocol", "Test"
    let version = "Test:456", "Version", "Test"

type Protocol () =  

    inherit CvContainer(Terms.protocol)

    member this.Version 
        with get() = 
            CvContainer.getSingleAs<CvParam> Protocol.VersionProperty this
            |> CvParam.getValueAsString
        and set(version : string) =
            CvParam.fromValue Terms.version version
            |> fun cvp -> CvContainer.setSingle cvp this

    static member VersionProperty = "Version"

[<AutoOpen>]
module private GenericContainer =
    
    let name = "MyContainer"
    let id = "Test:123456"
    let ref = "Test"

    let testTerm = id,name,ref

let private correctlyImplementsICvBase = testList "correctlyImplementsICvBase" [
    
    let container = CvContainer(testTerm)

    testCase "returns correct TAN" (fun _ ->
        let result = CvBase.getCvAccession container
        Expect.equal id result ""
    )
    testCase "returns correct Name" (fun _ ->
        let result = CvBase.getCvName container
        Expect.equal name result ""
    )
    testCase "returns correct TSR" (fun _ ->
        let result = CvBase.getCvRef container
        Expect.equal ref result ""
    )
    testCase "hasNoItems" (fun _ ->
        let result = Dictionary.count container
        Expect.equal 0 result ""
    )
]

let private setSingleProperty = testList "setSingleProperty" [

    let container = CvContainer(testTerm)

    /// Not sure if i like reusing these testcases over multiple files
    let param = Parameter.testCvParam1
    let name = (param :> ICvBase).Name

    do CvContainer.setSingle param container

    testCase "HasValue" (fun _ ->
        let result = Dictionary.containsKey name container
        Expect.isTrue result ""
    )
    testCase "HasOneItem" (fun _ ->
        let result = Dictionary.count container
        Expect.equal 1 result ""
    )
    testCase "CanRetrieve" (fun _ ->
        let result = CvContainer.getSingleAs<CvParam> name container
        Expect.equal param result ""
    )
]

let private setManyProperty = testList "setManyProperty" [

    let container = CvContainer(testTerm)

    let differentParams = seq [Parameter.testCvParam1 :> ICvBase;Parameter.testCvParam2]
    let correctParams = seq [Parameter.testCvParam1;Parameter.testCvParam1]
    let name = (Parameter.testCvParam1 :> ICvBase).Name

    testCase "FailsForDifferentCvs" (fun _ ->
        let hasFailed = 
            try 
                CvContainer.setMany differentParams container
                false
            with
            | _ -> true 
        Expect.isTrue hasFailed ""
    )
    testCase "WorksForSameCvs" (fun _ ->
        let hasFailed = 
            try 
                CvContainer.setMany (correctParams |> Seq.cast<ICvBase>) container
                false
            with
            | _ -> true
        Expect.isFalse hasFailed ""
    )

    do CvContainer.setMany (correctParams |> Seq.cast<ICvBase>) container

    testCase "HasValue" (fun _ ->
        let result = Dictionary.containsKey name container
        Expect.isTrue result ""
    )
    testCase "HasOneItem" (fun _ ->
        let result = Dictionary.count container
        Expect.equal 1 result ""
    )
    testCase "CanRetrieve" (fun _ ->
        let result = CvContainer.getManyAs<CvParam> name container
        Expect.equal correctParams result ""
    )
]

let private ExtendedContainer = testList "ExtendedContainer" [

    let version = "0.0.1"
    let protocol = new Protocol()

    do protocol.Version <- version

    testCase "HasValue" (fun _ ->
        let result = Dictionary.containsKey Protocol.VersionProperty protocol
        Expect.isTrue result ""
    )
    testCase "HasOneItem" (fun _ ->
        let result = Dictionary.count protocol
        Expect.equal 1 result ""
    )
    testCase "CanRetrieve" (fun _ ->
        let result = protocol.Version
        Expect.equal version result ""
    )
]

let main =
    testList "ParamTests" [
        correctlyImplementsICvBase
        setSingleProperty
        setManyProperty
        ExtendedContainer
    ]
