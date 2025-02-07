using Norison.Taskly.Todos.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddApi();
var app = builder.Build();
await app.UseApi().RunAsync();