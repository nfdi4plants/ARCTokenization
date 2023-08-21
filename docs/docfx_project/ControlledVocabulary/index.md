# ControlledVocabulary

ControlledVocabulary is a .NET library for modelling and working with controlled vocabularies.


```fsharp
#r "nuget: ControlledVocabulary, 1.0.0"
```


<div><div></div><div></div><div><strong>Installed Packages</strong><ul><li><span>ControlledVocabulary, 1.0.0</span></li><li><span>Plotly.NET, 4.2.0</span></li><li><span>Plotly.NET.Interactive, 4.2.1</span></li></ul></div></div>



    Loading extensions from `C:\Users\schne\.nuget\packages\plotly.net.interactive\4.2.1\interactive-extensions\dotnet\Plotly.NET.Interactive.dll`


In its barest form, a term from a controlled vocabulary consists of the term name itself, a URI for the term, and a reference to the controlled vocabulary that contains the term:


```fsharp
open ControlledVocabulary

CvTerm.create(
    accession = "TO:00042069", // the term's unique accession number
    name = "yup", // the term name
    ref = "https://link/to/reference/vocabulary/named/TO" // the reference vocabulary
)
```


<details open="open" class="dni-treeview"><summary><span class="dni-code-hint"><code>{ Accession = &quot;TO:00042069&quot;\n  Name = &quot;yup&quot;\n  RefUri = &quot;https://link/to/reference/vocabulary/named/TO&quot; }</code></span></summary><div><table><thead><tr></tr></thead><tbody><tr><td>Accession</td><td><div class="dni-plaintext"><pre>TO:00042069</pre></div></td></tr><tr><td>Name</td><td><div class="dni-plaintext"><pre>yup</pre></div></td></tr><tr><td>RefUri</td><td><div class="dni-plaintext"><pre>https://link/to/reference/vocabulary/named/TO</pre></div></td></tr></tbody></table></div></details><style>

.dni-code-hint {

    font-style: italic;

    overflow: hidden;

    white-space: nowrap;

}

.dni-treeview {

    white-space: nowrap;

}

.dni-treeview td {

    vertical-align: top;

    text-align: start;

}

details.dni-treeview {

    padding-left: 1em;

}

table td {

    text-align: start;

}

table tr { 

    vertical-align: top; 

    margin: 0em 0px;

}

table tr td pre 

{ 

    vertical-align: top !important; 

    margin: 0em 0px !important;

} 

table th {

    text-align: start;

}

</style>

