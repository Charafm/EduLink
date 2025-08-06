namespace SchoolSaas.Infrastructure.Common
{
    public class PdfViewModel<T>
    {
        public T Data { get; set; } = Activator.CreateInstance<T>();
    }
}
