namespace TcpServer.Services;

using System.Net.Sockets;

public class SocketListenerService : BackgroundService
{
    private readonly ILogger<SocketListenerService> _logger;
    private readonly IListenSocketService _listenSocketService;

    public SocketListenerService(
        ILogger<SocketListenerService> logger,
        IListenSocketService socketService
    )
    {
        _logger = logger;
        _listenSocketService = socketService;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            _listenSocketService.Listen(5000);

            await _listenSocketService.AcceptClientsAsync(cancellationToken);
        }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.OperationAborted)
        {
            _logger.LogInformation("Shutting down...");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ExecuteAsync");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _listenSocketService.Stop();

            await base.StopAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ExecuteAsync");
        }
    }
}
