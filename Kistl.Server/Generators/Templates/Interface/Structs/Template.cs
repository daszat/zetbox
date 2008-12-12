using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.Structs
{
    public partial class Template
    {
#if INTELLISENSE
        protected Arebis.CodeGeneration.IGenerationHost Host;
        protected string ResolveResourceUrl(string template) { return "mock"; }
#endif
        protected virtual void ApplyPropertyTemplate(Property p)
        {
            if (!p.IsListProperty())
            {
                this.Host.CallTemplate(ResolveResourceUrl("Interface.ObjectClasses.SimplePropertyTemplate.cst"), p);
            }
            else
            {
                this.Host.CallTemplate(ResolveResourceUrl("Interface.ObjectClasses.SimplePropertyListTemplate.cst"), p);
            }
        }
    }
}
