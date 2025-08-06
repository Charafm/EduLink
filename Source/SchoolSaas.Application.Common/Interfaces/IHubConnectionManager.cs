namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IHubConnectionManager
    {
        void KeepConnection(string userId, string connectionId, string hubIdentifier);
        void RemoveConnection(string connectionId);
        List<string> GetConnections(string userId, string hubIdentifier);
        Dictionary<string, List<string>> GetAllConnections();
    }
}
