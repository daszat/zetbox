using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.ObjectClasses
{
    public partial class Template
    {
#if INTELLISENSE
        protected Arebis.CodeGeneration.IGenerationHost Host;
        protected string ResolveResourceUrl(string template) { return "mock";  }

        protected Kistl.App.Base.ObjectClass objClass;

        protected Template(Arebis.CodeGeneration.IGenerationHost h, Kistl.App.Base.ObjectClass objClass) { }
#endif
        protected virtual string GetBaseClass()
        {
            if (objClass.BaseObjectClass != null)
            {
                return " : " + objClass.BaseObjectClass.Module.Namespace + "." + objClass.BaseObjectClass.ClassName;
            }
            return "";
        }

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

        protected virtual void ApplyMethodTemplate(Method m)
        {
            this.Host.CallTemplate(ResolveResourceUrl("Interface.ObjectClasses.Method.cst"), m);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return objClass.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
