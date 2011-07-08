namespace Albian.ObjectModel
{
    /// <summary>
    /// 实体签名接口
    /// </summary>
    public interface IAlbianObject
    {
        string Id { get; set; }
        bool IsNew { get; set; }
    }
}