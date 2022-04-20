using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Zetbox.Client.Blazor.Client;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Zetbox.API.Utils;
using Zetbox.API.Configuration;
using Zetbox.API;
using Zetbox.Client;
using Zetbox.Client.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.ConfigureContainer(new AutofacServiceProviderFactory(ConfigureContainer));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

void ConfigureContainer(ContainerBuilder builder)
{
    // Add custom registration here
    // Prepare zetbox
    Logging.Configure();

    var config = new ZetboxConfig()
    {
        ConfigName = "Blazor Client",
        Client = new ZetboxConfig.ClientConfig()
        {
            ServiceUri = "http://localhost:35201"
        },
    };

    AssemblyLoader.Bootstrap(config);
    AutoFacBuilder.CreateContainerBuilder(builder, config, null);

    builder.RegisterModule<BlazorClientModule>();
    builder.RegisterViewModels(typeof(Program).Assembly);
}