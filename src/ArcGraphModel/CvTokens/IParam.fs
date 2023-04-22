namespace ArcGraphModel

open System.Collections.Generic
open FSharpAux


type IParam =

    inherit ICvBase
    inherit IParamBase


module Param =

    let getValueAsString (iParam : IParam) =
        (iParam :> IParamBase).Value
        |> ParamValue.getValueAsString

// TO DO: create-Funktionen in UserParam und CvParam sollen IParam zurückgeben statt ihren eigenen Typ
