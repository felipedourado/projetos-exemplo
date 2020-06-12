using System.Net;
using System.Net.Http;

namespace Easynvest.ExtratoDal.Exceptions
{
    public class HttpException : HttpRequestException
    {
        public HttpStatusCode Code { get; set; }

        public HttpException(HttpStatusCode code, string mensagem)
            : base(mensagem)
        {
            Code = code;
        }

    }
}
