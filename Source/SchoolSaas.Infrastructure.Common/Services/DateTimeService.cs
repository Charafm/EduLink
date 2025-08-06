using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}