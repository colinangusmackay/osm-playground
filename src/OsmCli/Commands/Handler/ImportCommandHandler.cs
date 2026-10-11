using System.CommandLine;
using OsmCli.Commands.Cli;

namespace OsmCli.Commands.Handler;

public class ImportCommandHandler : ICommandHandler
{
    public async Task<int> HandleAsync(ParseResult parseResult, CancellationToken ct)
    {
        Console.WriteLine("Importing OSM data...");
        Console.WriteLine($"Verbosity: {parseResult.GetValue(RootCommandOptions.VerbosityOption)}");
        Console.WriteLine($"Log: {parseResult.GetValue(RootCommandOptions.LogOption)}");
        Console.WriteLine($"Level: {parseResult.GetValue(RootCommandOptions.LogLevelOption)}");
        Console.WriteLine($"Import file: {parseResult.GetValue(ImportCommandOptions.ImportFileOption)}");
        Console.WriteLine($"Import bounding box: {parseResult.GetValue(ImportCommandOptions.ImportBoundingBoxOption)}");
        Console.WriteLine($"Complete objects: {parseResult.GetValue(ImportCommandOptions.ImportCompleteObjectsOption)}");
        Console.WriteLine($"Existing data strategy: {parseResult.GetValue(ImportCommandOptions.ImportExistingDataOption)}");
        throw new NotImplementedException();
    }
}
