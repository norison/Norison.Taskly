using Norison.Taskly.Todos.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApi(builder.Configuration);
var app = builder.Build();
await app.UseApi().RunAsync();