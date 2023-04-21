namespace ArcGraphModel.Tests

//open Xunit
//open ArcGraphModel
//open FSharpAux

//module Terms = 
    
//    let protocol = "Test:123", "Protocol", "Test"
//    let version = "Test:456", "Version", "Test"

//type Protocol () =  

//    inherit CvContainer(Terms.protocol)

//    member this.Version 
//        with get() = 
//            CvContainer.getSingleAs<CvParam> Protocol.VersionProperty this
//            |> CvParam.getValueAsString
//        and set(version : string) =
//            CvParam.fromValue Terms.version version
//            |> fun cvp -> CvContainer.setSingle cvp this

//    static member VersionProperty = "Version"


//module GenericContainer =
    
//    let name = "MyContainer"
//    let id = "Test:123456"
//    let ref = "Test"

//    let testTerm = id,name,ref

      
//    module correctlyImplementsICvBase =

//        let container = CvContainer(testTerm)

//        [<Fact>]
//        let ``returns correct TAN`` () =
//            let retrievedTan = CvBase.getCvAccession container
//            Assert.Equal(id, retrievedTan)

//        [<Fact>]
//        let ``returns correct Name`` () =
//            let retrievedName = CvBase.getCvName container
//            Assert.Equal(name, retrievedName)

//        [<Fact>]
//        let ``returns correct TSR`` () =
//            let retrievedTsr = CvBase.getCvRef container
//            Assert.Equal(ref, retrievedTsr)

//        [<Fact>]
//        let ``hasNoItems`` () =
//            let c = Dictionary.count container
//            Assert.Equal(0, c)


//    module setSingleProperty =
        
//        let container = CvContainer(testTerm)

//        let param = Parameter.testCvParam1
//        let name = (param :> ICvBase).Name

//        do CvContainer.setSingle param container

//        [<Fact>]
//        let ``HasValue`` () =           
//            Assert.True (Dictionary.containsKey name container)

//        [<Fact>]
//        let ``HasOneItem`` () =
//            let c = Dictionary.count container
//            Assert.Equal(1, c)

//        [<Fact>]
//        let ``CanRetrieve`` () =
//            let v = CvContainer.getSingleAs<CvParam> name container
//            Assert.Equal<CvParam>(param, v)

//    module setManyProperty =
        
//        let container = CvContainer(testTerm)

//        let differentParams = seq [Parameter.testCvParam1 :> ICvBase;Parameter.testCvParam2]
//        let correctParams = seq [Parameter.testCvParam1;Parameter.testCvParam1]
//        let name = (Parameter.testCvParam1 :> ICvBase).Name

//        [<Fact>]
//        let ``FailsForDifferentCvs`` () =
//            let hasFailed = 
//                try 
//                    CvContainer.setMany differentParams container
//                    false
//                with
//                | _ -> true
//            Assert.True hasFailed

//        [<Fact>]
//        let ``WorksForSameCvs`` () =
//            let hasFailed = 
//                try 
//                    CvContainer.setMany (correctParams |> Seq.cast<ICvBase>) container
//                    false
//                with
//                | _ -> true
//            Assert.False hasFailed

//        do CvContainer.setMany (correctParams |> Seq.cast<ICvBase>) container

//        [<Fact>]
//        let ``hasOneItem`` () =
//            let c = Dictionary.count container
//            Assert.Equal(1, c)

//        [<Fact>]
//        let ``HasValue`` () =           
//            Assert.True (Dictionary.containsKey name container)

//        [<Fact>]
//        let ``CanRetrieve`` () =
//            let v = CvContainer.getManyAs<CvParam> name container
//            Assert.Equal<CvParam>(correctParams, v)

//module ExtendedContainer = 
    
//    let version = "0.0.1"
//    let protocol = Protocol()

//    do protocol.Version <- version

//    [<Fact>]
//    let ``hasOneItem`` () =
//        let c = Dictionary.count protocol
//        Assert.Equal(1, c)

//    [<Fact>]
//    let ``HasValue`` () =           
//        Assert.True (Dictionary.containsKey Protocol.VersionProperty protocol)

//    [<Fact>]
//    let ``CanRetrieve`` () =
//        let v = protocol.Version
//        Assert.Equal(version, v)