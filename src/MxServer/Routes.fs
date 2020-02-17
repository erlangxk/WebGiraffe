namespace MxServer

open Giraffe
open FSharpPlus


[<CLIMutable>]
type Credential =
    { UserName: string option
      Password: string option }

    static member Check(c: Credential) =
        let checkName (name: string) =
            let l = name.Length
            l >= 1 && l <= 64

        let checkPassword (pwd: string) =
            let l = pwd.Length
            l >= 1 && l <= 64

        monad {
            let! name = c.UserName
            let! pwd = c.Password
            if (checkName (name) && checkPassword (pwd))
            then return (name, pwd)
            else return! None
        }


module Handlers =
    let register: HttpHandler =
        bindJson<Credential> (fun c ->
            let greeting =
                match Credential.Check(c)  with
                | Some (name,_) -> sprintf "Hello World, from %s" name
                | None -> sprintf "sorry"
            text greeting)

