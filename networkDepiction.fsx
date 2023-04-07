//open System.IO
//open System.Collections.Generic

#r "nuget: DocumentFormat.OpenXml"
#r "nuget: FSharpAux"
#r "nuget: FsSpreadsheet.ExcelIO"
#r "nuget: FSharp.FGL.ArrayAdjacencyGraph"

open DocumentFormat.OpenXml
open FSharpAux
open FsSpreadsheet
open FSharp.FGL
open FSharp.FGL.ArrayAdjacencyGraph

#I @"src\ArcGraphModel\bin\Release\net6.0"
#r "ArcGraphModel.dll"



open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open FsSpreadsheet.DSL
open ArcGraphModel
open ArcGraphModel.Param

open System

let generateHash() = System.Guid.NewGuid().ToString()
let rnd = System.Random()

let extendToLength (l : 'V list) (length : int) =
    let indices = 
        [for i = 1 to length - l.Length do rnd.Next(0,l.Length)]
        |> List.countBy id 
        |> Map.ofList
    l
    |> List.indexed
    |> List.collect (fun (i,item) -> 
        match indices.TryFind(i) with
        | Option.Some n -> [for i = 1 to n+1 do item]
        | Option.None -> [item]
    )

let randomAlign (l1 : 'T list) (l2 : 'U list) : ('T*'U) list =   
    if l1.Length = l2.Length then List.zip l1 l2
    elif l1.Length < l2.Length then List.zip (extendToLength l1 l2.Length) l2
    else List.zip l1 (extendToLength l2 l1.Length)


type ArcGraph = ArrayAdjacencyGraph<string,ICvBase,ICvBase>

module Terms = 
    let assay                   : CvTerm = "ARC_00000150",                             "Assay",                ""
    let assayMeasurementType    : CvTerm = "ARC_1a2d506f_b67d_4f60_adb5_410418a287c8", "AssayMeasurementType", ""
    let assayTechnologyType     : CvTerm = "ARC_c68a2f6a_45d3_43c2_b7c9_aecf452675fd", "AssayTechnologyType",  ""
    let sample                  : CvTerm = "ARC_00002133",                             "Sample",                ""
    let data                    : CvTerm = "ARC_00002134",                             "Data",                ""

let pickUnit = 
    let cs = 
        [|
            for i = 0 to 30 do
                generateHash(),$"category_{i}",""
        |]
    fun () -> cs.[rnd.Next(0,30)]

let pickCategory = 
    let cs = 
        [|
            for i = 0 to 1000 do
                generateHash(),$"category_{i}",""
        |]
    fun () -> cs.[rnd.Next(0,1000)]

let randomParam(category : CvTerm) =
    match rnd.Next(0,3) with
    | 0 -> CvParam.fromValue(category,rnd.Next(0,1000))
    | 1 -> CvParam.fromCategory(category,pickCategory())
    | _ -> CvParam.fromValueWithUnit(category,rnd.Next(0,1000),pickUnit())


let getSheets minSamplesPerSheet maxSamplesPerSheet minHeadersPerSheet maxHeadersPerSheet numSheets = 
    //let minSamplesPerSheet = 5
    //let maxSamplesPerSheet = 10

    //let minHeadersPerSheet = 5
    //let maxHeadersPerSheet = 10

    //let numSheets = 100

    let generateSheet (inputSamples : LVertex<string,#ICvBase> list) = 
        let categories =
            [
                for i = 0 to rnd.Next(minHeadersPerSheet, maxHeadersPerSheet) do
                    pickCategory()
            ]
        let protocolName = generateHash()
        let outputSamples : LVertex<string,ICvBase> list =
            let hash = generateHash()         
            [
                for i = 0 to rnd.Next(minSamplesPerSheet, maxSamplesPerSheet) do
                    let hash = generateHash()
                    (hash,CvObject(Terms.sample,hash) :> ICvBase)
            ]
        let edges : LEdge<string,ICvBase> list = 
            randomAlign inputSamples outputSamples
            |> List.collect (fun ((v1,_),(v2,_)) ->
                categories
                |> List.map (fun category ->
                    v1,v2,randomParam category                             
                )
            )
        outputSamples,edges

    let rec loop i (sheets : (LVertex<string,ICvBase> list*LEdge<string,ICvBase> list) list) =
        if sheets.Length = i then 
            sheets
        else 
            let randomSheet,_ = sheets.[rnd.Next(0,sheets.Length)]
            let newSheet = generateSheet randomSheet
            loop i (List.append sheets [newSheet])

    let startSamples : LVertex<string,ICvBase> list =    
        [
            for i = 0 to rnd.Next(minSamplesPerSheet, maxSamplesPerSheet) do
                let hash = generateHash()
                (hash,CvObject(Terms.sample,hash) :> ICvBase)
        ]

    loop numSheets ([startSamples,[]])

let x = 

    let sheets = 
        getSheets 5 30 5 30 100

    let vertices,edges = 
        List.collect fst sheets, List.collect snd sheets

    let network : ArcGraph = 
    
        ArrayAdjacencyGraph(vertices,edges)

    vertices.Length
    edges.Length

x


// let sheets = getSheets 5 30 5 30 1000
// Real: 00:03:32.515, CPU: 00:03:09.343, GC gen0: 111, gen1: 22, gen2: 1
// val network: ArcGraph
// val it: int = 18169
// val it: int = 404467
network.GetVertices()
network.GetLabel("5cec766a-860b-47de-a197-ab41d69c423d")
network.GetInEdges("5cec766a-860b-47de-a197-ab41d69c423d")