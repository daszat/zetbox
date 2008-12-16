using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators
{
    public class TemplateFactory
    {
        public static TemplateFactory CreateFactory(string baseNameSpace, string providerNameSpace)
        {
            return new TemplateFactory(baseNameSpace, providerNameSpace);
        }

        public string BaseNameSpace { get; private set; }
        public string ProviderNameSpace { get; private set; }
        protected TemplateFactory(string baseNameSpace, string providerNameSpace)
        {
            BaseNameSpace = baseNameSpace;
            ProviderNameSpace = providerNameSpace;
        }

        public KistlCodeTemplate GetTemplate(string name, object[] args)
        {
            var t = Type.GetType(String.Format("{0}.{1}", ProviderNameSpace, name));

            if (t == null)
            {
                t = Type.GetType(String.Format("{0}.{1}", BaseNameSpace, name));
            }

            return (KistlCodeTemplate)Activator.CreateInstance(t, args);
        }
    }
}
