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
    [CustomAuthorize(Roles = "Configuracao-Usuario-Area")]
    public class UsuarioAreaController : BaseController
    {
        private UsuarioAreaBUS uaBUS = new UsuarioAreaBUS();
        int retorno = 0;

        // GET: UsuarioArea
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View(this.uaBUS.Lista().ToList());
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }
        }

        [CustomAuthorize(Roles = "Configuracao-Usuario-Area-Admin")]
        [HttpGet]
        public ActionResult Admin(int? id)
        {
            int idObj;
            UsuarioArea linha;

            try
            {
                if (id == null)
                {
                    return View();
                }
                else
                {
                    idObj = (int)id;

                    linha = uaBUS.BuscaPorId(idObj);

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

        [CustomAuthorize(Roles = "Configuracao-Usuario-Area-Admin")]
        [HttpPost]
        public ActionResult Admin(UsuarioArea obj)
        {
            try
            {
                if (obj.Id == 0)
                {
                    ModelState.Remove("Id");
                }

                if(obj.Cor != null)
                {
                    obj.Cor = obj.Cor.Replace("#", "");
                    ModelState.Clear();
                    TryValidateModel(obj);
                }

                if (ModelState.IsValid)
                {
                    if (obj.Id <= 0)
                    {
                        retorno = uaBUS.Insere(obj, Convert.ToInt32(User.Identity.Name));
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
                        retorno = uaBUS.Atualiza(obj, Convert.ToInt32(User.Identity.Name));
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

        [CustomAuthorize(Roles = "Configuracao-Usuario-Area-Admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int idObj;
            UsuarioArea linha;

            try
            {
                if (id != null)
                {
                    idObj = (int)id;

                    linha = uaBUS.BuscaPorId(idObj);

                    if (linha != null)
                    {
                        retorno = uaBUS.Apaga(idObj, Convert.ToInt32(User.Identity.Name));
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