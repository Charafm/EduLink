namespace SchoolSaas.Application.Common.Models
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
