using System;
using System.Collections.Generic;
using Albian.Kernel.Service.Impl;
using AppTest.DataAccess;
using AppTest.DataAccess.Imp;
using AppTest.Model.Imp;

namespace AppTest.Business.Imp
{
    public class BizofferOperation :FreeAlbianService, IBizofferOperation
    {
        #region Implementation of IBizofferOperation

        public IList<BizOffer> FindBizoffer()
        {
            IBizofferDao dao = ServiceRouter.ObjectGenerator<BizofferDao, IBizofferDao>();
            return dao.FindBizoffer();
        }

        #endregion
    }
}