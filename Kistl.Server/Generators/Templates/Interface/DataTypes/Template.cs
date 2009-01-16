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
                this.Host.CallTemplate("Interface.DataTypes.SimplePropertyTemplate", ctx, p);
            }
            else
            {
                this.Host.CallTemplate("Interface.DataTypes.SimplePropertyListTemplate", ctx, p);
            }
        }

        protected virtual void ApplyMethodTemplate(Kistl.App.Base.Method m)
        {
            this.Host.CallTemplate("Interface.DataTypes.Method", ctx, m);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return dataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
