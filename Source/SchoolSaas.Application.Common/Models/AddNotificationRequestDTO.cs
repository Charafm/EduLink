//using SchoolSaas.Domain.Backoffice.Entities;
using SchoolSaas.Domain.Common.Enums;
namespace SchoolSaas.Application.Common.Models;
public class AddNotificationRequestDTO
{

    public NotificationEnum Target { get; set; }
    public string UserId { get; set; }
    public int Status { get; set; }
}



