namespace MxServer

open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Giraffe

module Program =
    let configureLogging (builder:ILoggingBuilder) = 
        let filter (l:LogLevel) = l.Equals LogLevel.Error
        builder.AddFilter(filter).AddConsole().AddDebug() |> ignore

    let configureServices (services: IServiceCollection) = 
        services.AddGiraffe() |> ignore

    let errorHandler (ex : System.Exception) (logger : ILogger) =
        logger.LogError(EventId(), ex, "An unhandled exception has occurred.")
        clearResponse >=> setStatusCode 500 >=> text ex.Message

    let configureApp (app : IApplicationBuilder) =
        app.UseGiraffeErrorHandler(errorHandler)
            .UseGiraffe Routes.webApp
      
    [<EntryPoint>]
    let main _ =
        WebHostBuilder()
            .UseKestrel()
            .Configure(configureApp)
            .ConfigureServices(configureServices)
            .ConfigureLogging(configureLogging)
            .Build()
            .Run()
        0
