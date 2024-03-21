#r "nuget: FSharpAux"
//#r "nuget: FsOboParser"
#r "nuget: OBO.NET"
#r "nuget: FsSpreadsheet.ExcelIO, 4.1.0"

#I "src/ControlledVocabulary/bin/Debug/netstandard2.0"
#I "src/ControlledVocabulary/bin/Release/netstandard2.0"
#r "ControlledVocabulary.dll"
#I "src/ARCTokenization/bin/Debug/netstandard2.0"
#I "src/ARCTokenization/bin/Release/netstandard2.0"
#r "ARCTokenization.dll"

open FsSpreadsheet
open FsSpreadsheet.ExcelIO
//open FsOboParser
open OBO.NET
//open FsSpreadsheet.DSL
open ControlledVocabulary
open type ControlledVocabulary.ParamBase
open ARCTokenization
open ARCTokenization.StructuralOntology
open System.IO


let arcProt = @"C:\Repos\git.nfdi4plants.org\ArcPrototype"

let afts = FileSystem.parseAbsoluteFilePaths arcProt
afts |> Seq.iter (Param.getValueAsString >> printfn "%s")

let tryParseMetadataSheetFromToken (isaFileName: string) (isaMdsParsingF: string -> IParam list) (absFileToken: IParam) =
    let cvpStr = Param.getValueAsString absFileToken
    printfn $"cvpStr: {cvpStr}"
    //if String.contains isaFileName cvpStr then
    if Path.GetFileName cvpStr = isaFileName then
        try 
            Some (isaMdsParsingF cvpStr)
        with _ -> 
            None
    else None

afts 
|> Seq.map (
    fun cvp -> 
        printfn $"{Param.getValueAsString cvp}"
        tryParseMetadataSheetFromToken "isa.investigation.xlsx" (Investigation.parseMetadataSheetFromFile()) cvp
)
|> Seq.length

let its = Investigation.parseMetadataSheetsFromTokens() afts




let ot = List.head Terms.InvestigationMetadata.ontology.Terms

ARCTokenization.StructuralOntology.CodeGeneration.toCodeString ot
CodeGeneration.toSourceCode "Investigation" Terms.InvestigationMetadata.ontology

System.IO.Directory.GetCurrentDirectory()
let fakePath = CvParam(cvTerm = AFSO.``File Path``, v = System.IO.Directory.GetCurrentDirectory() + "/tests/ARCTokenization.Tests/Fixtures/correct/investigation_simple.xlsx")

let fakePath = CvParam(cvTerm = AFSO.``File Path``, v = "tests/ARCTokenization.Tests/Fixtures/correct/assay_simple.xlsx")
let actual = ParamBasedParsers.parseIsaMetadataSheetFromCvp "assay_simple.xlsx" Assay.parseMetadataSheetFromFile [fakePath] |> Seq.head
actual.Length
let exp =
    ARCMock.AssayMetadataTokens(
        Assay_File_Name = [@"measurement1\isa.assay.xlsx"],
        Assay_Performer_First_Name = ["Oliver"; "Marius"],
        Assay_Performer_Last_Name = ["Maus"; "Katz"],
        Assay_Performer_Mid_Initials = [""; "G."],
        Assay_Performer_Email = ["maus@nfdi4plants.org"],
        Assay_Performer_Affiliation = ["RPTU University of Kaiserslautern"],
        Assay_Performer_Roles = ["research assistant"],
        Assay_Performer_Roles_Term_Accession_Number = ["http://purl.org/spar/scoro/research-assistant"],
        Assay_Performer_Roles_Term_Source_REF = ["scoro"]
     )
    |> List.concat
exp.Length
actual |> List.fold (fun acc ip -> $"{acc}\n{ip.Name}") "" |> printfn "%s"
exp |> List.iter (fun ip -> printfn $"{ip.Name}")
for i = 0 to 33 do
    printfn $"{List.tryItem i actual |> Option.map (fun x -> x.Name) |> Option.defaultValue System.String.Empty}\t{List.tryItem i exp |> Option.map (fun x -> x.Name) |> Option.defaultValue System.String.Empty}"

let testAccession1 = "TO:00000001"
let testName1 = "Test"
let testRef1 = "TO"

