
using Microsoft.AspNetCore.SignalR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Backoffice.SignalR
{
    public class FrontOfficeHub : Hub
    {
        private readonly IHubConnectionManager _hubConnectionManager;
        private const string HubIdentifier = "FO"; // Identifier for FrontOfficeHub

        public FrontOfficeHub(IHubConnectionManager hubConnectionManager)
        {
            _hubConnectionManager = hubConnectionManager;
        }

        public async Task OnConnectCitizen(string citizenId)
        {
            try
            {
                _hubConnectionManager.KeepConnection(citizenId, Context.ConnectionId, HubIdentifier);
                Console.WriteLine($"User connected: CitizenId={citizenId}, ConnectionId={Context.ConnectionId}");
                await Clients.Caller.SendAsync("Connected", "Connected successfully.");
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("Error", $"Failed to connect: {ex.Message}");
                throw;
            }
        }

        public async Task OnDisconnectCitizen()
        {
            _hubConnectionManager.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(null);
        }

        public async Task SendNotificationToCitizen(string citizenId, NotificationBody notification)
        {
            var citizenConnections = _hubConnectionManager.GetConnections(citizenId, HubIdentifier);
            foreach (var connection in citizenConnections)
            {
                await Clients.Client(connection).SendAsync("ReceiveNotificationCitizen", notification);
            }
        }
    }
}
