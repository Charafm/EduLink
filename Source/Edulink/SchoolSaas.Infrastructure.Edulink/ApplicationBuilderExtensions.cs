
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Domain.Edulink;
using SchoolSaas.Infrastructure.Edulink.Context;

namespace SchoolSaas.Infrastructure.Edulink
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedEdulinkDatabase(this WebApplication app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<EdulinkContext>()!;
                if (context != null)
                {
                  //  SeedRegions(context);
                  //  SeedCities(context);
                    SeedSchool(context);

                    context.SaveChanges();
                }
            }

            return app;
        }

        private static void SeedRegions(EdulinkContext context)
        {
            var regions = new List<Region>
            {
                new Region { NameFr = "Casablanca-Settat", NameAr = "الدار البيضاء - سطات", NameEn = "Casablanca-Settat" },
                new Region { NameFr = "Marrakech-Safi", NameAr = "مراكش - آسفي", NameEn = "Marrakech-Safi" },
                new Region { NameFr = "Rabat-Salé-Kénitra", NameAr = "الرباط - سلا - القنيطرة", NameEn = "Rabat-Salé-Kénitra" },
                new Region { NameFr = "Fès-Meknès", NameAr = "فاس - مكناس", NameEn = "Fès-Meknès" },
                new Region { NameFr = "Tanger-Tétouan-Al Hoceïma", NameAr = "طنجة - تطوان - الحسيمة", NameEn = "Tanger-Tétouan-Al Hoceïma" },
                new Region { NameFr = "Souss-Massa", NameAr = "سوس - ماسة", NameEn = "Souss-Massa" },
                new Region { NameFr = "Oriental", NameAr = "الجهة الشرقية", NameEn = "Oriental" },
                new Region { NameFr = "Béni Mellal-Khénifra", NameAr = "بني ملال - خنيفرة", NameEn = "Béni Mellal-Khénifra" },
                new Region { NameFr = "Drâa-Tafilalet", NameAr = "درعة - تافيلالت", NameEn = "Drâa-Tafilalet" },
                new Region { NameFr = "Guelmim-Oued Noun", NameAr = "كلميم - واد نون", NameEn = "Guelmim-Oued Noun" },
                new Region { NameFr = "Laâyoune-Sakia El Hamra", NameAr = "العيون - الساقية الحمراء", NameEn = "Laâyoune-Sakia El Hamra" },
                new Region { NameFr = "Dakhla-Oued Ed-Dahab", NameAr = "الداخلة - وادي الذهب", NameEn = "Dakhla-Oued Ed-Dahab" }
            };

            foreach (var region in regions)
            {
                if (!context.Regions.Any(r => r.NameEn == region.NameEn))
                {
                    region.Id = Guid.NewGuid();
                    context.Regions.Add(region);
                }
            }
        }

        private static void SeedCities(EdulinkContext context)
        {
            var regionLookup = context.Regions.ToList();

            var cities = new List<(string NameFr, string NameAr, string NameEn, string RegionEn, Guid? FixedId)>
            {
                ("Casablanca", "الدار البيضاء", "Casablanca", "Casablanca-Settat", Guid.Parse("6B1E521E-AFF2-4E07-9221-88D2774A3A4E")),
                ("Mohammedia", "المحمدية", "Mohammedia", "Casablanca-Settat", null),
                ("Settat", "سطات", "Settat", "Casablanca-Settat", null),

                ("Marrakech", "مراكش", "Marrakech", "Marrakech-Safi", Guid.Parse("1E061BB6-92A6-498A-A1EF-35EF46C5732E")),
                ("Safi", "آسفي", "Safi", "Marrakech-Safi", null),

                ("Rabat", "الرباط", "Rabat", "Rabat-Salé-Kénitra", Guid.Parse("2E5EB92E-6D5C-4EF8-8B06-088BCF0C676D")),
                ("Salé", "سلا", "Salé", "Rabat-Salé-Kénitra", null),

                ("Fès", "فاس", "Fès", "Fès-Meknès", null),
                ("Meknès", "مكناس", "Meknès", "Fès-Meknès", null),

                ("Tanger", "طنجة", "Tangier", "Tanger-Tétouan-Al Hoceïma", null),
                ("Tétouan", "تطوان", "Tétouan", "Tanger-Tétouan-Al Hoceïma", null),

                ("Agadir", "أكادير", "Agadir", "Souss-Massa", null),
                ("Tiznit", "تيزنيت", "Tiznit", "Souss-Massa", null),

                ("Oujda", "وجدة", "Oujda", "Oriental", null),
                ("Nador", "الناظور", "Nador", "Oriental", null),

                ("Beni Mellal", "بني ملال", "Beni Mellal", "Béni Mellal-Khénifra", null),
                ("Khénifra", "خنيفرة", "Khénifra", "Béni Mellal-Khénifra", null),

                ("Errachidia", "الرشيدية", "Errachidia", "Drâa-Tafilalet", null),

                ("Guelmim", "كلميم", "Guelmim", "Guelmim-Oued Noun", null),

                ("Laâyoune", "العيون", "Laayoune", "Laâyoune-Sakia El Hamra", null),

                ("Dakhla", "الداخلة", "Dakhla", "Dakhla-Oued Ed-Dahab", null)
            };

            foreach (var (nameFr, nameAr, nameEn, regionEn, fixedId) in cities)
            {
                var region = regionLookup.FirstOrDefault(r => r.NameEn == regionEn);
                if (region != null && !context.Cities.Any(c => c.NameEn == nameEn))
                {
                    context.Cities.Add(new City
                    {
                        Id = fixedId ?? Guid.NewGuid(),
                        NameFr = nameFr,
                        NameAr = nameAr,
                        NameEn = nameEn,
                        RegionId = region.Id
                    });
                }
            }
        }
        private static void SeedSchool(EdulinkContext context)
        {
            if (!context.Schools.Any())
            {
                var casablancaCity = context.Cities.FirstOrDefault(c => c.NameEn == "Casablanca");

                if (casablancaCity != null)
                {
                    var school = new SchoolMetadata
                    {
                        Id = Guid.Parse("5E6E7D89-D45E-4F65-8AD2-78BD9E0201A4"),
                        NameFr = "École Centrale",
                        NameAr = "المدرسة المركزية",
                        NameEn = "Central School",
                        Code = "CENTRAL2025",
                        AddressFr = "123 Bd Zerktouni",
                        AddressAr = "123 شارع الزرقطوني",
                        AddressEn = "123 Zerktouni Boulevard",
                        CityId = casablancaCity.Id,
                        UseIsolatedDatabase = true,
                        City = casablancaCity,

                        LogoUrl = string.Empty,
                        TimeZoneId = "Africa/Casablanca",
                        BackOfficeDbConnectionString = "Server=tcp:edulink-database.database.windows.net,1433;Initial Catalog=BackOffice;Persist Security Info=False;User ID=charaf;Password=YahyaMjhd2001;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    };

                    context.Schools.Add(school);
                }
            }
        }

    }
}
