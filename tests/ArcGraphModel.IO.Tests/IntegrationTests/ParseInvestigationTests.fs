module IntegrationTests.Investigation

open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open ArcGraphModel
open ArcGraphModel.IO
open Xunit

open TestUtils


let inves = FsWorkbook.fromXlsxFile "Fixtures/isa.investigation.xlsx"
let invesWs = FsWorkbook.getWorksheets inves |> Seq.head
let invesWsParsed = Worksheet.parseRowsFlat invesWs

//let cvp1 = CvParam("", "ONTOLOGY SOURCE REFERENCE", "", ParamValue.Value "", [])
let up1 = UserParam("ONTOLOGY SOURCE REFERENCE", ParamValue.Value "", [])

[<Fact>]
let ``First Param is UserParam`` () =
    Assert.True (invesWsParsed.Head |> UserParam.tryUserParam).IsSome 

[<Fact>]
let ``First UserParam name`` () = UserParam.termNamesEqual (invesWsParsed.Head :?> UserParam) up1

