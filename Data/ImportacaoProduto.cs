using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ImportacaoProduto
    {
        [Required(ErrorMessage = "Please enter how many Stream Entries are displayed per page.")]
        [Display(Name = "Tipo movimento")] 
        public string TipoMovimentoFiltro { get; set; }
        public string NumeroMovimentoFiltro { get; set;}
        public string CodigoFiltro { get; set; }
        public string NomeFantasiaFiltro { get; set; }
        public string NumNoFabric { get; set; }
        public string NumeroCCF { get; set; }
        public string NomeFantasia { get; set; }
        public string CodUndVenda { get; set; }
        public int Id_Integracao { get; set; }
        public string Movimento { get; set; }
        public List<ImportacaoProduto> lstProdutos { get; set; }
        public int  IdPrd { get; set; }
        public int idMov { get; set; }
    }
}
