namespace MxServer

open Giraffe

module Routes =
    let webApp: HttpHandler =
        choose
            [ 
                route "/ping" >=> text "pong" 
                route "/register" >=> Handlers.register 
                setStatusCode 404 >=> text "Not Found" ]
