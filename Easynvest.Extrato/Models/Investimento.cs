using Newtonsoft.Json;
using System;

namespace Easynvest.Extrato.Models
{
    public class Investimento
    {
        public string Nome { get; set; }
        public decimal ValorInvestido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public decimal Ir { get; set; }
        public decimal ValorResgate { get; set; }
        [JsonIgnore]
        public DateTime DataCustodia { get; set; }

        public void CalcularIr(string tipo)
        {
            if (tipo.ToUpper().Equals("TD"))
            {
                Ir = ((ValorTotal - ValorInvestido) * 10) / 100;
            }
            else if (tipo.ToUpper().Equals("LCI"))
            {
                Ir = ((ValorTotal - ValorInvestido) * 5) / 100;
            }
            else
            {
                Ir = ((ValorTotal - ValorInvestido) * 15) / 100;
            }

        }

        public void CalcularResgate()
        {
            TimeSpan totalDias = Vencimento - DataCustodia;

            var resultado = Vencimento - DateTime.Now;

            TimeSpan metadeTempo = (totalDias * 50) / 100;

            if (metadeTempo.TotalDays > resultado.TotalDays)
            {
                ValorResgate = ValorTotal - ((ValorInvestido * 15) / 100);
            }
            else if (resultado.TotalDays <= 60)
            {
                ValorResgate = ValorTotal - ((ValorInvestido * 6) / 100);
            }
            else
            {
                ValorResgate = ValorTotal - ((ValorInvestido * 30) / 100);
            }

        }

    }
}
