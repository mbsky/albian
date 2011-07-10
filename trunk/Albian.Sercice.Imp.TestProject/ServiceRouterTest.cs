using Albian.Kernel.Service;
using Albian.Kernel.Service.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Albian.Kernel;
using System.Diagnostics;

namespace Albian.Sercice.Imp.TestProject
{
    
    
    /// <summary>
    ///这是 ServiceRouterTest 的测试类，旨在
    ///包含所有 ServiceRouterTest 单元测试
    ///</summary>
    [TestClass()]
    public class ServiceRouterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试属性
        // 
        //编写测试时，还可使用以下属性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Start 的测试
        ///</summary>
        [TestMethod()]
        public void StartTest()
        {
            AlbianBootService.Start();
            //ServiceRouter.Start();
            //IService service1 = ServiceRouter.GetService<IService>("Test1");
            //string val1 = service1.Say("sqy");

            //IServiceTest service = ServiceRouter.GetService<IServiceTest>("Test");
            //string val = service.Hello("Test");

            IIdService idService = ServiceRouter.GetService<IIdService>("IdService");
            string id = idService.Generator();
            Debug.Print(id);
            Debug.Print(id.Length.ToString());

            //Assert.AreEqual("Test", val);
        }

        /// <summary>
        ///GetService 的测试
        ///</summary>
        public void GetServiceTest1Helper<T>()
            where T : IAlbianService
        {
            string id = string.Empty; // TODO: 初始化为适当的值
            T expected = default(T); // TODO: 初始化为适当的值
            T actual;
            actual = ServiceRouter.GetService<T>(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetServiceTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 GetServiceTest1Helper<T>()。");
        }

        /// <summary>
        ///GetService 的测试
        ///</summary>
        public void GetServiceTestHelper<T>()
            where T : IAlbianService
        {
            string id = string.Empty; // TODO: 初始化为适当的值
            bool isNew = false; // TODO: 初始化为适当的值
            T expected = default(T); // TODO: 初始化为适当的值
            T actual;
            actual = ServiceRouter.GetService<T>(id, isNew);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetServiceTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 GetServiceTestHelper<T>()。");
        }

        /// <summary>
        ///ServiceRouter 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void ServiceRouterConstructorTest()
        {
            ServiceRouter target = new ServiceRouter();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        [TestMethod]
        public void IdGeneratorTest()
        {
            //Debug.Print(IdGenerator.Generator());
            //Debug.Print(IdGenerator.Generator("Test"));
        }
    }
}
