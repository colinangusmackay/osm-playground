using System.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OsmCli;
using OsmCli.Commands;
using OsmPlayground.Data;

IServiceProvider? provider = null;
var parsedCommand = new RootCommand("A CLI for dealing with OpenStreetMap data.")
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
