using SchoolSaas.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SchoolSaas.Infrastructure.Identity.Context
{
    public class FrontOfficeIdentityContextInitializer : IContextInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public FrontOfficeIdentityContextInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {

        }
    }
}
