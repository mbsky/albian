using Albian.Service;

namespace Albian.Sercice.Imp.TestProject
{
    public interface IService : IAlbianService
    {
        string Say(string val);
    }
}