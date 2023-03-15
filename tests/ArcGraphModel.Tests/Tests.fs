namespace ArcGraphModel.Tests

open System
open Xunit

module ``are test names nested?`` =
    [<Fact>]
    let ``My test`` () =
        Assert.True(true)

    module ``yes`` =
        [<Fact>]
        let ``yup?`` () =
            Assert.True(true)
        module ``but weirdly`` =
            module ``with a plus sign`` =
                [<Fact>]
                let ``for intermediate module namespaces`` () =
                    Assert.True(true)
    [<Fact>]
    let ``Maybe?`` () =
        Assert.True(true)