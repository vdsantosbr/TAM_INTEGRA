using Business;
using Entities;
using System;
using System.Linq;
using System.Web.Mvc;
using TAMIntegra.App_Start;

namespace TAMIntegra.Controllers
{
    [CustomAuthorize(Roles = "Configuracao-Modulo-Situacao")]
    public class UsuarioModuloSituacaoController : BaseController
    {
        private UsuarioModuloSituacaoBUS usuarioModuloSituacaoBUS = new UsuarioModuloSituacaoBUS();
        int retorno = 0;

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View(this.usuarioModuloSituacaoBUS.Lista().ToList());
            }
            catch(Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }            
        }

        [CustomAuthorize(Roles = "Configuracao-Modulo-Situacao-Admin")]
        [HttpGet]
        public ActionResult Admin(int? id)
        {
            int idObj;
            UsuarioModuloSituacao linha;

            try
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    idObj = (int)id;

                    linha = usuarioModuloSituacaoBUS.BuscaPorId(idObj);

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

        [CustomAuthorize(Roles = "Configuracao-Modulo-Situacao-Admin")]
        [HttpPost]
        public ActionResult Admin(UsuarioModuloSituacao obj)
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
                        retorno = usuarioModuloSituacaoBUS.Insere(obj, Convert.ToInt32(User.Identity.Name));
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
                        retorno = usuarioModuloSituacaoBUS.Atualiza(obj, Convert.ToInt32(User.Identity.Name));
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

        [CustomAuthorize(Roles = "Configuracao-Modulo-Situacao-Admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int idObj;
            UsuarioModuloSituacao linha;

            try
            {
                if (id != null)
                {
                    idObj = (int)id;

                    linha = usuarioModuloSituacaoBUS.BuscaPorId(idObj);

                    if (linha != null)
                    {
                        retorno = usuarioModuloSituacaoBUS.Apaga(idObj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:
                                TempData["Mensagem"] = "Registro removido com sucesso!";
                                break;
                            case 3:
                                TempData["Mensagem"] = "Há módulo(s) vinculado(s)! Verifique.";
                                break;
                        }   
                    }else
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