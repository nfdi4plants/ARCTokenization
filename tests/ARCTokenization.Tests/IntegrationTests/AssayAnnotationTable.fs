module IntegrationTests.Assay

open ControlledVocabulary
open FsSpreadsheet
open FsSpreadsheet.ExcelIO
open ARCTokenization
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
        let ``IOColumns count`` () =
            TokenizedAnnotationTable.hasIOColumnAmount 2 table
            
        [<Fact>]
        let ``TermRelatedBuildingBlocks count`` () =
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
        let ``IOColumns CvParams`` () = 
            table 
            |> TokenizedAnnotationTable.IOColumnsEqual expectedIOColumns


        [<Fact>]
        let ``TermRelatedBuildingBlocks CvParams`` () =
            table 
            |> TokenizedAnnotationTable.termRelatedBuildingBlocksEqual expectedTermRelatedBuildingBlocks


    module ``Assay with single characteristics`` =
        
        let assay = Assays.Correct.``assay with single characteristics``

        [<Fact>]
        let ``AnnotationTable count`` () =
            Assert.Equal(assay.Length, 1)

        let table = assay.[0] |> snd

        [<Fact>]
        let ``IOColumns count`` () =
            TokenizedAnnotationTable.hasIOColumnAmount 2 table
            
        [<Fact>]
        let ``TermRelatedBuildingBlocks count`` () =
            TokenizedAnnotationTable.hasTermRelatedBuildingBlockAmount 1 table

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

        let expectedTermRelatedBuildingBlocks = 
            [
                [
                    CvParam(
                        id = "Term Accession Number (OBI:0100026)",
                        name = "Characteristic [organism]",
                        ref = "Term Source REF (OBI:0100026)",
                        pv = (ParamValue.CvValue ("http://purl.obolibrary.org/obo/NCBITaxon_3702","Arabidopsis thaliana","NCBITaxon")),
                        attributes = []
                    )
                ]
            ]

        [<Fact>]
        let ``IOColumns CvParams`` () = 
            table 
            |> TokenizedAnnotationTable.IOColumnsEqual expectedIOColumns

        [<Fact>]
        let ``TermRelatedBuildingBlocks CvParams`` () =
            table 
            |> TokenizedAnnotationTable.termRelatedBuildingBlocksEqual expectedTermRelatedBuildingBlocks