using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class NullDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}