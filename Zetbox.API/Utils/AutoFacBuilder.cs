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

namespace Zetbox.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Autofac;
    using Autofac.Core;
    using Zetbox.API.Configuration;

    public static class AutoFacBuilder
    {
        public static ContainerBuilder CreateContainerBuilder(ZetboxConfig config)
        {
            return CreateContainerBuilder(config, null);
        }

        public static ContainerBuilder CreateContainerBuilder(ZetboxConfig config, ZetboxConfig.Module[] modules)
        {
            if (config == null) throw new ArgumentNullException("config");

            var builder = new ContainerBuilder();

            // register the configuration
            builder
                .RegisterInstance(config)
                .ExternallyOwned()
                .SingleInstance();

            foreach (var m in (modules ?? new ZetboxConfig.Module[] { }).Where(i => config.IsFallback == false || i.NotOnFallback == false))
            {
                try
                {
                    Logging.Log.InfoFormat("Adding module [{0}]", m.TypeName);
#if MONO
                    // workaround for https://bugzilla.novell.com/show_bug.cgi?id=661461
                    var parts = m.Split(",".ToCharArray(), 2);
                    if (parts.Length == 2)
                    {
                        var assemblyName = parts[1];
                        System.Reflection.Assembly.Load(assemblyName);
                    }
#endif
                    builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(m.TypeName, true)));
                }
                catch (Exception ex)
                {
                    Logging.Log.Error(String.Format("Unable to register Module [{0}] from Config", m.TypeName), ex);
                    throw;
                }
            }
            return builder;
        }
    }
}
