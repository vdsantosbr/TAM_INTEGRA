using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DiaDaSemana
    {
        [Key]
        public int Id { get; set; }

        public string Dia { get; set; }

        public int Ordem { get; set; }
    }
}
