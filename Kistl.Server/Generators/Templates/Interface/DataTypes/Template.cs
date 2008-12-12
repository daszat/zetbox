using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Interface.DataTypes
{
    public partial class Template
    {
#if INTELLISENSE
        protected Arebis.CodeGeneration.IGenerationHost Host;
        protected string ResolveResourceUrl(string template) { return "mock";  }

        protected Kistl.App.Base.DataType dataType;

        protected Template(Arebis.CodeGeneration.IGenerationHost h, Kistl.App.Base.DataType objClass) { }
#endif
        protected virtual string GetBaseClass()
        {
            if (dataType is Kistl.App.Base.ObjectClass)
            {
                var baseClass = (dataType as Kistl.App.Base.ObjectClass).BaseObjectClass;
                if (baseClass != null)
                {
                    return ": " + baseClass.Module.Namespace + "." + baseClass.ClassName;
                }
                else
                {
                    return ": IDataObject";
                }
            }
            else if (dataType is Struct)
            {
                return ": IStruct";
            }
            else if (dataType is Kistl.App.Base.Interface)
            {
                // no type hierarchy here
                return "";
            }
            else
            {
                throw new ApplicationException(String.Format("Do not know how to handle {0}", dataType));
            }

        }

        protected virtual void ApplyPropertyTemplate(Property p)
        {
            if (!p.IsListProperty())
            {
                this.Host.CallTemplate(ResolveResourceUrl("Interface.DataTypes.SimplePropertyTemplate.cst"), p);
            }
            else
            {
                this.Host.CallTemplate(ResolveResourceUrl("Interface.DataTypes.SimplePropertyListTemplate.cst"), p);
            }
        }

        protected virtual void ApplyMethodTemplate(Method m)
        {
            this.Host.CallTemplate(ResolveResourceUrl("Interface.DataTypes.Method.cst"), m);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return dataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
