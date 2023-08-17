(*** hide ***)

(*** condition: prepare ***)
#r "nuget: FSharpAux.Core, [2.0.0]"
#r "nuget: FsOboParser, [0.1.0]"
#r "nuget: FsSpreadsheet, [4.1.0]"
#r "nuget: FsSpreadsheet.ExcelIO, [4.1.0]"
#r "nuget: FsSpreadsheet.Interactive, [4.1.0]"
#r "../src/ControlledVocabulary/bin/Release/netstandard2.0/ControlledVocabulary.dll"
#r "../src/ARCTokenization/bin/Release/netstandard2.0/ARCTokenization.dll"

(**
# ARCTokenization

The ARCTokenization project consists of 2 libraries:

 - **ControlledVocabulary**: A generic data model for controlled vocabularies
 - **ARCTokenization** A library for tokenizing ARCs into controlled vocabulary tokens

*)