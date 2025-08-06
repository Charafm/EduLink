using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Infrastructure.Identity.SignalRHub
{
    public class HubConnectionManager : IHubConnectionManager
    {
        private static readonly Dictionary<string, List<string>> ConnectionMap = new Dictionary<string, List<string>>();
        private static readonly object ConnectionMapLocker = new object();

        // Use an enum or constants to define hub identifiers for clarity
        private const string FrontOfficeHubIdentifier = "FO";
        private const string BackOfficeHubIdentifier = "BO";

        // Method to generate a unique key based on user ID and hub type
        private string GenerateKey(string userId, string hubIdentifier) => $"{hubIdentifier}_{userId}";

        public void KeepConnection(string userId, string connectionId, string hubIdentifier)
        {
            lock (ConnectionMapLocker)
            {
                var key = GenerateKey(userId, hubIdentifier);
                if (!ConnectionMap.ContainsKey(key))
                {
                    ConnectionMap[key] = new List<string>();
                }
                ConnectionMap[key].Add(connectionId);
            }
        }

        public void RemoveConnection(string connectionId)
        {
            lock (ConnectionMapLocker)
            {
                foreach (var key in ConnectionMap.Keys.ToList())
                {
                    var connections = ConnectionMap[key];
                    if (connections.Remove(connectionId) && connections.Count == 0)
                    {
                        ConnectionMap.Remove(key);
                    }
                }
            }
        }

        public List<string> GetConnections(string userId, string hubIdentifier)
        {
            lock (ConnectionMapLocker)
            {
                var key = GenerateKey(userId, hubIdentifier);
                return ConnectionMap.TryGetValue(key, out var connections) ? connections : new List<string>();
            }
        }

        #region DEBUGGING
        public Dictionary<string, List<string>> GetAllConnections()
        {
            lock (ConnectionMapLocker)
            {
                // Returning a copy of the dictionary to prevent external modifications
                return new Dictionary<string, List<string>>(ConnectionMap);
            }
        }
        #endregion
    }
}
