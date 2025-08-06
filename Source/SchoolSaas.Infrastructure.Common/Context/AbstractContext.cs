using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Specifications;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Helpers;
using SchoolSaas.Domain.Common.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SchoolSaas.Infrastructure.Common.Configuration;
using System.Data;

namespace SchoolSaas.Infrastructure.Common.Context
{
    public abstract class AbstractContext<TContext> : DbContext, IContext
        where TContext : DbContext
    {
        private readonly ILogger<TContext> _logger;
        private readonly ITenantAccessor _tenantAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IStorage _storage;
        private readonly ICacheService _cacheService;
        private IDbContextTransaction? _currentTransaction;

        protected AbstractContext(ITenantAccessor tenantAccessor,
            ICurrentUserService currentUserService,
            IDateTime dateTime, IStorage storage, ICacheService cacheService, DbContextOptions<TContext> options) : base(options)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("ACG.MAEC.EConsulatMA", LogLevel.Debug)
                    .AddConsole();
            });
            _logger = loggerFactory.CreateLogger<TContext>();
            _tenantAccessor = tenantAccessor;
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            _storage = storage;
            _cacheService = cacheService;
        }

        public IQueryable<TEntity> ApplySpecification<TEntity>(ISpecification<TEntity> spec) where TEntity : class
        {
            return SpecificationEvaluator<TEntity>.GetQuery(Set<TEntity>().AsNoTracking(), spec);
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyBaseEntityConfiguration();
            builder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);

            foreach (var entity in builder.Model.GetEntityTypes()
                .Where(e => typeof(IDeletableEntity).IsAssignableFrom(e.ClrType)))
            {
                ReflectionHelper.InvokeGenericMethod(typeof(AbstractContext<TContext>), this, nameof(AddDeletedQueryFilter),
                    new Type[] { entity.ClrType }, builder);
            }

            foreach (var entity in builder.Model.GetEntityTypes()
                .Where(e => typeof(ITenantAware).IsAssignableFrom(e.ClrType)))
            {
                if (typeof(IDeletableEntity).IsAssignableFrom(entity.ClrType))
                    ReflectionHelper.InvokeGenericMethod(typeof(AbstractContext<TContext>), this, nameof(AddTenantIdAndDeletedQueryFilter),
                        new Type[] { entity.ClrType }, builder);
                else
                    ReflectionHelper.InvokeGenericMethod(typeof(AbstractContext<TContext>), this, nameof(AddTenantIdQueryFilter),
                        new Type[] { entity.ClrType }, builder);
            }

            CreateContextInitializer(builder).Seed();
        }

        protected abstract IContextInitializer CreateContextInitializer(ModelBuilder builder);

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
                await SaveChangesAsync(null, cancellationToken, TenantId).ConfigureAwait(false);
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

        public virtual async Task<int> SaveChangesAsync(Action<Exception>? onException = null, CancellationToken cancellationToken = default, string? tenantId = null)
        {
            string? userId = null;
            if (_currentUserService.IsLoggedIn())
            {
                userId = $"{_currentUserService.UserId}:{_currentUserService.UserName}:{_currentUserService.PreferredUsername}";
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

            // Assign tenent
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
                    throw new ApplicationException($"{Messages.ErrorWhileSavingData} {ex.Message}", ex);
                }
            }

            return result;
        }
    }
}
