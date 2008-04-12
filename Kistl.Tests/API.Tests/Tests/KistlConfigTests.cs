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

namespace API.Tests.Tests
{
    [TestFixture]
    public class KistlConfigTests
    {
        private void CheckConfig(KistlConfig cfg)
        {
            Assert.That(cfg.ConfigName, Is.Not.Empty);
            Assert.That(cfg.SourceFileLocation, Is.Not.Empty);

            Assert.That(cfg.Client, Is.Not.Null);
            Assert.That(cfg.Client.DocumentStore, Is.Not.Empty);

            Assert.That(cfg.Server, Is.Not.Null);
            Assert.That(cfg.Server.ConnectionString, Is.Not.Empty);
            Assert.That(cfg.Server.DatabaseProvider, Is.Not.Empty);
            Assert.That(cfg.Server.DocumentStore, Is.Not.Empty);
        }

        [Test]
        public void Current()
        {
            Assert.That(KistlConfig.Current, Is.Not.Null);
            Assert.That(KistlConfig.Current.ConfigFilePath, Is.Not.Empty);
            Assert.That(KistlConfig.Current.ConfigName, Is.Not.Empty);
        }

        [Test]
        public void FromStream()
        {
            using (FileStream s = File.OpenRead(@"..\..\DefaultConfig.xml"))
            {
                KistlConfig cfg = KistlConfig.FromStream(s);
                Assert.That(cfg.ConfigFilePath, Is.Null);
                CheckConfig(cfg);
            }
        }

        [Test]
        public void FromTextReader()
        {
            using (FileStream s = File.OpenRead(@"..\..\DefaultConfig.xml"))
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
            KistlConfig.Current.ToFile(filename);
            Assert.That(File.Exists(filename), Is.True);
            Assert.That(new FileInfo(filename).Length, Is.GreaterThan(0));
            File.Delete(filename);
        }

        [Test]
        public void ToStream()
        {
            MemoryStream ms = new MemoryStream();
            KistlConfig.Current.ToStream(ms);
            Assert.That(ms.Length, Is.GreaterThan(0));
        }

    }
}
