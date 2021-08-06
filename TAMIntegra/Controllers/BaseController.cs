using Business;
using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TAMIntegra.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var idUsuario = User.Identity.Name;
                if (!string.IsNullOrEmpty(idUsuario))
                {
                    UsuarioBUS usuarioBUS = new UsuarioBUS();
                    //Usuario usr = usuarioBUS.BuscaPorId(Convert.ToInt32(idUsuario));
                    //string nomeCompleto = string.Concat(new string[] { usr.Nome, " ", usr.Sobrenome });
                    //ViewData.Add("NomeCompleto", nomeCompleto);
                }
            }
            base.OnActionExecuted(filterContext);
        }
        public BaseController()
        {

        }
    }
}