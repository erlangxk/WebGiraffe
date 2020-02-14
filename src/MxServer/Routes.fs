namespace MxServer

open Microsoft.AspNetCore.Http
open Giraffe

module Routes = 
    let webApp: HttpFunc ->  HttpContext -> HttpFuncResult =
        choose [
            route "/ping" >=> text "pong"
        ]