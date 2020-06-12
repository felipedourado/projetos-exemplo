using Easynvest.Extrato.Models;
using Easynvest.ExtratoDal.Dtos;
using System.Collections.Generic;

namespace Easynvest.Extrato.Adapters
{
    public static class InvestimentoAdapter
    {
        public static List<Investimento> FundoToModel(this FundoDtoOutput dto)
        {
            var investimentos = new List<Investimento>();

            foreach (var item in dto.Fundos)
            {
                var model = new Investimento()
                {
                    Nome = item.Nome,
                    ValorInvestido = item.CapitalInvestido,
                    Vencimento = item.DataResgate,
                    ValorTotal = item.ValorAtual,
                    DataCustodia = item.DataCompra
                };

                model.CalcularIr(string.Empty);
                model.CalcularResgate();

                investimentos.Add(model);
            }

            return investimentos;
        }

        public static List<Investimento> TesouroToModel(this TesouroDtoOutput tesouro)
        {
            var investimentos = new List<Investimento>();

            foreach (var item in tesouro.Tds)
            {
                var model = new Investimento()
                {
                    Nome = item.Nome,
                    ValorInvestido = item.ValorInvestido,
                    ValorTotal = item.ValorTotal,
                    Vencimento = item.Vencimento,
                    DataCustodia = item.DataDeCompra
                };

                model.CalcularIr(item.Tipo);
                model.CalcularResgate();
                investimentos.Add(model);
            }

            return investimentos;
        }

        public static List<Investimento> LciToModel(this LciDtoOutput lci)
        {
            var investimentos = new List<Investimento>();

            foreach (var item in lci.Lcis)
            {
                var model = new Investimento()
                {
                    Nome = item.Nome,
                    ValorInvestido = item.CapitalInvestido,
                    Vencimento = item.Vencimento,
                    ValorTotal = item.CapitalAtual,
                    DataCustodia = item.DataOperacao
                };

                model.CalcularIr(item.Tipo);
                model.CalcularResgate();
                investimentos.Add(model);
            }

            return investimentos;
        }

    }
}
