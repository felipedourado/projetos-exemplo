using Easynvest.Extrato.Adapters;
using Easynvest.Extrato.Configurations;
using Easynvest.Extrato.Models;
using Easynvest.Extrato.Services.Interfaces;
using Easynvest.ExtratoDal.Services;
using Easynvest.ExtratoDal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easynvest.Extrato.Services
{
    public class ExtratoService : IExtratoService
    {
        private readonly IFundoServiceDal fundoService;
        private readonly ITesouroServiceDal tesouroService;
        private readonly ILciServiceDal lciService;
        private readonly FeaturesToogles features;

        public ExtratoService(IFundoServiceDal fundoService, ITesouroServiceDal tesouroService,
            ILciServiceDal lciService, FeaturesToogles features)
        {
            this.fundoService = fundoService;
            this.tesouroService = tesouroService;
            this.lciService = lciService;
            this.features = features;
        }

        public ExtratoConsolidado ExtratoConsolidado()
        {
            var investimentos = new List<Investimento>();

            if (features.FundoEnable)
                investimentos.AddRange(fundoService.Consultar().FundoToModel());

            if (features.TesouroEnable)
                investimentos.AddRange(tesouroService.Consultar().TesouroToModel());

            if (features.LciEnable)
                investimentos.AddRange(lciService.Consultar().LciToModel());

            var extratoConsolidado = new ExtratoConsolidado() { Investimentos = investimentos, Total = investimentos.Sum(x => x.ValorTotal) };

            return extratoConsolidado;
        }


        //pontos para escrever no readme
        //dependendo da estrutura do projeto poderia ser utilizado o pattern strategy de desenvolvimento
        //utilizei o conceito de dal, adapter mas poderia ter utilizado o repository direto no projeto ao invés da dal e feito as dtos como model e os adapters dentro da model tb.
        //as regras de calculos achei mais interessante deixar dentro da model investimento por causa do conceito do objeto 
        //fiquei em duvida de volumetria e na questão de assicrona para fins do exercicio deixei sem task


        //private async Task<List<Investimento>> Lci()
        //{
        //    var uriBuilder = new UriBuilder(uri.UrlTesouro);
        //    //passar a model que é retornada na api
        //    var result = await httpService.Get<List<LciDtoOutput>>(uriBuilder.Uri, null, null);

        //    //usar o adapter para investiment
        //    return new List<Investimento>();
        //}

    }
}


/*
Resultado esperado:
Readme explicando todas as decisões de projeto.
Caso tenha realizado o nível 3, fornecer o endpoint para consumo da Api
Critérios de Avaliação:
1. Organização do projeto
2. Boas práticas
3. Testes
4. Monitoria de Aplicação
Também usaremos os níveis abaixo para avaliar sua entrega:

Nível 2
Uma boa prática para aplicações de alto desempenho é cachear as informações. O resultado pode
ser cacheado até o dia seguinte (00:00).
Nível 3
Hospedar a aplicação em uma nuvem pública de sua escolha (heroku, next, aws, google cloud, azure,
etc). Pode ser utilizado o free-tier, não será realizado nenhum teste de carga intensa

 * TODO
 * redis
 * monitoramento (log, grafana, apm, healthcheck)
 * polly
 * swashbuckler
 * git
 * subir na cloud
 * testes
 */
