using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolSaas.Application.Common.Specifications;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IReadOnlyContext
    {
        IQueryable<TEntity> ApplySpecification<TEntity>(ISpecification<TEntity> spec) where TEntity : class;

        public IQueryable<TEntity> ApplySpecification<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec) where TEntity : class
        {
            return SpecificationEvaluator<TEntity>.GetQuery(query, spec);
        }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry Entry(object entity);
    }
}