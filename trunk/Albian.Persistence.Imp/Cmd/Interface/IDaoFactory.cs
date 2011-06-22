namespace Albian.Persistence.Imp.Command.Interface
{
    public interface IDaoFactory
    {
        /// <summary>
        /// 数据库工厂方法，根据不用的数据库生成不同的数据库执行对象。
        /// 若未配置数据库类型，默认为Sql Server数据库
        /// </summary>
        /// <returns></returns>
        IDao Factory();

        /// <summary>
        /// 数据库工厂方法，根据不用的数据库生成不同的数据库执行对象。
        /// 若未配置数据库类型，默认为Sql Server数据库
        /// ...............................................................................
        /// </summary>
        /// <param name="appSettingKey">配置数据库类型的appSetting标识.</param>
        /// <returns></returns>
        IDao Factory(string appSettingKey);
    }
}