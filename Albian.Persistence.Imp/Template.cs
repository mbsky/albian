using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Albian.ObjectModel;

namespace Albian.Persistence.Imp
{
    public class Template
    {
        /// <summary>
        /// 新建指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static int Create<T>(T entity)
            where T : IAlbianObject, new()
        {
            return 0;
        }

        public static int Create<T>(IList<T> entity)
            where T : IAlbianObject
        {
            return 0;
        }

        /// <summary>
        /// 更新指定实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static int Modify<T>(T entity)
            where T : IAlbianObject
        {
            return 0;
        }

        public static int Modify<T>(IList<T> entity)
            where T : IAlbianObject
        {
            return 0;
        }

        /// <summary>
        /// 覆盖指定实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static int ModifyAll<T>(T entity)
            where T : IAlbianObject
        {
            return 0;
        }

        public static int ModifyAll<T>(IList<T> entity)
            where T : IAlbianObject
        {
            return 0;
        }

        public static int Remove<T>(T entity)
            where T : IAlbianObject
        {
            return 0;
        }


        public static int Remove<T>(IList<T> entity)
            where T : IAlbianObject
        {
            return 0;
        }

        /// <summary>
        /// 覆盖保存指定实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static int SaveAll<T>(T entity) where T : IAlbianObject
        {
            return 0;
        }

        public static int SaveAll<T>(IList<T> entity) where T : IAlbianObject
        {
            return 0;
        }

        /// <summary>
        /// 保存指定实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static int Save<T>(T entity) where T : IAlbianObject
        {
            return 0;
        }

        public static int Save<T>(IList<T> entity) where T : IAlbianObject
        {
            return 0;
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Find<T>()
            where T : IAlbianObject, new()
        {
            return default(T);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Find<T>(string storageName)
            where T : IAlbianObject, new()
        {
            return default(T);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static T Find<T>(CommandType cmdType, string cmdText,
                                params DbParameter[] commandParameters)
            where T : IAlbianObject, new()
        {
            return default(T);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static T Find<T>(string storageName, CommandType cmdType, string cmdText,
                                params DbParameter[] commandParameters)
            where T : IAlbianObject, new()
        {
            return default(T);
        }


        /// <summary>
        /// 加载实体集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Load<T>()
            where T : IAlbianObject, new()
        {
            return null;
        }


        /// <summary>
        /// 加载实体集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> Load<T>(string storageName)
            where T : IAlbianObject, new()
        {
            return null;
        }


        /// <summary>
        /// 加载实体集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static IList<T> Load<T>(CommandType cmdType, string cmdText,
                                       params DbParameter[] commandParameters)
            where T : IAlbianObject, new()
        {
            return null;
        }

        /// <summary>
        /// 加载实体集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdType">Type of the CMD.</param>
        /// <param name="cmdText">The CMD text.</param>
        /// <param name="commandParameters">The command parameters.</param>
        /// <returns></returns>
        public static IList<T> Load<T>(string storageName, CommandType cmdType, string cmdText,
                                       params DbParameter[] commandParameters)
            where T : IAlbianObject, new()
        {
            return null;
        }
    }
}