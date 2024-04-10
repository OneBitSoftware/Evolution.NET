namespace IPC.API.Server.Services;

using Grpc.Core;
using IPC.Common.Greet;

public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        this._logger.Log(LogLevel.Information, "IPC request was received: {Request}", request.Message);
        return Task.FromResult(new HelloReply { Message = "Hello from the Server side!" });
    }
}