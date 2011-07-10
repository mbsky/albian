using Albian.Persistence.Model;

namespace Albian.Persistence
{
    /// <summary>
    /// 实体签名接口
    /// </summary>
    public interface IAlbianObject
    {
        [AlbianMember(PrimaryKey=true)]
        string Id { get; set; }
        [AlbianMember(IsSave=false)]
        bool IsNew { get; set; }
    }
}