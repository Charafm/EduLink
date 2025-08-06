using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Constants;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Edulink.Context
{
    public class EdulinkContextInitializer : IContextInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public EdulinkContextInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            //_modelBuilder.Entity<Contact>().HasData(GenerateContact());
        }

        public const string EdulinkId = "366459bf-e37c-4d1b-ba8e-78674f4707cc";

        private static Dictionary<Guid, List<Guid>> backofficesIds = new Dictionary<Guid, List<Guid>>()
        {
            { Guid.Parse(AuthorizationConstants.DefaultBackofficeId), new List<Guid>() { Guid.Parse("9b79feb4-3540-447e-b4cf-88a07a7522a7"), Guid.Parse("c005fb0c-d64f-4e6f-912c-b60fb7cf6d37") } }
        };

        private List<Contact> GenerateContact()
        {
            return new List<Contact>()
            {

                         new Contact()
                        {

                              Id=Guid.Parse("2a7cec14-dd33-480d-93da-041682ec5c82"),
                              FirstName= "Ayoub",
                              LastName= "Ayoub",
                              Address= "62 rue de la Boétie",
                              AddressLine2= "62 rue de la Boétie",
                              ZipCode= "97110",
                              CountryTaxId= Guid.Parse("3c6b0a8e-7654-4e5e-9efc-c8f2e4074072"),
                              PhoneNumber= "02.64.87.76.62",
                              Email= "Ayoub@gmail.com",
                              Created= DateTime.UtcNow,
                              LastModified= DateTime.UtcNow,

                        },
            };
        }
    }
}
