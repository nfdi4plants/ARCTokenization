namespace ArcGraphModel.IO

open ArcGraphModel
open FSharpAux
open FsSpreadsheet


module Worksheet =

    let parseRows (worksheet : FsWorksheet) = 
        worksheet.Rows
        |> List.choose (fun r -> 
            match r |> Tokenization.parseLine |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat

    let parseColumns (worksheet : FsWorksheet) = 
        worksheet.Tables.Head.Columns(worksheet.CellCollection)
        |> Seq.toList
        |> List.choose (fun r -> 
            match r |> Tokenization.parseLine |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat

    let parseTableColumns (worksheet : FsWorksheet) = 
        worksheet.Columns
        |> Seq.toList
        |> List.choose (fun r -> 
            match r |> Tokenization.parseLine |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat

