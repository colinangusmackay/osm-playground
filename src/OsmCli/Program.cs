using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OsmPlayground.Data;

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
