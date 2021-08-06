using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public enum FilterOperators
    {
        equal,
        not_equal,
        @in,
        not_in,
        less,
        less_or_equal,
        greater,
        greater_or_equal,
        between,
        not_between,
        begins_with,
        not_begins_with,
        contains,
        not_contains,
        ends_with,
        not_ends_with,
        is_empty,
        is_not_empty,
        is_null,
        is_not_null
    }
}
