namespace SchoolSaas.Application.Backoffice.Backoffices.Queries.GetBackofficeById
{
    //public class GetBackofficeByIdQuery : IRequest<Domain.Backoffice.Backoffice>
    //{
    //    public Guid Id { get; set; }
    //}

    //public class GetBackofficeByIdQueryHandler : IRequestHandler<GetBackofficeByIdQuery, Domain.Backoffice.Backoffice>
    //{
    //    private readonly IBackofficeReadOnlyContext _context;

    //    public GetBackofficeByIdQueryHandler(IBackofficeReadOnlyContext context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<Domain.Backoffice.Backoffice> Handle(GetBackofficeByIdQuery request,
    //        CancellationToken cancellationToken)
    //    {
    //        var entity = await _context.Set<Domain.Backoffice.Backoffice>()
    //            .IgnoreQueryFilters()
    //            .Where(e => e.Id == request.Id && !(e.IsDeleted ?? false))
    //            .Select(e => new Domain.Backoffice.Backoffice
    //            {
    //                Id = e.Id,
    //                Title = e.Title,
    //                Description = e.Description,
    //                ExternalId = e.ExternalId,

    //                HasInactiveInvoices = e.HasInactiveInvoices,
    //                Name = e.Name,
    //                StoreKey = e.StoreKey,
    //                Website = e.Website,
    //                CompanyName = e.CompanyName,
    //                Address = e.Address,
    //                AddressLine2 = e.AddressLine2,
    //                PhoneNumber = e.PhoneNumber,
    //                FaxNumber = e.FaxNumber,
    //                Email = e.Email,
    //                City = e.City,
    //                Country = e.Country,
    //                ZipCode = e.ZipCode,
    //                ReceptionPhoneNumber = e.ReceptionPhoneNumber,
    //                ReceptionEmail = e.ReceptionEmail,
    //                SalesDepartmentPhoneNumber = e.SalesDepartmentPhoneNumber,
    //                SalesDepartmentEmail = e.SalesDepartmentEmail,
    //                StayTax = e.StayTax,
    //                StayTaxUnit = e.StayTaxUnit,
    //                VATNumber = e.VATNumber,
    //                HasVisibleHalls = e.HasVisibleHalls,
    //                HasVisiblePrivateSpaces = e.HasVisiblePrivateSpaces,
    //                ShowAvailableHalls = e.ShowAvailableHalls,
    //                ShowAvailablePrivateSpaces = e.ShowAvailablePrivateSpaces,
    //                HideRoomsPrice = e.HideRoomsPrice,
    //                HideMenusPrice = e.HideMenusPrice,
    //                HideDrinksPrice = e.HideDrinksPrice,
    //                HideMenuPackagesPrice = e.HideMenuPackagesPrice,
    //                HideHallsPrice = e.HideHallsPrice,
    //                HideResidentialSeminarsPrice = e.HideResidentialSeminarsPrice,
    //                HideStudyDaysPrice = e.HideStudyDaysPrice,
    //                HideGourmetBreaksPrice = e.HideGourmetBreaksPrice,
    //                HidePrivateSpacesPrice = e.HidePrivateSpacesPrice,

    //                HideActivitiesPrice = e.HideActivitiesPrice,
    //                HideEntertainmentsPrice = e.HideEntertainmentsPrice,
    //                UIPrimaryColor = e.UIPrimaryColor,
    //                UISecondaryColor = e.UISecondaryColor,
    //                UIFontFamily = e.UIFontFamily,
    //                UIButtonShape = e.UIButtonShape,

    //                CountryTaxId = e.CountryTaxId,
    //                BillingSettingId = e.BillingSettingId,

    //                Created = e.Created,
    //                CreatedBy = e.CreatedBy,
    //                LastModified = e.LastModified,
    //                LastModifiedBy = e.LastModifiedBy,


    //                Documents = e.Documents.Where(ee => ee.ParentId == null && !(ee.IsDeleted ?? false)).Select(e => new BackofficeDocument()
    //                {

    //                    Created = e.Created,
    //                    LastModified = e.LastModified,
    //                    CreatedBy = e.CreatedBy,
    //                    LastModifiedBy = e.LastModifiedBy,
    //                    Spec = e.Spec,
    //                    Uri = e.Uri,
    //                    Id = e.Id,
    //                    BackofficeId = e.BackofficeId,
    //                    MimeType = e.MimeType,
    //                    Type = e.Type,

    //                }).ToList(),

    //            })
    //            .FirstOrDefaultAsync();

    //        NotFoundException.ThrowIfNull(entity, nameof(request.Id));

    //        return entity!;
    //    }
    //}
}
