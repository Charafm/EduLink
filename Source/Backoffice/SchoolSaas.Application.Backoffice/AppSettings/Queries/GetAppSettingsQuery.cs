namespace SchoolSaas.Application.Backoffice.AppSettings.Queries
{
    //public class GetAppSettingsQuery : GetByIdQuery<IBackofficeContext, AppSetting, Guid>
    //{
    //}

    //public class GetAppSettingsQueryHandler : GetByIdQueryHandler<GetAppSettingsQuery, IBackofficeContext, AppSetting, Guid>
    //{


    //    public GetAppSettingsQueryHandler(IBackofficeContext context) : base(context)
    //    {
    //    }

    //    public override async Task<AppSetting> Handle(GetAppSettingsQuery request, CancellationToken cancellationToken)
    //    {
    //        var entity = await DbContext.Set<AppSetting>().FirstOrDefaultAsync(e => e.BackofficeId == request.Id);

    //        if (entity == null)
    //        {
    //            entity = new AppSetting()
    //            {
    //                BackofficeId = request.Id,
    //            };

    //            DbContext.Set<AppSetting>().Add(entity);
    //            await DbContext.SaveChangesAsync(cancellationToken);
    //        }

    //        return entity;
    //    }
    //}
}


