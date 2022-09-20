namespace Validator.Domain.Core.Interfaces
{
    public interface ITemplateRazorService
    {
        Task<string> BuilderHtmlAsString<T>(string viewName, T model) where T : class, new();
    }
}
