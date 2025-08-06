//using SchoolSaas.Domain.Frontoffice;
//using SchoolSaas.Domain.Frontoffice.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IFrontofficeReadOnlyContext : IReadOnlyContext
    {
        DbSet<Sequence> Sequences { get; }
        DbSet<Notification> Notifications { get; }

    }

  
    public class Sequence
    {
    }
}
