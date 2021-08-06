using Business;
using Data;
using Entities;
using System;
using System.Web.Mvc;
using System.DirectoryServices;
using TAMIntegra.App_Start;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using ClosedXML.Excel;
using ExcelDataReader;
using System.IO;

namespace TAMIntegra.Controllers
{
    //[CustomAuthorize(Roles = "Configuracao-Usuario")]
    public class UsuarioController : BaseController
    {
        private DatabaseContext db = new DatabaseContext();
        private Usuario usuario = new Usuario();
        private UsuarioBUS usuarioBUS = new UsuarioBUS();
        private UsuarioSituacaoBUS usBUS = new UsuarioSituacaoBUS();
        private UsuarioPerfilBUS usuarioPerfilBUS = new UsuarioPerfilBUS();
        private UsuarioAreaBUS uaBUS = new UsuarioAreaBUS();

        [CustomAuthorize(Roles = "frmSeguranca")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(int id_pessoa = 0, int id_departamento = 0, string situacao = "")
        {
            try
            {
                CarregaDados();
                string user = User.Identity.Name;
                usuario.lstUsuario = usuarioBUS.UsuarioAcesso(id_pessoa, id_departamento, situacao).OrderBy(x => x.Nome).ToList();
                Session["lstDados"] = usuario.lstUsuario;

                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult ValidaAD(Usuario obj)
        {
            if (usuarioBUS.ValidaAD(obj.Login) == null)
            {
                TempData["Mensagem"] = "Usuário inválido!";
            }
            else
            {
                TempData["Mensagem"] = "Usuário válido!";
            }

            return View();
        }

        public ActionResult salvarUsuario(int id_pessoa = 0, int id_perfil = 0, int id_departamento = 0, string login = "", string nome = "", string email = "", 
            string administrador = "", string situacao = "")
        {
            List<Usuario> lst = new List<Usuario>();
            string login_fmt = login.Replace("“@tamexecutiva.com.br", "");
            bool autenticaLogin = usuarioBUS.AutenticaLogin(login_fmt);

            if (autenticaLogin == true)
            {
                lst = usuarioBUS.SalvarUsuario(id_pessoa, id_perfil, id_departamento, login, nome, email, "", administrador, situacao);
            }
            else
            {
                lst.Add(new Usuario
                {
                    Mensagem = "Login inválido!"
                });
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        private void CarregaDados()
        {
            List<Perfil> lstPerfil = new List<Perfil>();
            lstPerfil = usuarioBUS.Perfil().ToList();
            ViewBag.Perfil = new SelectList(lstPerfil, "id_perfil", "nome");

            List<Departamento> lstDepartamento = new List<Departamento>();
            lstDepartamento = usuarioBUS.Departamento().ToList();
            ViewBag.Departamento = new SelectList(lstDepartamento, "id_departamento", "nome");
        }
        public FileResult DownloadExcel(string strDataInicio, string strDataFim, string pedido = "", string aplicacao = "", string pn = "", string invoice = "", string conhecimento = "", string status_compra = "", string status_pedido = "", string processo = "")
        {
            string arquivo = "Usuarios";
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Start a new workbook
                    var wb = new XLWorkbook();

                    List<Usuario> lst = (List<Usuario>)Session["lstDados"];

                    IEnumerable<Usuario> result = lst;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("NOME COMPLETO");
                    dt.Columns.Add("LOGIN");
                    dt.Columns.Add("ADMINISTRADOR");
                    dt.Columns.Add("SITUAÇÃO");
                    dt.Columns.Add("DEPARTAMENTO");
                    dt.Columns.Add("PERFIL");

                    foreach (var res in result)
                    {
                        DataRow dr = dt.NewRow();
                        dr["NOME COMPLETO"] = res.Nome;
                        dr["LOGIN"] = res.Login;
                        dr["ADMINISTRADOR"] = res.Administrador;
                        dr["SITUAÇÃO"] = res.Situacao;
                        dr["DEPARTAMENTO"] = res.Departamento;
                        dr["PERFIL"] = res.Perfil;

                        dt.Rows.Add(dr);
                    }

                    // Add a DataTable as a worksheet
                    wb.Worksheets.Add(dt, arquivo);

                    wb.SaveAs(stream, false);

                    // Return a byte array
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", arquivo + "_" + DateTime.Now.ToString("yyyyddMHHmmss") + ".xlsx");
                    //return File(@"C:\Users\itala.cordeiro\Downloads", "application /text", "teste" + ".xlsx");
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}