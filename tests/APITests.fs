module APITests
open Expecto

[<Tests>]
let tests =
    testList "API Tests" [
        testCase "can access API functions" ( fun _ ->
            let expected = 42
            Expect.equal (API.test()) expected "Can't access API functions"
        )
    ]
