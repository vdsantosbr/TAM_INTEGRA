using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Entities
{
    public class Entidade
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required(ErrorMessageResourceName = "pessoaVazio", ErrorMessageResourceType = typeof(ErrorMsg))]

        public int? idEstado { get; set; }

        public int idTipo { get; set; }

    }
}
