using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_Pessoa { get; set; }

        [Required(ErrorMessageResourceName = "sobrenomeVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(50, ErrorMessageResourceName = "sobrenome50", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Sobrenome { get; set; }

        [Required(ErrorMessageResourceName = "loginVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(50, ErrorMessageResourceName = "login50", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Login { get; set; }

        

        //public bool Vendedor { get; set; }

        [Required(ErrorMessageResourceName = "areaVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        public int? IdArea { get; set; }

        

        [Required(ErrorMessageResourceName = "situacaoVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        public int IdSituacao { get; set; }

        public string Perfil { get; set; }

        [Display(Name = "Área")]
        public string Area { get; set; }

        [Display(Name = "Situação")]
        public string Situacao { get; set; }

        public UsuarioArea AreaObj { get; set; }

        public UsuarioPerfil PerfilObj { get; set; }

        public int IdPessoa { get; set; }
        public int IdDpepartamento { get; set; }

        [Required(ErrorMessageResourceName = "perfilVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        public int IdPerfil { get; set; }
        public int Id_Perfil { get; set; }
        [Required(ErrorMessageResourceName = "nomeVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(50, ErrorMessageResourceName = "nome50", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceName = "emailVazio", ErrorMessageResourceType = typeof(ErrorMsg))]
        [StringLength(100, ErrorMessageResourceName = "email100", ErrorMessageResourceType = typeof(ErrorMsg))]
        public string Email { get; set; }
        public string Telefone { get; set; }
        public char ExibirBarraFerramenta { get; set; }
        public char ExibirBarraStatus { get; set; }
        public char Admistrador { get; set; }
        public char SituacaoPessoa { get; set; }
        public bool Exporta_Excel { get; set; }
        public bool Permite_Edicao { get; set; }
        public List<Usuario> lstUsuario { get; set; }
        public string Mensagem { get; set; }
        public string Administrador { get; set; }
        public int Id_Departamento { get; set; }
        public string Departamento { get; set; }
        public string Formulario { get; set; }
        public List<Perfil> lstPerfil { get; set; }
    }
}
