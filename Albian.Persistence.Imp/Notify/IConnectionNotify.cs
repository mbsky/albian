using Albian.Kernel.Service;

namespace Albian.Persistence.Imp.Notify
{
    /// <summary>
    /// ���ݿ������쳣ʱ֪ͨ����
    /// </summary>
    public interface IConnectionNotify : IAlbianService
    {
        void SendMessage(string msg);
    }
}