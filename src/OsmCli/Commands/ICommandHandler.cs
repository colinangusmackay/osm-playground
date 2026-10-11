using System.CommandLine;

namespace OsmCli.Commands;

public interface ICommandHandler
{
    Task<int> HandleAsync(ParseResult parseResult, CancellationToken ct);
}