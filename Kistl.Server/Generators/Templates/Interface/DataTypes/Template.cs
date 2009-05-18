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

        /// <returns>The interfaces this type implements</returns>
        protected virtual string[] GetInterfaces()
        {
            if (dataType is Kistl.App.Base.ObjectClass)
            {
                var baseClass = (dataType as Kistl.App.Base.ObjectClass).BaseObjectClass;
                if (baseClass != null)
                {
                    return new string[] { baseClass.Module.Namespace + "." + baseClass.ClassName };
                }
                else
                {
                    return new string[] { "IDataObject" };
                }
            }
            else if (dataType is Struct)
            {
                return new string[] { "IStruct" };
            }
            else if (dataType is Kistl.App.Base.Interface)
            {
                // no type hierarchy here
                return new string[] { };
            }
            else
            {
                throw new ApplicationException(String.Format("Do not know how to handle {0}", dataType));
            }
        }

        /// <returns>a string defining the inheritance relations of this class</returns>
        protected virtual string GetInheritance()
        {
            string[] interfaces = GetInterfaces();
            if (interfaces.Length > 0)
            {
                return ": " + String.Join(", ", interfaces);
            }
            else
            {
                return "";
            }
        }

        protected virtual void ApplyPropertyTemplate(Property p)
        {
            if (!p.IsListProperty())
            {
                bool isReadonly = p is StructProperty;
                Interface.DataTypes.SimplePropertyTemplate.Call(Host, ctx, p, isReadonly);
            }
            else
            {
                Interface.DataTypes.SimplePropertyListTemplate.Call(Host, ctx, p);
            }
        }

        protected virtual void ApplyMethodTemplate(Kistl.App.Base.Method m)
        {
            Interface.DataTypes.Method.Call(Host, ctx, m);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return dataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
