using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace Zetbox.API.Common
{
    public interface IAssetsManager
    {
        ResourceManager GetResourceManager(Zetbox.App.Base.Module module, string baseName);
        string GetString(Zetbox.App.Base.Module module, string baseName, string key, string defaultString = null);

        void AddAssembly(string p, System.Reflection.Assembly a);
    }

    public class AssetsManager : IAssetsManager
    {
        private readonly Dictionary<string, System.Reflection.Assembly> _assetsAssemblies = new Dictionary<string, System.Reflection.Assembly>();
        private readonly Dictionary<string, ResourceManager> _resourceManagers = new Dictionary<string, ResourceManager>();
        private readonly object _lock = new object();

        public AssetsManager()
        {
        }

        private string ToKey(Zetbox.App.Base.Module module, string baseName)
        {
            return module.Name + "|" + baseName;
        }

        public ResourceManager GetResourceManager(Zetbox.App.Base.Module module, string baseName)
        {
            if (string.IsNullOrWhiteSpace(baseName)) throw new ArgumentNullException("baseName");
            if (module == null) throw new ArgumentNullException("module");

            ResourceManager rm = null;
            var key = ToKey(module, baseName);
            lock (_lock)
            {
                if (!_resourceManagers.TryGetValue(key, out rm))
                {
                    System.Reflection.Assembly assembly;
                    if (_assetsAssemblies.TryGetValue(module.Name, out assembly))
                    {
                        rm = new ResourceManager(assembly.GetSimpleName() + "." + baseName, assembly);
                    }
                    _resourceManagers[key] = rm;
                }
                return rm;
            }
        }

        public string GetString(Zetbox.App.Base.Module module, string baseName, string key, string defaultString = null)
        {
            try
            {
                var rm = GetResourceManager(module, baseName);
                if (rm == null) return defaultString;

                return rm.GetString(key);
            }
            catch
            {
                return defaultString;
            }
        }


        public void AddAssembly(string moduleName, System.Reflection.Assembly a)
        {
            _assetsAssemblies[moduleName] = a;
        }
    }
}
