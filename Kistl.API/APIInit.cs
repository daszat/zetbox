using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API
{
    [Serializable]
    public class APIInit : MarshalByRefObject
    {
        public void Init()
        {
            Init("");
        }

        public void Init(string configFile)
        {
            Configuration.KistlConfig.Init(configFile);
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(Kistl.API.AssemblyLoader.AssemblyResolve);
        }
    }
}
