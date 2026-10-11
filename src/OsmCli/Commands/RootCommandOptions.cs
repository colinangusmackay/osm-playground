using System.CommandLine;
using Microsoft.Extensions.Logging;

namespace OsmCli.Commands;

public static class RootCommandOptions
{
    public static readonly Option<Verbosity> VerbosityOption = new("--verbosity")
    {
        Description = "The verbosity level of the output.",
        HelpName = "detailed|normal|warning|error",
        DefaultValueFactory = _ => Verbosity.Normal,
        Recursive = true,
    };

    public static readonly Option<FileSystemInfo?> LogOption = new("--log")
    {
        Description =
            "The path of the log file. If the path is a folder, the file name is generated from the start time of the task. Existing log files are appended to.",
        HelpName = "file-path",
        Recursive = true,
    };

    public static readonly Option<LogLevel> LogLevelOption = new("--log-level")
    {
        Description = "The minimum level of messages written to the log file.",
        HelpName = "information|warning|error",
        DefaultValueFactory = _ => LogLevel.Information,
        Recursive = true,
    };
}
