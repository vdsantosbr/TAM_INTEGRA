using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Integracao
    {
        public int Id_integracao_Processo {get; set;}
        public int Id_integracao { get; set; }
        public int IdMov { get; set; }
        public int  Id_Pessoa { get; set; }
        public string Complemento { get; set; }
        public string Tipo { get; set; }
        public string Situacao { get; set; }
        public string Observacao { get; set; }
        public string PERMITE_EDICAO { get; set; }
        public string PERMITE_EXCLUSAO { get; set; }
        public string Qualificacao { get; set; }
        public string Nome { get; set; }
        public int Chave { get; set; }
        public string Serial { get; set; }

        public Integracao()
        {
            this.Id_integracao = 0;
            this.Id_integracao_Processo = 0;
            this.Id_Pessoa = 0;
            this.Complemento = "";
            this.Tipo = "";
            this.Situacao = "";
            this.Observacao = "";
            this.IdMov = 0;
            this.Chave = 0;
            this.Serial = "";
        }


    }
}
