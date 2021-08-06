using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsuarioPerfilModulo
    {
        [Key]
        [Required(ErrorMessage = "Selecione o módulo!")]
        public int IdModulo { get; set; }

        [Key]
        [Required(ErrorMessage = "Selecione o perfil!")]
        public int IdPerfil { get; set; }

        [Display(Name = "Perfil")]
        public string Perfil { get; set; }

        [Display(Name = "Módulo")]
        public string Modulo { get; set; }

        public int Id_Funcionalidade { get; set; }
        public string Formulario { get; set; }
        public int Id_Integracao_Processo { get; set; }
        public int Id_Fornecedor { get; set; }
        public string Funcionalidade { get; set; }
        public string processo { get; set; }
        public string Selecao { get; set; }

        public string Form_Principal { get; set; }
        public UsuarioPerfilModulo(int idPerfil, int idModulo)
        {
            IdModulo = idModulo;
            IdPerfil = idPerfil;
        }

        public UsuarioPerfilModulo()
        {

        }
    }
}
