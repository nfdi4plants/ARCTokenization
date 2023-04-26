module Tests

#if FABLE_COMPILER
open Fable.Mocha
#else
open Expecto

[<Tests>]
#endif
let all =
    testList "All"
        [
            ArcTypeTests.main
            CvBaseTests.main
            ParamTests.main
            ContainerTests.main
        ]

let [<EntryPoint>] main argv = 
    #if FABLE_COMPILER
    Mocha.runTests all
    #else
    Tests.runTestsWithCLIArgs [] argv all
    #endif
