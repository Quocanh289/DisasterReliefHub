using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DisasterRelief.Api.Hubs;

[Authorize]
public sealed class NotificationHub : Hub;