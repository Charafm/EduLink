using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Application.Backoffice.Backoffices
{
    public class BackofficeDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string Name { get; set; }
        public string? Website { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? AddressLine2 { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FaxNumber { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
        public string? ReceptionPhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? ReceptionEmail { get; set; }

        public string? UIPrimaryColor { get; set; } = "#000000";
        public string? UISecondaryColor { get; set; } = "#000000";
        public string? UIFontFamily { get; set; }
        public UIButtonShapeType? UIButtonShape { get; set; }

        public bool? HasVisibleHalls { get; set; }
        public bool? HasVisiblePrivateSpaces { get; set; }

        public bool? ShowAvailableHalls { get; set; }
        public bool? ShowAvailablePrivateSpaces { get; set; }

        public bool? HideRoomsPrice { get; set; }
        public bool? HideMenusPrice { get; set; }
        public bool? HideDrinksPrice { get; set; }
        public bool? HideMenuPackagesPrice { get; set; }
        public bool? HideHallsPrice { get; set; }
        public bool? HideResidentialSeminarsPrice { get; set; }
        public bool? HideStudyDaysPrice { get; set; }
        public bool? HideGourmetBreaksPrice { get; set; }
        public bool? HidePrivateSpacesPrice { get; set; }
        public bool? HideActivitiesPrice { get; set; }
        public bool? HideEntertainmentsPrice { get; set; }
        public string? FullNameManager { get; set; }
        public int QuoteCount { get; set; }=0;
        public CountryTaxlDto? CountryTax { get; set; }

        public List<BackofficeDto> CustomersEmails { get; set; } = new List<BackofficeDto>();
        public List<BackofficeDocument> Documents { get; set; } = new List<BackofficeDocument>();
    }

    public class CountryTaxlDto
    {
        public string? Code { get; set; }
        public string? CurrencySymbol { get; set; }
        public string? Locale { get; set; }

        public string? CurrencyCode { get; set; }
        public string? CurrencyName { get; set; }


    }
}