let testTerm1 = CvTerm.create(accession = testAccession1, name = testName1, ref = testRef1)

let testAccession2 = "TO:00000002"
let testName2 = "5"
let testRef2 = "TO"

let testTerm2 = CvTerm.create(accession = testAccession2, name = testName2, ref = testRef2)

let ``CvParam with ParamValue.Value`` = CvParam(testTerm1, ParamValue.Value 5)
let ``CvParam with ParamValue.CvValue`` = CvParam(testTerm1, ParamValue.CvValue testTerm2)
let ``CvParam with ParamValue.WithCvUnitAccession`` = CvParam(testTerm2, ParamValue.WithCvUnitAccession (5, testTerm1))

let testCvParams = 
    [
        ``CvParam with ParamValue.Value``
        ``CvParam with ParamValue.CvValue``
        ``CvParam with ParamValue.WithCvUnitAccession``
    ]
let testCvp1 = CvParam("test", "test", "test", ParamValue.Value "test", Dictionary<string,IParam>() |> fun d -> d.Add("test", testCvParams.Head); d) 
let testCvp2 = CvParam("test", "test", "test", ParamValue.Value "test", Dictionary<string,IParam>() |> fun d -> d.Add("test", testCvParams.Head); d) 
let actual = testCvp1 = testCvp2
testCvp1.GetHashCode()
testCvp2.GetHashCode()



let expectedTermValuesSimple = 
    [
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""; "iid"]
        [""; "ititle"]
        [""; "idesc"]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""; "Maus"; "Keider"; "müller"; "oih"]
        [""; "Oliver"; "andreas"]
        [""; "L. I."; "C."]
        [""; "maus@nfdi4plants.org"]
        [""]
        [""]
        [""]
        [""; "Affe"]
        [""]
        [""]
        [""]
        [""]
        [""; "sid"]
        [""; "stitle"]
        [""; "sdesc"]
        [""]
        [""]
        [""; "sid\isa.study.xlsx"]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""; "aid\isa.assay.xlsx"; "aid2\isa.assay.xlsx"]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""; "weil"]
        [""; "lukas"]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
        [""]
    ]

let allExpectedMetadataTermsFull =
    Terms.InvestigationMetadata.cvTerms
    |> List.skip 1 //(ignore root term)
    |> List.zip expectedTermValuesSimple
    |> List.map (fun (values,term) ->
        values
        |> List.mapi (fun i v ->
            if i = 0 then
                CvParam(term, ParamValue.CvValue (CvTerm("AGMO:00000001", "Metadata Section Key", "AGMO")), [])
            else
                CvParam(term, ParamValue.Value v, [])
        )
    )
    |> List.concat
allExpectedMetadataTermsFull.Length

let parsedInvestigationMetadataSimple = Investigation.parseMetadataSheetFromFile (__SOURCE_DIRECTORY__ + "/tests/ArcGraphModel.Tests/Fixtures/correct/investigation_simple.xlsx")
parsedInvestigationMetadataSimple.Length

let i_fs = FsWorkbook.fromXlsxFile (__SOURCE_DIRECTORY__ + "/tests/ArcGraphModel.Tests/Fixtures/correct/investigation_simple.xlsx")

(FsWorkbook.getWorksheetByName "isa_investigation" i_fs).CellCollection.GetCells()
|> Seq.filter(fun c -> c.RowNumber = 1)



parsedInvestigationMetadataSimple
|> List.skip 10
|> List.take 10

// Assay annotation table parsing

let assayTokens = Assay.parseAnnotationTablesFromFile (__SOURCE_DIRECTORY__ + "/tests/ArcGraphModel.Tests/Fixtures/correct/assay_with_single_characteristics.xlsx")

// Investigation metadata parsing

let investigationTokens = Investigation.parseMetadataSheetfromXlsxFile (__SOURCE_DIRECTORY__ + "/tests/ArcGraphModel.Tests/Fixtures/correct/full_investigation_mkay.xlsx")

//let inves = FsWorkbook.fromXlsxFile @"C:\Users\revil\OneDrive\CSB-Stuff\NFDI\testARC30\isa.investigation.xlsx"
//let inves = FsWorkbook.fromXlsxFile @"C:\Users\olive\OneDrive\CSB-Stuff\NFDI\testARC30\isa.investigation.xlsx"
let inves = FsWorkbook.fromXlsxFile (__SOURCE_DIRECTORY__ + "/tests/ArcGraphModel.Tests/Fixtures/correct/full_investigation_mkay.xlsx")

