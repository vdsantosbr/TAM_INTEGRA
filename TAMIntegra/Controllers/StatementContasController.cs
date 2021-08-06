using Business;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TAMIntegra.App_Start;


namespace TAMINTEGRA.Controllers
{
    //[CustomAuthorize(Roles = "frmStatementConta")]
    [CustomAuthorize(Roles = "frmStatement")]

    public class StatementContasController : Controller
    {
        private StatementContasBUS contasBUS = new StatementContasBUS();
        public ActionResult Index(int id_Conta = 0, string conta = null, string descricao = null, string situacaoFiltro = null)
        {
            CarregaDados();

            //string strSituacao = "";
            //if (situacao != null)
            //{
            //    foreach (string idSit in situacao)
            //    {
            //        if (strSituacao.Trim().Length > 0) { strSituacao += ","; }

            //        if ((strSituacao.Trim().Length + idSit.ToString().Length) < 4000)
            //        {
            //            strSituacao += idSit.ToString();
            //        }
            //    }
            //}

            StatementContas contas = new StatementContas();
            List<StatementContas> lstContas = new List<StatementContas>();
            lstContas = contasBUS.Lista(conta, descricao, situacaoFiltro);

            contas.lstContas = lstContas;


            contas.situacaoFiltro = situacaoFiltro;

           

            return View(contas);
        }

        public ActionResult Atualizar(int idConta, string conta, string descricao, string situacao)
        {
            if(situacao == "Ativo")
            {
                situacao = "A";
            }
            else
            {
                situacao = "I";
            }
            StatementContas contas = new StatementContas();
            contas = contasBUS.updateConta(idConta, conta, descricao, situacao);

            return RedirectToAction("Index");
        }

        public ActionResult Inserir(string conta, string descricao, string situacao)
        {
            if (situacao == "Ativo")
            {
                situacao = "A";
            }
            else
            {
                situacao = "I";
            }

            StatementContas contas = new StatementContas();
            contas = contasBUS.inserirConta(conta, descricao, situacao);
            if(contas == null)
            {
                contas = new StatementContas();
            }

            return Json(new { contas.Observacao }, JsonRequestBehavior.AllowGet);

            //return RedirectToAction("Index");
        }

        public ActionResult Excluir(int idConta)
        {
            StatementContas contas = new StatementContas();
            contas = contasBUS.excluirConta(idConta);

            return RedirectToAction("Index");
        }

        private void CarregaDados()
        {
            //ViewBag.Contas = new SelectList(contasBUS.Lista(), "Conta", "Conta");

            List<StatementContas> lstSituacoes = new List<StatementContas>();
            lstSituacoes.Add(new StatementContas { Situacao = "Ativo" });
            lstSituacoes.Add(new StatementContas {  Situacao = "Inativo" });

            ViewBag.Situacao = new SelectList(lstSituacoes, "Situacao", "Situacao");
        }
    }
}