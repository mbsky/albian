namespace Albian.Persistence.Model
{
    public interface ICacheAttribute
    {
        /// <summary>
        /// �Ƿ����û���
        /// </summary>
        bool Enable { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        int LifeTime { get; set; }
    }
}