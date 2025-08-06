using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Common.Context;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Edulink;


namespace SchoolSaas.Infrastructure.Edulink.Context
{
    public class EdulinkReadOnlyContext : AbstractReadOnlyContext<EdulinkReadOnlyContext>, IEdulinkReadOnlyContext
    {
        public EdulinkReadOnlyContext(ITenantAccessor tenantAccessor, ICurrentUserService currentUserService,
            ICacheService cacheService, DbContextOptions<EdulinkReadOnlyContext> options)
            : base(tenantAccessor, currentUserService, cacheService, options)
        {
        }
        
        public DbSet<City> Cities => Set<City>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<SchoolMetadata> Schools => Set<SchoolMetadata>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
         
            base.OnModelCreating(builder);

        }

    }
}
