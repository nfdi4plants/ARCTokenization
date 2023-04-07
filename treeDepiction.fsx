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
open ArcGraphModel.Param

open System
open System.Collections
open System.Collections.Generic

type CvSequence<'T when 'T :> ICvBase> (cvAccession:string,cvName:string,cvRefUri:string,items : seq<'T>) =

    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri    

    
    new ((id,name,ref) : CvTerm,items : seq<'T>) = CvSequence (id,name,ref,items)
    new ((id,name,ref) : CvTerm) = CvSequence (id,name,ref,Seq.empty)
    new (items : seq<'T>) = 
        let l =         
            items
            |> Seq.map (fun v -> v.GetType().FullName)
            |> Seq.countBy id 
            |> Seq.length      
        if l = 0 then          
            failwith "Cannot create CvSequence without any items or cv annotation"
            let h = Seq.head items
            CvSequence (h.ID,h.Name,h.RefUri,items)
        elif l > 1 then
            failwith "Cannot create CvSequence with items of different type"
            let h = Seq.head items
            CvSequence (h.ID,h.Name,h.RefUri,items)
        else 
            let h = Seq.head items
            CvSequence (h.ID,h.Name,h.RefUri,items)

    member this.Item i = Seq.item i items
             
    interface IEnumerable<ICvBase> with
        member this.GetEnumerator() = (items |> Seq.map (fun v -> v :> ICvBase)).GetEnumerator()

    interface IEnumerable with
        member this.GetEnumerator() = (this :> IEnumerable<ICvBase>).GetEnumerator() :> IEnumerator

type CvContainer (cvAccession:string,cvName:string,cvRefUri:string,items : seq<string*#ICvBase>) =
  
    let properties : IDictionary<string,#ICvBase> = Dictionary.ofSeq(items)

    interface ICvBase with 
        member this.ID     = cvAccession
        member this.Name   = cvName
        member this.RefUri = cvRefUri    

    
    new ((id,name,ref) : CvTerm,items : seq<string*#ICvBase>) = CvContainer (id,name,ref,items)
    new ((id,name,ref) : CvTerm) = CvContainer (id,name,ref,Seq.empty)
    
    member this.Get k = Dictionary.item k properties
    member this.TryGet k = Dictionary.tryFind k properties

    member this.Add k v = Dictionary.addInPlace k v properties

type Protocol (version : CvParam<IConvertible>, description : CvParam<IConvertible>) =  

    inherit CvContainer(Terms.protocol,seq ["Version",version  :> ICvBase ;"Description",description :> ICvBase])

    member this.Version = this.Get "Version" :?> CvParam<string>

    member this.Description = this.Get "Description" :?> CvParam<string>

type Process (name : CvParam<IConvertible>, protocol : Protocol) =  

    inherit CvContainer(Terms.processs,seq ["Name",name;"Protocol",protocol])

    member this.Name = this.Get "Name" :?> CvParam<string>

    member this.Protocol = this.Get "Protocol" :?> Protocol


type Assay (measurementType : CvParam<IConvertible>, processSequence : #seq<Process>) =

    inherit CvContainer(Terms.assay,seq ["MeasurementType",measurementType;"ProcessSequence",CvSequence(processSequence)])

    member this.MeasurementType = this.Get "MeasurementType" :?> CvParam<string>

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

