namespace ArcGraphModel.IO

open ArcGraphModel
open FSharpAux
open FsSpreadsheet


module Worksheet =

    let parseRows (worksheet : FsWorksheet) = 
        let sheetName = Address.createWorksheetParam worksheet.Name
        worksheet.Rows
        |> List.choose (fun r -> 
            match r |> Tokenization.parseLine |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat
        |> List.map (fun token ->        
            CvAttributeCollection.tryAddAttribute sheetName token |> ignore
            token
        )

    let parseTableColumns (worksheet : FsWorksheet) = 
        let sheetName = Address.createWorksheetParam worksheet.Name
        worksheet.Tables.Head.Columns(worksheet.CellCollection)
        |> Seq.toList
        |> List.choose (fun r -> 
            match r |> Tokenization.parseLine |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat
        |> List.map (fun token ->        
            CvAttributeCollection.tryAddAttribute sheetName token |> ignore
            token
        )

    let parseColumns (worksheet : FsWorksheet) = 
        let sheetName = Address.createWorksheetParam worksheet.Name
        worksheet.Columns
        |> Seq.toList
        |> List.choose (fun r -> 
            match r |> Tokenization.parseLine |> Seq.toList with
            | [] -> None
            | l -> Some l
        )
        |> List.concat
        |> List.map (fun token ->        
            CvAttributeCollection.tryAddAttribute sheetName token |> ignore
            token
        )

