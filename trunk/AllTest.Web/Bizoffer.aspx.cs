using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Albian.Kernel.Service.Impl;
using AppTest.Business;
using AppTest.Model.Imp;

namespace AllTest.Web
{
    public partial class Bizoffer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<BizOffer> bizoffers = ServiceRouter.GetService<IBizofferOperation>().FindBizoffer();
            gv.DataSource = bizoffers;
            gv.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}
