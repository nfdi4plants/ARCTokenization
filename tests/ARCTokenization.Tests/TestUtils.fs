module TestUtils

open Xunit
open ControlledVocabulary
open ARCTokenization
open ARCTokenization.AnnotationTable

module CvParam =

    let termValuesEqual (cvpExpectec : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (CvBase.getCvValue cvpExpectec),
            (CvBase.getCvValue cvpActual)
        )

    let hasTermValue (expectedValue : string) (cvpActual : CvParam) =
        Assert.Equal(
            expectedValue,
            (CvBase.getCvValue cvpActual)
        )

    let accessionsEqual (cvpExpectec : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (CvBase.getCvAccession cvpExpectec),
            (CvBase.getCvAccession cvpActual)
        )

    let hasAccession (expectedID : string) (cvpActual : CvParam) =
        Assert.Equal(
            expectedID,
            (CvBase.getCvAccession cvpActual)
        )

    let refUrisEqual (cvpExpectec : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (CvBase.getCvRef cvpExpectec),
            (CvBase.getCvRef cvpActual)
        )

    let hasRefUri (expectedRefUri : string) (cvpActual : CvParam) =
        Assert.Equal(
            expectedRefUri,
            (CvBase.getCvRef cvpActual)
        )

    let valuesEqual (cvpExpectec : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (Param.getParamValue cvpExpectec),
            (Param.getParamValue cvpActual)
        )

    let hasValue (expectedValue : ParamValue) (cvpActual : CvParam) =
        Assert.Equal(
            expectedValue,
            (Param.getParamValue cvpActual)
        )

    let structuralEquality (cvpExpectec : CvParam) (cvpActual : CvParam) =
        termValuesEqual cvpExpectec cvpActual
        accessionsEqual cvpExpectec cvpActual
        refUrisEqual cvpExpectec cvpActual
        valuesEqual cvpExpectec cvpActual
        

module UserParam =

    open ARCTokenization

    let termNamesEqual (upActual : UserParam) (upExpectec : UserParam) =
        Assert.Equal(
            (CvBase.getCvValue upActual),
            (CvBase.getCvValue upExpectec)
        )

module TokenizedAnnotationTable =
    
    let IOColumnsEqual (expectedIOColumns : CvParam list list) (table : TokenizedAnnotationTable) =
        (expectedIOColumns, table.IOColumns)
        ||> List.iter2 (fun expectedGroup actualGroup ->
            (expectedGroup, actualGroup)
            ||> List.iter2 (fun expectedParam actualParam ->
                CvParam.structuralEquality expectedParam actualParam
            )
        )

    let hasIOColumnAmount (expectedIOColumnAmount : int) (table : TokenizedAnnotationTable) =
        Assert.Equal(expectedIOColumnAmount, table.IOColumns.Length)


    let termRelatedBuildingBlocksEqual (expectedTermRelatedBuildingBlocks : CvParam list list) (table : TokenizedAnnotationTable) =
        (expectedTermRelatedBuildingBlocks, table.TermRelatedBuildingBlocks)
        ||> List.iter2 (fun expectedGroup actualGroup ->
            (expectedGroup, actualGroup)
            ||> List.iter2 (fun expectedParam actualParam ->
                CvParam.structuralEquality expectedParam actualParam
            )
        )

    let hasTermRelatedBuildingBlockAmount (expectedTermRelatedBuildingBlockAmount : int) (table : TokenizedAnnotationTable) =
        Assert.Equal(expectedTermRelatedBuildingBlockAmount, table.TermRelatedBuildingBlocks.Length)


