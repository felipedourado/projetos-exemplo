using System;

namespace Easynvest.ExtratoDal.Dtos
{
    public class LciDto
    {
        public decimal CapitalInvestido { get; set; }
        public decimal CapitalAtual { get; set; }
        public DateTime Vencimento { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public DateTime DataOperacao { get; set; }
    }
}
