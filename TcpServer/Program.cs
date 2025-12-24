using TcpServer.Extensions;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices().ConfigureLogging().Build().Run();
