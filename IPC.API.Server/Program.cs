using System.IO.Pipes;
using System.Security.AccessControl;
using System.Security.Principal;
using IPC.API.Server.Services;
using IPC.Common;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateSlimBuilder(args);

builder.WebHost.ConfigureKestrel(
    serverOptions =>
    {
        serverOptions.ListenNamedPipe(
            IpcConstants.PipeName,
            pipeOptions =>
            {
                // pipeOptions.UseConnectionLogging(loggerName: "IPC");
                pipeOptions.Protocols = HttpProtocols.Http2; // Used to enable gRPC.
            });
    });

if (OperatingSystem.IsWindows())
{
    // This is a very simple example demonstrating how we can integrate with Windows Security.
    builder.WebHost.UseNamedPipes(
        options =>
        {
            if (!OperatingSystem.IsWindows()) return;

            var security = new PipeSecurity();

            var identity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
            security.AddAccessRule(new PipeAccessRule(identity, PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance, AccessControlType.Allow));

            options.PipeSecurity = security;
            options.CurrentUserOnly = false;
        });
}

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GreeterService>();

app.Run();