
namespace Zetbox.ConfigEditor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Mono.Cecil;
    using Zetbox.API;
    using Zetbox.API.Utils;


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

        public ModuleDescriptor this[string typeName]
        {
            get
            {
                EnsureCache();
                if (_autofacModules.ContainsKey(typeName))
                    return _autofacModules[typeName];
                else
                    return new ModuleDescriptor() { TypeName = typeName };
            }
        }

        public bool Contains(string typeName)
        {
            EnsureCache();
            return _autofacModules.ContainsKey(typeName);
        }

        public IEnumerable<ModuleDescriptor> All
        {
            get
            {
                EnsureCache();
                return _autofacModules.Values;
            }
        }

        private Dictionary<string, ModuleDefinition> _modules;
        private Dictionary<string, ModuleDescriptor> _autofacModules;

        private void EnsureCache()
        {
            if (_modules == null)
            {
                _modules = new Dictionary<string, ModuleDefinition>();
                _autofacModules = new Dictionary<string, ModuleDescriptor>();
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

            foreach (var m in AutoFacBuilder.FindModules(module))
            {
                _autofacModules[m.TypeName] = m;
            }
        }

        private static string FileToCacheKey(string file)
        {
            return Path.GetFileNameWithoutExtension(file);
        }
    }
}
