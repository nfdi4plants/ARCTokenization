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
    let testBuildingBlock7 = Characteristics
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
        let ``Characteristics is false`` () =
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
        let ``Characteristics is true`` () =
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


    module ToNodeType =

        [<Fact>]
        let ``Source parses to Source`` () =
            Assert.Equal(NodeType.Source, testBuildingBlock1.ToNodeType())

        [<Fact>]
        let ``Sample parses to Sink ()`` =
            Assert.Equal(Sink, testBuildingBlock2.ToNodeType())

        [<Fact>]
        let ``RawDataFile parses to Sink`` () =
            Assert.Equal(Sink, testBuildingBlock3.ToNodeType())

        [<Fact>]
        let ``DerivedDataFile parses to Sink`` () =
            Assert.Equal(Sink, testBuildingBlock4.ToNodeType())

        [<Fact>]
        let ``ProtocolREF parses to ProtocolRef`` () =
            Assert.Equal(ProtocolRef, testBuildingBlock10.ToNodeType())

        [<Fact>]
        let ``Factor fails`` () =
            Assert.Throws<System.Exception> (fun _ -> testBuildingBlock5.ToNodeType() |> ignore)