﻿namespace Albian.Persistence.Imp.TestProject
{
    public class Order :AbstractAlbianObject, IOrder
    {
        protected string ProtectedTest { get; set; }
        private string PrivateTest { get; set; }

        #region IOrder Members

        //public string Id { get; set; }
        public string Name { get; set; }
        public string Buyer { get; set; }
        public decimal Money { get; set; }
        public string Seller { get; set; }

        #endregion

        #region IAlbianObject 成员


        //public bool IsNew { get; set; }

        #endregion
    }
}