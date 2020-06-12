using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Easynvest.Extrato.Configurations;
using Easynvest.Extrato.Services.Interfaces;
using Easynvest.Extrato.Services;
using Easynvest.ExtratoDal.Configurations;
using Easynvest.ExtratoDal.Infrastructure;
using Easynvest.ExtratoDal.Services;
using Easynvest.ExtratoDal.Services.Interfaces;

namespace Easynvest.Extrato
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Endpoints>>().Value);
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<FeaturesToogles>>().Value);
            
            services.AddSingleton<IHttpService, HttpService>();
            services.AddSingleton<IFundoServiceDal, FundoServiceDal>();
            services.AddSingleton<ITesouroServiceDal, TesouroServiceDal>();
            services.AddSingleton<ILciServiceDal, LciServiceDal>();
            services.AddSingleton<IExtratoService, ExtratoService>();


            // Verificando a disponibilidade do redis
            // da aplicação através de Health Checks
            services.AddHealthChecks();
            //    .AddDependencies(dadosDependencias);
            //  services.AddHealthChecksUI();

            services.Configure<FeaturesToogles>(Configuration.GetSection(nameof(FeaturesToogles)));
            services.Configure<Endpoints>(Configuration.GetSection(nameof(Endpoints)));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHealthChecks("/healthchecks", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            //  app.UseHealthChecksUI();

            // Endpoint para retorno de informações sobre Health Checks
            // a serem utilizados pelo Worker Process de Monitoramento
            app.UseHealthChecks("/status-monitoramento",
               new HealthCheckOptions()
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = JsonSerializer.Serialize(
                            report.Entries.Select(e => new
                            {
                                healthCheck = e.Key,
                                error = e.Value.Exception?.Message,
                                status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                            }));
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
               });
        }
    }
}
