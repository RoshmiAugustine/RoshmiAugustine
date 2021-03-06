using Microsoft.AspNetCore.Http;
using Opeeka.PICS.Api.Middleware;
using Opeeka.PICS.Api.Middleware.Entities;
using Opeeka.PICS.Infrastructure.Enums;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AntiXssMiddleware.Middleware
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate _next;
        private ErrorResponse _error;
        private readonly int _statusCode = (int)HttpStatusCode.BadRequest;

        public AntiXssMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            // Check XSS in URL
            if (!string.IsNullOrWhiteSpace(context.Request.Path.Value))
            {
                var url = context.Request.Path.Value;

                if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            // Check XSS in query string
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString.Value))
            {
                var queryString = WebUtility.UrlDecode(context.Request.QueryString.Value);

                if (CrossSiteScriptingValidation.IsDangerousString(queryString, out _))
                {
                    await RespondWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            // Check XSS in request content
            var originalBody = context.Request.Body;
            try
            {
                if (context.Request.Path.Value.ToLower() != "/api/report/family-report-downloadpdf" &&
                    context.Request.Path.Value.ToLower() != "/api/user-profile")
                {
                    var content = await ReadRequestBody(context);

                    if (CrossSiteScriptingValidation.IsDangerousString(content, out _))
                    {
                        await RespondWithAnError(context).ConfigureAwait(false);
                        return;
                    }
                }
                await _next(context).ConfigureAwait(false);
            }
            finally
            {
                context.Request.Body = originalBody;
            }
        }

        private static async Task<string> ReadRequestBody(HttpContext context)
        {
            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);
            context.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            context.Request.Body.Position = 0;

            return requestContent;
        }

        private async Task RespondWithAnError(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Headers.AddHeaders();
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = _statusCode;

            if (_error == null)
            {
                _error = new ErrorResponse
                {
                    Description = PCISEnum.StatusMessages.HTMLCodeDetected+" - "+ CultureInfo.CurrentCulture.Name + DateTime.Now.ToString(),
                    ErrorCode = PCISEnum.StatusCodes.HTMLCodeDetected
                };
            }

            await context.Response.WriteAsync(_error.ToJSON());
        }
    }
}