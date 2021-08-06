using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Perfil
    {
        public int Id_Perfil { get; set; }
        public int Id_Funcionalidade { get; set; }
        public string Nome { get; set; }
        public string Situacao { get; set; }
        public string Formulario { get; set; }
        public string Mensagem { get; set; }
        public string Form_Principal { get; set; }
        public string Permitir_Consultar { get; set; }
        public string Permitir_Editar { get; set; }
        public string Permitir_Exportar { get; set; }
        public string Descricao { get; set; }
        public List<Perfil> lstPerfil { get; set; }
        public List<Perfil> lstPerfilParametro { get; set; }
        public List<Perfil> lstPerfilParametrizacao { get; set; }
    }
}
