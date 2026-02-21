using JopoCraftFramework.Api.Hubs;
using JopoCraftFramework.Api.Json;
using JopoCraftFramework.Api.Middleware;
using JopoCraftFramework.Infrastructure;
using JopoCraftFramework.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new BaseEventDtoConverter()));
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
builder.Services.AddInfrastructure(connectionString);

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();
app.MapHub<GameEventHub>("/hubs/events");

app.Run();
