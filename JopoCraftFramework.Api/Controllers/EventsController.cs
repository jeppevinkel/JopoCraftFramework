using System.Text.Json;
using JopoCraftFramework.Api.Hubs;
using JopoCraftFramework.Contracts.Events;
using JopoCraftFramework.Domain.Entities;
using JopoCraftFramework.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace JopoCraftFramework.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController(AppDbContext db, IHubContext<GameEventHub> hub) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostEvent([FromBody] BaseEventDto dto)
    {
        var gameEvent = new GameEvent
        {
            EventId = dto.EventId,
            EventType = dto.EventType ?? string.Empty,
            ServerId = dto.ServerId,
            TimestampUtc = dto.Timestamp,
            Payload = JsonSerializer.Serialize(dto)
        };

        db.GameEvents.Add(gameEvent);
        await db.SaveChangesAsync();

        await hub.Clients.All.SendAsync("ReceiveEvent", dto);

        return Ok();
    }
}
