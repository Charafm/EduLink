namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IFrontofficeConnectedService
    {
        Task<bool> AddNotification(Guid userId, string registrationRequestNumber, int statut);

    }
}
