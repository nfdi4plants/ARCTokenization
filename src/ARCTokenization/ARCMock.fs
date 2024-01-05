namespace ARCTokenization

open ControlledVocabulary

type ARCMock =

    /// <summary>
    /// returns a mock list of CvParams lists which each represent a tokenized row of investigation metadata.
    /// each mandatory row is present, containing at least one CvParam annotated as MetadataSectionKey (the first column of an ISA-XLSX investigation metadata sheet).
    /// each row can be expanded with additional CvParams by setting the respective optional argument.
    /// </summary>
    static member InvestigationMetadataTokens(
        ?ONTOLOGY_SOURCE_REFERENCE: seq<string>,
        ?Term_Source_Name: seq<string>,
        ?Term_Source_File: seq<string>,
        ?Term_Source_Version: seq<string>,
        ?Term_Source_Description: seq<string>,
        ?INVESTIGATION: seq<string>,
        ?Investigation_Identifier: seq<string>,
        ?Investigation_Title: seq<string>,
        ?Investigation_Description: seq<string>,
        ?Investigation_Submission_Date: seq<string>,
        ?Investigation_Public_Release_Date: seq<string>,
        ?INVESTIGATION_PUBLICATIONS: seq<string>,
        ?Investigation_Publication_PubMed_ID: seq<string>,
        ?Investigation_Publication_DOI: seq<string>,
        ?Investigation_Publication_Author_List: seq<string>,
        ?Investigation_Publication_Title: seq<string>,
        ?Investigation_Publication_Status: seq<string>,
        ?Investigation_Publication_Status_Term_Accession_Number: seq<string>,
        ?Investigation_Publication_Status_Term_Source_REF: seq<string>,
        ?INVESTIGATION_CONTACTS: seq<string>,
        ?Investigation_Person_Last_Name: seq<string>,
        ?Investigation_Person_First_Name: seq<string>,
        ?Investigation_Person_Mid_Initials: seq<string>,
        ?Investigation_Person_Email: seq<string>,
        ?Investigation_Person_Phone: seq<string>,
        ?Investigation_Person_Fax: seq<string>,
        ?Investigation_Person_Address: seq<string>,
        ?Investigation_Person_Affiliation: seq<string>,
        ?Investigation_Person_Roles: seq<string>,
        ?Investigation_Person_Roles_Term_Accession_Number: seq<string>,
        ?Investigation_Person_Roles_Term_Source_REF: seq<string>,
        ?Comment_ORCID: seq<string>,
        ?STUDY: seq<string>,
        ?Study_Identifier: seq<string>,
        ?Study_Title: seq<string>,
        ?Study_Description: seq<string>,
        ?Study_Submission_Date: seq<string>,
        ?Study_Public_Release_Date: seq<string>,
        ?Study_File_Name: seq<string>,
        ?STUDY_DESIGN_DESCRIPTORS: seq<string>,
        ?Study_Design_Type: seq<string>,
        ?Study_Design_Type_Term_Accession_Number: seq<string>,
        ?Study_Design_Type_Term_Source_REF: seq<string>,
        ?STUDY_PUBLICATIONS: seq<string>,
        ?Study_Publication_PubMed_ID: seq<string>,
        ?Study_Publication_DOI: seq<string>,
        ?Study_Publication_Author_List: seq<string>,
        ?Study_Publication_Title: seq<string>,
        ?Study_Publication_Status: seq<string>,
        ?Study_Publication_Status_Term_Accession_Number: seq<string>,
        ?Study_Publication_Status_Term_Source_REF: seq<string>,
        ?STUDY_FACTORS: seq<string>,
        ?Study_Factor_Name: seq<string>,
        ?Study_Factor_Type: seq<string>,
        ?Study_Factor_Type_Term_Accession_Number: seq<string>,
        ?Study_Factor_Type_Term_Source_REF: seq<string>,
        ?STUDY_ASSAYS: seq<string>,
        ?Study_Assay_Measurement_Type: seq<string>,
        ?Study_Assay_Measurement_Type_Term_Accession_Number: seq<string>,
        ?Study_Assay_Measurement_Type_Term_Source_REF: seq<string>,
        ?Study_Assay_Technology_Type: seq<string>,
        ?Study_Assay_Technology_Type_Term_Accession_Number: seq<string>,
        ?Study_Assay_Technology_Type_Term_Source_REF: seq<string>,
        ?Study_Assay_Technology_Platform: seq<string>,
        ?Study_Assay_File_Name: seq<string>,
        ?STUDY_PROTOCOLS: seq<string>,
        ?Study_Protocol_Name: seq<string>,
        ?Study_Protocol_Type: seq<string>,
        ?Study_Protocol_Type_Term_Accession_Number: seq<string>,
        ?Study_Protocol_Type_Term_Source_REF: seq<string>,
        ?Study_Protocol_Description: seq<string>,
        ?Study_Protocol_URI: seq<string>,
        ?Study_Protocol_Version: seq<string>,
        ?Study_Protocol_Parameters_Name: seq<string>,
        ?Study_Protocol_Parameters_Term_Accession_Number: seq<string>,
        ?Study_Protocol_Parameters_Term_Source_REF: seq<string>,
        ?Study_Protocol_Components_Name: seq<string>,
        ?Study_Protocol_Components_Type: seq<string>,
        ?Study_Protocol_Components_Type_Term_Accession_Number: seq<string>,
        ?Study_Protocol_Components_Type_Term_Source_REF: seq<string>,
        ?STUDY_CONTACTS: seq<string>,
        ?Study_Person_Last_Name: seq<string>,
        ?Study_Person_First_Name: seq<string>,
        ?Study_Person_Mid_Initials: seq<string>,
        ?Study_Person_Email: seq<string>,
        ?Study_Person_Phone: seq<string>,
        ?Study_Person_Fax: seq<string>,
        ?Study_Person_Address: seq<string>,
        ?Study_Person_Affiliation: seq<string>,
        ?Study_Person_Roles: seq<string>,
        ?Study_Person_Roles_Term_Accession_Number: seq<string>,
        ?Study_Person_Roles_Term_Source_REF: seq<string>
    ) =
        let valueRows = 
            [
                ["" ; yield! ONTOLOGY_SOURCE_REFERENCE |> Option.defaultValue Seq.empty]
                ["" ; yield! Term_Source_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Term_Source_File |> Option.defaultValue Seq.empty]
                ["" ; yield! Term_Source_Version |> Option.defaultValue Seq.empty]
                ["" ; yield! Term_Source_Description |> Option.defaultValue Seq.empty]
                ["" ; yield! INVESTIGATION |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Identifier |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Title |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Description |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Submission_Date |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Public_Release_Date |> Option.defaultValue Seq.empty]
                ["" ; yield! INVESTIGATION_PUBLICATIONS |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Publication_PubMed_ID |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Publication_DOI |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Publication_Author_List |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Publication_Title |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Publication_Status |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Publication_Status_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Publication_Status_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! INVESTIGATION_CONTACTS |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Last_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_First_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Mid_Initials |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Email |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Phone |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Fax |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Address |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Affiliation |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Roles |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Roles_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Investigation_Person_Roles_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! Comment_ORCID |> Option.defaultValue Seq.empty]
                ["" ; yield! STUDY |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Identifier |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Title |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Description |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Submission_Date |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Public_Release_Date |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_File_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! STUDY_DESIGN_DESCRIPTORS |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Design_Type |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Design_Type_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Design_Type_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! STUDY_PUBLICATIONS |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Publication_PubMed_ID |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Publication_DOI |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Publication_Author_List |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Publication_Title |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Publication_Status |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Publication_Status_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Publication_Status_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! STUDY_FACTORS |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Factor_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Factor_Type |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Factor_Type_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Factor_Type_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! STUDY_ASSAYS |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_Measurement_Type |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_Measurement_Type_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_Measurement_Type_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_Technology_Type |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_Technology_Type_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_Technology_Type_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_Technology_Platform |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Assay_File_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! STUDY_PROTOCOLS |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Type |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Type_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Type_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Description |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_URI |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Version |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Parameters_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Parameters_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Parameters_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Components_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Components_Type |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Components_Type_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Protocol_Components_Type_Term_Source_REF |> Option.defaultValue Seq.empty]
                ["" ; yield! STUDY_CONTACTS |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Last_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_First_Name |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Mid_Initials |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Email |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Phone |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Fax |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Address |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Affiliation |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Roles |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Roles_Term_Accession_Number |> Option.defaultValue Seq.empty]
                ["" ; yield! Study_Person_Roles_Term_Source_REF |> Option.defaultValue Seq.empty]
            ]

        Terms.InvestigationMetadata.nonObsoleteNonRootCvTerms
        |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
        |> List.zip valueRows
        |> List.map (fun (values,term) ->
            values
            |> List.mapi (fun i v ->
                if i = 0 then
                    CvParam(term, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, [])
                else
                    CvParam(term, ParamValue.Value v, [])
            )
        )

    /// <summary>
    /// returns a mock list of CvParams lists which each represent a tokenized row of study metadata.
    /// each mandatory row is present, containing at least one CvParam annotated as MetadataSectionKey (the first column of an ISA-XLSX study metadata sheet).
    /// each row can be expanded with additional CvParams by setting the respective optional argument.
    /// </summary>
    static member StudyMetadataTokens(
        ?STUDY: seq<string>,
        ?Study_Identifier: seq<string>,
        ?Study_Title: seq<string>,
        ?Study_Description: seq<string>,
        ?Study_Submission_Date: seq<string>,
        ?Study_Public_Release_Date: seq<string>,
        ?Study_File_Name: seq<string>,
        ?STUDY_DESIGN_DESCRIPTORS: seq<string>,
        ?Study_Design_Type: seq<string>,
        ?Study_Design_Type_Term_Accession_Number: seq<string>,
        ?Study_Design_Type_Term_Source_REF: seq<string>,
        ?STUDY_PUBLICATIONS: seq<string>,
        ?Study_Publication_PubMed_ID: seq<string>,
        ?Study_Publication_DOI: seq<string>,
        ?Study_Publication_Author_List: seq<string>,
        ?Study_Publication_Title: seq<string>,
        ?Study_Publication_Status: seq<string>,
        ?Study_Publication_Status_Term_Accession_Number: seq<string>,
        ?Study_Publication_Status_Term_Source_REF: seq<string>,
        ?STUDY_FACTORS: seq<string>,
        ?Study_Factor_Name: seq<string>,
        ?Study_Factor_Type: seq<string>,
        ?Study_Factor_Type_Term_Accession_Number: seq<string>,
        ?Study_Factor_Type_Term_Source_REF: seq<string>,
        ?STUDY_ASSAYS: seq<string>,
        ?Study_Assay_Measurement_Type: seq<string>,
        ?Study_Assay_Measurement_Type_Term_Accession_Number: seq<string>,
        ?Study_Assay_Measurement_Type_Term_Source_REF: seq<string>,
        ?Study_Assay_Technology_Type: seq<string>,
        ?Study_Assay_Technology_Type_Term_Accession_Number: seq<string>,
        ?Study_Assay_Technology_Type_Term_Source_REF: seq<string>,
        ?Study_Assay_Technology_Platform: seq<string>,
        ?Study_Assay_File_Name: seq<string>,
        ?STUDY_PROTOCOLS: seq<string>,
        ?Study_Protocol_Name: seq<string>,
        ?Study_Protocol_Type: seq<string>,
        ?Study_Protocol_Type_Term_Accession_Number: seq<string>,
        ?Study_Protocol_Type_Term_Source_REF: seq<string>,
        ?Study_Protocol_Description: seq<string>,
        ?Study_Protocol_URI: seq<string>,
        ?Study_Protocol_Version: seq<string>,
        ?Study_Protocol_Parameters_Name: seq<string>,
        ?Study_Protocol_Parameters_Term_Accession_Number: seq<string>,
        ?Study_Protocol_Parameters_Term_Source_REF: seq<string>,
        ?Study_Protocol_Components_Name: seq<string>,
        ?Study_Protocol_Components_Type: seq<string>,
        ?Study_Protocol_Components_Type_Term_Accession_Number: seq<string>,
        ?Study_Protocol_Components_Type_Term_Source_REF: seq<string>,
        ?STUDY_CONTACTS: seq<string>,
        ?Study_Person_Last_Name: seq<string>,
        ?Study_Person_First_Name: seq<string>,
        ?Study_Person_Mid_Initials: seq<string>,
        ?Study_Person_Email: seq<string>,
        ?Study_Person_Phone: seq<string>,
        ?Study_Person_Fax: seq<string>,
        ?Study_Person_Address: seq<string>,
        ?Study_Person_Affiliation: seq<string>,
        ?Study_Person_Roles: seq<string>,
        ?Study_Person_Roles_Term_Accession_Number: seq<string>,
        ?Study_Person_Roles_Term_Source_REF: seq<string>
    ) =
        let valueRows = [
            [""; yield! STUDY |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Identifier |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Title |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Description |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Submission_Date |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Public_Release_Date |> Option.defaultValue Seq.empty ]
            [""; yield! Study_File_Name |> Option.defaultValue Seq.empty ]
            [""; yield! STUDY_DESIGN_DESCRIPTORS |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Design_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Design_Type_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Design_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! STUDY_PUBLICATIONS |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Publication_PubMed_ID |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Publication_DOI |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Publication_Author_List |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Publication_Title |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Publication_Status |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Publication_Status_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Publication_Status_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! STUDY_FACTORS |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Factor_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Factor_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Factor_Type_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Factor_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! STUDY_ASSAYS |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_Measurement_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_Measurement_Type_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_Measurement_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_Technology_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_Technology_Type_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_Technology_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_Technology_Platform |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Assay_File_Name |> Option.defaultValue Seq.empty ]
            [""; yield! STUDY_PROTOCOLS |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Type_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Description |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_URI |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Version |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Parameters_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Parameters_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Parameters_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Components_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Components_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Components_Type_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Protocol_Components_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! STUDY_CONTACTS |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Last_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_First_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Mid_Initials |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Email |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Phone |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Fax |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Address |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Affiliation |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Roles |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Roles_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Study_Person_Roles_Term_Source_REF |> Option.defaultValue Seq.empty ]
        ]

        Terms.StudyMetadata.nonObsoleteNonRootCvTerms
        |> List.filter (fun t -> not (t.Name.StartsWith("Comment"))) // ignore all comments
        |> List.zip valueRows
        |> List.map (fun (values,term) ->
            values
            |> List.mapi (fun i v ->
                if i = 0 then
                    CvParam(term, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, [])
                else
                    CvParam(term, ParamValue.Value v, [])
            )
        )

    /// <summary>
    /// returns a mock list of CvParams lists which each represent a tokenized row of assay metadata.
    /// each mandatory row is present, containing at least one CvParam annotated as MetadataSectionKey (the first column of an ISA-XLSX assay metadata sheet).
    /// each row can be expanded with additional CvParams by setting the respective optional argument.
    /// </summary>
    static member AssayMetadataTokens(
        ?ASSAY: seq<string>,
        ?Assay_Measurement_Type: seq<string>,
        ?Assay_Measurement_Type_Term_Accession_Number: seq<string>,
        ?Assay_Measurement_Type_Term_Source_REF: seq<string>,
        ?Assay_Technology_Type: seq<string>,
        ?Assay_Technology_Type_Term_Source_REF: seq<string>,
        ?Assay_Technology_Platform: seq<string>,
        ?Assay_File_Name: seq<string>,
        ?ASSAY_PERFORMERS: seq<string>,
        ?Assay_Performer_Last_Name: seq<string>,
        ?Assay_Performer_First_Name: seq<string>,
        ?Assay_Performer_Mid_Initials: seq<string>,
        ?Assay_Performer_Email: seq<string>,
        ?Assay_Performer_Phone: seq<string>,
        ?Assay_Performer_Fax: seq<string>,
        ?Assay_Performer_Address: seq<string>,
        ?Assay_Performer_Affiliation: seq<string>,
        ?Assay_Performer_Roles: seq<string>,
        ?Assay_Performer_Roles_Term_Accession_Number: seq<string>,
        ?Assay_Performer_Roles_Term_Source_REF: seq<string>
    ) =
        let valueRows = [
            [""; yield! ASSAY |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Measurement_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Measurement_Type_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Measurement_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Technology_Type |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Technology_Type_Term_Source_REF |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Technology_Platform |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_File_Name |> Option.defaultValue Seq.empty ]
            [""; yield! ASSAY_PERFORMERS |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Last_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_First_Name |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Mid_Initials |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Email |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Phone |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Fax |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Address |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Affiliation |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Roles |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Roles_Term_Accession_Number |> Option.defaultValue Seq.empty ]
            [""; yield! Assay_Performer_Roles_Term_Source_REF |> Option.defaultValue Seq.empty ]
        ]

        Terms.AssayMetadata.nonObsoleteNonRootCvTerms
        |> List.filter (fun t -> not (t.Name.StartsWith("Comment"))) // ignore all comments
        |> List.zip valueRows
        |> List.map (fun (values,term) ->
            values
            |> List.mapi (fun i v ->
                if i = 0 then
                    CvParam(term, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, [])
                else
                    CvParam(term, ParamValue.Value v, [])
            )
        )