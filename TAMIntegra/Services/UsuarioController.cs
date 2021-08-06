using Business;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TAMIntegra.Services
{
    [RoutePrefix("Api/Usuario")]
    public class UsuarioController : ApiController
    {
        private UsuarioBUS usuarioBUS = new UsuarioBUS();

        [HttpGet]
        [Route("Login")]
        public Usuario Login(string login = null, string senha = null)
        {
            Usuario usrObj = new Usuario();

            if (login != null && senha != null)
            {
                Usuario usr = usuarioBUS.BuscaPorLogin(login.Trim());
                if (usr != null)
                {
                    if (usuarioBUS.AutenticaAD(login.Trim(), senha.Trim()))
                    {
                        usrObj = usr;
                    }
                }
            }

            return usrObj;
        }
    }
}
