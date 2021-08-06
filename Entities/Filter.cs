using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Filter
    {
        public string id { get; set; }

        public string field { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public FilterType? type { get; set; }

        [JsonProperty(ItemConverterType = typeof(StringEnumConverter))]
        public FilterOperators @operator { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public InputType? input { get; set; }

        public object value { get; set; }

        public List<Filter> rules { get; set; }

        public string condition { get; set; }
    }
}
