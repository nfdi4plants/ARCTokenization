namespace CvTermTests


open ControlledVocabulary
open ReferenceObjects

open Xunit


module CheckForUri =

    [<Fact>]
    let ``correct check, actual URL 1`` () =
        let check = CvTerm.checkForUri testAccession3
        Assert.True check

    [<Fact>]
    let ``correct check, actual URL 2`` () =
        let check = CvTerm.checkForUri "https://purl.org/TO_00000003"
        Assert.True check

    [<Fact>]
    let ``correct check, no URL`` () =
        let check = CvTerm.checkForUri "purl/123_abc"
        Assert.False check


module UriToTan =

    [<Fact>]
    let ``correct TAN returned`` () =
        let expected = "TO:00000003"
        let actual = CvTerm.uriToTan testAccession3
        Assert.Equal(expected, actual)


module RefOfAccession =

    [<Fact>]
    let ``correct TSR returned`` () =
        let expected = "TO"
        let actual = CvTerm.refOfAccession testAccession1
        Assert.Equal(expected, actual)


module Create =

    [<Fact>]
    let ``correct CvTerm, primary create function overload`` () =
        let expected = testTerm1
        let actual = CvTerm.create(testAccession1, testName1, testRef1)
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``correct CvTerm, secondary create function overload (only name given)`` () =
        let expected = testTerm4
        let actual = CvTerm.create(testName2)
        Assert.Equal(expected, actual)

    [<Fact>]
    let ``correct CvTerm, create function with URL as accession`` () =
        let expected = testTerm3
        let actual = CvTerm.create(testAccession3, testName2, testRef2)
        Assert.Equal(expected, actual)