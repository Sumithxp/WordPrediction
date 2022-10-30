using System.Net.Http.Headers;
using System.Text;
using WordPrediction.Api.Extentions;

namespace WordPrediction.Api.Middlewares
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
            var credentialHeader = authHeader.Parameter.Base64Decode();
            var credentials = credentialHeader.Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];

            // for testing purpose only
            context.Items["credentials"] = username.Concat(password);


            await _next(context);
        }
    }
}
