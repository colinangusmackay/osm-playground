using System.CommandLine;

namespace OsmCli;

public static class RootCommandExtensions
{
    public static RootCommand Setup(this RootCommand rootCommand, string[] args)
    {
        return rootCommand;
    }
}
