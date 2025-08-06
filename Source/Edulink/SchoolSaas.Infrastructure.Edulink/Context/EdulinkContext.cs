using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Common.Context;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Backoffice.Academic;
using System.Reflection.Emit;
using SchoolSaas.Domain.Edulink;
using SchoolSaas.Infrastructure.Edulink.Context;

namespace SchoolSaas.Infrastructure.Edulink.Context
{
    public class EdulinkContext : AbstractContext<EdulinkContext>, IEdulinkContext
    {
        public EdulinkContext(ITenantAccessor tenantAccessor, ICurrentUserService currentUserService, IDateTime dateTime, IStorage storage,
            ICacheService cacheService, DbContextOptions<EdulinkContext> options)
            : base(tenantAccessor, currentUserService, dateTime, storage, cacheService, options)
        {
        }

        protected override IContextInitializer CreateContextInitializer(ModelBuilder builder)
        {


            return new EdulinkContextInitializer(builder);
        }

       
        public DbSet<City> Cities => Set<City>();
        public DbSet<Region> Regions => Set<Region>();
        public DbSet<SchoolMetadata> Schools => Set<SchoolMetadata>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // IMPORTANT: let your AbstractContext wire up audit fields, multi-tenancy, etc.
            base.OnModelCreating(builder);

            // Regions
            builder.Entity<Region>(entity =>
            {
                entity.ToTable("Regions");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.NameFr)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(r => r.NameAr)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(r => r.NameEn)
                      .IsRequired()
                      .HasMaxLength(200);

                // One Region ↔ Many Cities
                entity.HasMany(r => r.Cities)
                      .WithOne(c => c.Region)
                      .HasForeignKey(c => c.RegionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Cities
            builder.Entity<City>(entity =>
            {
                entity.ToTable("Cities");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.NameFr)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(c => c.NameAr)
                      .IsRequired()
                      .HasMaxLength(200);
                entity.Property(c => c.NameEn)
                      .IsRequired()
                      .HasMaxLength(200);

                // FK already configured above via Region, but you can repeat:
                entity.HasOne(c => c.Region)
                      .WithMany(r => r.Cities)
                      .HasForeignKey(c => c.RegionId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // SchoolMetadata
            builder.Entity<SchoolMetadata>(entity =>
            {
                entity.ToTable("Schools");
                entity.HasKey(s => s.Id);

                // Localized fields
                entity.Property(s => s.NameFr)
                      .IsRequired()
                      .HasMaxLength(250);
                entity.Property(s => s.NameAr)
                      .IsRequired()
                      .HasMaxLength(250);
                entity.Property(s => s.NameEn)
                      .IsRequired()
                      .HasMaxLength(250);

                // Localized address
                entity.Property(s => s.AddressFr)
                      .HasMaxLength(500);
                entity.Property(s => s.AddressAr)
                      .HasMaxLength(500);
                entity.Property(s => s.AddressEn)
                      .HasMaxLength(500);

                // Code & metadata
                entity.Property(s => s.Code)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(s => s.BackOfficeDbConnectionString)
                      .HasMaxLength(1000);
                entity.Property(s => s.LogoUrl)
                      .HasMaxLength(500);
                entity.Property(s => s.TimeZoneId)
                      .HasMaxLength(100);

                // Many SchoolMetadata → One City
                entity.HasOne(s => s.City)
                      .WithMany()              // no City.Schools navigation
                      .HasForeignKey(s => s.CityId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}