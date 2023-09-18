module TestUtils


open Xunit
open ControlledVocabulary
open ARCTokenization
open ARCTokenization.AnnotationTable


module CvParam =

    let termNamesEqual (cvpExpectec : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (CvBase.getCvName cvpExpectec),
            (CvBase.getCvName cvpActual)
        )

    let hasTermValue (expectedValue : string) (cvpActual : CvParam) =
        Assert.Equal(
            expectedValue,
            (CvBase.getCvName cvpActual)
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

    let structuralEquality (cvpExpected : CvParam) (cvpActual : CvParam) =
        termNamesEqual cvpExpected cvpActual
        accessionsEqual cvpExpected cvpActual
        refUrisEqual cvpExpected cvpActual
        valuesEqual cvpExpected cvpActual

    let termEquality onto (cvpExpected : CvParam) (cvpActual : CvParam) =
        let cvtExp = CvTerm.create(cvpExpected.Accession, cvpExpected.Name, cvpExpected.RefUri)
        let cvtAct = CvTerm.create(cvpActual.Accession, cvpActual.Name, cvpActual.RefUri)
        let synos


module UserParam =

    open ARCTokenization

    let termNamesEqual (upActual : UserParam) (upExpectec : UserParam) =
        Assert.Equal(
            (CvBase.getCvName upActual),
            (CvBase.getCvName upExpectec)
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


