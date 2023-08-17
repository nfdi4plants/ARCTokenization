module ReferenceObjects

open ControlledVocabulary
open ARCTokenization
open FsSpreadsheet

module Tokenization =
    
    module KeyParser = 

        let referenceKeys = [
            "1";"2";"3"
        ]

        let referenceTerms = [
            CvTerm.create(accession = "1", name = "1", ref = "1")
            CvTerm.create(accession = "2", name = "2", ref = "2")
            CvTerm.create(accession = "3", name = "3", ref = "3")
        ]

        let referenceParamValues = [
            ParamValue.Value 1
            ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2"))
            ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3"))
        ]

        let referenceCvParams = [
            CvParam(CvTerm.create(accession = "1", name = "1", ref = "1"), ParamValue.Value 1)
            CvParam(CvTerm.create(accession = "2", name = "2", ref = "2"), ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
            CvParam(CvTerm.create(accession = "3", name = "3", ref = "3"), ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
        ]

        let referenceCommentKeys = [
            "Comment[1]"
            "Comment[<2>]"
            "Comment[<3 even has a newline. 
            >]"
        ]

        let referenceCommentCvParams =
            [
                CvParam(Terms.StructuralTerms.userComment, ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.userComment, ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
                CvParam(Terms.StructuralTerms.userComment, ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
            ]

        let referenceIgnoreLineKeys = [
            "#Oh jeez"
            "#  i hope they"
            "#ignore
            this"
        ]

        let referenceIgnoreLineCvParams =
            [
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
            ]

        let referenceUserParamKeys =
            [
                "hahaha"
                "this follows no structure"
                "fk you 
                lmaoooo"
            ]

        let referenceUserParams =
            [
                UserParam("hahaha", ParamValue.Value 1)
                UserParam("this follows no structure", ParamValue.CvValue (CvTerm.create(accession = "2", name = "2", ref = "2")))
                UserParam("fk you 
                lmaoooo", ParamValue.WithCvUnitAccession(3, CvTerm.create(accession = "3", name = "3", ref = "3")))
            ]

        let referenceMixedKeys = 
            [
                "1"
                "Comment[<2>]"
                "#Oh jeez"
                "fk you 
                lmaoooo"
            ]

        let referenceMixedParamValues = [
            for i in 0 .. 3 -> ParamValue.Value 1
        ]

        let referenceMixedParams: IParam list =
            [
                CvParam(CvTerm.create(accession = "1", name = "1", ref = "1"), ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.userComment, ParamValue.Value 1)
                CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value 1)
                UserParam("fk you 
                lmaoooo", ParamValue.Value 1)
            ]