
namespace Kistl.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;

    public class ResourceTemplate
        : CodeTemplate
    {
        public ResourceTemplate(IGenerationHost host)
            : base(host)
        {
        }

        public override void Generate()
        {
        }

        /// <summary>
        /// An implementation suffix for properties. This is used in the QueryTranslator.
        /// </summary>
        public string ImplementationPropertySuffix
        {
            get { return Kistl.API.Helper.ImplementationSuffix; }
        }

        /// <summary>
        /// An implementation suffix for classes. This is only used in the local type transformations
        /// </summary>
        public string ImplementationSuffix
        {
            get { return this.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix; }
        }

        public string ImplementationNamespace
        {
            get { return this.Settings["implementationnamespace"]; }
        }

        public string[] RequiredNamespaces
        {
            get
            {
                var namespaces = this.Settings["namespaces"];
                if (String.IsNullOrEmpty(namespaces))
                {
                    return new string[0];
                }
                else
                {
                    return namespaces.Split(',');
                }
            }
        }

        protected string ResolveResourceUrl(string template)
        {
            return String.Format("res://{0}/{1}.{2}", Settings["providertemplateassembly"], Settings["providertemplatenamespace"], template);
        }
    }
}
