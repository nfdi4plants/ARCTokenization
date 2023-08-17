(**
---
title: Introduction to ControlledVocabularies
category: ControlledVocabulary
categoryindex: 1
index: 1
---
*)


(*** hide ***)

(*** condition: prepare ***)
#r "nuget: FSharpAux.Core, [2.0.0]"
#r "nuget: FsOboParser, [0.1.0]"
#r "nuget: FsSpreadsheet, [4.1.0]"
#r "nuget: FsSpreadsheet.ExcelIO, [4.1.0]"
#r "nuget: FsSpreadsheet.Interactive, [4.1.0]"
#r "../../src/ControlledVocabulary/bin/Release/netstandard2.0/ControlledVocabulary.dll"
#r "../../src/ARCTokenization/bin/Release/netstandard2.0/ARCTokenization.dll"

(**
# Introduction to ControlledVocabularies

`<insert introduction text about CVs and ontology terms here>`

*)

open ControlledVocabulary

let myTerm = 
    CvTerm.create(
        accession = "MYONT:00000001",
        name = "My Term",
        ref = "MyOntology"
    )

(*** include-value:myTerm ***)