module ParseInvestigationTests

open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open ArcGraphModel
open ArcGraphModel.IO
open Expecto

open TestUtils


let inves = FsWorkbook.fromXlsxFile "Fixtures/isa.investigation.xlsx"
let invesWs = FsWorkbook.getWorksheets inves |> Seq.head
let invesWsParsed = Worksheet.parseRowsFlat invesWs

//let cvp1 = CvParam("", "ONTOLOGY SOURCE REFERENCE", "", ParamValue.Value "", [])
let up1 = UserParam("ONTOLOGY SOURCE REFERENCE", ParamValue.Value "", [])

[<Tests>]
let ``Investigation File is parsed correctly`` =
    testList "Investigation from file" [
        //testList "CvParam" [
        //    testCase "First Param is CvParam" (fun _ ->
        //        Expect.isSome (invesWsParsed.Head |> CvParam.tryCvParam) "Is no CvParam"
        //    )
        //    testCase "First CvParam name" (fun _ ->
        //        CvParam.termNamesEqual (invesWsParsed.Head :?> CvParam) cvp1
        //    )
        //]
        testList "UserParam" [
            testCase "First Param is UserParam" (fun _ ->
                Expect.isSome (invesWsParsed.Head |> UserParam.tryUserParam) "Is no UserParam"
            )
            testCase "First UserParam name" (fun _ ->
                UserParam.termNamesEqual (invesWsParsed.Head :?> UserParam) up1
            )
        ]
    ]
