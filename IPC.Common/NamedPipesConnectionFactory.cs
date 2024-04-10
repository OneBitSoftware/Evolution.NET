namespace IPC.Common;

using System.IO.Pipes;
using System.Security.Principal;

public class NamedPipesConnectionFactory
{
    public static NamedPipesConnectionFactory Instance { get; } = new ();
    
    public async ValueTask<Stream> ConnectAsync(SocketsHttpConnectionContext _, CancellationToken cancellationToken = default)
    {
        var clientStream = new NamedPipeClientStream(
            serverName: ".",
            pipeName: IpcConstants.PipeName,
            direction: PipeDirection.InOut,
            options: PipeOptions.WriteThrough | PipeOptions.Asynchronous,
            impersonationLevel: TokenImpersonationLevel.Impersonation);

        var isSuccessfullyConnected = false;
        try
        {
            await clientStream.ConnectAsync(cancellationToken).ConfigureAwait(false);
            isSuccessfullyConnected = true;
            return clientStream;
        }
        finally
        {
            if (!isSuccessfullyConnected) await clientStream.DisposeAsync();
        }
    }
}