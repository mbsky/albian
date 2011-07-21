using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Albian.Kernel.Service.Impl;
using Albian.Persistence.Imp;
using AppTest.Business;
using AppTest.Model;
using AppTest.Model.Imp;

namespace AllTest.Web
{
    public partial class Bizoffer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<BizOffer> bizoffers = AlbianServiceRouter.GetService<IBizofferOperation>().FindBizoffer();
                gv.DataSource = bizoffers;
                gv.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            IBizOffer bizoffer = AlbianObjectFactory.CreateInstance<BizOffer>();
            bizoffer.Id = AlbianObjectFactory.CreateId("BOFF");
            bizoffer.CreateTime = DateTime.Now;
            bizoffer.Creator = txtSellerId.Text;
            bizoffer.Description = txtDesc.Text;
            bizoffer.Discount = null;
            bizoffer.IsDiscount = null;
            bizoffer.LastModifier = txtSellerId.Text;
            bizoffer.LastModifyTime = DateTime.Now;
            bizoffer.LastPrice = decimal.Parse(txtPrice.Text);
            bizoffer.Name = txtName.Text;
            bizoffer.Price = decimal.Parse(txtPrice.Text);
            bizoffer.SellerId = txtSellerId.Text;
            bizoffer.SellerName = txtSellerName.Text;
            bizoffer.State = BizofferState.Create;
            AlbianServiceRouter.GetService<IBizofferOperation>().Create(bizoffer);
        }
    }
}
