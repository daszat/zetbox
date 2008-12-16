using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    public partial class Template
    {

        protected ObjectClass DataType { get { return this.dataType; } }

        protected virtual string GetAdditionalImports()
        {
            return "";
        }

        protected virtual string MungeClassName(string name) { return name; }

        /// <summary>
        /// Is called to apply optional decoration in front of the class declaration, like Attributes.
        /// </summary>
        protected virtual void ApplyClassAttributeTemplate() { }

        protected virtual void ApplyIDPropertyTemplate() { }

        /// <returns>The base class to inherit from.</returns>
        protected virtual string GetBaseClass()
        {
            var baseClass = dataType.BaseObjectClass;
            if (baseClass != null)
            {
                return baseClass.Module.Namespace + "." + baseClass.ClassName;
            }
            else
            {
                return "";
            }
        }

        /// <returns>The interfaces this class implements</returns>
        protected virtual string[] GetInterfaces()
        {
            return new string[] { };
        }

        /// <returns>a string defining the inheritance relations of this class</returns>
        protected virtual string GetInheritance()
        {
            string baseClass = GetBaseClass();
            string[] interfaces = GetInterfaces();
            if (!String.IsNullOrEmpty(baseClass) && interfaces.Length > 0)
            {
                return ": " + baseClass + ", " + String.Join(", ", interfaces);
            }
            else if (!String.IsNullOrEmpty(baseClass) && interfaces.Length == 0)
            {
                return ": " + baseClass;
            }
            else if (String.IsNullOrEmpty(baseClass) && interfaces.Length > 0)
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
                this.Host.CallTemplate("Server.ObjectClasses.NotifyingValueProperty", p.GetPropertyType(), p.PropertyName);
            }
        }

        protected virtual void ApplyMethodTemplate(Method m)
        {
            //this.Host.CallTemplate(ResolveResourceUrl("Server.ObjectClasses.Method.cst"), m);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return dataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
