namespace ARCTokenization

open ControlledVocabulary
open FSharpAux
open FsSpreadsheet

open System
open ARCtrl
open ARCtrl.ISA

module AnnnotationTable = 

    let a ws =
        let t = ARCtrl.ISA.Spreadsheet.ArcTable.tryFromFsWorksheet ws

        t.Value.Columns
        |> Array.map (fun c ->
            match c.Header with

            | ISA.CompositeHeader.Input i | ISA.CompositeHeader.Output i -> 
                
                1

            | ISA.CompositeHeader.Characteristic headerOntology
            | ISA.CompositeHeader.Factor headerOntology
            | ISA.CompositeHeader.Parameter headerOntology ->
                2

            | ISA.CompositeHeader.FreeText s ->
                3

            | ISA.CompositeHeader.Component a -> 4

            | ISA.CompositeHeader.ProtocolDescription -> 5
            | ISA.CompositeHeader.ProtocolREF -> 6
            | ISA.CompositeHeader.ProtocolUri -> 7
            | ISA.CompositeHeader.ProtocolVersion -> 8
            | ISA.CompositeHeader.ProtocolType -> 9
            | ISA.CompositeHeader.Performer -> 10
            | ISA.CompositeHeader.Date -> 11
        )
