using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Persistence.Enum;

namespace Albian.Persistence.Model
{
    public interface IOrderByCondition
    {
        string PropertyName { get; set; }
        SortStyle SortStyle{get;set;}
    }
}
