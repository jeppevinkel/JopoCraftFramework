# JopoCraftFramework

A .NET backend service for tracking and displaying SCP: Secret Laboratory game server events.
Includes a REST API, real-time SignalR hub, React admin interface, Discord bot,
and an EXILED plugin for the game server.

---

## Architecture Overview

The system is made up of several components that each have a distinct responsibility.

### Components

**JopoCraftFramework.Api**
The core backend. Exposes a REST API that the game server plugin posts events to,
and a SignalR hub that pushes real-time updates to connected clients.

**yourapp-ui**
A React admin interface for administrators to look up players, browse events,
and view live status dashboards.

**JopoCraftFramework.DiscordBot**
A .NET Worker Service that connects to the API's SignalR hub as a client and relays
events to Discord channels in real time.

**JopoCraftFramework.Plugin**
An EXILED plugin for SCP: Secret Laboratory. Hooks into game events and forwards
them to the API over HTTP.

### Data Flow

```
SCP:SL Game Server
└── EXILED Plugin ──── HTTP POST ────► API ──── saves ────► Database
                                        │
                                        └── SignalR broadcast
                                                │
                                    ┌───────────┼───────────┐
                                    ▼           ▼           ▼
                                React UI   Discord Bot  (future clients)
```

- The game plugin sends events to the REST API
- The API persists the event then broadcasts it over SignalR
- The React UI subscribes to SignalR for live pages and calls REST endpoints for everything else
- The Discord bot listens on the SignalR hub and relays events to Discord channels
- The Discord bot never touches the database directly

---

## Project Structure

```
repo-root/
├── .github/
│   └── workflows/
│       ├── deploy-api.yml
│       ├── deploy-ui.yml
│       ├── deploy-discordbot.yml
│       └── deploy-plugin.yml
├── JopoCraftFramework.Domain/
├── JopoCraftFramework.Contracts/
├── JopoCraftFramework.Infrastructure/
├── JopoCraftFramework.Api/
├── JopoCraftFramework.DiscordBot/
├── JopoCraftFramework.Plugin/
├── yourapp-ui/
├── YourSolution.sln
├── .gitignore
└── README.md
```

The React app lives in the same repository as the .NET solution. In JetBrains IDEs,
open the solution root in Rider via the .sln file and open the `yourapp-ui` subfolder
separately in WebStorm to avoid IDE configuration conflicts.

---

## Projects

### JopoCraftFramework.Domain
**Type:** .NET Class Library
**Target:** .NET 10

The core of the application. Has no dependencies on any other project in the solution.
All other .NET projects depend on this either directly or indirectly.

Contains:
- Entities (Player, GameEvent, Session, etc.)
- Enums (EventType, PlayerStatus, etc.)
- Repository interfaces
- Domain service interfaces
- Domain specific exceptions
- Value objects

### JopoCraftFramework.Contracts
**Type:** .NET Class Library
**Target:** .NET Standard 2.0

Shared DTO models that define the shape of data crossing the API boundary.
Targets .NET Standard 2.0 to remain compatible with the EXILED plugin framework
alongside the .NET 10 backend projects.

Contains:
- Request models (what the plugin sends to the API)
- SignalR message models (what the API broadcasts to the hub clients)

### JopoCraftFramework.Infrastructure
**Type:** .NET Class Library
**Target:** .NET 10

All external technology concerns live here. References Domain.

Contains:
- EF Core DbContext
- Entity type configurations
- EF Core migrations
- Repository implementations
- Dependency injection registration extension method

### JopoCraftFramework.Api
**Type:** ASP.NET Core Web API
**Target:** .NET 10

The main runnable backend. References Domain, Infrastructure, and Contracts.

Contains:
- REST endpoints (game plugin posts events here)
- SignalR hub (React UI and Discord bot connect here)
- DTOs for API request and response models
- Application services that orchestrate between repositories and domain logic
- Middleware (error handling, request logging, API key authentication)
- Program.cs wiring everything together

### JopoCraftFramework.DiscordBot
**Type:** .NET Worker Service
**Target:** .NET 10

A long running background service. References Contracts only.
Does not access the database directly.

Contains:
- Worker hosted service entry point
- Discord client setup and configuration
- SignalR client connection to the API hub
- Handlers that react to incoming SignalR messages
- Discord message formatters and embed builders
- Discord slash command handlers if needed

