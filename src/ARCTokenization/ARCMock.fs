namespace ARCTokenization

open ControlledVocabulary
open ARCtrl
open ARCtrl.ISA

type ARCMock =

    /// <summary>
    /// returns a mock list of CvParams lists which each represent a tokenized row of investigation metadata.
    /// each mandatory row is present, containing at least one CvParam annotated as MetadataSectionKey (the first column of an ISA-XLSX investigation metadata sheet).
    /// each row can be expanded with additional CvParams by setting the respective optional argument.
    /// </summary>
    static member InvestigationMetadataTokens(
        deleteEmpty:bool,
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
                ONTOLOGY_SOURCE_REFERENCE
                Term_Source_Name
                Term_Source_File
                Term_Source_Version
                Term_Source_Description
                INVESTIGATION
                Investigation_Identifier
                Investigation_Title
                Investigation_Description
                Investigation_Submission_Date
                Investigation_Public_Release_Date
                INVESTIGATION_PUBLICATIONS
                Investigation_Publication_PubMed_ID
                Investigation_Publication_DOI
                Investigation_Publication_Author_List
                Investigation_Publication_Title
                Investigation_Publication_Status
                Investigation_Publication_Status_Term_Accession_Number
                Investigation_Publication_Status_Term_Source_REF
                INVESTIGATION_CONTACTS
                Investigation_Person_Last_Name
                Investigation_Person_First_Name
                Investigation_Person_Mid_Initials
                Investigation_Person_Email
                Investigation_Person_Phone
                Investigation_Person_Fax
                Investigation_Person_Address
                Investigation_Person_Affiliation
                Investigation_Person_Roles
                Investigation_Person_Roles_Term_Accession_Number
                Investigation_Person_Roles_Term_Source_REF
                Comment_ORCID
                STUDY
                Study_Identifier
                Study_Title
                Study_Description
                Study_Submission_Date
                Study_Public_Release_Date
                Study_File_Name
                STUDY_DESIGN_DESCRIPTORS
                Study_Design_Type
                Study_Design_Type_Term_Accession_Number
                Study_Design_Type_Term_Source_REF
                STUDY_PUBLICATIONS
                Study_Publication_PubMed_ID
                Study_Publication_DOI
                Study_Publication_Author_List
                Study_Publication_Title
                Study_Publication_Status
                Study_Publication_Status_Term_Accession_Number
                Study_Publication_Status_Term_Source_REF
                STUDY_FACTORS
                Study_Factor_Name
                Study_Factor_Type
                Study_Factor_Type_Term_Accession_Number
                Study_Factor_Type_Term_Source_REF
                STUDY_ASSAYS
                Study_Assay_Measurement_Type
                Study_Assay_Measurement_Type_Term_Accession_Number
                Study_Assay_Measurement_Type_Term_Source_REF
                Study_Assay_Technology_Type
                Study_Assay_Technology_Type_Term_Accession_Number
                Study_Assay_Technology_Type_Term_Source_REF
                Study_Assay_Technology_Platform
                Study_Assay_File_Name
                STUDY_PROTOCOLS
                Study_Protocol_Name
                Study_Protocol_Type
                Study_Protocol_Type_Term_Accession_Number
                Study_Protocol_Type_Term_Source_REF
                Study_Protocol_Description
                Study_Protocol_URI
                Study_Protocol_Version
                Study_Protocol_Parameters_Name
                Study_Protocol_Parameters_Term_Accession_Number
                Study_Protocol_Parameters_Term_Source_REF
                Study_Protocol_Components_Name
                Study_Protocol_Components_Type
                Study_Protocol_Components_Type_Term_Accession_Number
                Study_Protocol_Components_Type_Term_Source_REF
                STUDY_CONTACTS
                Study_Person_Last_Name
                Study_Person_First_Name
                Study_Person_Mid_Initials
                Study_Person_Email
                Study_Person_Phone
                Study_Person_Fax
                Study_Person_Address
                Study_Person_Affiliation
                Study_Person_Roles
                Study_Person_Roles_Term_Accession_Number
                Study_Person_Roles_Term_Source_REF
            ]

        if deleteEmpty then
            Terms.InvestigationMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
            |> List.zip valueRows
            |> List.choose (fun (values,term) -> 
                match values with
                | None -> None
                | Some v -> 
                    Some ([""; yield! v],term))
            |> List.map (fun (values,term) ->
                values
                |> List.mapi (fun i v ->
                    if i = 0 then
                        CvParam(term, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, [])
                    else
                        CvParam(term, ParamValue.Value v, [])
                )
            )
        else
            Terms.InvestigationMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
            |> List.zip valueRows
            |> List.map (fun (values,term) ->
                [""; yield! values  |> Option.defaultValue Seq.empty ]
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
        deleteEmpty:bool,
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
            STUDY 
            Study_Identifier
            Study_Title
            Study_Description
            Study_Submission_Date
            Study_Public_Release_Date 
            Study_File_Name 
            STUDY_DESIGN_DESCRIPTORS 
            Study_Design_Type 
            Study_Design_Type_Term_Accession_Number 
            Study_Design_Type_Term_Source_REF 
            STUDY_PUBLICATIONS 
            Study_Publication_PubMed_ID 
            Study_Publication_DOI 
            Study_Publication_Author_List 
            Study_Publication_Title 
            Study_Publication_Status 
            Study_Publication_Status_Term_Accession_Number 
            Study_Publication_Status_Term_Source_REF 
            STUDY_FACTORS 
            Study_Factor_Name 
            Study_Factor_Type 
            Study_Factor_Type_Term_Accession_Number 
            Study_Factor_Type_Term_Source_REF 
            STUDY_ASSAYS 
            Study_Assay_Measurement_Type 
            Study_Assay_Measurement_Type_Term_Accession_Number 
            Study_Assay_Measurement_Type_Term_Source_REF 
            Study_Assay_Technology_Type 
            Study_Assay_Technology_Type_Term_Accession_Number 
            Study_Assay_Technology_Type_Term_Source_REF 
            Study_Assay_Technology_Platform 
            Study_Assay_File_Name 
            STUDY_PROTOCOLS 
            Study_Protocol_Name 
            Study_Protocol_Type 
            Study_Protocol_Type_Term_Accession_Number 
            Study_Protocol_Type_Term_Source_REF 
            Study_Protocol_Description 
            Study_Protocol_URI 
            Study_Protocol_Version 
            Study_Protocol_Parameters_Name 
            Study_Protocol_Parameters_Term_Accession_Number 
            Study_Protocol_Parameters_Term_Source_REF 
            Study_Protocol_Components_Name 
            Study_Protocol_Components_Type 
            Study_Protocol_Components_Type_Term_Accession_Number 
            Study_Protocol_Components_Type_Term_Source_REF 
            STUDY_CONTACTS 
            Study_Person_Last_Name 
            Study_Person_First_Name 
            Study_Person_Mid_Initials 
            Study_Person_Email 
            Study_Person_Phone 
            Study_Person_Fax 
            Study_Person_Address 
            Study_Person_Affiliation 
            Study_Person_Roles 
            Study_Person_Roles_Term_Accession_Number 
            Study_Person_Roles_Term_Source_REF 
        ]

        if deleteEmpty then
            Terms.StudyMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
            |> List.zip valueRows
            |> List.choose (fun (values,term) -> 
                match values with
                | None -> None
                | Some v -> 
                    Some ([""; yield! v],term))
            |> List.map (fun (values,term) ->
                values
                |> List.mapi (fun i v ->
                    if i = 0 then
                        CvParam(term, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, [])
                    else
                        CvParam(term, ParamValue.Value v, [])
                )
            )
        else
            Terms.StudyMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
            |> List.zip valueRows
            |> List.map (fun (values,term) ->
                [""; yield! values  |> Option.defaultValue Seq.empty ]
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
        deleteEmpty:bool,
        ?ASSAY: seq<string>,
        ?Assay_Measurement_Type: seq<string>,
        ?Assay_Measurement_Type_Term_Accession_Number: seq<string>,
        ?Assay_Measurement_Type_Term_Source_REF: seq<string>,
        ?Assay_Technology_Type: seq<string>,
        ?Assay_Technology_Type_Term_Accession_Number: seq<string>,
        ?Assay_Technology_Type_Term_Source_REF: seq<string>,
        ?Assay_Technology_Platform: seq<string>,
        ?Assay_File_Name: seq<string>,
        ?ASSAY_PERFORMERS: seq<string>,
        ?Assay_Person_Last_Name: seq<string>,
        ?Assay_Person_First_Name: seq<string>,
        ?Assay_Person_Mid_Initials: seq<string>,
        ?Assay_Person_Email: seq<string>,
        ?Assay_Person_Phone: seq<string>,
        ?Assay_Person_Fax: seq<string>,
        ?Assay_Person_Address: seq<string>,
        ?Assay_Person_Affiliation: seq<string>,
        ?Assay_Person_Roles: seq<string>,
        ?Assay_Person_Roles_Term_Accession_Number: seq<string>,
        ?Assay_Person_Roles_Term_Source_REF: seq<string>
    ) =
        let valueRows = [
            ASSAY 
            Assay_Measurement_Type 
            Assay_Measurement_Type_Term_Accession_Number 
            Assay_Measurement_Type_Term_Source_REF 
            Assay_Technology_Type 
            Assay_Technology_Type_Term_Accession_Number 
            Assay_Technology_Type_Term_Source_REF 
            Assay_Technology_Platform 
            Assay_File_Name 
            ASSAY_PERFORMERS 
            Assay_Person_Last_Name 
            Assay_Person_First_Name 
            Assay_Person_Mid_Initials 
            Assay_Person_Email 
            Assay_Person_Phone 
            Assay_Person_Fax 
            Assay_Person_Address 
            Assay_Person_Affiliation 
            Assay_Person_Roles 
            Assay_Person_Roles_Term_Accession_Number 
            Assay_Person_Roles_Term_Source_REF 
        ]

        if deleteEmpty then
            Terms.AssayMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
            |> List.zip valueRows
            |> List.choose (fun (values,term) -> 
                match values with
                | None -> None
                | Some v -> 
                    Some ([""; yield! v],term))
            |> List.map (fun (values,term) ->
                values
                |> List.mapi (fun i v ->
                    if i = 0 then
                        CvParam(term, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, [])
                    else
                        CvParam(term, ParamValue.Value v, [])
                )
            )
        else
            Terms.AssayMetadata.nonObsoleteNonRootCvTerms
            |> List.filter (fun t -> (not (t.Name.StartsWith("Comment"))) || (t.Name.Equals("Comment[ORCID]"))) // ignore all comments except non-obsolete orcid
            |> List.zip valueRows
            |> List.map (fun (values,term) ->
                [""; yield! values  |> Option.defaultValue Seq.empty ]
                |> List.mapi (fun i v ->
                    if i = 0 then
                        CvParam(term, ParamValue.CvValue Terms.StructuralTerms.metadataSectionKey, [])
                    else
                        CvParam(term, ParamValue.Value v, [])
                )
            )

    static member ProcessGraphColumn(
        header: ARCtrl.ISA.CompositeHeader,
        cells: seq<ARCtrl.ISA.CompositeCell>
    ) =
        CompositeColumn.create(header, cells |> Array.ofSeq)
        |> Tokenization.ARCtrl.CompositeColumn.tokenize

    static member ProcessGraph(
        columns: seq<ARCtrl.ISA.CompositeHeader *  seq<ARCtrl.ISA.CompositeCell>>
    ) =
        let table = ArcTable.create("", new ResizeArray<_>(), new System.Collections.Generic.Dictionary<_,_>())
        
        columns
        |> Seq.map (fun (headerTerm, cells) -> 
            CompositeColumn.create(headerTerm, cells |> Array.ofSeq)
        )
        |> Array.ofSeq
        |> table.AddColumns
        
        table
        |> Tokenization.ARCtrl.ARCTable.tokenizeColumns