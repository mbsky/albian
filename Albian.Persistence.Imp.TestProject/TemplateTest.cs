using Albian.Persistence.Imp;
using Albian.Persistence.Imp.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Albian.ObjectModel;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Albian.Persistence.Imp.TestProject
{
    
    
    /// <summary>
    ///这是 TemplateTest 的测试类，旨在
    ///包含所有 TemplateTest 单元测试
    ///</summary>
    [TestClass()]
    public class TemplateTest
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            AbstractPersistenceParser target = new PersistenceParser(); // TODO: 初始化为适当的值
            string filePath = "Persistence.config"; // TODO: 初始化为适当的值
            target.Init(filePath);
            string storageFile = "Storage.config";
            IStorageParser storage = new StorageParser();
            storage.Init(storageFile);
        }
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
        ///SaveAll 的测试
        ///</summary>
        public void SaveAllTest1Helper<T>()
            where T : IAlbianObject
        {
            IList<T> entity = null; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.SaveAll<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SaveAllTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 SaveAllTest1Helper<T>()。");
        }

        /// <summary>
        ///SaveAll 的测试
        ///</summary>
        public void SaveAllTestHelper<T>()
            where T : IAlbianObject
        {
            T entity = default(T); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.SaveAll<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SaveAllTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 SaveAllTestHelper<T>()。");
        }

        /// <summary>
        ///Save 的测试
        ///</summary>
        public void SaveTest1Helper<T>()
            where T : IAlbianObject
        {
            IList<T> entity = null; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Save<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SaveTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 SaveTest1Helper<T>()。");
        }

        /// <summary>
        ///Save 的测试
        ///</summary>
        public void SaveTestHelper<T>()
            where T : IAlbianObject
        {
            T entity = default(T); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Save<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SaveTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 SaveTestHelper<T>()。");
        }

        /// <summary>
        ///Remove 的测试
        ///</summary>
        public void RemoveTest1Helper<T>()
            where T : IAlbianObject
        {
            T entity = default(T); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Remove<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void RemoveTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 RemoveTest1Helper<T>()。");
        }

        /// <summary>
        ///Remove 的测试
        ///</summary>
        public void RemoveTestHelper<T>()
            where T : IAlbianObject
        {
            IList<T> entity = null; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Remove<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void RemoveTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 RemoveTestHelper<T>()。");
        }

        /// <summary>
        ///ModifyAll 的测试
        ///</summary>
        public void ModifyAllTest1Helper<T>()
            where T : IAlbianObject
        {
            IList<T> entity = null; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.ModifyAll<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ModifyAllTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 ModifyAllTest1Helper<T>()。");
        }

        /// <summary>
        ///ModifyAll 的测试
        ///</summary>
        public void ModifyAllTestHelper<T>()
            where T : IAlbianObject
        {
            T entity = default(T); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.ModifyAll<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ModifyAllTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 ModifyAllTestHelper<T>()。");
        }

        /// <summary>
        ///Modify 的测试
        ///</summary>
        public void ModifyTest1Helper<T>()
            where T : IAlbianObject
        {
            T entity = default(T); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Modify<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ModifyTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 ModifyTest1Helper<T>()。");
        }

        /// <summary>
        ///Modify 的测试
        ///</summary>
        public void ModifyTestHelper<T>()
            where T : IAlbianObject
        {
            IList<T> entity = null; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Modify<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ModifyTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 ModifyTestHelper<T>()。");
        }

        /// <summary>
        ///Load 的测试
        ///</summary>
        public void LoadTest3Helper<T>()
            where T : IAlbianObject, new()
        {
            string storageName = string.Empty; // TODO: 初始化为适当的值
            CommandType cmdType = new CommandType(); // TODO: 初始化为适当的值
            string cmdText = string.Empty; // TODO: 初始化为适当的值
            DbParameter[] commandParameters = null; // TODO: 初始化为适当的值
            IList<T> expected = null; // TODO: 初始化为适当的值
            IList<T> actual;
            actual = Template.Load<T>(storageName, cmdType, cmdText, commandParameters);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void LoadTest3()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 LoadTest3Helper<T>()。");
        }

        /// <summary>
        ///Load 的测试
        ///</summary>
        public void LoadTest2Helper<T>()
            where T : IAlbianObject, new()
        {
            string storageName = string.Empty; // TODO: 初始化为适当的值
            IList<T> expected = null; // TODO: 初始化为适当的值
            IList<T> actual;
            actual = Template.Load<T>(storageName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void LoadTest2()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 LoadTest2Helper<T>()。");
        }

        /// <summary>
        ///Load 的测试
        ///</summary>
        public void LoadTest1Helper<T>()
            where T : IAlbianObject, new()
        {
            CommandType cmdType = new CommandType(); // TODO: 初始化为适当的值
            string cmdText = string.Empty; // TODO: 初始化为适当的值
            DbParameter[] commandParameters = null; // TODO: 初始化为适当的值
            IList<T> expected = null; // TODO: 初始化为适当的值
            IList<T> actual;
            actual = Template.Load<T>(cmdType, cmdText, commandParameters);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void LoadTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 LoadTest1Helper<T>()。");
        }

        /// <summary>
        ///Load 的测试
        ///</summary>
        public void LoadTestHelper<T>()
            where T : IAlbianObject, new()
        {
            IList<T> expected = null; // TODO: 初始化为适当的值
            IList<T> actual;
            actual = Template.Load<T>();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void LoadTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 LoadTestHelper<T>()。");
        }

        /// <summary>
        ///Find 的测试
        ///</summary>
        public void FindTest3Helper<T>()
            where T : IAlbianObject, new()
        {
            CommandType cmdType = new CommandType(); // TODO: 初始化为适当的值
            string cmdText = string.Empty; // TODO: 初始化为适当的值
            DbParameter[] commandParameters = null; // TODO: 初始化为适当的值
            T expected = new T(); // TODO: 初始化为适当的值
            T actual;
            actual = Template.Find<T>(cmdType, cmdText, commandParameters);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void FindTest3()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 FindTest3Helper<T>()。");
        }

        /// <summary>
        ///Find 的测试
        ///</summary>
        public void FindTest2Helper<T>()
            where T : IAlbianObject, new()
        {
            string storageName = string.Empty; // TODO: 初始化为适当的值
            CommandType cmdType = new CommandType(); // TODO: 初始化为适当的值
            string cmdText = string.Empty; // TODO: 初始化为适当的值
            DbParameter[] commandParameters = null; // TODO: 初始化为适当的值
            T expected = new T(); // TODO: 初始化为适当的值
            T actual;
            actual = Template.Find<T>(storageName, cmdType, cmdText, commandParameters);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void FindTest2()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 FindTest2Helper<T>()。");
        }

        /// <summary>
        ///Find 的测试
        ///</summary>
        public void FindTest1Helper<T>()
            where T : IAlbianObject, new()
        {
            T expected = new T(); // TODO: 初始化为适当的值
            T actual;
            actual = Template.Find<T>();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void FindTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 FindTest1Helper<T>()。");
        }

        /// <summary>
        ///Find 的测试
        ///</summary>
        public void FindTestHelper<T>()
            where T : IAlbianObject, new()
        {
            string storageName = string.Empty; // TODO: 初始化为适当的值
            T expected = new T(); // TODO: 初始化为适当的值
            T actual;
            actual = Template.Find<T>(storageName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void FindTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 FindTestHelper<T>()。");
        }

        /// <summary>
        ///Create 的测试
        ///</summary>
        public void CreateTest1Helper<T>()
            where T : IAlbianObject
        {
            IList<T> entity = null; // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Create<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CreateTest1()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 CreateTest1Helper<T>()。");
        }

        /// <summary>
        ///Create 的测试
        ///</summary>
        public void CreateTestHelper<T>()
            where T : IAlbianObject, new()
        {
            T entity = new T(); // TODO: 初始化为适当的值
            int expected = 0; // TODO: 初始化为适当的值
            int actual;
            actual = Template.Create<T>(entity);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CreateTest()
        {
            IOrder order = new Order();
            order.Buyer = "buyer";
            order.Id = "id";
            order.Money = 2;
            order.Name = "name";
            order.Seller = "seller";
            Template.Create<IAlbianObject>(order);
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 CreateTestHelper<T>()。");
        }

        /// <summary>
        ///Template 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void TemplateConstructorTest()
        {
            Template target = new Template();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
