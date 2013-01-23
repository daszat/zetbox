
namespace Zetbox.ConfigEditor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Mono.Cecil;
    using Zetbox.API;

    public class ModuleType
    {
        public TypeDefinition Type { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public bool IsFeature { get; set; }
        public bool NotOnFallback { get; set; }
    }

    public class ModulesCache
    {
        private ModulesCache()
        {
        }

        private static ModulesCache _instance;
        public static ModulesCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ModulesCache();
                }
                return _instance;
            }
        }

        public ModuleType this[string typeName]
        {
            get
            {
                EnsureCache();
                if (_autofacModules.ContainsKey(typeName))
                    return _autofacModules[typeName];
                else
                    return new ModuleType() { TypeName = typeName };
            }
        }

        public bool Contains(string typeName)
        {
            EnsureCache();
            return _autofacModules.ContainsKey(typeName);
        }

        public IEnumerable<ModuleType> All
        {
            get
            {
                EnsureCache();
                return _autofacModules.Values;
            }
        }

        private Dictionary<string, ModuleDefinition> _modules;
        private Dictionary<string, ModuleType> _autofacModules;

        private void EnsureCache()
        {
            if (_modules == null)
            {
                _modules = new Dictionary<string, ModuleDefinition>();
                _autofacModules = new Dictionary<string, ModuleType>();
                foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "*.dll", SearchOption.AllDirectories))
                {
                    if (file.ToLowerInvariant().EndsWith(".resources.dll")) continue;
                    try
                    {
                        LoadAssembly(file);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void LoadAssembly(string file)
        {
            var cacheKey = FileToCacheKey(file);
            if (_modules.ContainsKey(cacheKey)) return;

            var module = _modules[cacheKey] = ModuleDefinition.ReadModule(file);

            foreach (var type in module.Types.SelectMany(t => t.AndChildren(p => p.NestedTypes)))
            {
                if (type.BaseType != null && type.BaseType.FullName == "Autofac.Module") // By string, as we do not want to rely on a specific autofac version
                {
                    var mt = new ModuleType()
                    {
                        Type = type,
                        TypeName = string.Format("{0}, {1}", type.FullName.Replace('/', '+'), module.Assembly.Name.Name),
                        Description = ExtractDescription(type),
                        IsFeature = ExtractIsFeature(type),
                        NotOnFallback = ExtractNotOnFallback(type)
                    };
                    _autofacModules[mt.TypeName] = mt;
                }
            }
        }

        private static string ExtractDescription(TypeDefinition type)
        {
            var attr = type.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == typeof(System.ComponentModel.DescriptionAttribute).FullName);

            if (attr != null && attr.ConstructorArguments.Count > 0)
            {
                return (string)attr.ConstructorArguments.First().Value;
            }
            return string.Empty;
        }

        private static bool ExtractIsFeature(TypeDefinition type)
        {
            var attr = type.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == typeof(Zetbox.API.Configuration.FeatureAttribute).FullName);
            return attr != null;
        }

        private static bool ExtractNotOnFallback(TypeDefinition type)
        {
            bool notOnFallback = false;
            var attr = type.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == typeof(Zetbox.API.Configuration.FeatureAttribute).FullName);
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

        private static string FileToCacheKey(string file)
        {
            return Path.GetFileNameWithoutExtension(file);
        }
    }
}
