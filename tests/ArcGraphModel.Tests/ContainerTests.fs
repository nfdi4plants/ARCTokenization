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
open System.Collections.Generic

module Terms = 
    
    let protocol = "Test:123", "Protocol", "Test"
    let version = "Test:456", "Version", "Test"

type Protocol internal (
    cvAccession : string, 
    cvName : string, 
    cvRefUri : string, 
    attributes : IDictionary<string,IParam>,
    properties : IDictionary<string,seq<ICvBase>>) =

    inherit CvContainer(cvAccession,cvName,cvRefUri,attributes,properties)

    new () = 
        let (id,name,ref) = Terms.protocol

        Protocol(id,name,ref,Dictionary(),Dictionary())

    member this.Version 
        with get() = 
            CvContainer.getSingleAs<CvParam> Protocol.VersionProperty this
            |> Param.getValueAsString
        and set(version : string) =
            CvParam.fromValue Terms.version version
            |> fun cvp -> CvContainer.setSingle cvp this 
            |> ignore

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
   
    /// Not sure if i like reusing these testcases over multiple files
    let param = Parameter.testCvParam1
    let name = (param :> ICvBase).Name

    testCase "HasValue" (fun _ ->
        let container = CvContainer(testTerm)
        do CvContainer.setSingle param container |> ignore
        let result = CvContainer.containsProperty name container
        Expect.isTrue result ""
    )
    testCase "HasOneItem" (fun _ ->
        let container = CvContainer(testTerm)
        do CvContainer.setSingle param container |> ignore
        let result = CvContainer.countProperties container
        Expect.equal result 1 ""
    )
    testCase "CanRetrieve" (fun _ ->
        let container = CvContainer(testTerm)
        do CvContainer.setSingle param container |> ignore
        let result = CvContainer.getSingleAs<CvParam> name container
        Expect.equal result param ""
    )
]

let private setManyProperty = testList "setManyProperty" [

    

    let differentParams = seq [Parameter.testCvParam1 :> ICvBase;Parameter.testCvParam2]
    let correctParams = seq [Parameter.testCvParam1 :> ICvBase;Parameter.testCvParam1]
    let name = (Parameter.testCvParam1 :> ICvBase).Name

    testCase "FailsForDifferentCvs" (fun _ ->
        let container = CvContainer(testTerm)
        let hasFailed = 
            try 
                CvContainer.setMany differentParams container
                false
            with
            | _ -> true 
        Expect.isTrue hasFailed ""
    )
    testCase "WorksForSameCvs" (fun _ ->
        let container = CvContainer(testTerm)
        let hasFailed = 
            try 
                CvContainer.setMany (correctParams |> Seq.cast<ICvBase>) container
                false
            with
            | _ -> true
        Expect.isFalse hasFailed ""
    )


    testCase "HasValue" (fun _ ->
        let container = CvContainer(testTerm)
        do CvContainer.setMany correctParams container
        let result = CvContainer.containsProperty name container
        Expect.isTrue result ""
    )
    testCase "HasOneProperty" (fun _ ->
        let container = CvContainer(testTerm)
        do CvContainer.setMany correctParams container
        let result = CvContainer.countProperties container
        Expect.equal result 1 ""
    )
    testCase "HasTwoChildren" (fun _ ->
        let container = CvContainer(testTerm)
        do CvContainer.setMany correctParams container
        let result = CvContainer.countChildren container
        Expect.equal result 2 ""
    )
    testCase "CanRetrieve" (fun _ ->
        let container = CvContainer(testTerm)
        do CvContainer.setMany correctParams container
        let result = CvContainer.getMany name container
        Expect.equal result correctParams ""
    )
]

let private ExtendedContainer = testList "ExtendedContainer" [

    let version = "0.0.1"
    let protocol = new Protocol()

    do protocol.Version <- version

    testCase "HasValue" (fun _ ->
        let result = CvContainer.containsProperty Protocol.VersionProperty protocol
        Expect.isTrue result ""
    )
    testCase "HasOneItem" (fun _ ->
        let result = CvContainer.countProperties protocol
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
