using System;
using Albian.Persistence;

namespace AppTest.Model
{
    public interface IBizOffer : IAlbianObject
    {
        string Name { get; set; }

        string SellerId { get; set; }
        string SellerName { get; set; }

        DateTime CreateTime { get; set; }
        /// <summary>
        /// ������״̬
        /// </summary>
        BizofferState State { get; set; }
        /// <summary>
        /// ԭʼ�۸�
        /// </summary>
        decimal Price { get; set; }

        /// <summary>
        /// �Ƿ����
        /// </summary>
        bool? IsDiscount { get; set; }
        /// <summary>
        /// �ۿ�
        /// </summary>
        decimal? Discount { get; set; }
        /// <summary>
        /// ���׼۸�
        /// </summary>
        decimal LastPrice { get; set; }

        string Creator { get; set; }

        DateTime LastModifyTime { get; set; }

        string LastModifier { get; set; }
        /// <summary>
        /// ��������Ʒ����
        /// </summary>
        string Description { get; set; }
    }
}