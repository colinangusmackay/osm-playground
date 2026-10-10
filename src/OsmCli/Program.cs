using System.CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OsmCli;
using OsmPlayground.Data;

var rootCommand = new RootCommand("A CLI for dealing with OpenStreetMap data.").Setup(args);

var appBuilder = Host.CreateApplicationBuilder();

#if DEBUG
if (appBuilder.Environment.IsEnvironment("Local"))
{
    appBuilder.Configuration.AddUserSecrets<Program>();
}
#endif

var services = appBuilder.Services;

services.AddOsmData();



var host = appBuilder.Build();

await host.RunAsync();
