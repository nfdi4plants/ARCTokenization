module TestUtils

open Expecto


module CvParam =

    open ArcGraphModel

    let termNamesEqual (cvpActual : CvParam) (cvpExpectec : CvParam) =
        Expect.equal (CvBase.getCvName cvpActual) (CvBase.getCvName cvpExpectec) "CvParam names are not equal"

    //let attributesEqual (cvp1 : CvParam) (cvp2 : CvParam) =
    //    (cvp1.Keys, cvp1.Values |> Seq.map ) 
    //    ||> Seq.zip 
    //    |> Seq.toList 
    //    |> List.sortBy fst


module UserParam =

    open ArcGraphModel

    let termNamesEqual (upActual : UserParam) (upExpectec : UserParam) =
        Expect.equal (CvBase.getCvName upActual) (CvBase.getCvName upExpectec) "UserParam names are not equal"

    //let attributesEqual (cvp1 : CvParam) (cvp2 : CvParam) =
    //    (cvp1.Keys, cvp1.Values |> Seq.map ) 
    //    ||> Seq.zip 
    //    |> Seq.toList 
    //    |> List.sortBy fst