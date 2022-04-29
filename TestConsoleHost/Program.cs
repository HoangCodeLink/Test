using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TestLibrary.Clients;
using TestLibrary.Constants;

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            // Register services here

            services.AddHttpClient();

            services.AddScoped<TranslateClient>();
        });

var host = CreateHostBuilder(args).Build();

using (var scope = host.Services.CreateScope())
{
    var translateClient = scope.ServiceProvider.GetRequiredService<TranslateClient>();
    var text = await translateClient.Translate(TranslateLanguage.En, TranslateLanguage.Vi, "Make Voz Great Again");
    Console.WriteLine(text);
}
