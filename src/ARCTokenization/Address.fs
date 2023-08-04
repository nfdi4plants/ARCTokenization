namespace ARCTokenization

open ControlledVocabulary

module Address = 

    let column : CvTerm = "http://purl.obolibrary.org/obo/NCIT_C43379","Column","NCIT"

    let row : CvTerm = "http://purl.obolibrary.org/obo/NCIT_C43378","Row","NCIT"

    let worksheet : CvTerm = "http://purl.obolibrary.org/obo/NCIT_C73541","Worksheet","NCIT"

    //"isa_investigation!A1"

    let columnIndexOfParam (cvp : CvParam) =
        cvp.GetAttribute(column) |> ParamBase.getValue :?> int

    let rowIndexOfParam (cvp : CvParam) =
        cvp.GetAttribute(column) |> ParamBase.getValue :?> int

    let createColumnParam (columnIndex : int) =
        CvParam(column,ParamValue.Value columnIndex)

    let createRowParam (rowIndex : int) =
        CvParam(row,ParamValue.Value rowIndex)

    let createWorksheetParam (worksheetName : string) =
        CvParam(worksheet,ParamValue.Value worksheetName)