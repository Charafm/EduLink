using SchoolSaas.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SchoolSaas.Infrastructure.Identity.Context
{
    public class IdentityContextInitializer : IContextInitializer
    {
        private readonly ModelBuilder _modelBuilder;

        public IdentityContextInitializer(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Seed()
        {

        }
    }
}
