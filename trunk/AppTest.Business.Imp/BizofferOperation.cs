using System;
using System.Collections;
using System.Collections.Generic;
using Albian.Kernel.Service;
using Albian.Kernel.Service.Impl;
using Albian.Persistence;
using AppTest.DataAccess;
using AppTest.DataAccess.Imp;
using AppTest.Model;
using AppTest.Model.Imp;
using Albian.Persistence.Imp;

namespace AppTest.Business.Imp
{
    public class BizofferOperation :FreeAlbianService, IBizofferOperation
    {
        #region Implementation of IBizofferOperation

        public override void Loading()
        {

            HashAlbianObjectManager.RegisterHandler("IdRouting", AssemblyManager.GetFullTypeName(typeof(User)), HashAlbianObjectHandlerUser);
            HashAlbianObjectManager.RegisterHandler("CreateTimeRouting", AssemblyManager.GetFullTypeName(typeof(LogInfo)), HashAlbianObjectHandlerLog);
            base.Loading();
        }

        private string HashAlbianObjectHandlerUser(IAlbianObject target)
        {
            return string.Format("_{0}", Math.Abs(target.Id.GetHashCode() % 3));
        }
        //private string HashAlbianObjectHandlerByCreatrTime(IAlbianObject target)
        //{
        //    IBizOffer bizoffer = (IBizOffer)target;
        //    return string.Format("_{0}", Math.Abs(bizoffer.CreateTime.GetHashCode() % 3));
        //}
        private string HashAlbianObjectHandlerLog(IAlbianObject target)
        {
            ILogInfo user = (ILogInfo)target;
            return string.Format("_{0}", user.Style == InfoStyle.Publish ? "bizoffer" : string.Empty);
        }


        public IList<BizOffer> FindBizoffer()
        {
            IBizofferDao dao = ServiceRouter.ObjectGenerator<BizofferDao, IBizofferDao>();
            return dao.FindBizoffer();
        }

        public bool Create(IBizOffer bizoffer)
        {
            ILogInfo log = AlbianObjectFactory.CreateInstance<LogInfo>();
            log.Content = string.Format("创建发布单，发布单id为:{0}", bizoffer.Id);
            log.CreateTime = DateTime.Now;
            log.Creator = bizoffer.Id;
            log.Id = AlbianObjectFactory.CreateId("Log");
            log.Style = InfoStyle.Registr;

            IList<IAlbianObject> list = new List<IAlbianObject> {bizoffer, log};
            IBizofferDao dao = ServiceRouter.ObjectGenerator<BizofferDao, IBizofferDao>();
            return dao.Create(list);
        }

        #endregion
    }
}