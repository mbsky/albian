namespace Albian.Persistence.Imp.Command.Interface
{
    public interface IDbTypeFactory
    {
        /// <summary>
        /// 根据默认的appSettingKey节点得到数据库类型
        /// appSettingKey默认为DataBase
        /// </summary>
        /// <returns></returns>
        DatabaseType GetDbType();

        /// <summary>
        /// 根据condif文件中appsetting指定appSettingKey节点得到数据库类型
        /// </summary>
        /// <param name="appSettingKey">配置数据库类型的appSetting标识.</param>
        /// <returns></returns>
        DatabaseType GetDbType(string appSettingKey);
    }
}