using Blazor8Test.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

CommonRegistration.ConfigureCommonServices(builder.Services);

await builder.Build().RunAsync();
