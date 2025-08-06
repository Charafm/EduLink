using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Specifications;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using System.Data;

namespace SchoolSaas.Infrastructure.Identity.Context
{
    public class FrontOfficeIdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
            ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IFrontOfficeIdentityContext
    {
        private readonly ITenantAccessor _tenantAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction? _currentTransaction;

        public FrontOfficeIdentityContext(ITenantAccessor tenantAccessor,
            ICurrentUserService currentUserService, IDateTime dateTime, DbContextOptions<FrontOfficeIdentityContext> options) : base(options)
        {
            _tenantAccessor = tenantAccessor;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UtilityScope> UtilityScopes { get; set; }
        public DbSet<UtilityScopePermission> UtilityScopePermissions { get; set; }
        public DbSet<RoleUtilityScope> RoleUtilityScopes { get; set; }

        public IQueryable<TEntity> ApplySpecification<TEntity>(ISpecification<TEntity> spec) where TEntity : class
        {
            return ApplySpecification(Set<TEntity>().AsNoTracking(), spec);
        }

        public IQueryable<TEntity> ApplySpecification<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec) where TEntity : class
        {
            return SpecificationEvaluator<TEntity>.GetQuery(query, spec);
        }

        private void AddDeletedQueryFilter<TEntity>(ModelBuilder builder) where TEntity : class, IDeletableEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(b => !(b.IsDeleted ?? false));
        }

        private void AddTenantIdQueryFilter<TEntity>(ModelBuilder builder) where TEntity : class, ITenantAware
        {
            builder.Entity<TEntity>().HasQueryFilter(b => b.TenantId == _tenantAccessor.GetTenantId());
        }

        private void AddTenantIdAndDeletedQueryFilter<TEntity>(ModelBuilder builder) where TEntity : class, ITenantAware, IDeletableEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(b => b.TenantId == _tenantAccessor.GetTenantId() && !(b.IsDeleted ?? false));
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return;

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted)
                .ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = new CancellationToken(), string? TenantId = null)
        {
            try
            {
                await SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return SaveChangesAsync(null, cancellationToken);
        }

        public async Task<int> SaveChangesAsync(Action<Exception>? onException = null, CancellationToken cancellationToken = default, string? tenantId = null)
        {
            string? userId = null;
            if (_currentUserService.IsLoggedIn())
            {
                userId = $"{_currentUserService.UserName}:{_currentUserService.UserId}";
            }

            // Apply auditing 
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity<string>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.LastModifiedBy = userId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = userId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            // Assign tenant
            foreach (var entry in ChangeTracker.Entries().Where(e =>
                typeof(ITenantAware).IsAssignableFrom(e.Entity.GetType()) && e.State == EntityState.Added))
            {
                entry.Property(nameof(ITenantAware.TenantId)).CurrentValue = tenantId ?? _tenantAccessor.GetTenantId();
            }

            var result = 0;
            try
            {
                result = await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                if (onException != null)
                {
                    onException(ex);
                }
                else
                {
                    throw new ApplicationException($"An error occurred while saving data: {ex.Message}");
                }
            }

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder.Entity<UserPermission>()
                .HasKey(up => new { up.UserId, up.PermissionId });

            builder.Entity<UserPermission>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.UserId);

            builder.Entity<UserPermission>()
                .HasOne(up => up.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(up => up.PermissionId);

            builder.Entity<UtilityScopePermission>()
                .HasKey(usp => new { usp.UtilityScopeId, usp.PermissionId });

            builder.Entity<UtilityScopePermission>()
                .HasOne(usp => usp.UtilityScope)
                .WithMany(us => us.UtilityScopePermissions)
                .HasForeignKey(usp => usp.UtilityScopeId);

            builder.Entity<UtilityScopePermission>()
                .HasOne(usp => usp.Permission)
                .WithMany()
                .HasForeignKey(usp => usp.PermissionId);

            builder.Entity<RoleUtilityScope>()
                .HasKey(rus => new { rus.RoleId, rus.UtilityScopeId });

            builder.Entity<RoleUtilityScope>()
                .HasOne(rus => rus.Role)
                .WithMany(r => r.RoleUtilityScopes)
                .HasForeignKey(rus => rus.RoleId);

            builder.Entity<RoleUtilityScope>()
                .HasOne(rus => rus.UtilityScope)
                .WithMany(us => us.RoleUtilityScopes)
                .HasForeignKey(rus => rus.UtilityScopeId);

            foreach (var entity in builder.Model.GetEntityTypes()
                .Where(e => typeof(IDeletableEntity).IsAssignableFrom(e.ClrType)))
            {
                ReflectionHelper.InvokeGenericMethod(typeof(FrontOfficeIdentityContext), this, nameof(AddDeletedQueryFilter),
                    new Type[] { entity.ClrType }, builder);
            }
        }
    }
}
