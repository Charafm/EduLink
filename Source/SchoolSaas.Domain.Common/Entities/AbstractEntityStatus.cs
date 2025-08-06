namespace SchoolSaas.Domain.Common.Entities
{
    public abstract class AbstractEntityStatus<T> : BaseEntity<int> where T : struct
    {
        public virtual T Type { get; set; }

        public virtual string? Comment { get; set; } = null!;
    }
}