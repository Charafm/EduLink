using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Resources
{
        public class GradeResource : BaseEntity<Guid>
        {
            public Guid GradeLevelId { get; set; }
            public Guid? BookId { get; set; }
            public Guid? SupplyId { get; set; }
            public int? SupplyQuantity { get; set; }

            public GradeLevel GradeLevel { get; set; }
            public Book? Book { get; set; }
            public SchoolSupply? Supply { get; set; }
        }

}
