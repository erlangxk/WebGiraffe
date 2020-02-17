namespace MxServer

open Microsoft.AspNetCore.Hosting

open Microsoft.Extensions.Logging


module Program =
    let configureLogging (builder:ILoggingBuilder) = 
        let filter (l:LogLevel) = l.Equals LogLevel.Error
        builder.AddFilter(filter).AddConsole().AddDebug() |> ignore
    
    [<EntryPoint>]
    let main _ =
        WebHostBuilder()
            .UseKestrel()
            .UseStartup<Init.Startup>()
            .ConfigureLogging(configureLogging)
            .Build()
            .Run()
        0
