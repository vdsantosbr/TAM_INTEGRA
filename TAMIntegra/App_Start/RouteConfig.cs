using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TAMIntegra
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Tratativa",
                "EntidadeTratativa/{action}/{idEntidade}/{idTratativa}",
                new { controller = "EntidadeTratativa", action = "Index", idEntidade = "", idTratativa = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { culture = "pt-BR", controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
