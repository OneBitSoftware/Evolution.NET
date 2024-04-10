using System.Net.Mime;
using Grpc.Net.Client;
using IPC.Common;
using IPC.Common.Greet;

var builder = WebApplication.CreateSlimBuilder(args);

var app = builder.Build();

app.MapGet("/", async (ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("API.Root");
    
    var socketsHttpHandler = new SocketsHttpHandler { ConnectCallback = NamedPipesConnectionFactory.Instance.ConnectAsync };
    var grpcChannel = GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions { HttpHandler = socketsHttpHandler });
    var client = new Greeter.GreeterClient(grpcChannel);

    var request = new HelloRequest { Message = "This is the Client process!" };
    var response = await client.SayHelloAsync(request);
    logger.Log(LogLevel.Information, "IPC reply was received: {Response}", response.Message);

    var result = $"""
                  IPC communication was established successfully!

                  Request: {request.Message}
                  Response: {response.Message}
                  """;

    return Results.Text(result, MediaTypeNames.Text.Plain);
});

app.Run();