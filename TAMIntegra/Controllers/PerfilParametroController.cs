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
    public class PerfilParametroController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private Perfil perfil = new Perfil();
        private PerfilBUS perfilBUS = new PerfilBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [CustomAuthorize(Roles = "frmSeguranca")]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(int id_perfil = 0, string nome = "", string situacao = "")
        {
            try
            {
                CarregaDados();
                List<Perfil> lstPerfilParametro = new List<Perfil>();
                perfil.lstPerfilParametrizacao = perfilBUS.Parametrizacao(id_perfil).ToList();

                lstPerfilParametro.Add(new Perfil
                {
                    Nome = nome,
                    Situacao = situacao
                });
                foreach (var r in perfil.lstPerfilParametrizacao)
                {

                }

                perfil.lstPerfilParametro = lstPerfilParametro;
                return View(perfil);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }
        }
        public JsonResult salvar(List<Perfil> tabela = null) {
            Usuario usr = usuarioBUS.BuscaPorLogin(User.Identity.Name);
            List<Perfil> lst = new List<Perfil>();
            int id_perfil = 0;
            string permitir_consultar = "";
            string permitir_editar = "";
            string permitir_exportar = "";

            try
            {
                foreach (var r in tabela)
                {
                    id_perfil = r.Id_Perfil;
                }

               perfilBUS.AddPerfil(id_perfil, usr.Id_Pessoa);

                foreach (var r in tabela)
                {
                    if(r.Permitir_Consultar == "on")
                    {
                        r.Permitir_Consultar = "S";
                    }
                    else
                    {
                        r.Permitir_Consultar = "N";
                    }

                    if (r.Permitir_Editar == "on")
                    {
                        r.Permitir_Editar = "S";
                        r.Permitir_Consultar = "S";
                    }
                    else
                    {
                        r.Permitir_Editar = "N";
                    }

                    if (r.Permitir_Exportar == "on")
                    {
                        r.Permitir_Exportar = "S";
                    }
                    else
                    {
                        r.Permitir_Exportar = "N";
                    }

                    if (r.Permitir_Consultar == "N" & r.Permitir_Editar == "N")
                    {
                        r.Permitir_Exportar = "N";
                    }

                    lst = perfilBUS.AddPerfilFuncionalidade(r.Id_Perfil, r.Id_Funcionalidade, r.Permitir_Consultar, r.Permitir_Editar, r.Permitir_Exportar);
                }
                return Json(lst, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
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
            ViewBag.Menu = new SelectList(lstMenu, "formulario", "formulario");
        }
    }
}