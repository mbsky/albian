using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Persistence.Enum;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Model
{
    public class FilterCondition : IFilterCondition
    {
        private string _propertyName;

        private object _value;

        private Type _type;

        private RelationalOperators _relational = RelationalOperators.And;

        private LogicalOperation _logical = LogicalOperation.Equal;

        public RelationalOperators Relational
        {
            get { return _relational; }
            set { _relational = value; }
        }

        public virtual string PropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }

        public virtual object Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public virtual Type Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public LogicalOperation Logical
        {
            get { return _logical; }
            set { _logical = value; }
        }
    }
}
