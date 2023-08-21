# CvParam


```fsharp
#r "nuget: ControlledVocabulary, 1.0.0"
```


<div><div></div><div></div><div><strong>Installed Packages</strong><ul><li><span>ControlledVocabulary, 1.0.0</span></li></ul></div></div>


`CvParam` is a value that is annotated with a controlled vocabulary term.

Suppose we have the name of a person (`"Kevin Schneider"`).

We can annotate this value with a term to indicate that it is a name:


```fsharp
open ControlledVocabulary

let human =
    CvTerm.create(
        accession = "TO:00042069", // the term's unique accession number
        name = "Full Name", // the term name
        ref = "https://link/to/reference/vocabulary/named/TO" // the reference vocabulary
    )

let cvp = 
    CvParam(
        cvTerm = human,
        pv = ParamValue.Value "Kevin Schneider"
    )

cvp.ToString()
```


    CvParam: Full Name
    	ID: TO:00042069
    	RefUri: https://link/to/reference/vocabulary/named/TO
    	Value: Value "Kevin Schneider"
    	Attributes: []

