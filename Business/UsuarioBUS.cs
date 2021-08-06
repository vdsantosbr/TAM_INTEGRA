using Data;
using Entities;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class UsuarioBUS
    {
        UsuarioDAL dal = null;
        UsuarioPerfilModuloDAL dalPerfilModulo = null;
        //0 = Erro na operação
        //1 = Sucesso
        //2 = Duplicado
        int retorno = 0;

        public UsuarioBUS()
        {
            dal = new UsuarioDAL();
            dalPerfilModulo = new UsuarioPerfilModuloDAL();
        }

        public List<Usuario> Lista()
        {
            List<Usuario> lst = dal.Lista()
                .OrderBy(obj => obj.Nome)
                .ToList();
            return lst;
        }

        //public List<Usuario> ListaVendedores()
        //{
        //    List<Usuario> lst = dal.Lista()
        //        .Where(obj => obj.Vendedor == true)
        //        .OrderBy(obj => obj.Nome)
        //        .ToList();
        //    return lst;
        //}

        public Usuario BuscaPorId(int id)
        {
            return dal.BuscaPorId(id);
        }

        public Usuario BuscaPorIdPessoa(int idPessoa)
        {
            return dal.BuscaPorIdPessoa(idPessoa);
        }

        public Usuario BuscaPorLogin(string login)
        {
            return dal.BuscaPorLogin(login);
        }

        //public Usuario BuscaPorIdPerfil(int idPerfil)
        //{
        //    return dal.BuscaPorIdPerfil(idPerfil);
        //}

        public string BuscaPorIdPerfil(int idPerfil)
        {
            string str = "";

            List<UsuarioPerfilModulo> lstUpm = dalPerfilModulo.BuscaPorIdPerfilPessoa(idPerfil);
            foreach (UsuarioPerfilModulo upm in lstUpm)
            {
                str += upm.Formulario + ",";
            }

            return str;
        }

        public string BuscaPorIdPerfilFormularios(int idPerfil)
        {
            string str = "";

            List<UsuarioPerfilModulo> lstUpm = dalPerfilModulo.BuscaPorIdPerfilPessoa(idPerfil);
            foreach (UsuarioPerfilModulo upm in lstUpm)
            {
                if(upm.Selecao == "true")
                {
                    str += upm.Formulario + ",";
                }
                
            }

            return str;
        }

        public List<UsuarioPerfilModulo> BuscaPorIdPerfilFormulariosList(int idPerfil)
        {
            List<UsuarioPerfilModulo> lstUpm = dalPerfilModulo.BuscaPorIdPerfilPessoa(idPerfil);

            return lstUpm;
        }

        public List<Usuario> BuscaPorArea(int idArea)
        {
            return dal.BuscaPorArea(idArea);
        }

        public Usuario BuscaPorDuplicidade(Usuario obj)
        {
            return dal.BuscaPorDuplicidade(obj);
        }

        public string BuscaModulos(int idPerfil)
        {
            string str = "";

            List<UsuarioPerfilModulo> lstUpm = dalPerfilModulo.BuscaPorId(idPerfil);
            foreach (UsuarioPerfilModulo upm in lstUpm)
            {
                str += upm.Modulo + ",";
            }

            return str;
        }

        public int Insere(Usuario obj, int idUsuarioAutor)
        {
            //Validação de duplicidade
            if (dal.BuscaPorDuplicidade(obj) != null)
            {
                retorno = 2;
            }
            else
            {
                if (dal.Insere(obj, idUsuarioAutor))
                {
                    retorno = 1;
                }
            }
            return retorno;
        }

        public int Atualiza(Usuario obj, int idUsuarioAutor)
        {
            //Validação de duplicidade
            if (dal.BuscaPorDuplicidade(obj) != null)
            {
                retorno = 2;
            }
            else
            {
                if (dal.Atualiza(obj, idUsuarioAutor))
                {
                    retorno = 1;
                }
            }
            return retorno;
        }

        public int Apaga(int id, int idUsuarioAutor)
        {
            if (dal.Apaga(id, idUsuarioAutor))
            {
                retorno = 1;
            }
            return retorno;
        }

        public Usuario ValidaAD(string login)
        {
            Usuario u;

            DirectoryEntry adsEntry = new DirectoryEntry("LDAP://tammrl.com.br");
            DirectorySearcher adsSearcher = new DirectorySearcher(adsEntry);
            adsSearcher.Filter = "(SAMAccountName=" + login + ")";
            try
            {
                //User has been authenticated by Active Directory.
                SearchResult adsSearchResult = adsSearcher.FindOne();
                u = new Usuario();
                u.Email = adsSearchResult.GetDirectoryEntry().Properties["mail"].Value.ToString();
                u.Nome = adsSearchResult.GetDirectoryEntry().Properties["givenName"].Value.ToString();
                u.Sobrenome = adsSearchResult.GetDirectoryEntry().Properties["sn"].Value.ToString();

                return u;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
            finally
            {
                adsEntry.Close();
                adsEntry = null;
                adsSearcher = null;
            }
        }

        public bool AutenticaAD(string login, string senha)
        {
            DirectoryEntry adsEntry = new DirectoryEntry("LDAP://tammrl.com.br", login, senha);
            DirectorySearcher adsSearcher = new DirectorySearcher(adsEntry);
            adsSearcher.Filter = "(sAMAccountName=" + login + ")";
            try
            {
                //User has been authenticated by Active Directory.
                SearchResult adsSearchResult = adsSearcher.FindOne();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                adsEntry.Close();
                adsEntry = null;
                adsSearcher = null;
            }
        }
        public List<Usuario> UsuarioAcesso(int id_pessoa = 0, int id_departamento = 0, string situacao = "")
        {
            List<Usuario> lst = new List<Usuario>();
            try
            {
                lst = dal.UsuarioAcesso(id_pessoa, id_departamento, situacao);
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
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
        public List<Departamento> Departamento()
        {
            List<Departamento> lst = new List<Departamento>();
            try
            {
                lst = dal.Departamento();
            }
            catch (Exception e)
            {
                var erro = e.Message;
            }
            return lst;
        }
        public bool AutenticaLogin(string login)
        {
            using (DirectorySearcher searcher = new DirectorySearcher("LDAP://tammrl.com.br"))
            {
                searcher.Filter = string.Format("(&(objectClass=user)(sAMAccountName={0}))", login);

                using (SearchResultCollection results = searcher.FindAll())
                {
                    if (results.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public List<Usuario> SalvarUsuario(int id_pessoa = 0, int id_perfil = 0, int id_departamento = 0, string login = "", string nome = "", string email = "",
            string telefone = "", string administrador = "", string situacao = "")
        {
            List<Usuario> lst = new List<Usuario>();
            try
            {
                lst = dal.SalvarUsuario(id_pessoa, id_perfil, id_departamento, login, nome, email, telefone, administrador, situacao);
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
    }
}
