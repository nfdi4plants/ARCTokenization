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

    module ConvertMetadataTokens =
        
        let referenceTerms = [
            CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1")
            CvTerm.create(accession = "2", name = "ReferenceTerm2", ref = "2")
            CvTerm.create(accession = "3", name = "ReferenceTerm3", ref = "3")
        ]

        let referenceRow = [
            FsCell("ReferenceTerm1", address = FsAddress(rowNumber = 0, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 0, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 0, columnNumber = 2))
        ]

        let referenceCvParams = [
            CvParam(CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1"), ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            CvParam(CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1"), ParamValue.Value "some value")
            CvParam(CvTerm.create(accession = "1", name = "ReferenceTerm1", ref = "1"), ParamValue.Value "another value")
        ]

        let referenceCommentRow = [
            FsCell("Comment[xD]", address = FsAddress(rowNumber = 1, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 1, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 1, columnNumber = 2))
        ]

        let referenceCommentCvParams = [
            CvParam(Terms.StructuralTerms.userComment, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            CvParam(Terms.StructuralTerms.userComment, ParamValue.Value "some value")
            CvParam(Terms.StructuralTerms.userComment, ParamValue.Value "another value")
        ]

        let referenceIgnoreLineRow = [
            FsCell("# why am i even here?", address = FsAddress(rowNumber = 0, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 0, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 0, columnNumber = 2))
        ]

        let referenceIgnoreLineCvParams = [
            CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value "some value")
            CvParam(Terms.StructuralTerms.ignoreLine, ParamValue.Value "another value")
        ]

        let referenceUserParamRow = [
            FsCell("fk u lmaooooo", address = FsAddress(rowNumber = 0, columnNumber = 0))
            FsCell("some value", address = FsAddress(rowNumber = 0, columnNumber = 1))
            FsCell("another value", address = FsAddress(rowNumber = 0, columnNumber = 2))
        ]

        let referenceUserParams = [
            UserParam("fk u lmaooooo", ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey)
            UserParam("fk u lmaooooo", ParamValue.Value "some value")
            UserParam("fk u lmaooooo", ParamValue.Value "another value")
        ]

    module FileSystem =
        
        let referenceRelativeDirectoryPaths = 
            [
                @"1"
                @"2"
                @"1/1_1"
                @"2/2_1"
                @"2/2_2"
                @"2/2_2/2_2_1"
            ]
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000010","Directory Path","AFSO"),
                    v = v
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

        let referenceAbsoluteDirectoryPaths(root) =
            [
                @"1"
                @"2"
                @"1/1_1"
                @"2/2_1"
                @"2/2_2"
                @"2/2_2/2_2_1"
            ]
            |> List.map (fun f -> System.IO.Path.Combine(root, f))
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000010","Directory Path","AFSO"),
                    v = v.Replace("\\", "/")
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

        let referenceRelativeFilePaths = 
            [
                @"1/1_1/.gitkeep"
                @"2/2_1/.gitkeep"
                @"2/2_2/2_2_1/.gitkeep"
            ]
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000009","File Path","AFSO"),
                    v = v
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)

        let referenceAbsoluteFilePaths(root) =
            [
                @"1/1_1/.gitkeep"
                @"2/2_1/.gitkeep"
                @"2/2_2/2_2_1/.gitkeep"
            ]
            |> List.map (fun f -> System.IO.Path.Combine(root, f))
            |> List.map (fun v ->
                CvParam(
                    cvTerm = CvTerm.create("AFSO:00000009","File Path","AFSO"),
                    v = v.Replace("\\", "/")
                )
            )
            |> List.sortBy (fun cvp -> cvp.Value |> ParamValue.getValueAsString)