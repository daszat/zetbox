
namespace Kistl.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Kistl.API.Configuration;
    
    using NUnit.Framework;

    public class ConfigTestApplicationContext : ApplicationContext
    {
        public ConfigTestApplicationContext(KistlConfig config)
            : base(HostType.None, config)
        {

        }

        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            throw new NotImplementedException();
        }
    }

    public class CustomActionsTestApplicationContext : ApplicationContext
    {
        public CustomActionsTestApplicationContext(string configfilename)
            : base(HostType.None, KistlConfig.FromFile(configfilename))
        { }

        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            throw new NotImplementedException();
        }
    }

    public class TypesTestApplicationContext : ApplicationContext
    {
        public TypesTestApplicationContext(string configfilename)
            : base(HostType.None, KistlConfig.FromFile(configfilename))
        { }

        // public Type BasePersistenceObjectType { get; protected set; }
        // public Type BaseDataObjectType { get; protected set; }
        // public Type BaseCompoundObjectType { get; protected set; }
        // public Type BaseCollectionEntryType { get; protected set; }

        internal void SetBpoType(Type t) { this.BasePersistenceObjectType = t; }
        internal void SetBdoType(Type t) { this.BaseDataObjectType = t; }
        internal void SetBsoType(Type t) { this.BaseCompoundObjectType = t; }
        internal void SetBceType(Type t) { this.BaseCollectionEntryType = t; }

        public override void LoadFrozenActions(IReadOnlyKistlContext ctx)
        {
            throw new NotImplementedException();
        }
    }


    [TestFixture]
    public partial class ApplicationContextTests
    {
        readonly static string ConfigFile = "Kistl.API.Tests.Config.xml";

        [Test]
        public void InitDefaultConfig()
        {
            var config = KistlConfig.FromFile(ConfigFile);
            var testCtx = new ConfigTestApplicationContext(config);

            Assert.IsNotNull(ApplicationContext.Current);
            Assert.AreSame(config, ApplicationContext.Current.Configuration);
        }

        [Test]
        public void PersistsTypes()
        {
            var testCtx = new TypesTestApplicationContext(ConfigFile);
            testCtx.SetBceType(typeof(char));
            testCtx.SetBdoType(typeof(double));
            testCtx.SetBpoType(typeof(Predicate<int>));
            testCtx.SetBsoType(typeof(string));

            Assert.AreEqual(typeof(char), testCtx.BaseCollectionEntryType);
            Assert.AreEqual(typeof(double), testCtx.BaseDataObjectType);
            Assert.AreEqual(typeof(Predicate<int>), testCtx.BasePersistenceObjectType);
            Assert.AreEqual(typeof(string), testCtx.BaseCompoundObjectType);
        }
    }
}
