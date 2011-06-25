using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albian.Persistence.Imp.TransactionCluster
{
    public enum TransactionClusterState
    {
        /// <summary>
        /// 未开始事务
        /// </summary>
        NoStarted,
        /// <summary>
        /// 正在打开中
        /// </summary>
        Opening,
        /// <summary>
        /// 已经打开，正在运行
        /// </summary>
        OpenedAndRuning,
        /// <summary>
        /// 执行命令完毕，准备提交
        /// </summary>
        RunnedAndCommiting,
        /// <summary>
        /// 提交完毕
        /// </summary>
        Commited,
        /// <summary>
        /// 正在回滚
        /// </summary>
        Rollbacking,
        /// <summary>
        /// 回滚完毕
        /// </summary>
        Rollbacked,
    }
}