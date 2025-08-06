using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IEdulinkReadOnlyContext : IReadOnlyContext
    {
        DbSet<City> Cities { get; }
        DbSet<Region> Regions { get; }
        DbSet<SchoolMetadata> Schools { get; }
    }
}
