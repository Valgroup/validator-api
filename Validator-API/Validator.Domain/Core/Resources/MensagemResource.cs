namespace Validator.Domain.Core.Resources
{
    public class MensagemResource
    {
        public static string EhObrigatorio(string campo) => $"{campo} é obrigatório";
    }
}