### JopoCraftFramework.Plugin
**Type:** .NET Class Library
**Target:** Match EXILED framework requirement

Compiled to a DLL and dropped into the game server's EXILED plugins folder.
References Contracts only.

Contains:
- Plugin.cs extending EXILED's Plugin base class
- Config.cs using EXILED's YAML config system (holds API URL and key)
- EventHandlers.cs subscribing to EXILED game events
- ApiClient.cs for HTTP calls to the backend
- In-game commands if needed

### jopocraftframework-ui
**Type:** React application (Vite)
**Target:** N/A

Completely standalone. Communicates with the API over HTTP and SignalR only.
Has no .NET project references.

```
jopocraftframework-ui/
└── src/
    ├── api/           # Fetch/axios wrappers for REST endpoints
    ├── hooks/         # Custom hooks including SignalR connection management
    ├── components/    # Reusable UI components
    ├── pages/         # Full page components tied to routes
    └── types/         # TypeScript interfaces mirroring Contracts DTOs
```

TypeScript types are maintained manually to mirror the models in JopoCraftFramework.Contracts.

---

## Project Dependencies

```
JopoCraftFramework.Domain
└── no project references

JopoCraftFramework.Contracts
└── no project references

JopoCraftFramework.Infrastructure
└── JopoCraftFramework.Domain

JopoCraftFramework.Api
├── JopoCraftFramework.Domain
├── JopoCraftFramework.Infrastructure
└── JopoCraftFramework.Contracts

JopoCraftFramework.DiscordBot
└── JopoCraftFramework.Contracts

JopoCraftFramework.Plugin
└── JopoCraftFramework.Contracts

jopocraftframework-ui
└── no project references
```

Domain and Contracts are the two roots of the dependency graph.
Nothing pulls into them, keeping the graph clean and cycle free.

---

## Real-Time Strategy

Not every page uses SignalR. The approach is kept pragmatic.

| Page Type | Approach |
|---|---|
| Live status dashboard | SignalR |
| Live event feed | SignalR |
| Historical reports and stats | REST on load |
| Admin search and lookups | REST on demand |

The React app uses the `@microsoft/signalr` npm package.
The Discord bot uses the .NET SignalR client.

---

## Deployment

### Infrastructure

The API, UI, Discord bot, and database run in a Docker Compose setup on a self-hosted server.
External access is handled entirely through a Cloudflare Tunnel with no open inbound ports on the host.

```
docker-compose.yml services:
├── jopocraftframework-api        (Watchtower managed)
├── jopocraftframework-ui         (Watchtower managed, served via Nginx)
├── jopocraftframework-discordbot (Watchtower managed)
├── jopocraftframework-db         (not Watchtower managed, pinned version)
└── cloudflared        (not Watchtower managed)
```

Watchtower is configured to be opt-in via Docker labels.
Only the three application containers carry the Watchtower label.
The database and Cloudflared containers are excluded and never updated automatically.

### CI/CD

GitHub Actions handles all builds and deployments.
The server is never directly accessed by GitHub Actions.

**Backend and UI services** build a Docker image and push it to GitHub Container Registry.
Watchtower detects the new image and recreates the container automatically.

```
Push to main → GitHub Actions → Build image → Push to GHCR → Watchtower pulls → Container recreated
```

**The EXILED plugin** is built in Release mode and the output DLL is uploaded to the
game server via FTP. A manual plugin reload or game server restart is required to
pick up the new version.

```
Push to main → GitHub Actions → Build plugin → FTP DLL to game server → Manual reload
```

### Workflows

```
.github/workflows/
├── deploy-api.yml          # Triggers on changes in JopoCraftFramework.Api and related projects
├── deploy-ui.yml           # Triggers on changes in jopocraftframework-ui
├── deploy-discordbot.yml   # Triggers on changes in JopoCraftFramework.DiscordBot
└── deploy-plugin.yml       # Triggers on changes in JopoCraftFramework.Plugin, FTPs DLL
```

Each workflow is scoped to trigger only when files in its relevant project directory change,
avoiding unnecessary builds and deployments.

### Secrets

| Location | Contents |
|---|---|
| GitHub Actions Secrets | FTP credentials, container registry credentials |
| Docker Compose env file on server | Database connection string, API keys, Discord bot token |
| EXILED plugin config on game server | API base URL, API key |

Nothing sensitive is stored in the repository.