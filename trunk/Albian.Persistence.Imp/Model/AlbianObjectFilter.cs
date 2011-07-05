using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Albian.Persistence.Enum;
using Albian.Persistence.Model;

namespace Albian.Persistence.Imp.Model
{
    public class AlbianObjectFilter : IAlbianObjectFilter
    {
        private string[] _propertyNames;

        private string _tableName;

        private string _where;

        private SortStyle _orderby = SortStyle.Asc;

        private IDataParameter[] _paras;

        public string[] PropertyNames
        {
            get { return _propertyNames; }
            set { _propertyNames = value; }
        }

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public string Where
        {
            get { return _where; }
            set { _where = value; }
        }

        public SortStyle Orderby
        {
            get { return _orderby; }
            set { _orderby = value; }
        }

        public IDataParameter[] Paras
        {
            get { return _paras; }
            set { _paras = value; }
        }
    }
}
