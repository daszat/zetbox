
namespace Kistl.API.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Kistl.API.Configuration;
    using NUnit.Framework;

    [TestFixture]
    public class ConfigurationTests
        : AbstractApiTestFixture
    {
        readonly static string ConfigFile = "Kistl.API.Tests.xml";

        private void CheckConfig(KistlConfig cfg)
        {
            Assert.That(cfg.ConfigName, Is.Not.Empty, "ConfigName");
            Assert.That(cfg.AssemblySearchPaths, Is.Not.Null, "AssemblySearchPaths");
            Assert.That(cfg.AssemblySearchPaths.Paths, Is.TypeOf<IEnumerable>(), "AssemblySearchPaths");
            Assert.That(cfg.AssemblySearchPaths.Paths, Is.Not.Empty, "AssemblySearchPaths");

            Assert.That(cfg.Client, Is.Not.Null, "Client");

            Assert.That(cfg.Server, Is.Not.Null, "Server");
            var connectionString = cfg.Server.GetConnectionString(Helper.KistlConnectionStringKey);
            Assert.That(connectionString.ConnectionString, Is.Not.Empty, "ConnectionString");
            Assert.That(connectionString.SchemaProvider, Is.Not.Empty, "SchemaProvider");
            Assert.That(connectionString.DatabaseProvider, Is.Not.Empty, "DatabaseProvider");
            Assert.That(cfg.Server.DocumentStore, Is.Not.Empty, "DocumentStore");
        }

        [Test]
        public void DefaultLoading()
        {
            var defaultDest = Path.Combine("Configs", "DefaultConfig.xml");
            if (!File.Exists(defaultDest))
            {
                File.Copy("TestConfig.xml", defaultDest);
            }
            var config = KistlConfig.FromFile(String.Empty, "DefaultConfig.xml");

            Assert.That(config, Is.Not.Null, "Configuration");
            Assert.That(config.ConfigFilePath, Is.Not.Empty, "ConfigFilePath");
            Assert.That(config.ConfigName, Is.Not.Empty, "ConfigName");
        }

        [Test]
        public void LoadFile()
        {
            var config = KistlConfig.FromFile("TestConfig.xml", "DoesNotExist.xml");

            Assert.That(config, Is.Not.Null, "Configuration");
            Assert.That(config.ConfigFilePath, Is.EqualTo("TestConfig.xml"), "ConfigFilePath");
            Assert.That(config.ConfigName, Is.Not.Empty, "ConfigName");
        }

        [Test]
        public void FromStream()
        {
            var filename = KistlConfig.GetDefaultConfigName(ConfigFile);
            Assert.That(filename, Is.Not.Empty);
            Assert.That(File.Exists(filename), Is.True, String.Format("configfile {0} doesn't exist", filename));

            using (FileStream s = File.OpenRead(filename))
            {
                
                KistlConfig cfg = KistlConfig.FromStream(s);
                Assert.That(cfg, Is.Not.Null);
                Assert.That(cfg.ConfigFilePath, Is.Null);
                CheckConfig(cfg);
            }
        }

        [Test]
        public void FromTextReader()
        {
            using (FileStream s = File.OpenRead(KistlConfig.GetDefaultConfigName(ConfigFile)))
            {
                TextReader rd = new StreamReader(s);
                KistlConfig cfg = KistlConfig.FromStream(rd);
                Assert.That(cfg.ConfigFilePath, Is.Null);
                CheckConfig(cfg);
            }
        }

        [Test]
        public void ToFile()
        {
            string filename = @"testconfig_to_see_if_saving_works.xml";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            var config = KistlConfig.FromFile(String.Empty, "DefaultConfig.xml");
            config.ToFile(filename);
            Assert.That(File.Exists(filename), Is.True);
            Assert.That(new FileInfo(filename).Length, Is.GreaterThan(0));
            File.Delete(filename);
        }

        [Test]
        public void ToStream()
        {
            MemoryStream ms = new MemoryStream();
            var config = new KistlConfig();
            config.ToStream(ms);
            Assert.That(ms.Length, Is.GreaterThan(0));
        }

        [Test]
        public void ConfigurationExceptionWithMessage()
        {
            var message = "message";
            var ex = new ConfigurationException(message);
            Assert.That(ex.Message, Is.EqualTo(message));
        }

        [Test]
        public void ConfigurationExceptionWithoutMessage()
        {
            var ex = new ConfigurationException();
            Assert.That(ex.Message, Is.Not.Empty);
        }
    }
}
