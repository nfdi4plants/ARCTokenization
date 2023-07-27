module IntegrationTests.Investigation

open ControlledVocabulary
open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open ArcGraphModel
open Xunit

open TestUtils

let inves = FsWorkbook.fromXlsxFile "Fixtures/isa.investigation.xlsx"
let invesWs = FsWorkbook.getWorksheets inves |> Seq.head
let invesWsParsed = Worksheet.parseRowsFlat invesWs

//let cvp1 = CvParam("", "ONTOLOGY SOURCE REFERENCE", "", ParamValue.Value "", [])
let up1 = CvParam(("INVMSO:00000002","ONTOLOGY SOURCE REFERENCE", "INVMSO"), ParamValue.Value "", [])

[<Fact>]
let ``First Param is CvParam`` () =
    Assert.True (invesWsParsed.Head |> CvParam.tryCvParam).IsSome 

[<Fact>]
let ``First CvParam`` () = CvParam.structuralEquality (invesWsParsed.Head :?> CvParam) up1

