namespace SchoolSaas.Application.Backoffice.AppSettings.Commands.Update
{
    //public class UpdateAppSettingsCommand : UpdateCommand<IBackofficeContext, AppSetting, Guid>
    //{
    //    public UpdateAppSettingsCommand(Guid id, Action<AppSetting> action)
    //    {
    //        Id = id;
    //        Action = action;
    //    }

    //    public Action<AppSetting> Action { get; set; }
    //}

    //public class UpdateAppSettingsCommandHandler : UpdateCommandHandler<UpdateAppSettingsCommand, IBackofficeContext, AppSetting, Guid>
    //{
    //    public UpdateAppSettingsCommandHandler(IBackofficeContext dbContext) : base(dbContext)
    //    {
    //    }

    //    public override async Task<Unit> Handle(UpdateAppSettingsCommand request, CancellationToken cancellationToken)
    //    {
    //        var appSetting = await DbContext.Set<AppSetting>()
    //            .Where(e => e.BackofficeId == request.Id).FirstAsync();

    //        NotFoundException.ThrowIfNull(appSetting, nameof(appSetting));

    //        request.Action.Invoke(appSetting);

    //        DbContext.Entry(appSetting).State = EntityState.Modified;
    //        await DbContext.SaveChangesAsync(cancellationToken);

    //        return Unit.Value;
    //    }
    //}
}
