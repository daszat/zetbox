using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation
{
    public partial class TypeBase
    {

        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return new string[] { };
        }

        protected string GetTypeName()
        {
            return MungeClassName(DataType.ClassName);
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

        /// <summary>
        /// Is called to apply the contents of the default constructor
        /// </summary>
        protected virtual void ApplyConstructorTemplate() { }

        /// <summary>
        /// is called to apply a optional tail part within the class
        /// </summary>
        protected virtual void ApplyClassTailTemplate() { }

        /// <summary>
        /// is called to apply a optional ApplyChangesFrom override
        /// </summary>
        protected virtual void ApplyApplyChangesFromMethod() { }

        protected virtual void ApplyAttachToContextMethod() { }

        protected virtual void ApplyIDPropertyTemplate() { }

        /// <returns>The base class to inherit from.</returns>
        protected virtual string GetBaseClass()
        {
            return "";
        }

        /// <returns>The interfaces this class implements</returns>
        protected virtual string[] GetInterfaces()
        {
            return new string[] { this.DataType.ClassName };
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

        protected virtual void ApplyMethodTemplate(Kistl.App.Base.Method m)
        {
            this.Host.CallTemplate("Implementation.ObjectClasses.Method", ctx, this.DataType, m);
        }

        /// <summary>
        /// A list of all methods that should be generated for this datatype. By default these are only the methods defined directly on this datatype.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Kistl.App.Base.Method> MethodsToGenerate()
        {
            return this.DataType.Methods; //.Where(m => !m.IsDefaultMethod());
        }

    }
}
