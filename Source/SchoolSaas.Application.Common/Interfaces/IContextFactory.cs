using Microsoft.EntityFrameworkCore;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IContextFactory
    {
        Type ResolveContextType(string clientId);
        DbContext CreateContext(string clientId);
    }
}
