namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IPdfGenerationService
    {
        Task<byte[]> GeneratePdfAsync<TModel>(string template, TModel model, bool isLandscape = false) where TModel : class;
    }
}