let invesWs = FsWorkbook.getWorksheets inves |> Seq.head
invesWs.RescanRows()
invesWs.CellCollection
invesWs.Rows[20] |> Seq.toList

//let invesWsParsed = ArcGraphModel.IO.Worksheet.parseRowsAggregated invesWs
//let invesWsParsed = ArcGraphModel.IO.Worksheet.parseColumnsAggregated invesWs
let invesWsParsed = ArcGraphModel.Worksheet.parseRowsFlat invesWs
//invesWsParsed |> Seq.cast<CvParam>
invesWsParsed |> Seq.cast<IParamBase> |> Seq.cast<CvParam>
//invesWsParsed |> List.map (fun x -> x :> CvParam)
invesWsParsed
//|> List
|> List.mapi (
    fun i x ->
        printfn "\n%i\n" i
        match x with
        | :? CvParam as p -> 
            printfn "CvParam"
            CvBase.getCvName p |> printfn "CvName: %s"
            Param.getValue p |> printfn "Value: %A"
            //p.ToString()
            p.Attributes.ToString() |> printfn "Attributes: %A"
        //| :? UserParam as p -> 
        //    printfn "UserParam"
        //    CvBase.getCvName p |> printfn "CvName: %s"
        //    Param.getValue p |> printfn "Value: %A"
        //    //p.ToString()
        //    p.Attributes.ToString() |> printfn "Attributes: %A"
        | _ -> ()
)

invesWsParsed
invesWsParsed |> List.iter ((printfn "%A"))
invesWsParsed.Head |> printfn "%A"
invesWsParsed.Head |> printfn "%O"
(invesWsParsed.Head :?> CvAttributeCollection)
invesWsParsed.Head.ToString()

[1 .. 10] |> printfn "%A"
[1 .. 10] |> printfn "%O"

open System.Text.RegularExpressions

let namePattern = @"(?<=\[).*(?=[\]])"
let key = "Comment[<Investigation Person ORCID>]"
Regex.tryParseValue namePattern key
|> Option.map (fun n ->
    CvTerm("",n,"")
)
|> Option.get
|> fun term -> CvParam(term,ParamValue.Value "Hallo",[])

let lol = CvParam("lol", "lol", "lol", ParamValue.Value "lol")
let lol2 = CvParam("lol", "lol", "lol", ParamValue.Value "lol")

lol = lol2

let dict1 = Dictionary<string,string>()
dict1.Add("kek", "lil")
let dict2 = Dictionary<string,string>()
dict2.Add("kek", "lil")

dict1 = dict2


// new CvTypes - testin' and foolin' around

//type CvParam(cvAccession : string, cvName : string, cvRefUri : string, paramValue : ParamValue, qualifiers : IDictionary<string,CvParam>) =

//    inherit Dictionary<string,CvParam>(qualifiers)

//    let mutable _cvContainers = Dictionary<string,CvContainer list>()

//    interface ICvBase with 
//        member this.ID     = cvAccession
//        member this.Name   = cvName
//        member this.RefUri = cvRefUri
//    interface IParamBase with 
//        member this.Value  = paramValue

//    new (id,name,ref,pv,qualifiers : seq<CvParam>) = 
//        let dict = 
//            qualifiers
//            |> Seq.map (fun cvp -> (cvp :> ICvBase).Name, cvp)
//            |> Dictionary.ofSeq
//        CvParam (id,name,ref,pv,dict)
//    new (id,name,ref,pv) = 
//        CvParam (id,name,ref,pv,Seq.empty)

//    new ((id,name,ref) : CvTerm,pv,qualifiers : seq<CvParam>) = 
//        CvParam (id,name,ref,pv,qualifiers)
//    new (cvTerm,pv : ParamValue) = 
//        CvParam (cvTerm,pv,Seq.empty)

//    member this.CvContainers
//        with private get() = _cvContainers
//        //with get(item) = _cvContainers[item]
//        and private set(item, value) = _cvContainers[item] <- value

//    member this.CvContainers.Item item =
//        this.CvContainers[item]

