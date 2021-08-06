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
    [CustomAuthorize(Roles = "Configuracao-Perfil-Situacao")]
    public class UsuarioPerfilSituacaoController : BaseController
    {
        private UsuarioPerfilSituacaoBUS UsuarioPerfilSituacaoBUS = new UsuarioPerfilSituacaoBUS();
        int retorno = 0;

        // GET: UsuarioPerfilSituacao
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View(this.UsuarioPerfilSituacaoBUS.Lista().ToList());
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }
        }

        [CustomAuthorize(Roles = "Configuracao-Perfil-Situacao-Admin")]
        [HttpGet]
        public ActionResult Admin(int? id)
        {
            int idObj;
            UsuarioPerfilSituacao linha;

            try
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    idObj = (int)id;

                    linha = UsuarioPerfilSituacaoBUS.BuscaPorId(idObj);

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

        [CustomAuthorize(Roles = "Configuracao-Perfil-Situacao-Admin")]
        [HttpPost]
        public ActionResult Admin(UsuarioPerfilSituacao obj)
        {
            try
            {
                if (obj.Id == 0)
                {
                    ModelState.Remove("Id");
                }

                if (ModelState.IsValid)
                {
                    if (obj.Id <= 0)
                    {
                        retorno = UsuarioPerfilSituacaoBUS.Insere(obj, Convert.ToInt32(User.Identity.Name));
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
                        retorno = UsuarioPerfilSituacaoBUS.Atualiza(obj, Convert.ToInt32(User.Identity.Name));
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

        [CustomAuthorize(Roles = "Configuracao-Perfil-Situacao-Admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int idObj;
            UsuarioPerfilSituacao linha;

            try
            {
                if (id != null)
                {
                    idObj = (int)id;

                    linha = UsuarioPerfilSituacaoBUS.BuscaPorId(idObj);

                    if (linha != null)
                    {
                        retorno = UsuarioPerfilSituacaoBUS.Apaga(idObj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:
                                TempData["Mensagem"] = "Registro removido com sucesso!";
                                break;
                            case 3:
                                TempData["Mensagem"] = "Há perfi(s) vinculado(s)! Verifique.";
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
    }
}