namespace SchoolSaas.Infrastructure.Common.Areas.Emails.Views
{
    public class TemplateViewModel<T>
    {
        public Guid? Userid { get; set; }
        public T Data { get; set; } = Activator.CreateInstance<T>();
        public TemplateViewModel() { }

        public TemplateViewModel(T data)
        {
            Data = data;
        }
    }
}
