namespace Validator.Data.Dapper
{
    public interface INotificacaoReadOnlyRespository
    {
        Task EnviarNotificacaoPendente(string url);
    }
}