//    static member addCvContainerItem (item : CvContainer list) (cvParam : CvParam) =
//        cvParam.CvContainers.Add(item.Head |> CvBase.getCvName, item)


//// working backwards

//let tryAddVertex vertex graph =
//    try ArrayAdjacencyGraph.Vertices.add vertex graph
//    with _ -> graph

//let tryAddEdge vertex1 vertex2 edge graph =
//    try ArrayAdjacencyGraph.Edges.add (vertex1, vertex2, edge) graph
//    with _ -> graph

//let cvParamsToGraph (cvParams : CvParam list) = 
//    cvParams
//    |> List.fold (
//        fun graph cvp ->
//            let label = 
//                cvp :> IParamBase 
//                |> ParamBase.getValue :?> IParamBase
//                |> ParamBase.getValue :?> CvParam
//            match BuildingBlockType.tryOfString (cvp :> ICvBase).Name with
//            | Some Sample
//            | Some RawDataFile
//            | Some DerivedDataFile
//            | Some Source -> 
//                let vertexId = 
//                    label :> IParamBase 
//                    |> ParamBase.getValue :?> ICvBase
//                    |> CvBase.getCvName
//                let vertex = LVertex (vertexId, label)
//                tryAddVertex vertex graph
//            | Some Parameter
//            | Some Characteristic
//            | Some Factor
//            | Some Component ->
//                let vertex1id = 
//                    cvp["Address"] // string * int * int
//                tryAddEdge 
//            //| Some (Freetext e) ->
//            //    let vertex1id = 
//            //        cvp["Address"] // string * int * int
//            //    tryAddEdge 
//    ) (Graph.create [] [])




//let fp = @"C:\Users\olive\OneDrive\CSB-Stuff\NFDI\testARC30\assays\aid\isa.assay.xlsx"
////let fp = @"C:\Users\revil\OneDrive\CSB-Stuff\NFDI\testARC30\assays\aid\isa.assay.xlsx"
//let wb = FsWorkbook.fromXlsxFile fp
//let shts = FsWorkbook.getWorksheets wb

////let mutable testDic = new Dictionary<string,int>()
////testDic.Add("first", 24)
////testDic.Add("second", 1337)
////testDic.Add("third", 69)
////let oldTestDic = testDic
////testDic <- new Dictionary<string,int>()
////let testDic2 = new Dictionary<int,Dictionary<int,string>>()
////let testDic3 = new Dictionary<int,string>()
////testDic3.Add(3,"sheesh")
////testDic3.Add(4,"shnash")
////testDic2.Add(2,testDic3)
////testDic2.Add(6,testDic3)
////testDic2.Values |> Seq.minBy (fun d -> d.Keys |> Seq.min) |> fun d -> d.Keys |> Seq.min

//let tbls = FsWorkbook.getTables wb
//let tblsFiltered = tbls |> List.filter (fun t -> String.contains "annotationTable" t.Name)
//let tbl1 = tblsFiltered.Head
//let associatedWorksheet = shts.Head
//let fcc = associatedWorksheet.CellCollection
////tbl1.Field("Source Name", fcc).DataCells(fcc, false) |> Seq.length
////tbl1.Field("Source Name", fcc).DataCells(fcc, true)
////tbl1.RescanRange()
////tbl1.Field("test", fcc).DataCells(fcc, false)
////tbl1.FieldNames

///// Takes an FsWorkbook and returns all Annotation Tables it contains.
//let getAnnotationTables workbook =
//    let tables = FsWorkbook.getTables workbook
//    tables |> List.filter (fun t -> String.contains "annotationTable" t.Name)

//(getAnnotationTables wb).Head.CellsCollection.Count

//let getColumnHeaders (table : FsTable) =
//    let range = table.HeadersRow()
//    range.RangeAddress
//    table.GetHeaderCell

////let ioToCvParam headerCell dataCell =
    

////let buildingBlockToCvParam headerCell dataCells =


