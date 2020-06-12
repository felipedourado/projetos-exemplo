using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easynvest.Extrato.Configurations;
using Easynvest.Extrato.Models;
using Easynvest.Extrato.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easynvest.Extrato.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtratoController : ControllerBase
    {
        private FeaturesToogles featureToogles;
        private readonly IExtratoService extratoService;

        public ExtratoController(IExtratoService extratoService)
        {
            this.extratoService = extratoService;
        }

        [HttpGet("consolidado")]
        public ActionResult<ExtratoConsolidado> Get()
            //passar um jwt
            //pegar id cliente na rota?
            
        {
            try
            {
                //validar o dado recebido
                //chamar a service (injetar?)
                //colocar o swagger
                return extratoService.ExtratoConsolidado();

                //return Ok(new List<ExtratoController>());
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}