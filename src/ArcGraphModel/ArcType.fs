namespace ArcGraphModel


/// <summary>
/// Types that describe an ARC object.
/// </summary>
module ArcType =

    /// <summary>
    /// Model of the different types of those Building Blocks that can be described as nodes in a graph-based model.
    /// </summary>
    type NodeType =
        | Source
        | Sink
        | ProtocolRef

    /// <summary>
    /// Model of the different types of Building Blocks in an ARC Annotation Table.
    /// </summary>
    type BuildingBlockType =
        // Term columns
        | Parameter
        | Factor
        | Characteristics
        | Component
        // Source columns
        | Source
        // Output columns
        | Sample
        | Data // DEPRECATED at v0.6.0 [<ObsoleteAttribute>] 
        | RawDataFile
        | DerivedDataFile
        // Featured Columns
        | ProtocolType
        // Single Columns
        | ProtocolREF
        // everything else
        | Freetext of string

        /// <summary>
        /// Is true if this Building Block type is an InputColumn.
        /// </summary>
        member this.IsInputColumn =
            match this with | Source -> true | anythingElse -> false

        /// <summary>
        /// Is true if this Building Block type is an OutputColumn.
        /// </summary>
        member this.IsOutputColumn =
            match this with | Data | Sample | RawDataFile | DerivedDataFile -> true | anythingElse -> false

        /// <summary>
        /// Is true if this Building Block type is a TermColumn.
        ///
        /// The name "TermColumn" refers to all columns with the syntax "Parameter/Factor/etc [TERM-NAME]" and featured columns
        /// such as Protocol Type as these are also represented as a triplet of Maincolumn-TSR-TAN.
        /// </summary>
        member this.IsTermColumn =
            match this with | Parameter | Factor | Characteristics | Component | ProtocolType -> true | anythingElse -> false

        /// <summary>
        /// Is true if the Building Block type is a FeaturedColumn. 
        ///
        /// A FeaturedColumn can be abstracted by Parameter/Factor/Characteristics and describes one common usecase of either.
        /// Such a block will contain TSR and TAN and can be used for directed Term search.
        /// </summary>
        member this.IsFeaturedColumn =
            match this with | ProtocolType -> true | anythingElse -> false

        /// <summary>
        /// Is true if the Building Block type is deprecated and should not be used anymore.
        /// </summary>
        member this.IsDeprecated =
            match this with | Data -> true | anythingElse -> false

        override this.ToString() =
            match this with
            | Parameter         -> "Parameter"
            | Factor            -> "Factor"
            | Characteristics   -> "Characteristics"
            | Component         -> "Component"
            | Sample            -> "Sample Name"
            | Data              -> "Data File Name"
            | RawDataFile       -> "Raw Data File"
            | DerivedDataFile   -> "Derived Data File"
            | ProtocolType      -> "Protocol Type" 
            | Source            -> "Source Name"
            | ProtocolREF       -> "Protocol REF"
            | Freetext str      -> str

        /// <summary>
        /// Parses this BuildingBlockType to the respective NodeType.
        /// </summary>
        /// <exception cref="System.Exception">if the BuildingBlockType cannot be parsed to any NodeType.</exception>
        member this.ToNodeType() =
            if this.IsInputColumn then NodeType.Source
            elif this.IsOutputColumn then NodeType.Sink
            elif this = ProtocolREF then ProtocolRef
            else failwith $"BuildingBlockType {this} is no NodeType."