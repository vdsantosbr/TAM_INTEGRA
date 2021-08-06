using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PerfilDAL
    {
        public List<Perfil> Perfil()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    SqlParameter pSituacao = new SqlParameter("@SITUACAO", "A");

                    var linha = db.Database.SqlQuery<Perfil>("EXEC STO_S_PERFIL @SITUACAO", pSituacao).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    return lst;
                }
            }
        }
        public List<Perfil> MenuInicializacao()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    var linha = db.Database.SqlQuery<Perfil>("EXEC sto_s_menu_inicializacao").ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    return lst;
                }
            }
        }
        public List<Perfil> SalvarPerfil(int id_pessoa = 0, int id_perfil = 0, string nome = "", string menu = "", string situacao = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id_pessoa);
                    SqlParameter pIdPerfil = new SqlParameter("@ID_PERFIL", id_perfil);
                    SqlParameter pNome = new SqlParameter("@NOME", nome);
                    SqlParameter pMenu = new SqlParameter("@FORM_PRINCIPAL", menu);
                    SqlParameter pSituacao = new SqlParameter("@SITUACAO", situacao);

                    if(id_perfil > 0)
                    {
                        var linha = db.Database.SqlQuery<Perfil>("EXEC STO_U_PERFIL @ID_PERFIL, @NOME, @SITUACAO, @ID_PESSOA",
                        pIdPerfil, pNome, pSituacao, pIdPessoa).ToList();

                        if (linha.Count > 0)
                        {
                            return linha;
                        }
                        else
                        {
                            return lst;
                        }
                    }
                    else
                    {
                        var linha = db.Database.SqlQuery<Perfil>("EXEC STO_I_PERFIL @ID_PERFIL, @NOME, @FORM_PRINCIPAL, @SITUACAO, @ID_PESSOA",
                        pIdPerfil, pNome, pMenu, pSituacao, pIdPessoa).ToList();

                        if (linha.Count > 0)
                        {
                            return linha;
                        }
                        else
                        {
                            return lst;
                        }
                    }
                }
                catch (Exception e)
                {
                    lst.Add(new Perfil
                    {
                        Mensagem = e.Message
                    });

                    return lst;
                }
            }
        }
        public List<Perfil> Parametrizacao(int id_perfil = 0, string controllerName = "", string adm = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    SqlParameter pIdPerfil = new SqlParameter("@ID_PERFIL", id_perfil);

                        var linha = db.Database.SqlQuery<Perfil>("EXEC STO_S_PERFIL_FUNCIONALIDADE @ID_PERFIL",
                        pIdPerfil).ToList();

                        if (linha.Count > 0)
                        {
                            return linha;
                        }
                        else
                        {
                            return lst;
                        }
                }
                catch (Exception e)
                {
                    lst.Add(new Perfil
                    {
                        Mensagem = e.Message
                    });

                    return lst;
                }
            }
        }
        public List<Perfil> AddPerfil(int id_perfil = 0, int id_pessoa = 0)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    SqlParameter pIdPerfil = new SqlParameter("@ID_PERFIL", id_perfil);
                    SqlParameter pIdPessoa = new SqlParameter("@ID_PESSOA", id_perfil);

                    var linha = db.Database.SqlQuery<Perfil>("EXEC STO_D_PERFIL_FUNCIONALIDADE @ID_PERFIL, @ID_PESSOA",
                    pIdPerfil, pIdPessoa).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    lst.Add(new Perfil
                    {
                        Mensagem = e.Message
                   });

                    return lst;
                }
            }
        }
        public List<Perfil> AddPerfilFuncionalidade(int id_perfil = 0, int id_funcionalidade = 0, string permitir_consultar = "", string permitir_editar = "", string permitir_exportar = "")
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                List<Perfil> lst = new List<Perfil>();
                try
                {
                    SqlParameter pIdPerfil = new SqlParameter("@ID_PERFIL", id_perfil);
                    SqlParameter pIdFuncionalidade = new SqlParameter("@ID_FUNCIONALIDADE ", id_funcionalidade);
                    SqlParameter pPermitir_Consultar = new SqlParameter("@PERMITIR_CONSULTAR", permitir_consultar);
                    SqlParameter pPermitir_Editar = new SqlParameter("@PERMITIR_EDITAR", permitir_editar);
                    SqlParameter pPermitir_Exportar = new SqlParameter("@PERMITIR_EXPORTAR", permitir_exportar);

                    var linha = db.Database.SqlQuery<Perfil>("EXEC STO_I_PERFIL_PARAMETROS @ID_PERFIL, @ID_FUNCIONALIDADE, @PERMITIR_CONSULTAR, @PERMITIR_EDITAR, @PERMITIR_EXPORTAR",
                    pIdPerfil, pIdFuncionalidade, pPermitir_Consultar, pPermitir_Editar, pPermitir_Exportar).ToList();

                    if (linha.Count > 0)
                    {
                        return linha;
                    }
                    else
                    {
                        return lst;
                    }
                }
                catch (Exception e)
                {
                    lst.Add(new Perfil
                    {
                        Mensagem = e.Message
                    });

                    return lst;
                }
            }
        }
    }
}