//let columnHeadersRowAddress = tbl1.HeadersRow().RangeAddress.FirstAddress.RowNumber
//let columnHeaders = associatedWorksheet.CellCollection.GetCellsInRow columnHeadersRowAddress |> List.ofSeq
//let nodeHeaders, edgeHeaders = columnHeaders |> List.partition (fun c -> List.contains c.Value nodeColumnNames)
//let groupedEdgeHeaders = edgeHeaders |> Seq.groupWhen (fun h -> String.contains "[" h.Value) |> List.ofSeq |> List.map List.ofSeq
////let nodeHeaders = columnHeaders |> List.filter (fun ch -> List.contains ch.Value nodeColumnNames)
////let groupedHeadersStr = 

////let ftf = FsTableField()
////let header1 = tbl1.Field("Source Name", fcc)
////header1.DataCells(fcc, true)
////header1.DataCells(fcc, false) |> Seq.length
////header1.Index
////let header2 = tbl1.Field("Parameter [Protocol]", fcc)
////header2.Index
////tbl1.RescanRange()
////tbl1.FieldNames(obj)


////parse true (groupedHeaders |> Array.collect (Array.map (fun c -> c.Value)) |> List.ofArray)
////groupedHeaders |> Array.map (parse true (groupedHeaders |> Array.collect (Array.map (fun c -> c.Value)) |> List.ofArray))

////// for FsCells & CvParams
////type BuildingBlock<'a> =
////    | Triple of ('a * 'a * 'a)
////    | Quadruple of ('a * 'a * 'a * 'a)


//let parsedNodes = List.map (parseNode fcc) nodeHeaders
//let parsedEdges = List.map (parseEdges true fcc) groupedEdgeHeaders
//let nodeTypes = parsedNodes |> List.map (List.map getNodeType)
//parsedNodes.Head.Head |> ArcGraphModel.ParamBase.getValue
//parsedNodes.Head[1] |> ArcGraphModel.ParamBase.getValue
//parsedNodes.Length
//parsedNodes.Head.Length
//parsedEdges.Head.Head |> ArcGraphModel.CvBase.getCvAccession
//parsedEdges.Head.Head |> ArcGraphModel.ParamBase.getValue
//parsedEdges.Head[1] |> ArcGraphModel.ParamBase.getValue
//parsedEdges |> List.map (List.map ParamBase.getValue)
//parsedEdges |> List.map (List.map CvBase.getCvName)



//let sourceNodes, sinkNodes, protocolRefNodes = separateNodes (List.concat parsedNodes)


//let t2 = tbls.Item 1
//let assWs2 = FsTable.getWorksheetOfTable wb t2
//let fcc2 = assWs2.CellCollection
//let columnHeadersRowAddress2 = t2.HeadersRow().RangeAddress.FirstAddress.RowNumber
//let columnHeaders2 = fcc2.GetCellsInRow columnHeadersRowAddress2 |> List.ofSeq
//let nodeHeaders2, edgeHeaders2 = columnHeaders2 |> List.partition (fun c -> List.contains c.Value nodeColumnNames)
//let groupedEdgeHeaders2 = edgeHeaders2 |> Seq.groupWhen (fun h -> String.contains "[" h.Value) |> List.ofSeq |> List.map List.ofSeq
//let parsedEdges2 = groupedEdgeHeaders2 |> List.map (parseEdges true fcc2)
////let nodeHeaders2 = columnHeaders2 |> List.filter (fun ch -> List.contains ch.Value nodeColumnNames)
//let parsedNodes2 = nodeHeaders2 |> List.map (parseNode fcc2)


//let testGraph = initGraphWithElements (snd sourceNodes) parsedEdges (snd sinkNodes)
//testGraph.EdgeCount
//testGraph.VertexCount
//let extractedVertices = testGraph.GetVertices()
////let extractedVertices : LVertex<int,CvParam<string>> = testGraph.GetVertices()
//let extractedEdges = testGraph.GetEdges()
////Graph.get
//testGraph.GetLabels()
////testGraph.
//Graph.extractVerticesWithLabels

//let (sourceNodes2, sinkNodes2, protocolRefNodes2) = separateNodes (List.concat parsedNodes2)

//let testParamValue = ParamValue.Value "testParamValue.Value"
//let testCvParam = CvParam("testCvParamTAN", "testCvParamName", "testCvParamTSR", testParamValue)



//// function input

//let newSources = snd sourceNodes2
//let newSinks = snd sinkNodes2
//let newEdges = parsedEdges2
//newEdges.Length
//newEdges.Head.Length
//let graph = testGraph

// function body



