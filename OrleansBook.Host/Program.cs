// See https://aka.ms/new-console-template for more information

using GrainClasses;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;

var host = new HostBuilder()
    .UseOrleans(siloBuilder  =>
    {
        siloBuilder.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(RobotGrain).Assembly)
                .WithReferences())
            .UseLocalhostClustering()
            .ConfigureLogging(logging=>
            {
                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Warning);
            });
    })
    
    .Build();

await host.StartAsync();

Console.WriteLine("Press key to stop the Server");
Console.ReadKey();

await host.StopAsync();