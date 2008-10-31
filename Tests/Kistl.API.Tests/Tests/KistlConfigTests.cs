using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using NUnit.Framework.SyntaxHelpers;
using Kistl.API;
using Kistl.API.Configuration;
using System.Reflection;
using System.Linq.Expressions;
using System.IO;

namespace Kistl.API.Tests
{
    [TestFixture]
    public class KistlConfigTests
    {
        private void CheckConfig(KistlConfig cfg)
        {
            Assert.That(cfg.ConfigName, Is.Not.Empty, "ConfigName");
            Assert.That(cfg.SourceFileLocation, Is.Not.Empty, "SourceFileLocation");

            Assert.That(cfg.Client, Is.Not.Null, "Client");
            Assert.That(cfg.Client.DocumentStore, Is.Not.Empty, "DocumentStore");

            Assert.That(cfg.Server, Is.Not.Null, "Server");
            Assert.That(cfg.Server.ConnectionString, Is.Not.Empty, "ConnectionString");
            Assert.That(cfg.Server.DatabaseProvider, Is.Not.Empty, "DatabaseProvider");
            Assert.That(cfg.Server.DocumentStore, Is.Not.Empty, "DocumentStore");
        }

        [Test]
        public void Current()
        {
            Assert.That(ApplicationContext.Current.Configuration, Is.Not.Null);
            Assert.That(ApplicationContext.Current.Configuration.ConfigFilePath, Is.Not.Empty);
            Assert.That(ApplicationContext.Current.Configuration.ConfigName, Is.Not.Empty);
        }

        [Test]
        public void FromStream()
        {
            using (FileStream s = File.OpenRead(@"TestConfig.xml"))
            {
                KistlConfig cfg = KistlConfig.FromStream(s);
                Assert.That(cfg.ConfigFilePath, Is.Null);
                CheckConfig(cfg);
            }
        }

        [Test]
        public void FromTextReader()
        {
            using (FileStream s = File.OpenRead(@"TestConfig.xml"))
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
            string filename = @"testconfig.xml";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            ApplicationContext.Current.Configuration.ToFile(filename);
            Assert.That(File.Exists(filename), Is.True);
            Assert.That(new FileInfo(filename).Length, Is.GreaterThan(0));
            File.Delete(filename);
        }

        [Test]
        public void ToStream()
        {
            MemoryStream ms = new MemoryStream();
            ApplicationContext.Current.Configuration.ToStream(ms);
            Assert.That(ms.Length, Is.GreaterThan(0));
        }

    }
}
