using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Persistence.Enum;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Model
{
    public class OrderByCondition : IOrderByCondition
    {
        private string _propertyName;

        private SortStyle _sortStyle = SortStyle.Asc;

        public virtual string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        public virtual SortStyle SortStyle
        {
            get { return _sortStyle; }
            set { _sortStyle = value; }
        }
    }
}
