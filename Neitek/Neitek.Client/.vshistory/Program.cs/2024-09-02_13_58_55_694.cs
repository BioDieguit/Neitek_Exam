using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DevExpress.Blazor;
using Neitek.Client.IServices;
using Neitek.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddDevExpressBlazor();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IMetasService, MetasService>();
builder.Services.AddScoped<ITareasService, TareasService>();
builder.Services.Configure<DevExpress.Blazor.Configuration.GlobalOptions>(options => {options.BootstrapVersion = BootstrapVersion.v5; options.SizeMode = SizeMode.Small; });
await builder.Build().RunAsync();
