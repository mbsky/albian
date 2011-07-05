using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Persistence.Enum;

namespace Albian.Persistence.Model
{
    public interface IFilterCondition
    {
        RelationalOperators Relational { get; set; }
        string PropertyName { get; set; }
        object Value { get; set; }
        Type Type { get; set; }
        LogicalOperation Logical { get; set; }
    }
}
