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
    //[CustomAuthorize(Roles = "frmStatementClassificacao")]
    [CustomAuthorize(Roles = "frmStatement")]

    public class StatementClassificacaoController : Controller
    {
        // GET: StatementClassificacao
        private StatementClassificacaoBUS classificacaoBUS = new StatementClassificacaoBUS();
        public ActionResult Index(int id_Conta = 0, string classificacao = null, string descricao = null, string situacaoFiltro = null)
        
{
            CarregaDados();

            StatementClassificacao classificacoes = new StatementClassificacao();
            List<StatementClassificacao> lstClassificacao = new List<StatementClassificacao>();
            lstClassificacao = classificacaoBUS.Lista(classificacao, descricao, situacaoFiltro).Where(x => x.Visivel.Equals("S")).ToList();
            classificacoes.lstClassificacoes = lstClassificacao;
            classificacoes.situacaoFiltro = situacaoFiltro;

            return View(classificacoes);
        }

        public ActionResult Atualizar(int idConta, string classificacao = null, string descricao = null, string situacao = null)
        {
            if (situacao == "Ativo")
            {
                situacao = "A";
            }
            else
            {
                situacao = "I";
            }
            StatementClassificacao classificacoes = new StatementClassificacao();
            classificacoes = classificacaoBUS.updateclassificacao(idConta, classificacao, descricao, situacao);

            return RedirectToAction("Index");
        }

        public ActionResult Inserir(string descricao, string situacao)
        {
            if (situacao == "Ativo")
            {
                situacao = "A";
            }
            else
            {
                situacao = "I";
            }

            StatementClassificacao classificacoes = new StatementClassificacao();
            classificacoes = classificacaoBUS.inserirclassificacao(descricao, situacao);

            return RedirectToAction("Index");
        }

        public ActionResult Excluir(int idClassificacao)
        {
        
            StatementClassificacao classificacoes = new StatementClassificacao();
            classificacoes = classificacaoBUS.excluirClassificacao(idClassificacao);

            return RedirectToAction("Index");
        }
        private void CarregaDados()
        {
            //ViewBag.Contas = new SelectList(contasBUS.Lista(), "Conta", "Conta");

            List<StatementContas> lstSituacoes = new List<StatementContas>();
            lstSituacoes.Add(new StatementContas { Situacao = "Ativo" });
            lstSituacoes.Add(new StatementContas { Situacao = "Inativo" });

            ViewBag.Situacao = new SelectList(lstSituacoes, "Situacao", "Situacao");
        }
    }
}