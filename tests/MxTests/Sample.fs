module Tests

open Expecto
open Microsoft.FSharpLu.Json


type Person = 
  { Username: string
    Password: string}


[<Tests>]
let tests =
  testList "samples" [
    testCase "universe exists" <| fun _ ->
      let subject = false
      Expect.isTrue subject "I compute, therefore I am."
    
    testCase "json" <| fun _ ->
      let a = { Username= "aaa"; Password= "bbb";}
      let s = Compact.serialize a
      printfn "%s" s
      Expect.isTrue true "OK"

    testCase "read json string" <| fun _ ->
      let s = """{ "Username": "aaa",  "Password": "bbb"} """
      let a = Compact.tryDeserialize<Person> s
      match a with 
      | Choice1Of2 p -> printf "person %A" p
      | Choice2Of2 e -> printf "shit %s" e
      Expect.isTrue true "OK"

  ]
