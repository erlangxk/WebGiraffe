module Tests

open Expecto

[<Tests>]
let tests =
  testList "samples" [
    testCase "universe exists" <| fun _ ->
      let subject = false
      Expect.isTrue subject "I compute, therefore I am."
  ]
