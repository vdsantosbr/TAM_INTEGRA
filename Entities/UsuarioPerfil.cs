using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsuarioPerfil
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id_Perfil { get; set; }

        [Required(ErrorMessageResourceName = "nomeVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(50, ErrorMessageResourceName = "nome50", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceName = "situacaoVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        public int IdSituacao { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        [Display(Name = "Visão global")]
        public bool VisaoGlobal { get; set; }

        public UsuarioPerfilSituacao UsuarioPerfilSituacao { get; set; }
      public int Id { get; set; }
        public string Perfil { get; set; }
    }
}
