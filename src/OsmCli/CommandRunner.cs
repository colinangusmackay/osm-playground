using System.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using OsmCli.Commands;

namespace OsmCli;

public static class CommandRunner
{
    public static Func<ParseResult, CancellationToken, Task<int>> CreateHandler<TCommandHandler>(Func<IServiceProvider> getServices)
        where TCommandHandler : ICommandHandler
        => async (parseResult, ct) => await RunAsync<TCommandHandler>(getServices, parseResult, ct);

    private static async Task<int> RunAsync<TCommandHandler>(Func<IServiceProvider> getServices, ParseResult parseResult, CancellationToken ct)
        where TCommandHandler : ICommandHandler
    {
        ArgumentNullException.ThrowIfNull(getServices);
        ArgumentNullException.ThrowIfNull(parseResult);
        var services = getServices()
                       ?? throw new InvalidOperationException("Unable to resolve the service provider.");

        await using var scope = services.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<TCommandHandler>();
        return await handler.HandleAsync(parseResult, ct);
    }
}
