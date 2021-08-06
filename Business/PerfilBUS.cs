using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Business
{
    public class PerfilBUS
    {
        PerfilDAL dal = new PerfilDAL();

        public List<Perfil> Perfil()
        {
            List<Perfil> lst = new List<Perfil>();
            try
            {
                lst = dal.Perfil();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Perfil> MenuInicializacao()
        {
            List<Perfil> lst = new List<Perfil>();
            try
            {
                lst = dal.MenuInicializacao();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Perfil> SalvarPerfil(int id_pessoa = 0, int id_perfil = 0, string nome = "", string menu = "", string situacao = "")
        {
            List<Perfil> lst = new List<Perfil>();
            try
            {
                lst = dal.SalvarPerfil(id_pessoa, id_perfil, nome, menu, situacao);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Perfil> Parametrizacao(int id_perfil = 0)
        {
            List<Perfil> lst = new List<Perfil>();
            try
            {
                lst = dal.Parametrizacao(id_perfil);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public string ParametrizacaoAutorizar(int id_perfil = 0, string controllerName = "", string adm = "")
        {
            string str = "";

            List<Perfil> lstUpm = dal.Parametrizacao(id_perfil, controllerName, adm);
            foreach (Perfil upm in lstUpm)
            {
                if (upm.Permitir_Consultar == "S"|| upm.Permitir_Consultar == "SIM")
                {
                    if(upm.Formulario == "frmContabilizacaoVC")
                    {
                        if (upm.Permitir_Editar == "S" || upm.Permitir_Editar == "SIM")
                        {
                            str += "frmContabilizacaoVCEditar" + ",";
                            str += upm.Formulario + ",";
                        }
                        else
                        {
                            str += upm.Formulario + ",";
                        }
                    }
                    else if (upm.Formulario == "frmImportacaoProduto")
                    {
                        if (upm.Permitir_Editar == "S" || upm.Permitir_Editar == "SIM")
                        {
                            str += "frmImportacaoProdutoEditar" + ",";
                            str += upm.Formulario + ",";
                        }
                        //else
                        //{
                        //    str += upm.Formulario + ",";
                        //}
                    }
                    else if (upm.Formulario == "frmImportacaoFornecedor")
                    {
                        if (upm.Permitir_Editar == "S" || upm.Permitir_Editar == "SIM")
                        {
                            str += "frmImportacaoFornecedorEditar" + ",";
                            str += upm.Formulario + ",";
                        }
                        //else
                        //{
                        //    str += upm.Formulario + ",";
                        //}
                    }
                    else if (upm.Formulario == "frmImportacaoDocumento")
                    {
                        if (upm.Permitir_Editar == "S" || upm.Permitir_Editar == "SIM")
                        {
                            str += "frmImportacaoDocumentoEditar" + ",";
                            str += upm.Formulario + ",";
                        }
                        //else
                        //{
                        //    str += upm.Formulario + ",";
                        //}
                    }
                    else if (upm.Formulario == "frmImportacaoNota")
                    {
                        if (upm.Permitir_Editar == "S" || upm.Permitir_Editar == "SIM")
                        {
                            str += "frmImportacaoNotaEditar" + ",";
                            str += upm.Formulario + ",";
                        }
                        //else
                        //{
                        //    str += upm.Formulario + ",";
                        //}
                    }
                    else if (upm.Formulario == "frmSeguranca")
                    {
                        if (adm.ToUpper() == "SIM")
                        {
                            str += upm.Formulario + ",";
                        }
                    }
                    else
                    {
                        str += upm.Formulario + ",";
                    }
                }

            }

            return str;
        }
        public List<Perfil> AddPerfil(int id_perfil = 0, int id_pessoa = 0)
        {
            List<Perfil> lst = new List<Perfil>();
            try
            {
                lst = dal.AddPerfil(id_perfil, id_pessoa);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public List<Perfil> AddPerfilFuncionalidade(int id_perfil = 0, int id_funcionalidade = 0, string permitir_consultar = "", string permitir_editar = "", string permitir_exportar = "")
        {
            List<Perfil> lst = new List<Perfil>();
            try
            {
                lst = dal.AddPerfilFuncionalidade(id_perfil, id_funcionalidade, permitir_consultar, permitir_editar, permitir_exportar);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
    }
}
