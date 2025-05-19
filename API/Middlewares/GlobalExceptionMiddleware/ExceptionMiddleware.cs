using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Middlewares.GlobalExceptionMiddleware
{
    /// <summary>
    /// Handler the exceptions
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return;
            }

            try
            {
                httpContext.Request.EnableBuffering();
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw; 
                }

                string identifier = GenerateShortUniqueCode();
                await httpContext.HandleExceptionAsync(ex);
            }
        }

        private static string GenerateShortUniqueCode()
        {
            DateTime currentDateTime = DateTime.Now;
            string dateTimeStr = currentDateTime.ToString("yyMMddmmss");
            string randomChars = GenerateRandomString(4);
            string shortCode = dateTimeStr + randomChars;
            return shortCode;
        }

        private static string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
