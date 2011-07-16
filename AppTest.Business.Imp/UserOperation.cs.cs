using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Albian.Kernel;
using Albian.Kernel.Service.Impl;
using Albian.Persistence;
using Albian.Persistence.Imp;
using AppTest.DataAccess;
using AppTest.DataAccess.Imp;
using AppTest.Model;
using AppTest.Model.Imp;

namespace AppTest.Business.Imp
{
    public class UserOperation :FreeAlbianService,IUserOperation
    {

        public override void Loading()
        {
            HashAlbianObjectManager.RegisterHandler<IUser>(HashAlbianObjectHandler);
            base.Loading();
        }

        private string HashAlbianObjectHandler(IUser target)
        {
            return string.Format("_{0}", target.Id.GetHashCode() % 3);
        }

        public bool Create(IUser user)
        {
            IUserDao dao = ServiceRouter.ObjectGenerator<UserDao,IUserDao>();
            ILogInfo log = AlbianObjectGenerator.CreateInstance<LogInfo>();
            log.Content = string.Format("创建用户，用户id为:{0}", user.Id);
            log.CreateTime = DateTime.Now;
            log.Creator = user.Id;
            log.Id = ServiceRouter.GetService<IIdService>().Generator("User");
            log.Style = InfoStyle.Registr;
            IList<IAlbianObject> infos = new List<IAlbianObject> {user, log};
            return dao.Create(infos);
        }
    }
}
