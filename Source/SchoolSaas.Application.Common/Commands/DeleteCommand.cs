namespace SchoolSaas.Application.Common.Commands
{
    //public class DeleteCommand<TContext, TEntity, TId> : IRequest
    //    where TEntity : class, IDeletableEntity
    //    where TContext : IContext
    //{
    //    public TId Id { get; set; }
    //}

    //public class DeleteCommandHandler<TCommand, TContext, TEntity, TId> : IRequestHandler<TCommand>
    //    where TEntity : class, IDeletableEntity
    //    where TContext : IContext
    //    where TCommand : DeleteCommand<TContext, TEntity, TId>
    //{
    //    protected TContext DbContext { get; private set; }

    //    public DeleteCommandHandler(TContext dbContext)
    //    {
    //        DbContext = dbContext;
    //    }

    //    public virtual async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    //    {
    //        var entity = await DbContext.Set<TEntity>().FindAsync(request.Id);

    //        if (entity == null)
    //            throw new NotFoundException(nameof(TEntity), request.Id);

    //        entity.IsDeleted = true;

    //        DbContext.Entry(entity).State = EntityState.Modified;

    //        await DbContext.SaveChangesAsync(cancellationToken);

    //        return Unit.Value;
    //    }
    //}
}