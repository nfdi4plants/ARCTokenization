$post_path=$args[0]
$lang=$args[1]
$parent=(Split-Path $post_path)
$output_file=$post_path.Replace(".ipynb",".md")
jupyter nbconvert $post_path --to markdown --output-dir=$parent
(Get-Content $output_file) -Replace 'polyglot-notebook', $lang | Set-Content $output_file