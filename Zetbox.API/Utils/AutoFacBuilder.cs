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
    using System.IO;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Autofac.Core;
    using Mono.Cecil;
    using Zetbox.API.Configuration;

    public static class AutoFacBuilder
    {
        public static ContainerBuilder CreateContainerBuilder(ZetboxConfig config)
        {
            return CreateContainerBuilder(config, null);
        }

        public static ContainerBuilder CreateContainerBuilder(ZetboxConfig config, IEnumerable<ZetboxConfig.Module> modules)
        {
            if (config == null) throw new ArgumentNullException("config");

            var builder = new ContainerBuilder();

            // register the configuration
            builder
                .RegisterInstance(config)
                .ExternallyOwned()
                .SingleInstance();

            foreach (var m in (modules ?? new ZetboxConfig.Module[] { }))
            {
                try
                {
                    var moduleDescriptor = FindModule(config, m.TypeName);

                    if (moduleDescriptor == null)
                    {
                        throw new ConfigurationException(string.Format("Module loading aborted: cannot find '{0}'", m.TypeName));
                    }

                    if (config.IsFallback && moduleDescriptor.NotOnFallback)
                    {
                        Logging.Log.InfoFormat("Skipped module [{0}] in fallback mode", m.TypeName);
                    }
                    else
                    {
                        Logging.Log.InfoFormat("Adding module [{0}]", m.TypeName);
                        builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(moduleDescriptor.TypeName, true)));
                    }
                }
                catch (Exception ex)
                {
                    Logging.Log.Error(String.Format("Unable to register Module [{0}] from Config", m.TypeName), ex);
                    throw;
                }
            }

            foreach (var file in GetAssemblyFiles(config))
            {
                foreach (var m in FindModules(ModuleDefinition.ReadModule(file)).Where(m => m.IsAutoloaded))
                {
                    Logging.Log.InfoFormat("Adding autoloaded module [{0}]", m.TypeName);
                    builder.RegisterModule((IModule)Activator.CreateInstance(Type.GetType(m.TypeName, true)));
                }
            }
            return builder;
        }

        private static IEnumerable<string> GetAssemblyFiles(ZetboxConfig config)
        {
            if (config.AssemblySearchPaths == null || config.AssemblySearchPaths.Paths == null || config.AssemblySearchPaths.Paths.Length == 0)
                yield break;

            foreach (var searchPath in config.AssemblySearchPaths.Paths.Select(p => AssemblyLoader.QualifySearchPath(p)))
            {
                var files = Directory.GetFiles(searchPath, "*.dll").ToList();
                var additionalPath = searchPath + (config.IsFallback ? ".Fallback" : ".Generated");
                if (Directory.Exists(additionalPath))
                {
                    files.AddRange(Directory.GetFiles(additionalPath, "*.dll"));
                }

                foreach (var file in files.Where(f => !f.ToLowerInvariant().EndsWith(".resources.dll")))
                {
                    yield return file;
                }
            }
        }

        [CLSCompliant(false)]
        public static IEnumerable<ModuleDescriptor> FindModules(ModuleDefinition module)
        {
            if (module == null) throw new ArgumentNullException("module");

            return module.Types
                .SelectMany(t => t.AndChildren(p => p.NestedTypes))
                .Where(type => type.BaseType != null && type.BaseType.FullName == "Autofac.Module") // By string, as we do not want to rely on a specific autofac version
                .Select(type => CreateModuleDescriptor(type));
        }

        private static ModuleDescriptor CreateModuleDescriptor(TypeDefinition type)
        {
            return new ModuleDescriptor()
            {
                Type = type,
                TypeName = string.Format("{0}, {1}", type.FullName.Replace('/', '+'), type.Module.Assembly.Name.Name),
                Description = ExtractDescription(type),
                IsFeature = ExtractIsFeature(type),
                IsAutoloaded = ExtractIsAutoloaded(type),
                NotOnFallback = ExtractNotOnFallback(type)
            };
        }

        [CLSCompliant(false)]
        public static ModuleDescriptor FindModule(ZetboxConfig config, string typeName)
        {
            if (config == null) throw new ArgumentNullException("config");
            if (string.IsNullOrWhiteSpace(typeName)) throw new ArgumentNullException("typeName");

            var parts = typeName.Split(new[] { ',' }, 2);

            if (parts.Length != 2)
            {
                throw new ConfigurationException(string.Format("need an assembly qualified typename, not '{0}'", typeName));
            }

            parts[1] = parts[1].Trim().ToLowerInvariant();

            foreach (var file in GetAssemblyFiles(config))
            {
                if (Path.GetFileNameWithoutExtension(file).ToLowerInvariant() != parts[1])
                    continue;

                var module = ModuleDefinition.ReadModule(file);

                var typeDef = module.GetType(parts[0], true).Resolve();
                if (typeDef == null)
                {
                    throw new ConfigurationException(string.Format("Cannot find module '{0}'", typeName));
                }
                return CreateModuleDescriptor(typeDef);
            }

            return null;
        }

        private static string ExtractDescription(ICustomAttributeProvider type)
        {
            var attr = type.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == typeof(System.ComponentModel.DescriptionAttribute).FullName);

            if (attr != null && attr.ConstructorArguments.Count > 0)
            {
                return (string)attr.ConstructorArguments.First().Value;
            }
            return string.Empty;
        }

        private static bool ExtractIsFeature(ICustomAttributeProvider type)
        {
            var attr = type.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == typeof(Zetbox.API.Configuration.FeatureAttribute).FullName);
            return attr != null;
        }

        private static bool ExtractIsAutoloaded(ICustomAttributeProvider type)
        {
            var attr = type.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == typeof(Zetbox.API.Configuration.AutoLoadAttribute).FullName);
            return attr != null;
        }

        private static bool ExtractNotOnFallback(ICustomAttributeProvider type)
        {
            bool notOnFallback = false;
            var attr = type.CustomAttributes
                .FirstOrDefault(i => i.AttributeType.FullName == typeof(Zetbox.API.Configuration.FeatureAttribute).FullName
                                  || i.AttributeType.FullName == typeof(Zetbox.API.Configuration.AutoLoadAttribute).FullName);
            if (attr != null)
            {
                var namedArg = attr.Properties.Where(i => i.Name == "NotOnFallback").ToArray();
                if (namedArg.Length > 0)
                {
                    notOnFallback = namedArg[0].Argument.Value as bool? == true;
                }
            }
            return notOnFallback;
        }
    }

    [CLSCompliant(false)]
    public class ModuleDescriptor
    {
        public TypeDefinition Type { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public bool IsFeature { get; set; }
        public bool IsAutoloaded { get; set; }
        public bool NotOnFallback { get; set; }
    }
}
