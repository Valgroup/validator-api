namespace Validator.Application.Emails
{
    public interface ISendgridService
    {
        Task Send(string html, string email, string nome);
    }
}
