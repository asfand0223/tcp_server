namespace TcpServer.Extensions;

using TcpServer.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        ConfigureAppServices(services);

        return services;
    }

    private static IServiceCollection ConfigureAppServices(IServiceCollection services)
    {
        services.AddHostedService<SocketListenerService>();

        services.AddSingleton<IClientSocketService, ClientSocketService>();
        services.AddSingleton<IListenSocketService, ListenSocketService>();

        return services;
    }
}
