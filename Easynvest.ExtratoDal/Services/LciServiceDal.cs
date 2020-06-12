using Easynvest.ExtratoDal.Configurations;
using Easynvest.ExtratoDal.Dtos;
using Easynvest.ExtratoDal.Infrastructure;
using Easynvest.ExtratoDal.Services.Interfaces;
using System;

namespace Easynvest.ExtratoDal.Services
{
    public class LciServiceDal : ILciServiceDal
    {
        private readonly IHttpService httpService;
        private readonly Endpoints endpoints;

        public LciServiceDal(IHttpService httpService, Endpoints endpoints)
        {
            this.httpService = httpService;
            this.endpoints = endpoints;
        }


        public LciDtoOutput Consultar()
        {
            var uriBuilder = new UriBuilder(endpoints.UrlLci);

            return httpService.Get<LciDtoOutput>(uriBuilder.Uri, null, null).Result;
        }
    }
}
