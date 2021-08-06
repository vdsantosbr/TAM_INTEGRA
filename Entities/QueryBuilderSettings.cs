using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class QueryBuilderSettings
    {
        public List<Filter> filters { get; set; }
        public bool allow_empty { get; set; }
        public int allow_groups { get; set; }
    }
}
