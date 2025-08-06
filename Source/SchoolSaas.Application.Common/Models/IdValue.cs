namespace SchoolSaas.Application.Common.Models
{
    public class IdValue<T,V>
    {
        public T Id { get; set; }   
        public V Value { get; set; }
        public V? ValueAr { get; set; }

    }

    public class IdValue<T>
    {
        public T Id { get; set; }
        public string Value { get; set; }
        public string? ValueAr { get; set; }
    }

    public class IdValue
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string? ValueAr { get; set; }
    }

    public class IdValueNumber
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string? ValueAr { get; set; }
    }
}
