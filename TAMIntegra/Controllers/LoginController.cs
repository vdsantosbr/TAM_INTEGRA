using Business;
using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace TAMIntegra.Controllers
{
    public class LoginController : BaseController
    {
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        UsuarioPerfilModuloDAL dalPerfilModulo = new UsuarioPerfilModuloDAL();
        // GET: Login
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login(string login, string senha)

        {
            Usuario usr = usuarioBUS.BuscaPorLogin(login);

            string[] roles;

            if (usr != null)
            {
                if (/*usuarioBUS.AutenticaAD(login, senha) &&*/ usr.Situacao == "Ativo" )
                {
                    //login = "vitor.fozzatti";
                    //usr = usuarioBUS.BuscaPorLogin(login);
                    //roles = usuarioBUS.BuscaModulos(usr.Id_Perfil).Split(',');
                    //ARMAZENA INFORMAÇÕES DO USUÁRIO
                    var ticket = new FormsAuthenticationTicket(0,
                        //usr.Id.ToString(),
                        login,
                        DateTime.Now,
                        DateTime.Now.AddDays(1),
                        false,
                        "AAA,BBB",
                        FormsAuthentication.FormsCookiePath);

                    string encryptedCookieContent = FormsAuthentication.Encrypt(ticket);

                    var formsAuthenticationTicketCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookieContent)
                    {
                        Domain = FormsAuthentication.CookieDomain,
                        Path = FormsAuthentication.FormsCookiePath,
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL
                    };

                    formsAuthenticationTicketCookie.Expires = DateTime.Now.AddDays(1);

                    System.Web.HttpContext.Current.Response.Cookies.Add(formsAuthenticationTicketCookie);

                    Session["login"] = login;
                    ViewBag.Login = login;

                    List<UsuarioPerfilModulo> lstUpm = dalPerfilModulo.BuscaPorIdPerfilPessoa(usr.Id_Perfil);
                    if(lstUpm.Count == 0)
                    {
                        TempData["Mensagem2"] = "Sem acesso";
                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        var controller = lstUpm
                     .Select(a => a.Form_Principal)
                     .First();


                        if (lstUpm.Where(a => a.Selecao.Equals("true") && a.Formulario.Equals("frmFiscal")).Count()>0)
                        {
                            return RedirectToAction("Index", "RecebimentoAvalara");
                        }
                        //else if (controller.Equals("frmCavok"))
                        //{
                        //    return RedirectToAction("Index", "Cavok");
                        //}
                        //else if (controller.Equals("frmImportacaoDocumento"))
                        //{
                        //    return RedirectToAction("Index", "ImportacaoDocumento");
                        //}
                        //else if (controller.Equals("frmStatementConciliacao"))
                        //{
                        //    return RedirectToAction("Index", "StatementConciliacao");
                        //}
                        //else if (controller.Equals("frmProcesso"))
                        //{
                        //    return RedirectToAction("Index", "FinanceiroProcesso");
                        //}
                        //else if (controller.Equals("frmFinanceiroCambioSys"))
                        //{
                        //    return RedirectToAction("Index", "FinanceiroCambioSYS");
                        //}
                        //else 
                        //{
                        //    return RedirectToAction("Index", "Home");
                        //}

                        else
                        {
                            TempData["Mensagem"] = "Acesso Negado!!!";
                        }


                    }                    

                  
                }
                else
                {
                    TempData["Mensagem"] = "Usuário e/ou senha não confere(m)!";
                }
            }
            else
            {
                TempData["Mensagem"] = "Usuário não cadastrado ou inativo!";
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["login"] = "";
            return RedirectToAction("Index", "Login");
        }       
    }
}