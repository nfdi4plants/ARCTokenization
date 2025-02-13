namespace ARCTokenization


open ARCtrl


module AnnnotationTable = 

    let a ws =
        let t = ARCtrl.Spreadsheet.ArcTable.tryFromFsWorksheet ws

        t.Value.Columns
        |> Array.map (fun c ->
            match c.Header with
            | CompositeHeader.Input i 
            | CompositeHeader.Output i                      -> 1
            | CompositeHeader.Characteristic headerOntology
            | CompositeHeader.Factor headerOntology
            | CompositeHeader.Parameter headerOntology      -> 2
            | CompositeHeader.FreeText s                    -> 3
            | CompositeHeader.Component a                   -> 4
            | CompositeHeader.ProtocolDescription           -> 5
            | CompositeHeader.ProtocolREF                   -> 6
            | CompositeHeader.ProtocolUri                   -> 7
            | CompositeHeader.ProtocolVersion               -> 8
            | CompositeHeader.ProtocolType                  -> 9
            | CompositeHeader.Performer                     -> 10
            | CompositeHeader.Date                          -> 11
        )
