using System.Net.Http;
using System.Web.Http;

namespace Vacinas
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {

            configuration.Routes.MapHttpRoute("DefaultApiWithAction", "api/{controller}/{action}/{id}");

            configuration.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
                new { action = "DefaultAction", id = System.Web.Http.RouteParameter.Optional });
        }
    }
}