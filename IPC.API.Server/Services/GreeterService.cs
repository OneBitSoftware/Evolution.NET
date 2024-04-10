namespace IPC.API.Server.Services;

using Grpc.Core;
using IPC.Common.Greet;
using Microsoft.AspNetCore.Connections.Features;

public class GreeterService(ILogger<GreeterService> logger) : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        this._logger.Log(LogLevel.Information, "IPC request was received: {Request}", request.Message);

        var namedPipeFeature = context.GetHttpContext().Features.Get<IConnectionNamedPipeFeature>();
        if (namedPipeFeature is not null)
        {
            var namedPipeStream = namedPipeFeature.NamedPipe;
            var username = namedPipeStream.GetImpersonationUserName();

            if (!string.IsNullOrWhiteSpace(username))
                this._logger.Log(LogLevel.Information, "The user \"{Username}\" was impersonated.", username);
        }

        return Task.FromResult(new HelloReply { Message = "Hello from the Server side!" });
    }
}