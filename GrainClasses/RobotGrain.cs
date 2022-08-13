using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Core;
using Orleans.Runtime;
using OrleansBook.GrainInterfaces;

namespace GrainClasses;

public class RobotGrain : Grain, IRobotGrain
{
    private readonly Queue<string> _instructions = new();
    private ILogger<RobotGrain> _logger; 
 
    public RobotGrain(ILogger<RobotGrain> logger)
    {
        _logger = logger;
    }

    public Task AddInstruction(string instruction)
    {
        var key = this.GetPrimaryKeyString();
        _logger.LogWarning("{Key} adding '{Instruction}'", key,instruction);
        
        
        _instructions.Enqueue(instruction);
        return Task.CompletedTask;
    }

    public Task<string> GetNextInstruction()
    {
        if(_instructions.Count == 0) return Task.FromResult<string>(null);

        var instruction = _instructions.Dequeue();
        
        var key = this.GetPrimaryKeyString();
        _logger.LogWarning("{Key} executing '{Instruction}'", key,instruction);
        
        return Task.FromResult(instruction);
    }

    public Task<int> GetInstructionCount() => Task.FromResult(_instructions.Count);
}