using Blazor8Test.Client;
using Blazor8Test.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddHttpClient(HttpClientNames.Backend, (_, h) =>
{
    h.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress );
});

CommonRegistration.ConfigureCommonServices(builder.Services);

await builder.Build().RunAsync();