//let maxIndex = testGraph.VertexCount
//let indexedSources = List.mapi (fun i s -> i + maxIndex + 1, s) newSources
//let indexedSinks = List.mapi (fun i s -> i + maxIndex + indexedSources.Length + 1, s) newSinks
//testGraph.AddManyVertices()





///// <summary>
///// Takes an indexed input list (e.g. a list of Sources) and groups them by their occurence. Returns the first index of the occurence
///// and the value as tuple.
///// </summary>
///// <example>
///// E.g.: `["Hello"; "Hello"; "Hello"; "World"; "World"; "lol"] |> List.indexed |> convolve` leads to
///// `[(0, "Hello"); (3, "World"); (5, "lol")]`
///// </example>
//let convolve input = 
//    input 
//    |> List.groupBy snd
//    |> List.map (fun (k,il) -> List.map fst il |> List.min, k)

//["Hello"; "Hello"; "Hello"; "World"; "World"; "lol"] |> List.indexed |> convolve



//[["R1,C1"; "R2,C1"]; ["R1,C2"; "R2,C2"]] |> List.transpose

//let sources = snd sourceNodes
//let sources = List.init sources.Length (fun _ -> sources.Head)
//let sinks = snd sinkNodes
//let edges = parsedEdges



//extractVerticesWithLabels graph



//let toVertices paramList : LVertex<string,CvParam<string>> list =
//    paramList 
//    |> List.map (
//        fun s -> Param.getValue s, s
//    )
//    // since a graph must not have duplicate keys, we distinct by key here
//     //|> List.distinctBy fst     // do that later
//let sourceVertices = sources |> toVertices
//let sinkVertices = sinks |> toVertices
//let sourceVertices, sinkVertices, connectedEdges =
//    (sources, sinks, edges)
//    |||> List.map3 (
//        fun source sink edge ->
//            0
//    )
//let connectedEdges =
//    edges
//    |> List.map (
//        List.mapi (
//            fun i e -> fst sourceVertices[i], fst sinkVertices[i], e
//        )
//    )


///// 
//let initGraphWithElements sources edges sinks =
//    let sourceVertices : LVertex<string,CvParam<string>> list = 
//        sources 
//        |> 

//let buildSourceSinkConnection sources edges sinks =
//let initGraphWithElements sources edges sinks =
//    let sources = snd sourceNodes
//    let sinks = snd sinkNodes
//    let edges = parsedEdges
//    let indexedSources = List.indexed sources
//    //let indexedEdges = List.indexed edges
//    let maxIndex = indexedSources.Length - 1
//    let indexedSinks = List.mapi (fun i s -> i + maxIndex + 1, s) sinks
//    //let convolvedSources = convolve indexedSources
//    //let convolvedSinks = convolve indexedSinks
//    //let links =
//    //    let len = indexedSources.Length
//    //    let newIndSinks = convolvedSinks |> List.map (fun (i,s) -> i + len, s)
//    //if indexedSinks

//    let sourceVertices : LVertex<'a,'b> list = indexedSources
//    let sinkVertices : LVertex<'a,'b> list = indexedSinks
//    let indexedEdges : LEdge<'a,'b> list list = 
//        edges
//        |> List.map (
//            List.mapi (
//                fun i e -> i, i + maxIndex + 1, e
//            )
//        )

//    Graph.create (sourceVertices @ sinkVertices) (List.concat indexedEdges)










//let testVertex : LVertex<int,string> = (0,"Hallo")
//let testVertex2 : LVertex<int,string> = (1,"Hallo")
//let testEdge : LEdge<int,string> = (0,1,"ich bin edgy")
//let testGraph2 = Graph.create [testVertex; testVertex2] [testEdge]
//(testGraph2.GetVertices(), testGraph2.GetLabels()) ||> Array.map2 (fun x y -> x,y)



//let nodeList = parsedNodes
//let linkList = parsedEdges
//let singleSourceNode = nodeList.Head.Head
//let singleSinkNode = nodeList[1].Head
//let singleEdge = linkList.Head.Head
//let singleSourceNode = nodeList.Head.Head :?> UserParam<string>
//let singleSinkNode = nodeList[1].Head :?> UserParam<string>
//let singleEdge = linkList.Head.Head :?> CvParam<string>

