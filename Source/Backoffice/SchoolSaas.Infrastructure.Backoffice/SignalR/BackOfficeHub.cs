using SchoolSaas.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Backoffice.SignalR

{
    public class BackOfficeHub : Hub
    {
        private readonly IHubConnectionManager _hubConnectionManager;
        private const string HubIdentifier = "BO"; // Identifier for BackOfficeHub

        public BackOfficeHub(IHubConnectionManager hubConnectionManager)
        {
            _hubConnectionManager = hubConnectionManager;
        }

        public async Task OnConnectAgent(string agentId)
        {
            if (!string.IsNullOrEmpty(agentId))
            {
                Console.WriteLine($"User connected: AgentId={agentId}, ConnectionId={Context.ConnectionId}");
                _hubConnectionManager.KeepConnection(agentId, Context.ConnectionId, HubIdentifier);
            }

            await base.OnConnectedAsync();
        }

        public async Task OnDisconnectAgent(Exception exception)
        {
            _hubConnectionManager.RemoveConnection(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotificationToAgent(string agentId, NotificationBody notification)
        {
            var agentConnections = _hubConnectionManager.GetConnections(agentId, HubIdentifier);
            foreach (var connection in agentConnections)
            {
                await Clients.Client(connection).SendAsync("ReceiveNotificationAgent", notification);
            }
        }
    }
}
