namespace ARCTokenization


open ControlledVocabulary
open FSharpAux
open System.IO


type ParamBasedParsers =

    static member parseIsaMetadataSheetFromCvp isaFileName isaMdsParsingF absFileTokens =
        absFileTokens
        |> Seq.choose (
            fun cvp ->
                let cvpStr = Param.getValueAsString cvp
                //printfn $"cvpStr: {cvpStr}"
                if String.contains isaFileName cvpStr then
                    Some (isaMdsParsingF cvpStr)
                else None
        )

    static member parseInvestigationMetadataSheetFromCvp absFileTokens =
        ParamBasedParsers.parseIsaMetadataSheetFromCvp "isa.investigation.xlsx" ARCTokenization.Investigation.parseMetadataSheetFromFile absFileTokens

    static member parseStudyMetadataSheetFromCvp absFileTokens =
        ParamBasedParsers.parseIsaMetadataSheetFromCvp "isa.study.xlsx" ARCTokenization.Study.parseMetadataSheetfromFile absFileTokens

    static member parseAssayMetadataSheetFromCvp absFileTokens =
        ParamBasedParsers.parseIsaMetadataSheetFromCvp "isa.assay.xlsx" ARCTokenization.Assay.parseMetadataSheetFromFile absFileTokens

    static member tryParseIsaMetadataSheetFromCvp (isaFileName : string) isaMdsParsingF absFileTokens =
        absFileTokens
        |> Seq.choose (
            fun cvp ->
                let cvpStr = Param.getValueAsString cvp
                //printfn $"cvpStr: {cvpStr}"
                if String.contains isaFileName (Path.GetFileName cvpStr) then
                    try Some (isaMdsParsingF cvpStr)
                    with _ -> None
                else None
        )

    static member tryParseInvestigationMetadataSheetFromCvp (absFileTokens : #IParam seq) =
        try ParamBasedParsers.tryParseIsaMetadataSheetFromCvp "isa.investigation.xlsx" ARCTokenization.Investigation.parseMetadataSheetFromFile absFileTokens 
            |> Seq.concat
        with _ -> Seq.empty

    static member tryParseStudyMetadataSheetFromCvp (absFileTokens : #IParam seq) =
        ParamBasedParsers.tryParseIsaMetadataSheetFromCvp "isa.study.xlsx" ARCTokenization.Study.parseMetadataSheetfromFile absFileTokens

    static member tryParseAssayMetadataSheetFromCvp (absFileTokens : #IParam seq) =
        ParamBasedParsers.tryParseIsaMetadataSheetFromCvp "isa.assay.xlsx" ARCTokenization.Assay.parseMetadataSheetFromFile absFileTokens