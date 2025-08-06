using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.Resources
{
    public class Classroom : BaseEntity<Guid>
    {
        public Guid? BuildingId { get; set; }

        public string? RoomNumber { get; set; }
        public string? RoomTitleFr { get; set; }
        public string? RoomTitleAr { get; set; }
        public ClassroomTypeEnum RoomType { get; set; } 
        public int Capacity { get; set; }

        public Building? Building { get; set; }
    }
}
