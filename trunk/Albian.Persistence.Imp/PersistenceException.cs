using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Persistence.Imp
{
    public class PersistenceException: Exception
    {
        public PersistenceException(string message) : base(message)
        {
        }

        public PersistenceException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public PersistenceException()
            : base()
        {
        }
    }
}

