using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NMock2;
using NUnit.Framework;

namespace Kistl.API.Tests
{
    public class ConfigTestApplicationContext : ApplicationContext
    {
        public ConfigTestApplicationContext(string configFilePath)
            : base(HostType.None, configFilePath)
        {

        }
    }

    public class CustomActionsTestApplicationContext : ApplicationContext
    {
        public CustomActionsTestApplicationContext()
            : base(HostType.None, "")
        { }

        // export protected function for tests
        internal new void SetCustomActionsManager(ICustomActionsManager m)
        {
            base.SetCustomActionsManager(m);
        }
    }

    public class TypesTestApplicationContext : ApplicationContext
    {
        public TypesTestApplicationContext()
            : base(HostType.None, "")
        { }

        // public Type BasePersistenceObjectType { get; protected set; }
        // public Type BaseDataObjectType { get; protected set; }
        // public Type BaseStructObjectType { get; protected set; }
        // public Type BaseCollectionEntryType { get; protected set; }

        internal void SetBpoType(Type t) { this.BasePersistenceObjectType = t; }
        internal void SetBdoType(Type t) { this.BaseDataObjectType = t; }
        internal void SetBsoType(Type t) { this.BaseStructObjectType = t; }
        internal void SetBceType(Type t) { this.BaseCollectionEntryType = t; }
    }


    [TestFixture]
    public partial class ApplicationContextTests
    {

        [Test]
        public void InitDefaultConfig()
        {
            var testCtx = new ConfigTestApplicationContext("");

            Assert.IsNotNull(ApplicationContext.Current);
            Assert.AreEqual("Default Test Configuration", ApplicationContext.Current.Configuration.ConfigName);
        }

        [Test]
        public void InitConfigFile()
        {
            var testCtx = new ConfigTestApplicationContext("TestConfig.xml");

            Assert.IsNotNull(ApplicationContext.Current);
            Assert.AreEqual("Test Configuration", ApplicationContext.Current.Configuration.ConfigName);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InitMissingConfigFile()
        {
            var testCtx = new ConfigTestApplicationContext("MissingTestConfig.xml");
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void SetCustomActionsManagerNull()
        {
            var testCtx = new CustomActionsTestApplicationContext();
            testCtx.SetCustomActionsManager(null);
        }

        [Test]
        public void SetCustomActionsManagerCallsInit()
        {
            using (var m = new Mockery())
            {
                var mock = m.NewMock<ICustomActionsManager>();
                Expect.Once.On(mock).Method("Init").WithNoArguments();
                var testCtx = new CustomActionsTestApplicationContext();
                testCtx.SetCustomActionsManager(mock);
                Assert.AreSame(mock, testCtx.CustomActionsManager);
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetCustomActionsManagerTwice()
        {
            using (var m = new Mockery())
            {
                var firstMock = m.NewMock<ICustomActionsManager>();
                Stub.On(firstMock).Method("Init").WithNoArguments();
                var secondMock = m.NewMock<ICustomActionsManager>();
                Expect.Never.On(secondMock).Method("Init");
                var testCtx = new CustomActionsTestApplicationContext();

                testCtx.SetCustomActionsManager(firstMock);
                testCtx.SetCustomActionsManager(secondMock);
            }
        }

        [Test]
        public void PersistsTypes()
        {
            var testCtx = new TypesTestApplicationContext();
            testCtx.SetBceType(typeof(char));
            testCtx.SetBdoType(typeof(double));
            testCtx.SetBpoType(typeof(Predicate<int>));
            testCtx.SetBsoType(typeof(string));

            Assert.AreEqual(typeof(char), testCtx.BaseCollectionEntryType);
            Assert.AreEqual(typeof(double), testCtx.BaseDataObjectType);
            Assert.AreEqual(typeof(Predicate<int>), testCtx.BasePersistenceObjectType);
            Assert.AreEqual(typeof(string), testCtx.BaseStructObjectType);

        }
    }

}
