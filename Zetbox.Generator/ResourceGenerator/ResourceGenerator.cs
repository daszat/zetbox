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

namespace Zetbox.Generator.ResourceGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.API.Utils;

    internal class ResourceCmdLineHandler
    {
        private readonly static log4net.ILog Log = Logging.Server;

        private readonly ILifetimeScope _container;
        private readonly IEnumerable<IResourceGeneratorTask> _tasks;

        public ResourceCmdLineHandler(ILifetimeScope container, IEnumerable<IResourceGeneratorTask> tasks)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (tasks == null) throw new ArgumentNullException("tasks");

            _container = container;
            _tasks = tasks;
        }

        public void Generate(string[] arg)
        {
            var ctx = _container.Resolve<IZetboxServerContext>();
            var config = _container.Resolve<ZetboxConfig>();

            var modules = GetModules(ctx, arg);

            var basePath = System.IO.Path.Combine(config.Server.CodeGenOutputPath, "Assets");
            System.IO.Directory.CreateDirectory(basePath);

            var gen = _container.Resolve<ResourceGeneratorFactory>().Invoke(basePath);
            foreach (var task in _tasks)
            {
                task.Generate(gen, ctx, modules);
            }
        }

        private static Zetbox.App.Base.Module[] GetModules(IReadOnlyZetboxContext ctx, string[] moduleNames)
        {
            var moduleList = new List<Zetbox.App.Base.Module>();
            if (moduleNames.Contains("*"))
            {
                moduleList.AddRange(ctx.GetQuery<Zetbox.App.Base.Module>());
            }
            else
            {
                foreach (var name in moduleNames)
                {
                    var module = ctx.GetQuery<Zetbox.App.Base.Module>().Where(m => m.Name == name).FirstOrDefault();
                    if (module == null)
                    {
                        Log.WarnFormat("Module {0} not found, skipping entry", name);
                        continue;
                    }
                    moduleList.Add(module);
                }
            }
            return moduleList.OrderBy(m => m.Name).ToArray();
        }
    }

    internal class ResourceGenerator : IResourceGenerator
    {
        private string _basePath;
        public ResourceGenerator(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath)) throw new ArgumentNullException("basePath");

            _basePath = basePath;
        }

        public IResourceWriter AddFile(string name)
        {
            var fileName = System.IO.Path.Combine(_basePath, name + ".resx");
            var dirName = System.IO.Path.GetDirectoryName(fileName);
            System.IO.Directory.CreateDirectory(dirName);

            return new ResXWriter(fileName);
        }
    }

    internal class ResXWriter : IResourceWriter
    {
        private readonly XmlTextWriter _writer;

        public ResXWriter(string fileName)
        {
            _writer = new XmlTextWriter(fileName, Encoding.UTF8);
            _writer.Formatting = Formatting.Indented;
            _writer.WriteStartElement("root");

            _writer.WriteStartElement("resheader");
            _writer.WriteAttributeString("name", "resmimetype");
            _writer.WriteElementString("value", "text/microsoft-resx");
            _writer.WriteEndElement();

            _writer.WriteStartElement("resheader");
            _writer.WriteAttributeString("name", "version");
            _writer.WriteElementString("value", "2.0");
            _writer.WriteEndElement();

            _writer.WriteStartElement("resheader");
            _writer.WriteAttributeString("name", "reader");
            _writer.WriteElementString("value", "System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            _writer.WriteEndElement();

            _writer.WriteStartElement("resheader");
            _writer.WriteAttributeString("name", "writer");
            _writer.WriteElementString("value", "System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
            _writer.WriteEndElement();
        }

        public void AddResource(string name, byte[] value)
        {
            throw new NotImplementedException();
        }

        public void AddResource(string name, object value)
        {
            throw new NotImplementedException();
        }

        public void AddResource(string name, string value)
        {
            // 
            _writer.WriteStartElement("data");
            _writer.WriteAttributeString("name", name);
            _writer.WriteAttributeString("space", "preserve");
            _writer.WriteElementString("value", value ?? string.Empty);
            _writer.WriteEndElement();
        }

        public void Dispose()
        {
            _writer.WriteEndElement();
            _writer.Flush();
            _writer.Close();
            _writer.Dispose();
        }
    }
}
