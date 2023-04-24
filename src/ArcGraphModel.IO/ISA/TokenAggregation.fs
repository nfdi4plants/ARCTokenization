namespace ArcGraphModel.IO

open ArcGraphModel

module List = 
    
    let splitByPredicate (predicate : 'T -> bool) (l : list<'T>) : 'T list * 'T list = 
        let rec loop toDo positives negatives = 
            match toDo with
            | head :: tail when predicate head -> loop tail (head :: positives) negatives
            | head :: tail -> loop tail positives (head :: negatives)
            | [] -> positives |> List.rev,negatives |> List.rev
        loop l [] []

module TokenAggregation = 

    let mergeParams (child : IParam) (parent : IParam) =
        if child |> Param.equalsTerm Terms.annotationID then
            Param.tryAddAnnotationID (Param.getValueAsString child) parent
        elif child |> Param.equalsTerm Terms.termSourceRef then
            Param.tryAddReference (Param.getValueAsString child) parent
        else
            None
        |> Option.defaultValue parent    

    let aggregateParams (parameters : IParam list) : IParam list = 
        let rec loop (left : IParam list) (current : IParam) (aggregated : IParam list ) = 
            match left with
            | head :: tail when CvAttributeCollection.isStructuralChildOf current head ->
                loop tail (mergeParams head current) aggregated
            | head :: tail -> loop tail head (aggregated @ [current])
            | [] -> aggregated @ [current]
        match parameters with
        | head :: tail -> loop tail head []
        | [] -> []

    let fillContainer (container : CvContainer) (tokens : IParam list) : ICvBase list = 
        if List.isEmpty tokens then 
            [container]
        else
            let packable,unpackable =        
                tokens
                |> List.splitByPredicate (fun t -> 
                    CvAttributeCollection.containsAttribute (CvTerm.getName Address.column) t 
                    && CvAttributeCollection.containsAttribute (CvTerm.getName Address.row) t 
                    && (CvAttributeCollection.isStructuralChildOf container t || CvBase.equals container t))   
            packable
            |> List.groupBy (fun t -> CvAttributeCollection.tryGetAttribute (CvTerm.getName Address.column) t |> Option.map Param.getValue)
            |> List.map (fun (_,parameters) -> 
                let container = CvContainer(container |> CvBase.getTerm, container.Attributes)
                let parameters = aggregateParams parameters
                CvContainer.addMany (parameters |> Seq.map (fun v -> v :> ICvBase)) container               
                container :> ICvBase)
            |> fun l -> List.append l (unpackable |> List.map (fun v -> v :> ICvBase))


    let aggregateTokens (tokens : ICvBase list) : ICvBase list = 
        let rec loop (unvisitedTokens : ICvBase list) (currentContainer : CvContainer) (currentParams : IParam list) (packedTokens : ICvBase list) : ICvBase list=
            match unvisitedTokens with
            | (:? CvContainer as head) :: tail -> 
                let packedContainers = fillContainer currentContainer currentParams
                loop tail head [] (packedTokens @ packedContainers)
            | (:? CvParam as head) :: tail when CvParam.isStructuralChildOf currentContainer head || CvBase.equals currentContainer head ->
                loop tail currentContainer (currentParams @ [head]) packedTokens
            | (:? UserParam as head) :: tail when CvParam.isStructuralChildOf currentContainer head || CvBase.equals currentContainer head ->
                loop tail currentContainer (currentParams @ [head]) packedTokens
            | head :: tail ->
                loop tail currentContainer currentParams (packedTokens @ [head])
            | [] -> 
                let packedContainers = fillContainer currentContainer currentParams
                (packedTokens @ packedContainers)
        let rec findContainer (unvisitedTokens : ICvBase list) packedTokens = 
            match unvisitedTokens with
            | (:? CvContainer as head) :: tail -> 
                loop tail head [] packedTokens
            | head :: tail ->
                findContainer tail (packedTokens @ [head])
            | [] -> packedTokens
        findContainer tokens []