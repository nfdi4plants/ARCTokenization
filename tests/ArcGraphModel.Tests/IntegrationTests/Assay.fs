module IntegrationTests.Assay

open ControlledVocabulary
open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open ArcGraphModel
open Xunit

open TestUtils
open TestObjects

module Correct  =
    
    module ``Assay with only source and sample column`` = 
        
        let assay = Assays.Correct.``assay with only source and sample column``

        [<Fact>]
        let ``AnnotationTable count`` () =
            Assert.Equal(assay.Length, 1) 

        let table = assay.[0] |> snd

        [<Fact>]
        let ``TokenizedAnnotationTable IOColumn count`` () =
            TokenizedAnnotationTable.hasIOColumnAmount 2 table
            
        [<Fact>]
        let ``TokenizedAnnotationTable TermRelatedBuildingBlocksAmount`` () =
            TokenizedAnnotationTable.hasTermRelatedBuildingBlockAmount 0 table

        let expectedIOColumns = 
            [
                [
                    CvParam(
                        id = "(n/a)",
                        name = "Source Name",
                        ref = "(n/a)",
                        pv = (ParamValue.Value "Source A"),
                        attributes = []
                    )
                ]
                [
                    CvParam(
                        id = "(n/a)",
                        name = "Sample Name",
                        ref = "(n/a)",
                        pv = (ParamValue.Value "Sample A"),
                        attributes = []
                    )
                ]
            ]

        let expectedTermRelatedBuildingBlocks: CvParam list list = []

        [<Fact>]
        let ``TokenizedAnnotationTable IOColumns`` () =
            (expectedIOColumns, table.IOColumns)
            ||> List.iter2 (fun expectedGroup actualGroup ->
                (expectedGroup, actualGroup)
                ||> List.iter2 (fun expectedParam actualParam ->
                    CvParam.structuralEquality expectedParam actualParam
                )
            )

        [<Fact>]
        let ``TokenizedAnnotationTable TermRelatedBuildingBlocks`` () =
            (expectedTermRelatedBuildingBlocks, table.TermRelatedBuildingBlocks)
            ||> List.iter2 (fun expectedGroup actualGroup ->
                (expectedGroup, actualGroup)
                ||> List.iter2 (fun expectedParam actualParam ->
                    CvParam.structuralEquality expectedParam actualParam
                )
            )

