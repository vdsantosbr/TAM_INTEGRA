using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsuarioArea
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "nomeVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(50, ErrorMessageResourceName = "nome50", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Nome { get; set; }

        [StringLength(maximumLength: 6, MinimumLength = 6, ErrorMessageResourceName = "cor6", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Cor { get; set; }

        [Display(Name = "Administrativa")]
        public bool Administrativa { get; set; }
    }
}
