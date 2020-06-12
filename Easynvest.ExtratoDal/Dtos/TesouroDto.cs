using System;

namespace Easynvest.ExtratoDal.Dtos
{
    public class TesouroDto
    {
        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataDeCompra { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}
