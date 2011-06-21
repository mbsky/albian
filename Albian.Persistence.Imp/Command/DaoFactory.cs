using Albian.Persistence.Imp.Command.Interface;

namespace Albian.Persistence.Imp.Command
{
    /// <summary>
    /// 数据工厂类
    /// 目前只支持Sql Server和Oralce两种数据库
    /// </summary>
    public sealed class DaoFactory : IDaoFactory
    {
        /// <summary>
        /// 得到数据库单例
        /// </summary>
        public static IDaoFactory Instance
        {
            get { return new DaoFactory(); }
        }

        #region IDaoFactory Members

        /// <summary>
        /// 数据库工厂方法，根据不用的数据库生成不同的数据库执行对象
        /// </summary>
        /// <returns>SqlServer或者Oracle的实例</returns>
        public IDao Factory()
        {
            return Factory("DataBase");
        }

        /// <summary>
        /// 数据库工厂方法，根据不用的数据库生成不同的数据库执行对象。
        /// 若未配置数据库类型，默认为Sql Server数据库
        /// </summary>
        /// <param name="appSettingKey">配置数据库类型的appSetting标识.</param>
        /// <returns></returns>
        public IDao Factory(string appSettingKey)
        {
            //switch (Database.GetDbType(appSettingKey))
            //{
            //    case DatabaseType.SqlServer: return Builder<IDao,SqlServer>();
            //    case DatabaseType.Oracle: return Builder<IDao, Oracle>();
            //    default: return Builder<IDao, SqlServer>();
            //}
            return null;
        }

        #endregion
    }
}