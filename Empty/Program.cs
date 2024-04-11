

// logging, server, routing, config providers, middleware, exception pages, user secrets, JSON, many others
var builder = WebApplication.CreateBuilder(args); // larger app

// less configuration, limited routing without regex, still has JSON, logging, appsettings and secrets
var slimBuilder = WebApplication.CreateSlimBuilder(args); // 10MB on Windows with AOT, 90MB without

// pretty empty, no server, no routing
var emptyBuilder = WebApplication.CreateEmptyBuilder(new ());
emptyBuilder.WebHost.UseKestrel();

var app = builder.Build();

// with AOT, this is interpreted at publish time. Without routing, this throws.
app.MapGet("/", () => "Hello World!");

app.Use(async (context, next) => { await next.Invoke(); });

app.Run();
