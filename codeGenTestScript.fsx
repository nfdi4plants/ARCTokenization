#I "src/ControlledVocabulary/bin/Debug/netstandard2.0"
#I "src/ControlledVocabulary/bin/Release/netstandard2.0"
#r "ControlledVocabulary.dll"
#I "src/ARCTokenization/bin/Debug/netstandard2.0"
#I "src/ARCTokenization/bin/Release/netstandard2.0"
#r "ARCTokenization.dll"

#r "nuget: OBO.NET"
#r "nuget: FSharpAux"


open ControlledVocabulary
open ARCTokenization
open OBO.NET
open FSharpAux


[<Literal>]
let baseString = """module INVSMO =

"""

let onto = OboOntology.fromFile false @"C:\Repos\nfdi4plants\ARCTokenization\src\ARCTokenization\structural_ontologies\investigation_metadata_structural_ontology.obo"

let toUnderscoredName (term : OboTerm) = 
    term.Name
    |> String.replace " " "_"

let toTermSourceRef (term : OboTerm) =
    term.Id
    |> String.takeWhile ((<>) ':')

let toCodeString (term : OboTerm) = 
    $"    let {toUnderscoredName term} = CvTerm.create(\"{term.Id}\", \"{term.Name}\", \"{toTermSourceRef term}\"){System.Environment.NewLine}{System.Environment.NewLine}"

toCodeString onto.Terms.Head

let toSourceCode (onto : OboOntology) =
    let concattedSingleValues = String.init onto.Terms.Length (fun i -> $"{toCodeString onto.Terms[i]}")
    $"{baseString}{concattedSingleValues}"

toSourceCode onto