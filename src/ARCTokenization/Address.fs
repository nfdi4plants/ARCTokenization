namespace ARCTokenization

open ControlledVocabulary

module Address = 

    let column = CvTerm.create(accession = "http://purl.obolibrary.org/obo/NCIT_C43379", value = "Column", ref = "NCIT")

    let row = CvTerm.create(accession = "http://purl.obolibrary.org/obo/NCIT_C43378", value = "Row", ref = "NCIT")

    let worksheet = CvTerm.create(accession = "http://purl.obolibrary.org/obo/NCIT_C73541", value = "Worksheet", ref = "NCIT")

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