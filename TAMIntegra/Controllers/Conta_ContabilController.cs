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
    public class Conta_ContabilController : Controller
    { 
    private ContaContabil cont = new ContaContabil();
    private ContaContabilBUS contBUS = new ContaContabilBUS();
    private UsuarioBUS usuarioBUS = new UsuarioBUS();

    [CustomAuthorize(Roles = "frmContabilizacaoVCEditar")]
        public ActionResult Index(string exibir = "", int ano = 0)
        {
            ano = DateTime.Now.Year;

            List<Conta> lstGRid = new List<Conta>();
            List<Conta> lstContas = new List<Conta>();

            //lstGRid = contBUS.Exibir();

            lstContas = contBUS.Contas();
            //lstGRid = lstContas.Where(x => x.ID_CONTACONTABIL == 18).ToList();

            //cont.lstFechamentoAtual = lstGRid;
            cont.lstContas = lstContas.OrderBy(x => x.ID_CONTACONTABIL).ToList();

            return View(cont);
        }
        public ActionResult Salvar(int id_conta = 0, string cod_conta = "", int id_pessoa = 0)
        {
            List<Conta> lst = new List<Conta>();
            Usuario usuario = usuarioBUS.BuscaPorLogin(User.Identity.Name);

            lst = contBUS.SalvarConta(id_conta, cod_conta, usuario.Id_Pessoa);

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