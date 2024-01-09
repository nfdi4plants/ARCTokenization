module TestUtils

open Xunit
open ControlledVocabulary
open ARCTokenization

module CvParam =

    let termNamesEqual (cvpExpected : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (CvBase.getCvName cvpExpected),
            (CvBase.getCvName cvpActual)
        )

    let hasTermValue (expectedValue : string) (cvpActual : CvParam) =
        Assert.Equal(
            expectedValue,
            (CvBase.getCvName cvpActual)
        )

    let accessionsEqual (cvpExpected : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (CvBase.getCvAccession cvpExpected),
            (CvBase.getCvAccession cvpActual)
        )

    let hasAccession (expectedID : string) (cvpActual : CvParam) =
        Assert.Equal(
            expectedID,
            (CvBase.getCvAccession cvpActual)
        )

    let refUrisEqual (cvpExpected : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (CvBase.getCvRef cvpExpected),
            (CvBase.getCvRef cvpActual)
        )

    let hasRefUri (expectedRefUri : string) (cvpActual : CvParam) =
        Assert.Equal(
            expectedRefUri,
            (CvBase.getCvRef cvpActual)
        )

    let valuesEqual (cvpExpected : CvParam) (cvpActual : CvParam) =
        Assert.Equal(
            (Param.getParamValue cvpExpected),
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
        

module UserParam =

    open ARCTokenization

    let termNamesEqual (upExpected : UserParam) (upActual : UserParam) =
        Assert.Equal(
            (CvBase.getCvName upExpected),
            (CvBase.getCvName upActual)
        )

    let hasTermValue (expectedValue : string) (upActual : UserParam) =
        Assert.Equal(
            expectedValue,
            (CvBase.getCvName upActual)
        )

    let accessionsEqual (upExpected : UserParam) (upActual : UserParam) =
        Assert.Equal(
            (CvBase.getCvAccession upExpected),
            (CvBase.getCvAccession upActual)
        )

    let hasAccession (expectedID : string) (upActual : UserParam) =
        Assert.Equal(
            expectedID,
            (CvBase.getCvAccession upActual)
        )

    let refUrisEqual (upExpected : UserParam) (upActual : UserParam) =
        Assert.Equal(
            (CvBase.getCvRef upExpected),
            (CvBase.getCvRef upActual)
        )

    let hasRefUri (expectedRefUri : string) (upActual : UserParam) =
        Assert.Equal(
            expectedRefUri,
            (CvBase.getCvRef upActual)
        )

    let valuesEqual (upExpected : UserParam) (upActual : UserParam) =
        Assert.Equal(
            (Param.getParamValue upExpected),
            (Param.getParamValue upActual)
        )

    let hasValue (expectedValue : ParamValue) (upActual : UserParam) =
        Assert.Equal(
            expectedValue,
            (Param.getParamValue upActual)
        )

    let structuralEquality (upActual : UserParam) (upExpected : UserParam) =
        termNamesEqual upExpected upActual
        accessionsEqual upExpected upActual
        refUrisEqual upExpected upActual
        valuesEqual upExpected upActual

module Param =
    
    open ARCTokenization

    let termNamesEqual (ipExpected : IParam) (ipActual : IParam) =
        Assert.Equal(
            (CvBase.getCvName ipExpected),
            (CvBase.getCvName ipActual)
        )

    let hasTermValue (expectedValue : string) (ipActual : IParam) =
        Assert.Equal(
            expectedValue,
            (CvBase.getCvName ipActual)
        )

    let accessionsEqual (ipExpected : IParam) (ipActual : IParam) =
        Assert.Equal(
            (CvBase.getCvAccession ipExpected),
            (CvBase.getCvAccession ipActual)
        )

    let hasAccession (expectedID : string) (ipActual : IParam) =
        Assert.Equal(
            expectedID,
            (CvBase.getCvAccession ipActual)
        )

    let refUrisEqual (ipExpected : IParam) (ipActual : IParam) =
        Assert.Equal(
            (CvBase.getCvRef ipExpected),
            (CvBase.getCvRef ipActual)
        )

    let hasRefUri (expectedRefUri : string) (ipActual : IParam) =
        Assert.Equal(
            expectedRefUri,
            (CvBase.getCvRef ipActual)
        )

    let valuesEqual (ipExpected : IParam) (ipActual : IParam) =
        Assert.Equal(
            (Param.getParamValue ipExpected),
            (Param.getParamValue ipActual)
        )

    let hasValue (expectedValue : ParamValue) (ipActual : IParam) =
        Assert.Equal(
            expectedValue,
            (Param.getParamValue ipActual)
        )

    let structuralEquality (ipActual : IParam) (ipExpected : IParam) =
        termNamesEqual ipExpected ipActual
        accessionsEqual ipExpected ipActual
        refUrisEqual ipExpected ipActual
        valuesEqual ipExpected ipActual

    let typedStructuralEquality (ipExpected : IParam) (ipActual : IParam) =
        if Param.is<CvParam> ipExpected && Param.is<CvParam> ipActual then
            structuralEquality ipExpected ipActual
        elif 
            Param.is<UserParam> ipExpected && Param.is<UserParam> ipActual then
            structuralEquality ipExpected ipActual
        else
            Assert.True(false, "Expected and actual parameters are not of the same param subtype")

module TokenizedAnnotationTable =
    
    ()


