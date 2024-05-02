using MessagesService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services => {
        services.AddLogging();
        services.AddSingleton<UniscaleSession>();
        services.AddSingleton<MessagesService.Messages.InterceptorHandler>();
    })
    .Build();

host.Run();
