namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeResourceDTO
    {

        public Guid Id { get; set; }
        public Guid GradeLevelId { get; set; }
        public Guid? BookId { get; set; }
        public Guid? SchoolSupplyId { get; set; }
        public int? SupplyQuantity { get; set; }
        public string GradeLevelName { get; set; }
        public string ResourceTitle { get; set; }
    }
}
