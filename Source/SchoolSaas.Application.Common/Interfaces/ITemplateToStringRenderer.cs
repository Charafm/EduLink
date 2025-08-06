namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ITemplateToStringRenderer
    {
        Task<string> RenderTemplateToStringAsync<TModel>(string templateName, TModel model);
    }
}
