using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Albian.Kernel.Service.Impl;

namespace Albian.Kernel.ThreadPool.Impl
{
    public class ThreadPoolService : AbstractAlbianService,IThreadPoolService
    {
        private static Impl.ThreadPool pools;

        public override void Loading()
        {
            Init();
            base.Loading();
        }

        /// <summary>
        /// 初始化线程池
        /// </summary>
        public void Init()
        {
            pools = new Impl.ThreadPool();
            pools.Start();
        }

        /// <summary>
        /// 初始化线程池
        /// </summary>
        /// <param name="size">线程池大小，最好设置为CPU个数的25倍</param>
        public void Init(int size)
        {
            pools = new Impl.ThreadPool(size);
            pools.Start();

        }

        /// <summary>
        /// 初始化线程池
        /// </summary>
        /// <param name="isFlowExecutionContext">是否设置执行线程上下文信息，默认为true</param>
        public void Init(bool isFlowExecutionContext)
        {
            pools = new Impl.ThreadPool(isFlowExecutionContext);
            pools.Start();

        }

        /// <summary>
        /// 初始化线程
        /// </summary>
        /// <param name="size">线程池大小，最优设置为CPU个数的25倍</param>
        /// <param name="isFlowExecutionContext">是否设置执行线程上下文信息，默认为true</param>
        public void Init(int size, bool isFlowExecutionContext)
        {
            pools = new Impl.ThreadPool(size, isFlowExecutionContext);
            pools.Start();

        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="callback">执行方法的代理</param>
        /// <param name="obj">执行方法的参数</param>
        public void QueueUserWorkItem(WaitCallback callback, object obj)
        {
            pools.QueueUserWorkItem(callback, obj);
        }


        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="callback">执行方法的代理</param>
        public void QueueUserWorkItem(WaitCallback callback)
        {
            pools.QueueUserWorkItem(callback);
        }
    }
}