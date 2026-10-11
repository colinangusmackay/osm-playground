using System.CommandLine;
using Microsoft.Extensions.Logging;
using OsmCli.Commands.Cli;
using OsmCli.Commands.Handler;

namespace OsmCli.Commands.Cli;

public static class RootCommandExtensions
{
    static RootCommandExtensions()
    {
        RootCommandOptions.LogOption.AcceptLegalFilePathsOnly();
        RootCommandOptions.LogLevelOption.Validators.Add(result =>
        {
            if (result.GetValueOrDefault<LogLevel>() is not (LogLevel.Information or LogLevel.Warning or LogLevel.Error))
                result.AddError("--log-level must be one of: information, warning, error.");
        });
        ImportCommandOptions.ImportFileOption.AcceptExistingOnly();
    }

    public static RootCommand Setup(this RootCommand rootCommand, string[] args, Func<IServiceProvider> getServices)
    {
        rootCommand.Options.Add(RootCommandOptions.VerbosityOption);
        rootCommand.Options.Add(RootCommandOptions.LogOption);
        rootCommand.Options.Add(RootCommandOptions.LogLevelOption);

        rootCommand.Subcommands.Add(CreateImportCommand(getServices));

        return rootCommand;
    }

    private static Command CreateImportCommand(Func<IServiceProvider> getServices)
    {
        var importCommand = new Command("import", "Imports OSM data into the database.")
        {
            ImportCommandOptions.ImportFileOption,
            ImportCommandOptions.ImportBoundingBoxOption,
            ImportCommandOptions.ImportCompleteObjectsOption,
            ImportCommandOptions.ImportExistingDataOption,
        };

        importCommand.SetAction(CommandRunner.CreateHandler<ImportCommandHandler>(getServices));

        return importCommand;
    }
}
