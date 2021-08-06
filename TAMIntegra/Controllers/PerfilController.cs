using Business;
using Data;
using Entities;
using System;
using System.Web.Mvc;
using System.DirectoryServices;
using TAMIntegra.App_Start;
using System.Collections.Generic;
using System.Linq;

namespace TAMINTEGRA.Controllers
{
    public class PerfilController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private Perfil perfil = new Perfil();
        private PerfilBUS perfilBUS = new PerfilBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [CustomAuthorize(Roles = "frmSeguranca")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(string origem, int id_perfil = 0, string nome = "", string situacao = "")
        {
            try
            {
                CarregaDados();
                string user = User.Identity.Name;
                perfil.lstPerfil = perfilBUS.Perfil().ToList();

                return View(perfil);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }
        }

        public ActionResult salvarPerfil(int id_perfil = 0, string nome = "", string menu = "", string situacao = "")
        {
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            List<Perfil> lst = new List<Perfil>();
            lst = perfilBUS.SalvarPerfil(usr.Id_Pessoa, id_perfil, nome, menu, situacao);

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        private void CarregaDados()
        {
            List<Perfil> lstPerfil = new List<Perfil>();
            lstPerfil = perfilBUS.Perfil().ToList();
            ViewBag.Perfil = new SelectList(lstPerfil, "id_perfil", "nome");

            List<Perfil> lstMenu = new List<Perfil>();
            lstMenu = perfilBUS.MenuInicializacao().ToList();
            ViewBag.Menu = new SelectList(lstMenu, "formulario", "descricao");
        }
    }
}