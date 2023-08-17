module ReferenceObjects

open ControlledVocabulary

let testAccession1 = "TO:00000001"
let testName1 = "Test"
let testRef1 = "TO"

let testTerm1 = CvTerm.create(accession = testAccession1, name = testName1, ref = testRef1)

let testAccession2 = "TO:00000002"
let testName2 = "5"
let testRef2 = "TO"

let testTerm2 = CvTerm.create(accession = testAccession2, name = testName2, ref = testRef2)

let ``CvParam with ParamValue.Value`` = CvParam(testTerm1, ParamValue.Value 5)
let ``CvParam with ParamValue.CvValue`` = CvParam(testTerm1, ParamValue.CvValue testTerm2)
let ``CvParam with ParamValue.WithCvUnitAccession`` = CvParam(testTerm2, ParamValue.WithCvUnitAccession (5, testTerm1))

let testCvParams = 
    [
        ``CvParam with ParamValue.Value``
        ``CvParam with ParamValue.CvValue``
        ``CvParam with ParamValue.WithCvUnitAccession``
    ]