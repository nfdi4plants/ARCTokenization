namespace ARCTokenization.StructuralOntology

open ControlledVocabulary
open OBO.NET
open FSharpAux


/// Functions to generate F# source code from OBO files.
module CodeGeneration =

    [<Literal>]
    let baseString = """namespace ARCTokenization.StructuralOntology

    open ControlledVocabulary

    module <name> =

    """

    /// Takes an OboTerm and returns its name but with all spaces replaced by underscores.
    let toUnderscoredName (term : OboTerm) = 
        term.Name
        |> String.replace " " "_"

    /// Takes an OboTerm and returns its TermSourceRef as string.
    let toTermSourceRef (term : OboTerm) =
        term.Id
        |> String.takeWhile ((<>) ':')

    /// Takes an OboTerm and transforms it into an F# code string for structural ontology libraries.
    let toCodeString (term : OboTerm) = 
        $"    let {toUnderscoredName term} = CvTerm.create(\"{term.Id}\", \"{term.Name}\", \"{toTermSourceRef term}\"){System.Environment.NewLine}{System.Environment.NewLine}"

    /// Takes a module name and a
    let toSourceCode moduleName (onto : OboOntology) =
        let concattedSingleValues = String.init onto.Terms.Length (fun i -> $"{toCodeString onto.Terms[i]}")
        let updatedBaseString = String.replace "<name>" moduleName
        $"{updatedBaseString}{concattedSingleValues}"

    let toFile moduleName (onto : OboOntology) path =
        System.IO.File.WriteAllText(path, toSourceCode moduleName onto)