using Microsoft.AspNetCore.Http;

namespace SO.Application.Middleware
{
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Continue down the Middleware pipeline, eventually returning to this class
            await _next(context);

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
            if (!context.Response.Headers.ContainsKey("X-Content-Type-Options"))
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            }

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
            if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            }

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
            const string csp = "default-src 'self'; object-src 'none'; frame-ancestors 'none'; sandbox allow-forms allow-same-origin allow-scripts; base-uri 'self';";


            // once for standards compliant browsers
            if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
            {
                context.Response.Headers.Add("Content-Security-Policy", csp);
            }
            // and once again for IE
            if (!context.Response.Headers.ContainsKey("X-Content-Security-Policy"))
            {
                context.Response.Headers.Add("X-Content-Security-Policy", csp);
            }

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
            const string referrerPolicy = "no-referrer";
            if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
            {
                context.Response.Headers.Add("Referrer-Policy", referrerPolicy);
            }

            if (context.Response.Headers.ContainsKey("Server"))
            {
                context.Response.Headers.Remove("Server");
            }

            const string strictTransportSecurity = "max-age=31536000; includeSubDomains; preload";
            if (!context.Response.Headers.ContainsKey("Strict-Transport-Security"))
            {
                context.Response.Headers.Add("Strict-Transport-Security", strictTransportSecurity);
            }
        }

    }

}