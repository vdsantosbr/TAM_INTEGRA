using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;
using Business;
using Entities;

namespace TAMINTEGRA.Controllers
{
    public class ConfiguracaoController : Controller
    {
        ConfiguracaoBUS configBUS = new ConfiguracaoBUS();
        Configuracao config = new Configuracao();

        [CustomAuthorize(Roles = "frmConfiguracao")]
        public ActionResult Index(string id_projeto = null, string documento = null)
        {
            CarregarDados();

            List<Configuracao> lstConfig = new List<Configuracao>();

            if(id_projeto != null & documento != null)
            {
                lstConfig = configBUS.Lista(id_projeto, documento).ToList();
            }

            config.lstConfiguracao = lstConfig;

            return View(config);
        }

        private void CarregarDados()
        {
            ViewBag.Projeto = new SelectList(configBUS.ListaProjeto(), "Projeto", "Projeto");
            ViewBag.Documento = new SelectList(configBUS.ListaDocumento(), "Tipo_documento", "Tipo_documento");
        }
    }
}