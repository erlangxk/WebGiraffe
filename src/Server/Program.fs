namespace Server

open Microsoft.AspNetCore.Hosting

module Program =
    [<EntryPoint>]
    let main _ =
        WebHostBuilder()
            .UseKestrel()
            .UseStartup<Init.Startup>()
            .Build()
            .Run()
        0
