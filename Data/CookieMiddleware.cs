using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Data

{
    public class CookieMiddleware: IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // verificar si la cookie específica está presente
            var cookieValue = context.Request.Cookies["UserCookie"];

            if (!string.IsNullOrEmpty(cookieValue))
            {
                // si la cookie está presente, establecer una variable global
                context.Items["CookiePresente"] = true;
            }
            else
            {
                context.Items["CookiePresente"] = false;
            }

            await next(context);
        }
    }
}
