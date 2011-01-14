using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Kistl.API.Configuration;
using Kistl.API.Mocks;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class KistlConfigTests : AbstractApiTestFixture
    {
        readonly static string ConfigFile = "Kistl.API.Tests.Config.xml";

        private void CheckConfig(KistlConfig cfg)
        {
            Assert.That(cfg.ConfigName, Is.Not.Empty, "ConfigName");
            Assert.That(cfg.SourceFileLocation, Is.Not.Empty, "SourceFileLocation");

            Assert.That(cfg.Client, Is.Not.Null, "Client");

            Assert.That(cfg.Server, Is.Not.Null, "Server");
            Assert.That(cfg.Server.ConnectionString, Is.Not.Empty, "ConnectionString");
            Assert.That(cfg.Server.DatabaseProvider, Is.Not.Empty, "DatabaseProvider");
            Assert.That(cfg.Server.DocumentStore, Is.Not.Empty, "DocumentStore");
        }

        [Test]
        public void DefaultLoading()
        {
            if (!File.Exists("DefaultConfig.xml"))
            {
                File.Copy(ConfigFile, "DefaultConfig.xml");
            }
            var config = KistlConfig.FromFile(String.Empty);

            Assert.That(config, Is.Not.Null, "Configuration");
            Assert.That(config.ConfigFilePath, Is.Not.Empty, "ConfigFilePath");
            Assert.That(config.ConfigName, Is.Not.Empty, "ConfigName");
        }

        [Test]
        public void LoadFile()
        {
            var config = KistlConfig.FromFile(ConfigFile);

            Assert.That(config, Is.Not.Null, "Configuration");
            Assert.That(config.ConfigFilePath, Is.EqualTo(ConfigFile), "ConfigFilePath");
            Assert.That(config.ConfigName, Is.Not.Empty, "ConfigName");
        }

        [Test]
        public void FromStream()
        {
            using (FileStream s = File.OpenRead(ConfigFile))
            {
                KistlConfig cfg = KistlConfig.FromStream(s);
                Assert.That(cfg.ConfigFilePath, Is.Null);
                CheckConfig(cfg);
            }
        }

        [Test]
        public void FromTextReader()
        {
            using (FileStream s = File.OpenRead(ConfigFile))
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
            var config = KistlConfig.FromFile(String.Empty);
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
