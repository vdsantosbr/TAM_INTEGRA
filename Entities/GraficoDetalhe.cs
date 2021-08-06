using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class GraficoDetalhe
    {
        public string Usuario { get; set; }
        public double Valor { get; set; }

        public GraficoDetalhe(string usuario, double valor)
        {
            Usuario = usuario;
            Valor = valor;
        }

    }
}
