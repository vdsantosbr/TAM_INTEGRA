using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ImportacaoConciliacao
    {
        public int Id_Conciliacao { get; set; }
        public int Id_Statement { get; set; }
        public int Id_RM_Fluxus { get; set; }
        public int Id_Pessoa { get; set; }
        public DateTime Data_Conciliacao { get; set; }
        public DateTime Data_Base { get; set; }
        public int Qtd_Registro { get; set; }
        public decimal Amount { get; set; }
        public DateTime Imagem_RM { get; set; }
        public string Tipo { get; set; }
        public string Processamento { get; set; }
        public string Situacao { get; set; }
        public int Id_Conciliacao_Item { get; set; }
        public int Id_Statement_Detail { get; set; }
        public int Id_Situacao { get; set; }
        public int Id_Classificacao { get; set; }
        public int Id_Departamento { get; set; }
        public string Tipo_Processo { get; set; }
        public string Deposito_Especial { get; set; }
        public string Canal { get; set; }
        public string NF_Observacao { get; set; }
        public decimal RM_Valor { get; set; }
        public decimal Diferenca { get; set; }
        public int Id_Pesooa { get; set; }
        public DateTime Data_Atualizacao { get; set; }
        public string Situacao_Analise { get; set; }
        public string Situacao_Conciliacao { get; set; }
        public string Conta { get; set; }
        public List<ImportacaoConciliacao> lstConcilicacao { get; set; }
    }
}
