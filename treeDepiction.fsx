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

#load "terms.fsx"
open Terms

open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open FsSpreadsheet.DSL
open ArcGraphModel
open ArcGraphModel

open System
open System.Collections
open System.Collections.Generic




type Protocol (version : CvParam, description : CvParam) =  

    inherit CvContainer(Terms.protocol,seq [version :> ICvBase;description])

    member this.Version 
        with get() = 
            CvContainer.getSingleAs<CvParam> Protocol.VersionProperty this
            |> CvParam.getValueAsString
        and set(version : string) =
            CvParam.fromValue Terms.version version
            |> fun cvp -> CvContainer.setSingle Protocol.VersionProperty cvp this


    static member VersionProperty = "Version"

    //member this.GetVersion() = 
    //    Dictionary.item "Version" this (*:?> CvParam*)
    //member this.SetVersion() = 
    //    Dictionary.item "Version" this



    member this.GetDescription() = Dictionary.item "Description" this :?> CvParam


type Process (name : CvParam, protocol : Protocol) =  

    inherit CvContainer(Terms.processs,seq ["Name",name;"Protocol",protocol])

    member this.Name = this.Get "Name" :?> CvParam

    member this.Protocol = this.Get "Protocol" :?> Protocol


type Assay (measurementType : CvParam, processSequence : #seq<Process>) =

    inherit CvContainer(Terms.assay,seq ["MeasurementType",measurementType;"ProcessSequence",CvSequence(processSequence)])

    member this.MeasurementType = this.Get "MeasurementType" :?> CvParam

    member this.ProcessSequence = this.Get "ProcessSequence" :?> CvSequence<Process>

        
let thisVersion = CvParam.fromValue (Terms.version, "0.1.0")                             
let thisDescription = CvParam.fromValue (Terms.description, "Stuff is done in this way.")

let thisName = CvParam.fromValue (Terms.identifyingName, "Stuff was done on that day xddd")

let thisMeasurementType = CvParam.fromValue (Terms.assayMeasurementType, "temporal")

let protocol = Protocol(thisVersion,thisDescription) 
let processs = Process(thisName,protocol)
let assay = Assay(thisMeasurementType,[processs])



assay.ProcessSequence.[0].Protocol.Version


// Offene Fragen:
// CvParam<IConvertible> ist tödlich
// Wie geht man mit Sequenzen um?
// Als Rückgabewert CvParam oder nur Wert
// Typen enttypisieren
// Options oder nicht options als Rückgabewerte

