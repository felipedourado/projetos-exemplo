using Easynvest.ExtratoDal.Configurations;
using Easynvest.ExtratoDal.Dtos;
using Easynvest.ExtratoDal.Infrastructure;
using Easynvest.ExtratoDal.Services.Interfaces;
using System;

namespace Easynvest.ExtratoDal.Services
{
    public class TesouroServiceDal : ITesouroServiceDal
    {
        private readonly IHttpService httpService;
        private readonly Endpoints endpoints;

        public TesouroServiceDal(IHttpService httpService, Endpoints endpoints)
        {
            this.httpService = httpService;
            this.endpoints = endpoints;
        }


        public TesouroDtoOutput Consultar()
        {
            var uriBuilder = new UriBuilder(endpoints.UrlTesouro);

            return httpService.Get<TesouroDtoOutput>(uriBuilder.Uri, null, null).Result;
        }

    }
}
