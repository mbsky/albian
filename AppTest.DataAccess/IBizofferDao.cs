using System.Collections.Generic;
using Albian.Persistence;
using AppTest.Model;
using AppTest.Model.Imp;

namespace AppTest.DataAccess
{
    public interface IBizofferDao
    {
        bool Create(IList<IAlbianObject> bizoffer);
        IBizOffer Load(string id);
        IList<BizOffer> FindBizoffer();
    }
}