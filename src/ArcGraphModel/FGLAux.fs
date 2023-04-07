/// Additional functions to work with FSharp.FGL graphs.
module FGLAux

open FSharp.FGL
open FSharp.FGL.ArrayAdjacencyGraph

/// <summary>
/// Takes a graph and returns an array of its LVertices with keys and labels.
/// </summary>
let extractVerticesWithLabels (graph : ArrayAdjacencyGraph<'a,'b,'c>) : LVertex<'a,'b> [] =
    (graph.GetVertices(), graph.GetLabels()) ||> Array.zip