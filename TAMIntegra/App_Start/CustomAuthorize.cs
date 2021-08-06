using Business;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace TAMIntegra.App_Start
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private PerfilBUS perfilBUS = new PerfilBUS();

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (user.Identity.Name != "")
            {
                //Usuario usr2 = usuarioBUS.BuscaPorIdPessoa(Convert.ToInt32(user.Identity.Name));
                //string[] temx = Convert.ToString(HttpContext.Current.Request.LogonUserIdentity.Name).Split('\\');
                //string login = temx[1];
                //string login2 = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString().Replace("TAMMRL\\", "");
               
                Usuario usr = usuarioBUS.BuscaPorLogin(user.Identity.Name);
                Usuario adm = usuarioBUS.BuscaPorIdPessoa(usr.Id_Pessoa);
                HttpContext.Current.Session["login"] = user.Identity.Name;
                string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                ////HttpContext.Current.Session["login"] = user.Identity.Name;



                string[] roles = null;

                //roles = usuarioBUS.BuscaPorIdPerfil(usr.Id_Perfil).Split(',');
                //roles = usuarioBUS.BuscaPorIdPerfilFormularios(usr.Id_Perfil).Split(',');
                roles = perfilBUS.ParametrizacaoAutorizar(usr.Id_Perfil, controllerName, adm.Administrador).Split(',');


                var formsIdentity = filterContext.HttpContext.User.Identity as FormsIdentity;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.User = new System.Security.Principal.GenericPrincipal(formsIdentity, roles);

                HttpContext.Current.Session["Id_Perfil"] = usr.Id_Perfil;
            }


            if (this.AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    filterContext.Result = new RedirectResult("~/Erro/AcessoNegado");
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login");
                }
            }
        }
    }
}