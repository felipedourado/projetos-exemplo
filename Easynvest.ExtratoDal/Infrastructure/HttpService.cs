using RestSharp;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading.Tasks;
using Easynvest.ExtratoDal.Exceptions;

namespace Easynvest.ExtratoDal.Infrastructure
{
    public class HttpService : IHttpService
    {
        public HttpContent Get(Uri uri, string apikey, string token)
        {
            var handler = new TimeoutHandler()
            {
                DefaultTimeout = TimeSpan.FromSeconds(10),
                InnerHandler = new HttpClientHandler()
                {
                    SslProtocols = SslProtocols.Tls12
                }
            };

            using (var timeoutHandler = new TimeoutHandler())
            {
                timeoutHandler.DefaultTimeout = TimeSpan.FromSeconds(10);

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.SslProtocols = SslProtocols.Tls12;

                    try
                    {
                        var client = new HttpClient(timeoutHandler);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        if (!string.IsNullOrEmpty(apikey))
                        {
                            client.DefaultRequestHeaders.Add("Api-Key", apikey);
                        }

                        if (!string.IsNullOrEmpty(token))
                        {
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        }

                        var response = client.GetAsync(uri).Result;

                        if (response.StatusCode != HttpStatusCode.OK || response.StatusCode != HttpStatusCode.NoContent)
                        {
                            throw new HttpException(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                        }

                        return response.Content;

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Erro ao executar get na api: " + uri, ex);
                    }
                }
            }

        }

        public async Task<T> Get<T>(Uri uri, string subscriptionKey, string token)
        {
            var client = new RestClient(uri);

            var request = new RestRequest(Method.GET);
            request.AddHeader("Apim-Subscription-Key", subscriptionKey);

            //colocar o bearer JWT
            //validar os httpstatus code
            try
            {
                var result = await client.GetAsync<T>(request);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
