module DocumentationTasks

open Helpers
open ProjectInfo
open BasicTasks

open BlackFox.Fake
open System.IO
open Fake.Core
open Fake.IO

let notebooks =  Directory.EnumerateFiles("docs", "*.ipynb", SearchOption.AllDirectories)

let preprocessNotebooks =
    BuildTask.create "PreprocessNotebooks" [ ] {
        notebooks
        |> Seq.iter (fun nb -> 
            let dir = Path.GetDirectoryName nb
            let file = Path.GetFileName nb
            CreateProcess.fromRawCommandLine "jupyter" $"nbconvert {nb} --to markdown --output-dir={dir}"
            |> Proc.run // start with the above configuration
            |> ignore // ignore exit code
            File.ReadAllText(nb.Replace(".ipynb", ".md"))
            |> String.replace "polyglot-notebook" "fsharp"
            |> fun p -> File.WriteAllText(nb.Replace(".ipynb", ".md"), p)
        )
    }

let buildDocs =
    BuildTask.create "BuildDocs" [ build; preprocessNotebooks ] {
        runDotNet "docfx docs/docfx_project/docfx.json" "./"
    }

let watchDocs =
    BuildTask.create "WatchDocs" [ build; preprocessNotebooks ] {
        runDotNet "docfx docs/docfx_project/docfx.json --serve" "./"
    }