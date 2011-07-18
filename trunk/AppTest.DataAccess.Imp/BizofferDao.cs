using System.Collections.Generic;
using Albian.Persistence;
using Albian.Persistence.Imp;
using Albian.Persistence.Imp.Model;
using AppTest.Model;
using AppTest.Model.Imp;

namespace AppTest.DataAccess.Imp
{
    public class BizofferDao :IBizofferDao
    {

        public bool Create(IList<IAlbianObject> bizoffer)
        {
           return PersistenceService.Save(bizoffer);
        }

        public IBizOffer Load(string id)
        {
            return PersistenceService.LoadObject<BizOffer>(id);
        }

        public IList<BizOffer> FindBizoffer()
        {
            return PersistenceService.FindObjects<BizOffer>("CreateTimeRouting",new OrderByCondition[]{
            new OrderByCondition()
            {
                PropertyName = "CreateTime",
                SortStyle = Albian.Persistence.Enum.SortStyle.Desc,
            }
            });
        }

   }
}