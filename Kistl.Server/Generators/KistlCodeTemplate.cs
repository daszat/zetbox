using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;

namespace Kistl.Server.Generators
{
    public class KistlCodeTemplate
        : CodeTemplate
    {
        public KistlCodeTemplate(IGenerationHost host)
            : base(host)
        {
        }

        public override void Generate()
        {
        }

        protected string ResolveResourceUrl(string template)
        {
            return String.Format("res://{0}/{1}.{2}", Settings["providertemplateassembly"], Settings["providertemplatenamespace"], template);
        }
    }
}
