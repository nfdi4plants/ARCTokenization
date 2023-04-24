namespace ArcGraphModel.IO

open ArcGraphModel
open FSharpAux
open FsSpreadsheet


module Worksheet =

    let parseRows (worksheet : FsWorksheet) = 
        worksheet.Rows
        |> List.choose (fun r -> 
            match r.Cells |> Tokenization.parseLine |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat

