using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("UsuarioModulo")]
    public class UsuarioModulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "nomeVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(100, ErrorMessageResourceName = "nome100", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceName = "situacaoVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        public int IdSituacao { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }
    }
}
