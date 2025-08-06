using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Specifications;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolSaas.Infrastructure.Common.Configuration;
using System.Reflection;

namespace SchoolSaas.Infrastructure.Common.Context
{
    public abstract class AbstractReadOnlyContext<TContext> : DbContext, IReadOnlyContext
        where TContext : DbContext
    {
        private readonly ILogger<TContext> _logger;
        private readonly ITenantAccessor _tenantAccessor;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICacheService _cacheService;

        protected AbstractReadOnlyContext(ITenantAccessor tenantAccessor,
            ICurrentUserService currentUserService, ICacheService cacheService, DbContextOptions<TContext> options) : base(options)
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
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            foreach (var entity in builder.Model.GetEntityTypes()
                .Where(e => typeof(IDeletableEntity).IsAssignableFrom(e.ClrType)))
            {
                ReflectionHelper.InvokeGenericMethod(typeof(AbstractReadOnlyContext<TContext>), this, nameof(AddDeletedQueryFilter),
                    new Type[] { entity.ClrType }, builder);
            }

            foreach (var entity in builder.Model.GetEntityTypes()
                .Where(e => typeof(ITenantAware).IsAssignableFrom(e.ClrType)))
            {
                if (typeof(IDeletableEntity).IsAssignableFrom(entity.ClrType))
                    ReflectionHelper.InvokeGenericMethod(typeof(AbstractReadOnlyContext<TContext>), this, nameof(AddTenantIdAndDeletedQueryFilter),
                        new Type[] { entity.ClrType }, builder);
                else
                    ReflectionHelper.InvokeGenericMethod(typeof(AbstractReadOnlyContext<TContext>), this, nameof(AddTenantIdQueryFilter),
                        new Type[] { entity.ClrType }, builder);
            }
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            throw new InvalidOperationException();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            throw new InvalidOperationException();
        }
    }
}
