namespace SchoolSaas.Domain.Common.Entities
{
    public interface IDeletableEntity
    {
        public bool? IsDeleted { get; set; }
    }
}