//let sourceNodeList, sinkNodeList = 
//    let part (input : ('a*'b) list) = snd input.Head
//    parsedNodes 
//    |> List.map (List.groupBy getNodeType >> List.head) 
//    |> List.partition (fst >> (=) Source)
//    |> fun (sonl,sinl) -> part sonl, part sinl

//sourceNodeList

//let vertexList : LVertex<int,CvParam<string>> list = [
////let vertexList : LVertex<int,UserParam<string>> list = [
//    0, singleSourceNode
//    1, singleSinkNode
//]

//let edgeList : LEdge<int,CvParam<string>> list = [
////let edgeList : LEdge<int,CvParam<string>> list = [
//    (0,1,singleEdge)
//]



//groupedHeaders2 |> List.map (parseEdges true fcc2)
//groupedHeaders2.Head.Length
//groupedHeaders2 |> List.map List.length
//groupedHeaders2 |> List.mapi (fun i x -> printfn "%i" i; parseEdges true fcc2 x)
//groupedHeaders2[2].Length
//groupedHeaders2.Head |> parseEdges true fcc2
//groupedHeaders2[1] |> parseEdges true fcc2
//groupedHeaders2[2] |> parseEdges true fcc2
//groupedHeaders2[2].Head
//groupedHeaders2[2][1]
//groupedHeaders2[2][2]
//groupedHeaders2[2][3]
//FsCellsCollection.getCellsInColumn groupedHeaders2[2].Head.ColumnNumber fcc |> Seq.toList
//fcc[1,7]
//groupedHeaders2[2].Head.ColumnNumber
//groupedHeaders2[2][1]
//groupedHeaders2[2][2]

//let res = groupedHeaders2 |> List.map (parseEdges true)
//let antiEmptyChecker str = if str = "" then "(empty)" else str
//res 
//|> List.collect (
//    List.map (fun (c1,c2,c3) -> 
//        (antiEmptyChecker c1.Value, antiEmptyChecker c2.Value, antiEmptyChecker c3.Value)
//        |> fun (c1n,c2n,c3n) -> 
//            printfn "%s    %s    %s" c1n c2n c3n; c1n, c2n, c3n)
//)

//assWs2.CellCollection.GetCellsInColumn(11)


//let getKvTriplets (headerTriplets : FsCell [] []) (workbook : FsWorkbook) (table : FsTable) = 
//    let workbook = wb
//    let table = tbl1
//    let headerTriplets = groupedHeaders

//    let associatedWorksheet = table.GetWorksheetOfTable workbook
//    let columnHeadersRowAddress = table.HeadersRow().RangeAddress.FirstAddress.RowNumber
//    //let columnHeadersColAddress = 
//    //let columnHeadersValues = table.FieldNames(associatedWorksheet.CellCollection)
//    let columnHeadersValues = 
//        headerTriplets
//        |> Array.map (
//            Array.map (
//                fun c ->
//                    table.Field(c.Value, associatedWorksheet.CellCollection)
//            )
//        )

//    let mutable tripleCounter = 0
//    let mutable doubleCounter = 0
//    //let raiseTc () = 
//    //    if tripleCounter = 3 then tripleCounter <- 1
//    //    else tripleCounter <- tripleCounter + 1
//    let raiseDc () =
//        if doubleCounter = 2 then doubleCounter <- 1
//        else doubleCounter <- doubleCounter + 1
//    let satisfier = String.contains "["

//    headerTriplets
//    |> Array.map (
//        fun ht ->
//            ht
//            |> Array.map (
//                fun h ->
//                    if satisfier h then tripleCounter <- 1
//                    else tripleCounter <- tripleCounter + 1
//                    match tripleCounter with
//                    | 1 -> 
//                    | x when x <= 3 && x > 1 ->
//                    | x when x > 3 ->
//                        raiseDc ()

//            )
//    )

//    let rec loop tripleCounter doubleCounter =
//        if en.MoveNext() then
//            let e = en.Current
//            let updatedTripleCounter = tripleCounter + 1
//            let colI = cellsColl.TryGetCell 

//        else 

    //headerTriplets
    //|> Seq.map (
    //    fun seq1 ->
    //        use en = input.GetEnumerator()
    //        loop id
    //)
    //0



// let rec sheesh tripleCounter doubleCounter 