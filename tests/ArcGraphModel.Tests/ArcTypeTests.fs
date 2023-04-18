namespace ArcGraphModel.Tests

open Xunit
open ArcGraphModel.ArcType


module ArcType =

    let testBuildingBlock1 = Source
    let testBuildingBlock2 = Sample
    let testBuildingBlock3 = RawDataFile
    let testBuildingBlock4 = DerivedDataFile
    let testBuildingBlock5 = Factor
    let testBuildingBlock6 = Parameter
    let testBuildingBlock7 = Characteristic
    let testBuildingBlock8 = Component
    let testBuildingBlock9 = ProtocolType
    let testBuildingBlock10 = ProtocolREF
    let testBuildingBlock11 = Data

    module IsInputColumn =

        [<Fact>]
        let ``Source is true`` () =
            Assert.True testBuildingBlock1.IsInputColumn

        [<Fact>]
        let ``Sample is false`` () =
            Assert.False testBuildingBlock2.IsInputColumn

        [<Fact>]
        let ``Factor is false`` () =
            Assert.False testBuildingBlock5.IsInputColumn


    module IsOutputColumn =

        [<Fact>]
        let ``Source is false`` () =
            Assert.False testBuildingBlock1.IsOutputColumn

        [<Fact>]
        let ``Sample is true`` () =
            Assert.True testBuildingBlock2.IsOutputColumn

        [<Fact>]
        let ``RawDataFile is true`` () =
            Assert.True testBuildingBlock3.IsOutputColumn

        [<Fact>]
        let ``DerivedDataFile is true`` () =
            Assert.True testBuildingBlock4.IsOutputColumn

        [<Fact>]
        let ``Characteristic is false`` () =
            Assert.False testBuildingBlock7.IsOutputColumn


    module IsTermColumn =

        [<Fact>]
        let ``Source is false`` () =
            Assert.False testBuildingBlock1.IsTermColumn

        [<Fact>]
        let ``Sample is false`` () =
            Assert.False testBuildingBlock2.IsTermColumn

        [<Fact>]
        let ``Factor is true`` () =
            Assert.True testBuildingBlock5.IsTermColumn

        [<Fact>]
        let ``Parameter is true`` () =
            Assert.True testBuildingBlock6.IsTermColumn

        [<Fact>]
        let ``Characteristic is true`` () =
            Assert.True testBuildingBlock7.IsTermColumn

        [<Fact>]
        let ``Component is true`` () =
            Assert.True testBuildingBlock8.IsTermColumn


    module IsFeaturedColumn =

        [<Fact>]
        let ``ProtocolType is true`` () =
            Assert.True testBuildingBlock9.IsFeaturedColumn


    module IsDeprecated =

        [<Fact>]
        let ``Data is true`` () =
            Assert.True testBuildingBlock11.IsDeprecated

        [<Fact>]
        let ``Source is false`` () =
            Assert.False testBuildingBlock1.IsDeprecated


    //module ToNodeType =

    //    [<Fact>]
    //    let ``Source parses to Source`` () =
    //        Assert.Equal(NodeType.Source, testBuildingBlock1.ToNodeType())

    //    [<Fact>]
    //    let ``Sample parses to Sink ()`` =
    //        Assert.Equal(Sink, testBuildingBlock2.ToNodeType())

    //    [<Fact>]
    //    let ``RawDataFile parses to Sink`` () =
    //        Assert.Equal(Sink, testBuildingBlock3.ToNodeType())

    //    [<Fact>]
    //    let ``DerivedDataFile parses to Sink`` () =
    //        Assert.Equal(Sink, testBuildingBlock4.ToNodeType())

    //    [<Fact>]
    //    let ``ProtocolREF parses to ProtocolRef`` () =
    //        Assert.Equal(ProtocolRef, testBuildingBlock10.ToNodeType())

    //    [<Fact>]
    //    let ``Factor fails`` () =
    //        Assert.Throws<System.Exception> (fun _ -> testBuildingBlock5.ToNodeType() |> ignore)


    module tryOfString =

        [<Fact>]
        let ``is Some`` () =
            Assert.True (BuildingBlockType.tryOfString "test").IsSome

        [<Fact>]
        let ``creates Freetext correctly`` () =
            Assert.Equal(Freetext "test" |> Some, BuildingBlockType.tryOfString "test")

        [<Fact>]
        let ``creates Source correctly`` () =
            Assert.Equal(Some Source, BuildingBlockType.tryOfString "Source Name")

        [<Fact>]
        let ``creates Sample correctly`` () =
            Assert.Equal(Some Sample, BuildingBlockType.tryOfString "Sample Name")

        [<Fact>]
        let ``creates RawDataFile correctly`` () =
            Assert.Equal(Some RawDataFile, BuildingBlockType.tryOfString "Raw Data File")

        [<Fact>]
        let ``creates DerivedDataFile correctly`` () =
            Assert.Equal(Some DerivedDataFile, BuildingBlockType.tryOfString "Derived Data File")

        [<Fact>]
        let ``creates Parameter correctly`` () =
            Assert.Equal(Some Parameter, BuildingBlockType.tryOfString "Parameter")
            Assert.Equal(Some Parameter, BuildingBlockType.tryOfString "Parameter Value")

        [<Fact>]
        let ``creates Characteristic correctly`` () =
            Assert.Equal(Some Characteristic, BuildingBlockType.tryOfString "Characteristic")
            Assert.Equal(Some Characteristic, BuildingBlockType.tryOfString "Characteristics")
            Assert.Equal(Some Characteristic, BuildingBlockType.tryOfString "Characteristics Value")

        [<Fact>]
        let ``creates Factor correctly`` () =
            Assert.Equal(Some Factor, BuildingBlockType.tryOfString "Factor")
            Assert.Equal(Some Factor, BuildingBlockType.tryOfString "Factor Value")

        [<Fact>]
        let ``creates Component correctly`` () =
            Assert.Equal(Some Component, BuildingBlockType.tryOfString "Component")

        [<Fact>]
        let ``creates Data correctly`` () =
            Assert.Equal(Some Data, BuildingBlockType.tryOfString "Data File Name")

        [<Fact>]
        let ``creates ProtocolType correctly`` () =
            Assert.Equal(Some ProtocolType, BuildingBlockType.tryOfString "Protocol Type")

        [<Fact>]
        let ``creates ProtocolREF correctly`` () =
            Assert.Equal(Some ProtocolREF, BuildingBlockType.tryOfString "Protocol REF")