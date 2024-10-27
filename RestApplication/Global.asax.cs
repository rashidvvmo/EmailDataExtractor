
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity.Configuration;
using NLog;
using NLog.Web;
using Unity;
using Unity.WebApi;

namespace RestApplication

{
    public class WebApiApplication : System.Web.HttpApplication

    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(Server.MapPath("~/config/NLog.config"));

            var container = new UnityContainer();
            container.LoadConfiguration();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}