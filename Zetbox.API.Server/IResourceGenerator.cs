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

namespace Zetbox.API.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IResourceWriter : IDisposable
    {
        void AddResource(string name, byte[] value);
        void AddResource(string name, object value);
        void AddResource(string name, string value);
    }

    public delegate IResourceGenerator ResourceGeneratorFactory(string basePath);
    public interface IResourceGenerator
    {

        /// <summary>
        /// Adds a resource file with path but without extension
        /// </summary>
        /// <param name="name">filename with path but without extension</param>
        /// <returns></returns>
        IResourceWriter AddFile(string name);
    }

    public interface IResourceGeneratorTask
    {
        void Generate(IResourceGenerator generator, IZetboxServerContext ctx, IEnumerable<Zetbox.App.Base.Module> modules);
    }
}
