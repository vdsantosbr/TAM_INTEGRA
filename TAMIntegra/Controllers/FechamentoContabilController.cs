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
    public class FechamentoContabilController : Controller
    {
        private FechamentoContabil fechCambial = new FechamentoContabil();
        private FechamentoContabilBUS fechBUS = new FechamentoContabilBUS();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [CustomAuthorize(Roles = "frmContabilizacaoVCEditar")]
        public ActionResult Index(int ano = 0, string exibir = "")
        {
            Session["Situação_Modal"] = "";

            List<FechamentoContabil> lstGRid = new List<FechamentoContabil>();
            List<FechamentoContabil> lstAno = new List<FechamentoContabil>();

            if (ano == 0)
            {
                ano = Convert.ToInt32(DateTime.Today.ToString("yyyy"));
            }

            lstGRid = fechBUS.Filtro(ano);
            foreach(var r in lstGRid)
            {
                if (r.Situacao == "Fechado")
                {
                    Session["Situação"] = "N";
                }
            }

            fechCambial.Ano = ano;

            if ((ano == DateTime.Now.Year) || exibir != "")
            {
                fechCambial.lstGrid = lstGRid;
            }

            lstAno = fechBUS.FechamentoAno();
            ViewBag.Ano = new SelectList(lstAno, "Ano", "Ano");


            return View(fechCambial);
        }
        public JsonResult Fechamento_Novo()
        {
            List<FechamentoContabil> lst = new List<FechamentoContabil>();
            Usuario usuario = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            lst = fechBUS.Fechamento_Novo(); ;

            if (lst != null)
            {
                var resultado = (from info in lst
                                 select new
                                 {
                                     Id_fechamento = info.Id_Fechamento,
                                     Fechamento = info.Fechamento,
                                     Dt_Contabilizacao = info.Data_contabilizacao,
                                     Situacao = info.Situacao,
                                     Exibir_Salvar = info.Exibir_Salvar,
                                     Usuario = usuario.Nome,
                                     Observacao = info.Observacao
                                 }).ToList();
                return Json(new { Resultado = resultado}, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Fechamento(int id_fechamento = 0, int ano = 0, int mes = 0)
        {
            List<FechamentoContabil> lst = new List<FechamentoContabil>();
            List<IntegracaoCambioSys> lstCambio = new List<IntegracaoCambioSys>();

            lst = fechBUS.Fechamento(id_fechamento); ;
            lstCambio = fechBUS.GridCambioSys(mes, ano);
            Session["GridCambio"] = lstCambio;
            foreach(var r in lst)
            {
                if(r.Situacao == "F")
                {
                    Session["Situação_Modal"] = "N";
                }
            }
            if (lst != null)
            {
                var resultado = (from info in lst
                                 select new
                                 {
                                     Id_fechamento = info.Id_Fechamento,
                                     Data_Registro = info.Data_Registro,
                                     Ano = info.Ano,
                                     Mes = info.Mes,
                                     Dt_Contabilizacao = info.Data_contabilizacao,
                                     Situacao = info.Situacao,
                                     Responsavel_Abertura = info.Responsavel_Abertura,
                                     Responsavel_Fechamento = info.Responsavel_Fechamento,
                                     Data_Abertura = info.Data_Abertura,
                                     Data_Fechamento = info.Data_Fechamento,
                                     Observação = info.Observacao,
                                     Exibir_Salvar = info.Exibir_Salvar
                                 }).ToList();
                

                return Json(new { Resultado = resultado, Cambio = lstCambio }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Salvar(int id_fechamento = 0, string fechamento = "", string dt_contabil = "", string situacao = "", string observacao = "", int id_integracao = 0)
        {
            Usuario usuario = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.ParseExact(dt_contabil, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            List<FechamentoContabil> lst = fechBUS.Salvar(id_fechamento, id_integracao, fechamento, dt, situacao, usuario.Id_Pessoa, observacao);
            string mensagem = "";
            if(lst != null){
                foreach (var r in lst)
                {
                    mensagem = r.Mensagem;
                }

            }
            return Json(new { mensagem }, JsonRequestBehavior.AllowGet);

        }
    }
}