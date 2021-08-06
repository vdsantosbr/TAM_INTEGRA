using Business;
using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAMIntegra.App_Start;

namespace TAMIntegra.Controllers
{
    [CustomAuthorize(Roles = "Configuracao-Perfil")]
    public class UsuarioPerfilController : BaseController
    {
        private DatabaseContext db = new DatabaseContext();
        private UsuarioPerfilBUS usuarioPerfilBUS = new UsuarioPerfilBUS();
        private UsuarioPerfilSituacaoBUS upsBUS = new UsuarioPerfilSituacaoBUS();
        private UsuarioModuloBUS usuarioModuloBUS = new UsuarioModuloBUS();
        private UsuarioPerfilModuloBUS usuarioPerfilModuloBUS = new UsuarioPerfilModuloBUS();
        int retorno = 0;

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View(this.usuarioPerfilBUS.Lista());
            }
            catch (Exception ex)
            {
                TempData["Mensagem"] = ex.Message;
                return View();
            }

        }

        [CustomAuthorize(Roles = "Configuracao-Perfil-Admin")]
        [HttpGet]
        public ActionResult Admin(int? id)
        {
            int idObj;
            UsuarioPerfil linha;
            List<UsuarioModulo> lstUsuarioModulo;
            List<UsuarioPerfilModulo> lstUsuarioPerfilModulo;

            try
            {
                ViewBag.UsuarioPerfilSituacao = new SelectList(db.UsuarioPerfilSituacoes, "Id", "Nome");
                ViewBag.UsuarioModulo = new MultiSelectList(usuarioModuloBUS.Lista(), "Id", "Nome");

                if (id == null)
                {
                    ViewBag.UsuarioPerfilModulo = new MultiSelectList(new List<UsuarioPerfilModulo>(), "IdModulo", "Modulo");

                    return View();
                }
                else
                {
                    idObj = (int)id;

                    lstUsuarioModulo = usuarioModuloBUS.Lista();
                    lstUsuarioPerfilModulo = usuarioPerfilModuloBUS.BuscaPorId(idObj);

                    usuarioPerfilBUS.RemoveModulosExistentes(ref lstUsuarioModulo, lstUsuarioPerfilModulo);

                    ViewBag.UsuarioModulo = new MultiSelectList(lstUsuarioModulo, "Id", "Nome");
                    ViewBag.UsuarioPerfilModulo = new MultiSelectList(lstUsuarioPerfilModulo, "IdModulo", "Modulo");
                    linha = usuarioPerfilBUS.BuscaPorId(idObj);

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
                return View();
            }
        }

        [CustomAuthorize(Roles = "Configuracao-Perfil-Admin")]
        [HttpPost]
        public ActionResult Admin(UsuarioPerfil obj, FormCollection form)
        {
            List<UsuarioModulo> lstUsuarioModulo;
            List<UsuarioPerfilModulo> lstUsuarioPerfilModulo;

            lstUsuarioModulo = usuarioModuloBUS.Lista();
            lstUsuarioPerfilModulo = usuarioPerfilModuloBUS.BuscaPorId(obj.Id_Perfil);

            try
            {
                ViewBag.UsuarioPerfilSituacao = new SelectList(db.UsuarioPerfilSituacoes, "Id", "Nome");
                ViewBag.UsuarioModulo = new MultiSelectList(lstUsuarioModulo, "Id", "Nome");

                if (obj.Id_Perfil == 0)
                {
                    ModelState.Remove("Id");
                    ViewBag.UsuarioPerfilModulo = new MultiSelectList(new List<UsuarioPerfilModulo>(), "IdModulo", "Modulo");
                }else
                {
                    usuarioPerfilBUS.RemoveModulosExistentes(ref lstUsuarioModulo, lstUsuarioPerfilModulo);
                    ViewBag.UsuarioPerfilModulo = new MultiSelectList(usuarioPerfilModuloBUS.BuscaPorId(obj.Id_Perfil), "IdModulo", "Modulo");
                }

                if (ModelState.IsValid)
                {
                    if (obj.Id_Perfil <= 0)
                    {
                        retorno = usuarioPerfilBUS.Insere(obj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            default:
                                string[] selitems = form.GetValues("IdModulo");
                                if(selitems != null)
                                {
                                    foreach (string item in selitems)
                                    {
                                        usuarioPerfilModuloBUS.Insere(new UsuarioPerfilModulo(retorno, Convert.ToInt32(item)), Convert.ToInt32(User.Identity.Name));
                                    }
                                }                                

                                TempData["Mensagem"] = "Registro inserido com sucesso!";
                                TempData["FechaPopUp"] = 1;
                                break;
                        }
                    }
                    else
                    {
                        retorno = usuarioPerfilBUS.Atualiza(obj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:

                                usuarioPerfilModuloBUS.ApagaModulosDoPerfil(obj.Id_Perfil, Convert.ToInt32(User.Identity.Name));
                                string[] selitems = form.GetValues("IdModulo");
                                if (selitems != null)
                                {
                                    foreach (string item in selitems)
                                    {
                                        usuarioPerfilModuloBUS.Insere(new UsuarioPerfilModulo(obj.Id_Perfil, Convert.ToInt32(item)), Convert.ToInt32(User.Identity.Name));
                                    }
                                }

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

        [CustomAuthorize(Roles = "Configuracao-Perfil-Admin")]
        [HttpGet]
        public ActionResult Inativa(int? id)
        {
            int idObj;
            UsuarioPerfil linha;

            try
            {
                if (id != null)
                {
                    idObj = (int)id;

                    linha = usuarioPerfilBUS.BuscaPorId(idObj);

                    if (linha != null)
                    {
                        linha.IdSituacao = upsBUS.Lista().Where(a => a.Nome == "Inativo").First().Id;

                        retorno = usuarioPerfilBUS.Atualiza(linha, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:
                                TempData["Mensagem"] = "Registro inativado com sucesso!";
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

        [CustomAuthorize(Roles = "Configuracao-Perfil-Admin")]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            int idObj;
            UsuarioPerfil linha;

            try
            {
                if (id != null)
                {
                    idObj = (int)id;

                    linha = usuarioPerfilBUS.BuscaPorId(idObj);

                    if (linha != null)
                    {                        
                        usuarioPerfilModuloBUS.ApagaModulosDoPerfil(idObj, Convert.ToInt32(User.Identity.Name));

                        retorno = usuarioPerfilBUS.Apaga(idObj, Convert.ToInt32(User.Identity.Name));
                        switch (retorno)
                        {
                            case 0:
                                TempData["Mensagem"] = "Erro na operação!";
                                break;
                            case 1:
                                TempData["Mensagem"] = "Registro removido com sucesso!";
                                break;
                            case 3:
                                TempData["Mensagem"] = "Há usuário(s) vinculado(s)! Verifique.";
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