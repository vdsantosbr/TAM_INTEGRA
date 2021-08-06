using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Entities;
using Business;
using System.Globalization;
using System.IO;
using ClosedXML.Excel;
using System.Data;

namespace TAMINTEGRA.Controllers
{
    public class CasosExcecaoController : Controller
    {
        private CasosExcecao exc = new CasosExcecao();
        private CasosExcecaoBUS exBUS = new CasosExcecaoBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [CustomAuthorize(Roles = "frmContabilizacaoVCEditar")]
        public ActionResult Index()
        {
            List<CasosExcecao> lstGRid = new List<CasosExcecao>();
            List<CasosExcecao> lstRegistros = new List<CasosExcecao>();

            lstGRid = exBUS.Exibir();
            lstRegistros = exBUS.GridExcecoes();

            exc.lstGrid = lstGRid;
            exc.lstRegistroIntegracao = lstRegistros;

            return View(exc);
        }
        public ActionResult SalvarExcecoes(int id_excecao = 0, string referencia = "", string observacao = "")
        {
            List<CasosExcecao> lst = new List<CasosExcecao>();
            Usuario usuario = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            lst = exBUS.AddExcecoes(id_excecao, usuario.Id_Pessoa, referencia, observacao);

            if(lst != null)
            {
                return Json(new { Resultado = lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("" , JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RemoverExcecao(int id_excecao = 0)
        {
            List<CasosExcecao> lst = new List<CasosExcecao>();

            lst = exBUS.RemoverExcecao(id_excecao);

            if (lst != null)
            {
                return Json(new { Resultado = lst }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}