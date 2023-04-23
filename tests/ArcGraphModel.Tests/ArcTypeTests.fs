module ArcTypeTests

open ArcGraphModel.ArcType
#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto
#endif


[<AutoOpen>]
module private ArcType =

    let bbt_Source = Source
    let bbt_Sample = Sample
    let bbt_RawDataFile = RawDataFile
    let bbt_DerivedDataFile = DerivedDataFile
    let bbt_Factor = Factor
    let bbt_Parameter = Parameter
    let bbt_Characteristic = Characteristic
    let bbt_Component = Component
    let bbt_ProtocolType = ProtocolType
    let bbt_ProtocolREF = ProtocolREF
    let bbt_Data = Data


let private IsInputColumn = testList "IsInputColumn" [
    testCase "Source is true" (fun _ ->
        Expect.isTrue bbt_Source.IsInputColumn ""
    )
    testCase "Sample is false" (fun _ ->
        Expect.isFalse bbt_Sample.IsInputColumn ""
    )
    testCase "Factor is false" (fun _ ->
        Expect.isFalse bbt_Factor.IsInputColumn ""
    )
]

let private IsOutputColumn = testList "IsOutputColumn" [
    testCase "Source is false" (fun _ ->
        Expect.isFalse bbt_Source.IsOutputColumn ""
    )
    testCase "Sample is true" (fun _ ->
        Expect.isTrue bbt_Sample.IsOutputColumn ""
    )
    testCase "RawDataFile is true" (fun _ ->
        Expect.isTrue bbt_RawDataFile.IsOutputColumn ""
    )
    testCase "DerivedDataFile is true" (fun _ ->
        Expect.isTrue bbt_DerivedDataFile.IsOutputColumn ""
    )
    testCase "Characteristic is false" (fun _ ->
        Expect.isFalse bbt_Characteristic.IsOutputColumn ""
    )
]

let private IsTermColumn = testList "IsTermColumn" [
    testCase "Source is false" (fun _ ->
        Expect.isFalse bbt_Source.IsTermColumn ""
    )
    testCase "Sample is false" (fun _ ->
        Expect.isFalse bbt_Sample.IsTermColumn ""
    )
    testCase "Factor is true" (fun _ ->
        Expect.isTrue bbt_Factor.IsTermColumn ""
    )
    testCase "Parameter is true" (fun _ ->
        Expect.isTrue bbt_Parameter.IsTermColumn ""
    )
    testCase "Characteristic is true" (fun _ ->
        Expect.isTrue bbt_Characteristic.IsTermColumn ""
    )
    testCase "Component is true" (fun _ ->
        Expect.isTrue bbt_Component.IsTermColumn ""
    )
]

let private IsFeaturedColumn = testList "IsFeaturedColumn" [
    testCase "Source is false" (fun _ ->
        Expect.isFalse bbt_Source.IsFeaturedColumn ""
    )
    testCase "Sample is false" (fun _ ->
        Expect.isFalse bbt_Sample.IsFeaturedColumn ""
    )
    testCase "Factor is false" (fun _ ->
        Expect.isFalse bbt_Factor.IsFeaturedColumn ""
    )
    testCase "Parameter is false" (fun _ ->
        Expect.isFalse bbt_Parameter.IsFeaturedColumn ""
    )
    testCase "Characteristic is false" (fun _ ->
        Expect.isFalse bbt_Characteristic.IsFeaturedColumn ""
    )
    testCase "Component is false" (fun _ ->
        Expect.isFalse bbt_Component.IsFeaturedColumn ""
    )
    testCase "ProtocolType is true" (fun _ ->
        Expect.isTrue bbt_ProtocolType.IsFeaturedColumn ""
    )
]

let private IsDeprecated = testList "IsDeprecated" [
    testCase "Source is false" (fun _ ->
        Expect.isFalse bbt_Source.IsDeprecated ""
    )
    testCase "Data is true" (fun _ ->
        Expect.isTrue bbt_Data.IsDeprecated ""
    )
]

let private TryOfString = testList "TryOfString" [
    testCase "Freetext isSome" (fun _ ->
        Expect.isTrue (BuildingBlockType.tryOfString "test").IsSome ""
    )
    testCase "creates Freetext correctly" (fun _ ->
        Expect.equal (Freetext "test" |> Some) (BuildingBlockType.tryOfString "test") ""
    )
    testCase "creates Source correctly" (fun _ ->
        Expect.equal (Some Source) (BuildingBlockType.tryOfString "Source Name") ""
    )
    testCase "creates Sample correctly" (fun _ ->
        Expect.equal (Some Sample) (BuildingBlockType.tryOfString "Sample Name") ""
    )
    testCase "creates RawDataFile correctly" (fun _ ->
        Expect.equal (Some RawDataFile) (BuildingBlockType.tryOfString "Raw Data File") ""
    )
    testCase "creates DerivedDataFile correctly" (fun _ ->
        Expect.equal (Some DerivedDataFile) (BuildingBlockType.tryOfString "Derived Data File") ""
    )
    testCase "creates Parameter correctly" (fun _ ->
        Expect.equal (Some Parameter) (BuildingBlockType.tryOfString "Parameter") "Parameter"
        Expect.equal (Some Parameter) (BuildingBlockType.tryOfString "Parameter Value") "Parameter Value"
    )
    testCase "creates Characteristic correctly" (fun _ ->
        Expect.equal (Some Characteristic) (BuildingBlockType.tryOfString "Characteristic") "Characteristic"
        Expect.equal (Some Characteristic) (BuildingBlockType.tryOfString "Characteristics") "Characteristics"
        Expect.equal (Some Characteristic) (BuildingBlockType.tryOfString "Characteristics Value") "Characteristics Value"
    )
    testCase "creates Factor correctly" (fun _ ->
        Expect.equal (Some Factor) (BuildingBlockType.tryOfString "Factor") "Factor"
        Expect.equal (Some Factor) (BuildingBlockType.tryOfString "Factor Value") "Factor Value"
    )
    testCase "creates Component correctly" (fun _ ->
        Expect.equal (Some Component) (BuildingBlockType.tryOfString "Component") ""
    )
    testCase "creates Data correctly" (fun _ ->
        Expect.equal (Some Data) (BuildingBlockType.tryOfString "Data File Name") ""
    )
    testCase "creates ProtocolType correctly" (fun _ ->
        Expect.equal (Some ProtocolType) (BuildingBlockType.tryOfString "Protocol Type") ""
    )
    testCase "creates ProtocolREF correctly" (fun _ ->
        Expect.equal (Some ProtocolREF) (BuildingBlockType.tryOfString "Protocol REF") ""
    )
]

let main =
    testList "ArcTypeTests" [
        IsInputColumn
        IsOutputColumn
        IsTermColumn
        IsFeaturedColumn
        IsDeprecated
        TryOfString
    ]

