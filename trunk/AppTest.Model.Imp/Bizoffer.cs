using System;
using Albian.Persistence.Model.Impl;

namespace AppTest.Model.Imp
{
    public class BizOffer : FreeAlbianObject,IBizOffer
    {
        #region Implementation of IBizOffer

        private string _name;

        private string _sellerId;

        private string _sellerName;

        private DateTime _createTime;

        private BizofferState _state;

        private decimal _price;

        private bool? _isDiscount;

        private decimal? _discount;

        private decimal _lastPrice;

        private string _creator;

        private DateTime _lastModifyTime;

        private string _lastModifier;

        private string _description;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string SellerId
        {
            get { return _sellerId; }
            set { _sellerId = value; }
        }

        public virtual string SellerName
        {
            get { return _sellerName; }
            set { _sellerName = value; }
        }

        public virtual DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        /// <summary>
        /// ������״̬
        /// </summary>
        public virtual BizofferState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// ԭʼ�۸�
        /// </summary>
        public virtual decimal Price
        {
            get { return _price; }
            set { _price = value; }
        }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        public virtual bool? IsDiscount
        {
            get { return _isDiscount; }
            set { _isDiscount = value; }
        }

        /// <summary>
        /// �ۿ�
        /// </summary>
        public virtual decimal? Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        /// <summary>
        /// ���׼۸�
        /// </summary>
        public virtual decimal LastPrice
        {
            get { return _lastPrice; }
            set { _lastPrice = value; }
        }

        public virtual string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }

        public virtual DateTime LastModifyTime
        {
            get { return _lastModifyTime; }
            set { _lastModifyTime = value; }
        }

        public virtual string LastModifier
        {
            get { return _lastModifier; }
            set { _lastModifier = value; }
        }

        /// <summary>
        /// ��������Ʒ����
        /// </summary>
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion
    }
}