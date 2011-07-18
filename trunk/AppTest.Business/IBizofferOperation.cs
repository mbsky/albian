using System.Collections.Generic;
using Albian.Kernel.Service;
using AppTest.Model.Imp;

namespace AppTest.Business
{
    public interface IBizofferOperation : IAlbianService
    {
        IList<BizOffer> FindBizoffer();
    }
}