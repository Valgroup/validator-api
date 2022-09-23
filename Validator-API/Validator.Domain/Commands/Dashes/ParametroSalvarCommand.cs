namespace Validator.Domain.Commands.Dashes
{
    public class ParametroSalvarCommand
    {
        public int QtdeSugestaoMin { get; set; }
        public int QtdeSugestaoMax { get; set; }
        public int QtdeAvaliador { get; set; }
        public int QtdDiaFinaliza { get; set; }
    }
}
