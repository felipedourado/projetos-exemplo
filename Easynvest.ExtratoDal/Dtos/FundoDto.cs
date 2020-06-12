using System;

namespace Easynvest.ExtratoDal.Dtos
{
    public class FundoDto
    {
        public decimal CapitalInvestido { get; set; }
        public decimal ValorAtual { get; set; }
        public DateTime DataResgate { get; set; }
        public DateTime DataCompra { get; set; }
        public string Nome { get; set; }
        public decimal TotalTaxas { get; set; }
    }
}
