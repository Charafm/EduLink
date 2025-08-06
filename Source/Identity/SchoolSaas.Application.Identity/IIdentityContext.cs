using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Specifications;

namespace SchoolSaas.Application.Identity
{
    public interface IIdentityContext : IContext
    {
        IQueryable<TEntity> ApplySpecification<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec) where TEntity : class;
    }
}
