using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsuarioPerfilSituacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "nomeVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(50, ErrorMessageResourceName = "nome50", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Nome { get; set; }

        public virtual ICollection<UsuarioPerfil> UsuarioPerfis { get; set; }
    }
}
