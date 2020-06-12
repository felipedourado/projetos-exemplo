using Easynvest.ExtratoDal.Configurations;
using Easynvest.ExtratoDal.Dtos;
using Easynvest.ExtratoDal.Infrastructure;
using Easynvest.ExtratoDal.Services.Interfaces;
using System;

namespace Easynvest.ExtratoDal.Services
{
    public class FundoServiceDal : IFundoServiceDal
    {
        private readonly IHttpService httpService;
        private readonly Endpoints endpoints;

        public FundoServiceDal(IHttpService httpService, Endpoints endpoints)
        {
            this.httpService = httpService;
            this.endpoints = endpoints;
        }

           
        public FundoDtoOutput Consultar()
        {
            var uriBuilder = new UriBuilder(endpoints.UrlFundo);

            return httpService.Get<FundoDtoOutput>(uriBuilder.Uri, null, null).Result;
        }
    }
}
