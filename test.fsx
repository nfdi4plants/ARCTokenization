
#r "nuget: FSharpAux.Core,2.0.0"
#r "nuget: FsSpreadsheet, 1.3.0-preview"
#r "nuget: FsSpreadsheet.ExcelIO"

//#I @"src\ArcGraphModel\bin\Release\net6.0"
#I @"src\ArcGraphModel.IO\bin\Release\net6.0"
#r "ArcGraphModel.dll"
#r "ArcGraphModel.IO.dll"

#r "nuget: ArcGraphModel, 0.1.0-preview.1"


open FsSpreadsheet
open ArcGraphModel
open ArcGraphModel.IO
open FSharpAux
open FsSpreadsheet.ExcelIO

//fsi.AddPrinter (fun (cvp : CvParam) -> 
//    cvp.ToString()
//)
//fsi.AddPrinter (fun (cvp : CvContainer) -> 
//    cvp.ToString()
//)

fsi.AddPrinter (fun (cvp : ICvBase) -> 
    match cvp with
    | :? UserParam as cvp -> $"UserParam [{CvBase.getCvName cvp}]" 
    | :? CvParam as cvp -> $"CvParam [{CvBase.getCvName cvp}]" 
    | :? CvContainer as cvp -> $"CvContainer [{CvBase.getCvName cvp}]" 
    | _ -> $"ICvBase [{CvBase.getCvName cvp}]"    
)

let p = @"C:\Users\HLWei\Downloads\testArc\isa.investigation.xlsx"
let shortP = @"C:\Users\HLWei\Downloads\testArc\testPersons.xlsx"
let pa = @"C:\Users\HLWei\Downloads\testArc\testAssay.xlsx"


let inv = FsWorkbook.fromXlsxFile pa

let worksheet = 
    let ws = inv.GetWorksheets().Head
    ws.RescanRows()
    ws



let tokens = 
    //worksheet.Tables.[0].Columns(worksheet.CellCollection)
    //|> Seq.toList
    //|> List.choose (fun r -> 
    //        match r.Cells |> Tokenization.parseLine |> Seq.toList with
    //        | [] -> None
    //        | l -> Some l
    //    )
    //|> List.concat
    worksheet
    |> Worksheet.parseTableColumns


tokens
|> Seq.head
|> CvAttributeCollection.tryGetAttribute (CvTerm.getName Address.worksheet)
|> Option.get
|> Param.getValue


tokens.[tokens.Length - 1].ToString()



tokens
|> List.choose (CvBase.tryAs<IParam>)
|> List.map (fun param ->
    CvBase.getTerm param,
    Param.getValue param
)

let containers = 
    tokens
    |> TokenAggregation.aggregateTokens 
    |> List.filter (fun cv -> CvBase.is<UserParam> cv |> not)

containers
|> List.choose (CvContainer.tryCvContainer)
|> List.filter (CvBase.equalsTerm Terms.assay)
|> List.item 0
|> CvContainer.getSingleAs<IParam>("File Name") 
|> Param.getValue




containers.[2].ToString()


containers
|> Seq.choose CvContainer.tryCvContainer
|> Seq.filter (fun cv -> CvBase.equalsTerm Terms.assay cv )
|> Seq.head
|> CvContainer.getSingleParam "File Name"
|> Param.getValue






//let paramLine = 
//    [
//        FsCell("Investigation Person Last Name")
//        FsCell("Müller")   
//    ]

//let paramLine2 = 
//    [
//        FsCell("Factor [temperature]")
//        FsCell("5")   
//    ]

//let paramToken = 
//    let attributes = 
//        [
//            CvParam(Terms.investigation,ParamValue.Value "") :> IParam
//            CvParam(Terms.person,ParamValue.Value "")
//        ]
//    CvParam(Terms.familyName,ParamValue.Value "Müller", attributes)


//let containerLine = 
//    [
//        FsCell("INVESTIGATION CONTACTS")
//    ] 

//let cvParams = Line.convertTokens paramLine2


//cvParams.Length

//cvParams.Head
//|> CvBase.getCvName

//cvParams.Head
//|> ParamBase.getValue





//let tripletLines = 
//    [
//        [
//            FsCell("Investigation Publication Status")
//            FsCell("Pending")   
//        ]
//        [
//            FsCell("Investigation Publication Status Term Accession Number")
//            FsCell("ontobee.com/PO_123")   
//        ]
//        [
//            FsCell("Investigation Publication Status Term Source REF")
//            FsCell("PO")   
//        ]
//    ]


//let tripletToken = 
//    let attributes = 
//        [
//            CvParam(Terms.investigation,ParamValue.Value "") :> IParam
//            CvParam(Terms.publication,ParamValue.Value "")
//        ]
//    let attributes2 = 
//        [
//            CvParam(Terms.investigation,ParamValue.Value "") :> IParam
//            CvParam(Terms.publication,ParamValue.Value "")
//            CvParam(Terms.status,ParamValue.Value "")
//        ]
//    [
//        CvParam(Terms.status,ParamValue.Value "Pending", attributes)
//        CvParam(Terms.annotationID,ParamValue.Value "ontobee.com/PO_123", attributes)
//        CvParam(Terms.termSourceRef,ParamValue.Value "PO", attributes2)
//    ]

//let combinedToken =
//    let attributes = 
//        [
//            CvParam(Terms.investigation,ParamValue.Value "") :> IParam
//            CvParam(Terms.publication,ParamValue.Value "")
//        ]
//    CvParam(Terms.status,ParamValue.CvValue ("ontobee.com/PO_123","Pending","PO"), attributes)
