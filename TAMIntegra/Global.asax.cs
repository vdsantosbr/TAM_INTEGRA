using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TAMIntegra.App_Start;
using TAMIntegra.MessageHandlers;

namespace TAMIntegra
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configuration.MessageHandlers.Add(new APIKeyHandler());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            //if (HttpContext.Current.User == null) return;
            //if (!HttpContext.Current.User.Identity.IsAuthenticated) return;
            //if (HttpContext.Current.User.Identity is FormsIdentity)
            //{
            //    var id = (FormsIdentity)HttpContext.Current.User.Identity;
            //    FormsAuthenticationTicket ticket = id.Ticket;
            //    string userData = ticket.UserData;
            //    string[] roles = userData.Split(',');
            //    HttpContext.Current.User = new GenericPrincipal(id, roles);
            //}
        }
        //protected void Application_BeginRequest()
        //{
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        //    Response.Cache.SetNoStore();
        //}
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
