using System.Collections.Generic;

namespace Easynvest.Extrato.Models
{
    public class ExtratoConsolidado
    {
        public decimal Total { get; set; }
        public List<Investimento> Investimentos { get; set; }
    }
}
