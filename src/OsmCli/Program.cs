using System.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OsmCli.Commands.Cli;
using OsmCli.Commands.Handler;
using OsmPlayground.Data;

IServiceProvider? provider = null;
var parsedCommand = new RootCommand("A CLI for dealing with OpenStreetMap data.")
    // ReSharper disable once AccessToModifiedClosure - That's the point that we get the modified closure
    .Setup(args, () => provider ?? throw new InvalidOperationException("The host has not been built."))
    .Parse(args);

var appBuilder = Host.CreateApplicationBuilder();

#if DEBUG
if (appBuilder.Environment.IsEnvironment("Local"))
{
    appBuilder.Configuration.AddUserSecrets<Program>();
}
#endif

var services = appBuilder.Services;

services.AddOsmData();
services.AddScoped<ImportCommandHandler>();

using var host = appBuilder.Build();
provider = host.Services;

return await parsedCommand.InvokeAsync();
