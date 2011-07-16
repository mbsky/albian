﻿using System;
using Albian.Persistence.Imp.Parser.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Albian.Persistence.Imp.TestProject
{
    /// <summary>
    ///这是 AbstractPersistenceParserTest 的测试类，旨在
    ///包含所有 AbstractPersistenceParserTest 单元测试
    ///</summary>
    [TestClass()]
    public class AbstractPersistenceParserTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
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

        internal virtual AbstractPersistenceParser CreateAbstractPersistenceParser()
        {
            Type type = GetType();
            string fullName = type.FullName;
            // TODO: 实例化相应的具体类。
            AbstractPersistenceParser target = null;
            return target;
        }

        /// <summary>
        ///Init 的测试
        ///</summary>
        [TestMethod()]
        public void ParserPersistenceConfigTest()
        {
            AbstractPersistenceParser target = new PersistenceParser(); // TODO: 初始化为适当的值
            string filePath = "Persistence.config"; // TODO: 初始化为适当的值
            target.Init(filePath);
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void TypeFullNameTest()
        {
            Type type = GetType();
            string fullName = type.FullName;
            //type.Assembly.FullName;
            return;
        }
    }
}