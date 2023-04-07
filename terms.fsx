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

open ArcGraphModel.Param

module Terms = 
    let protocol                : CvTerm = "ARC_00000150",                             "Protocol",                ""
    let processs                : CvTerm = "ARC_00000150",                             "Process",                ""
    let version                 : CvTerm = "ARC_00000150",                             "Version",                ""
    let description             : CvTerm = "ARC_00000150",                             "Description",            ""
    let identifyingName         : CvTerm = "ARC_00000150",                             "IdentifyingName",            ""


    let assay                   : CvTerm = "ARC_00000150",                             "Assay",                ""
    let assayMeasurementType    : CvTerm = "ARC_1a2d506f_b67d_4f60_adb5_410418a287c8", "AssayMeasurementType", ""
    let assayTechnologyType     : CvTerm = "ARC_c68a2f6a_45d3_43c2_b7c9_aecf452675fd", "AssayTechnologyType",  ""
    let sample                  : CvTerm = "ARC_00002133",                             "Sample",                ""
    let data                    : CvTerm = "ARC_00002134",                             "Data",                ""
