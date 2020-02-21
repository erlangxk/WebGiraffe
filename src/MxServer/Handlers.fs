namespace MxServer

open FSharp.Control.Tasks.V2
open Microsoft.AspNetCore.Http
open Giraffe

module Handlers =
    [<CLIMutable>]
    type Credential =
        { Username: string
          Password: string }

        static member Check(c: Credential) =
                let l = c.Username.Length
                if(l >= 1 && l <= 64) 
                then 
                    let l = c.Password.Length
                    l >= 1 && l <= 64
                else false

    
    let register: HttpHandler =
        fun (next:HttpFunc) (ctx:HttpContext) ->
            task {
                    try
                        let! c = ctx.BindJsonAsync<Credential>()
                        let greeting = 
                            if(Credential.Check(c))
                            then  sprintf "Hello World, from %s" c.Username
                            else sprintf "sorry"
                        return! text greeting next ctx
                    with
                        | ex-> return! RequestErrors.BAD_REQUEST ex next ctx
            }

