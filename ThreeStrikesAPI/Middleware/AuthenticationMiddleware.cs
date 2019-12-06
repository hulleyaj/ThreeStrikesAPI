using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ThreeStrikesAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            bool isProd = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic") && isProd)
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                if (context.Request.Path.StartsWithSegments(new PathString("/api/PushNotifications"), StringComparison.OrdinalIgnoreCase) &&
                    username == Environment.GetEnvironmentVariable("API_AUTH_NAME_PUSH_NOTIFICATION") &&
                    password == Environment.GetEnvironmentVariable("API_AUTH_PASS_PUSH_NOTIFICATION"))
                {
                    await _next.Invoke(context);
                }
                else if (username == Environment.GetEnvironmentVariable("API_AUTH_NAME") && 
                    password == Environment.GetEnvironmentVariable("API_AUTH_PASS"))
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                return;
            }
        }
    }
}
