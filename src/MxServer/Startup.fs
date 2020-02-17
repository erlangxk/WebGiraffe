namespace MxServer

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe

module Routes =
    let webApp: HttpHandler =
        choose
            [ 
                route "/ping" >=> text "pong" 
                route "/register" >=> Handlers.register 
                setStatusCode 404 >=> text "Not Found" ]

module Init =
    type Startup() =
        member __.ConfigureServices(services: IServiceCollection) =
            services.AddGiraffe() |> ignore

        member __.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
            app.UseGiraffe Routes.webApp