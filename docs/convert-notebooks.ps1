$lang = $args[0]
$paths = ".\docfx_project\ARCTokenization", ".\docfx_project\ControlledVocabulary"
foreach ($path in $paths) {
    Write-Host "Converting notebooks in $path to $lang "
    $files = Get-ChildItem -Path $path -Filter *.ipynb -Recurse
    Write-Host "found notebooks: $files "
    foreach ($file in $files) {
        $filePath = Join-Path $path $file
        $parent=(Split-Path $filePath)
        $output_file=$filePath.Replace(".ipynb",".md")
        jupyter nbconvert $filePath --to markdown --output-dir=$parent
        (Get-Content $output_file) -Replace 'polyglot-notebook', $lang | Set-Content $output_file
    }
}
