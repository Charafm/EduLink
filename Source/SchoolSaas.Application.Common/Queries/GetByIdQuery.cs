using SchoolSaas.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Exceptions;
using SchoolSaas.Application.Common.Interfaces;
using System.Linq.Expressions;

namespace SchoolSaas.Application.Common.Queries
{
    public class GetByIdQuery<TContext, TEntity, TId> : IRequest<TEntity>
        where TEntity : IdBasedEntity<TId>, IDeletableEntity
        where TContext : IReadOnlyContext
    {
        public TId Id { get; set; }
    }

    public class GetByIdQueryHandler<TQuery, TContext, TEntity, TId> : IRequestHandler<TQuery, TEntity>
        where TEntity : IdBasedEntity<TId>, IDeletableEntity
        where TContext : IReadOnlyContext
        where TQuery : GetByIdQuery<TContext, TEntity, TId>
    {
        protected TContext DbContext { get; }

        public GetByIdQueryHandler(TContext context)
        {
            DbContext = context;
        }

        public virtual async Task<TEntity> Handle(TQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await GetQuery().IgnoreQueryFilters().FirstOrDefaultAsync(e => e.Id!.Equals(request.Id) && !(e.IsDeleted ?? false));

            NotFoundException.ThrowIfNull(request.Id, nameof(TEntity));

            return entity!;
        }

        protected virtual IQueryable<TEntity> GetQuery()
        {
            var query = DbContext.Set<TEntity>().AsQueryable<TEntity>();

            foreach (Expression<Func<TEntity, object>> incude in GetIncludes())
            {
                query = query.Include(incude);
            }
            foreach (var incude in GetIncludeStrings())
            {
                query = query.Include(incude);
            }

            return query;
        }

        protected virtual List<Expression<Func<TEntity, object>>> GetIncludes()
        {
            return new List<Expression<Func<TEntity, object>>>();
        }

        protected virtual List<string> GetIncludeStrings()
        {
            return new List<string>();
        }
    }

}