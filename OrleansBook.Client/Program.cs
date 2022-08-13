// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using Orleans;
using OrleansBook.GrainInterfaces;

var client = new ClientBuilder()
    .UseLocalhostClustering()
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Warning);
    })
    .Build();

using (client)
{
    await client.Connect();

    while (true)
    {
        Console.WriteLine("Please enter a robot name...");
        var grainId = Console.ReadLine();
        var grain = client.GetGrain<IRobotGrain>(grainId);
        
        Console.WriteLine("Please enter an instruction");
        var instruction = Console.ReadLine();
        await grain.AddInstruction(instruction);

        var count = await grain.GetInstructionCount();
        Console.WriteLine($"{grainId} has {count} instruction(s)");
    }
}