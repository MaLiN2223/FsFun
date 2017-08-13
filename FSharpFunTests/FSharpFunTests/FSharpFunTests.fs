module FSharpFunTests
open Expecto

[<Tests>]
let tests =
  testCase "yes" <| fun () ->
    let subject = "Hello World"
    Expect.equal subject "Hello world"
                 "The strings should equal"

[<EntryPoint>]
let main argv =
    printfn "%A" argv
    0 // return an integer exit code
