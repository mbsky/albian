using System;

namespace Albian.Persistence.Imp.TestProject
{
    public class BizOffer : IBizOffer
    {
        #region IBizOffer Members

        public string Id { get; set; }
        public bool IsNew
        {
            get;
            set;
        }

        public string Name { get; set; }
        public string Buyer { get; set; }
        public decimal Money { get; set; }
        public string Seller { get; set; }

        #endregion
    }
}