namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IServiceHelper
    {
        Task<T> ExecuteWithTransactionAsync<T>(Func<Task<T>> operation);
    }
    public interface IEdulinkServiceHelper
    {
        Task<T> ExecuteWithTransactionAsync<T>(Func<Task<T>> operation);
    }
}
