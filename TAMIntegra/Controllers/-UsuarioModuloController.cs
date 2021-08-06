using Data;
using Business;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;

namespace TAMIntegra.Controllers
{
    [CustomAuthorize(Roles = "Configuracao-Modulo")]
    public class UsuarioModuloController : BaseController
    {
        private DatabaseContext db = new DatabaseContext();
        private UsuarioModuloBUS usuarioModuloBUS = new UsuarioModuloBUS();
        int retorno = 0;

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(string nome, string idSituacao)
        {
            int filtro = 0;

            try
            {
                CarregaDados();
                IEnumerable<UsuarioModulo> result = usuarioModuloBUS.Lista();
                if (!string.IsNullOrEmpty(nome))
                {
                    result = result.Where(s => s.Nome.ToUpper().Contains(nome.ToUpper()));
                    filtro = 1;
                }
                if (!string.IsNullOrEmpty(idSituacao))
                {
                    result = result.Where(s => s.IdSituacao == Convert.ToInt16(idSituacao));
                    filtro = 1;
                }

                if (filtro == 0)
                {
                    result = result.Take(10);
                }

                return View(result);
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }

        }

        [CustomAuthorize(Roles = "Configuracao-Modulo-Admin")]
        [HttpGet]
        public ActionResult Admin(int? id)
        {
            int idObj;
            UsuarioModulo linha;

            try
            {
                CarregaDados();

                if (id == null)
                {
                    return View();
                }
                else
                {
                    idObj = (int)id;

                    linha = usuarioModuloBUS.BuscaPorId(idObj);

                    if (linha != null)
                    {
                        return View(linha);
                    }
                    else
                    {
                        TempData["Mensagem"] = "Nenhum registro encontrado!";
                        return RedirectToAction("Admin");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [CustomAuthorize(Roles = "Configuracao-Modulo-Admin")]
        [HttpPost]
        public ActionResult Admin(UsuarioModulo obj)
        {
            try
            {
                CarregaDados();

                if (obj.Id == 0)
                {
                    ModelState.Remove("Id");
                }

                if (ModelState.IsValid)
                {
                    if (obj.Id <= 0)
                    {
                        retorno = usuarioModuloBUS.Insere(obj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:
                                TempData["Mensagem"] = "Registro inserido com sucesso!";
                                TempData["FechaPopUp"] = 1;
                                break;
                            case 2:
                                TempData["Mensagem"] = "J\u00e1 existe um registro com as mesmas características!";
                                break;
                        }
                    }
                    else
                    {
                        retorno = usuarioModuloBUS.Atualiza(obj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:
                                TempData["Mensagem"] = "Registro alterado com sucesso!";
                                TempData["FechaPopUp"] = 1;
                                break;
                            case 2:
                                TempData["Mensagem"] = "Já existe um registro com as mesmas características!";
                                break;
                        }
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Admin");
            }
        }

        [CustomAuthorize(Roles = "Configuracao-Modulo-Admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int idObj;
            UsuarioModulo linha;

            try
            {
                if (id != null)
                {
                    idObj = (int)id;

                    linha = usuarioModuloBUS.BuscaPorId(idObj);

                    if (linha != null)
                    {
                        retorno = usuarioModuloBUS.Apaga(idObj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:
                                TempData["Mensagem"] = "Registro removido com sucesso!";
                                break;
                        }
                    }
                    else
                    {
                        TempData["Mensagem"] = "Nenhum registro encontrado!";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        private void CarregaDados()
        {
            ViewBag.UsuarioModuloSituacao = new SelectList(db.UsuarioModuloSituacoes, "Id", "Nome");
        }
    }
}