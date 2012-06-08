// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.API.Tests
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
    using Zetbox.API.Configuration;
    using NUnit.Framework;

    [TestFixture]
    public class ConfigurationTests
        : AbstractApiTestFixture
    {
        readonly static string ConfigFile = "Zetbox.API.Tests.xml";

        private void CheckConfig(ZetboxConfig cfg)
        {
            Assert.That(cfg.ConfigName, Is.Not.Empty, "ConfigName");
            Assert.That(cfg.AssemblySearchPaths, Is.Not.Null, "AssemblySearchPaths");
            Assert.That(cfg.AssemblySearchPaths.Paths, Is.Not.Empty, "AssemblySearchPaths");

            Assert.That(cfg.Client, Is.Not.Null, "Client");

            Assert.That(cfg.Server, Is.Not.Null, "Server");
            var connectionString = cfg.Server.GetConnectionString(Helper.ZetboxConnectionStringKey);
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
            var config = ZetboxConfig.FromFile(String.Empty, "DefaultConfig.xml");

            Assert.That(config, Is.Not.Null, "Configuration");
            Assert.That(config.ConfigFilePath, Is.Not.Empty, "ConfigFilePath");
            Assert.That(config.ConfigName, Is.Not.Empty, "ConfigName");
        }

        [Test]
        public void LoadFile()
        {
            var config = ZetboxConfig.FromFile("TestConfig.xml", "DoesNotExist.xml");

            Assert.That(config, Is.Not.Null, "Configuration");
            Assert.That(config.ConfigFilePath, Is.EqualTo("TestConfig.xml"), "ConfigFilePath");
            Assert.That(config.ConfigName, Is.Not.Empty, "ConfigName");
        }

        [Test]
        public void FromStream()
        {
            var filename = ZetboxConfig.GetDefaultConfigName(ConfigFile);
            Assert.That(filename, Is.Not.Empty);
            Assert.That(File.Exists(filename), Is.True, String.Format("configfile {0} doesn't exist", filename));

            using (FileStream s = File.OpenRead(filename))
            {
                
                ZetboxConfig cfg = ZetboxConfig.FromStream(s);
                Assert.That(cfg, Is.Not.Null);
                Assert.That(cfg.ConfigFilePath, Is.Null);
                CheckConfig(cfg);
            }
        }

        [Test]
        public void FromTextReader()
        {
            using (FileStream s = File.OpenRead(ZetboxConfig.GetDefaultConfigName(ConfigFile)))
            {
                TextReader rd = new StreamReader(s);
                ZetboxConfig cfg = ZetboxConfig.FromStream(rd);
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
            var config = ZetboxConfig.FromFile(String.Empty, "DefaultConfig.xml");
            config.ToFile(filename);
            Assert.That(File.Exists(filename), Is.True);
            Assert.That(new FileInfo(filename).Length, Is.GreaterThan(0));
            File.Delete(filename);
        }

        [Test]
        public void ToStream()
        {
            MemoryStream ms = new MemoryStream();
            var config = new ZetboxConfig();
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
