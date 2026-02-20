using JopoCraftFramework.Api.Hubs;
using JopoCraftFramework.Api.Middleware;
using JopoCraftFramework.Infrastructure;
using JopoCraftFramework.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);

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
