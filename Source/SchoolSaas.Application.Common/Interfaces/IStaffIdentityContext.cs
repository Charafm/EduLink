using SchoolSaas.Application.Common.Specifications;

namespace SchoolSaas.Application.Common.Interfaces
{
  
    public interface IFrontOfficeIdentityContext : IContext
    {
        IQueryable<TEntity> ApplySpecification<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> spec) where TEntity : class;
    }
}
