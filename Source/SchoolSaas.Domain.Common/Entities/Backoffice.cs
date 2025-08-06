using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.Entities
{
    public class Backoffice : TitledMultiLingualEntity<BackofficeTranslation, Guid>, IHasDocuments<BackofficeDocument>
    {
        public string? ExternalId { get; set; }

        public string Name { get; set; }

        public Guid? BillingSettingId { get; set; }

        public Guid? CountryTaxId { get; set; }

        public bool HasInactiveInvoices { get; set; } = false;

        public string? StoreKey { get; set; }

        public string? Website { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }

        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? ReceptionPhoneNumber { get; set; }
        public string? ReceptionEmail { get; set; }
        public string? SalesDepartmentPhoneNumber { get; set; }
        public string? SalesDepartmentEmail { get; set; }

        //public double StayTax { get; set; }
        //public TaxUnit StayTaxUnit { get; set; } = TaxUnit.Currency;
        //public string? VATNumber { get; set; }

        public bool? HasVisibleHalls { get; set; } = true;
        public bool? HasVisiblePrivateSpaces { get; set; } = true;

        public bool? ShowAvailableHalls { get; set; } = false;
        public bool? ShowAvailablePrivateSpaces { get; set; } = false;

        public bool? HideRoomsPrice { get; set; } = false;
        public bool? HideMenusPrice { get; set; } = false;
        public bool? HideDrinksPrice { get; set; } = false;
        public bool? HideMenuPackagesPrice { get; set; } = false;
        public bool? HideHallsPrice { get; set; } = false;
        public bool? HideResidentialSeminarsPrice { get; set; } = false;
        public bool? HideStudyDaysPrice { get; set; } = false;
        public bool? HideGourmetBreaksPrice { get; set; } = false;
        public bool? HidePrivateSpacesPrice { get; set; } = false;
        public bool? HideActivitiesPrice { get; set; } = false;
        public bool? HideEntertainmentsPrice { get; set; } = false;

        public string? UIPrimaryColor { get; set; } = "#000000";
        public string? UISecondaryColor { get; set; } = "#000000";
        public string? UIFontFamily { get; set; }
        public UIButtonShapeType? UIButtonShape { get; set; } = UIButtonShapeType.Default;

        //public string[]? NotificationEmails { get; set; } = new string[] { };

        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<BackofficeDocument> Documents { get; set; } = new List<BackofficeDocument>();

    }

    public class BackofficeTranslation : TitledEntityTranslation<Backoffice, Guid>
    {

    }
}
