using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class Template
    {

        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return new string[] { };
        }

        protected virtual string MungeClassName(string name) { return name; }

        /// <summary>
        /// is called to apply a optional preamble in the global scope
        /// </summary>
        protected virtual void ApplyGlobalPreambleTemplate() { }

        /// <summary>
        /// is called to apply a optional preamble within the namespace
        /// </summary>
        protected virtual void ApplyNamespacePreambleTemplate() { }

        /// <summary>
        /// is called to apply a optional tail part within the namespace
        /// </summary>
        protected virtual void ApplyNamespaceTailTemplate() { }

        /// <summary>
        /// Is called to apply optional decoration in front of the class declaration, like Attributes.
        /// </summary>
        protected virtual void ApplyClassAttributeTemplate() { }

        protected virtual void ApplyIDPropertyTemplate() { }

        /// <returns>The base class to inherit from.</returns>
        protected virtual string GetBaseClass()
        {
            var baseClass = this.DataType.BaseObjectClass;
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
                this.Host.CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty", ctx, p.GetPropertyType(), p.PropertyName);
            }
            else
            {
                this.Host.CallTemplate("Implementation.ObjectClasses.ListProperty", ctx, this.DataType, p.GetPropertyType(), p.PropertyName, p);
            }
        }

        protected virtual void ApplyMethodTemplate(Kistl.App.Base.Method m)
        {
            this.Host.CallTemplate("Implementation.ObjectClasses.Method", ctx, m);
        }

        protected IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return this.DataType.Methods.Where(m => !m.IsDefaultMethod());
        }
    }
}
