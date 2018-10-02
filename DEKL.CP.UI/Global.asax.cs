using DEKL.CP.UI.Mappers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DEKL.CP.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.RegisterMappings();
        }
    }
}
