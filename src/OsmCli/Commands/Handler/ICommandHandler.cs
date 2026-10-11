using System.CommandLine;

namespace OsmCli.Commands.Handler;

public interface ICommandHandler
{
    Task<int> HandleAsync(ParseResult parseResult, CancellationToken ct);
}
