// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

    /// <summary>
    /// The basic structural template for all complex types.
    /// </summary>
    public partial class TypeBase
    {
        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return RequiredNamespaces;
        }

        protected string GetTypeName()
        {
            return MungeClassName(DataType.Name);
        }

        protected virtual string MungeClassName(string name) { return name + ImplementationSuffix; }

        protected virtual string GetClassModifiers() { return string.Empty; }

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
        /// Is called to apply additional infrastructure to the beginning of the class.
        /// </summary>
        protected virtual void ApplyClassHeadTemplate() { }

        /// <summary>
        /// Is called to apply the constructors for this class.
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

        protected virtual void ApplySetNewMethod() { }

        /// <returns>The base class to inherit from.</returns>
        protected virtual string GetBaseClass()
        {
            return String.Empty;
        }

        /// <returns>The interfaces this class implements</returns>
        protected virtual string[] GetInterfaces()
        {
            if (DataType is ObjectClass && ((ObjectClass)DataType).ImplementsIExportable(false))
            {
                return new string[] { this.DataType.Name, "Zetbox.API.IExportableInternal" };
            }
            return new string[] { this.DataType.Name };
        }

        /// <returns>a string defining the inheritance relations of this class</returns>
        protected virtual string GetInheritance()
        {
            string baseClass = GetBaseClass();
            string[] interfaces = GetInterfaces().OrderBy(s => s).ToArray();
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
                return String.Empty;
            }
        }

        protected virtual string GetExportGuidBackingStoreReference()
        {
            return "this._ExportGuid";
        }

        protected virtual void ApplyMethodTemplate(Method m, int index)
        {
            ObjectClasses.Method.Call(Host, ctx, this.DataType, m, index);
        }

        /// <summary>
        /// A list of all methods that should be generated for this datatype. By default these are only the methods defined directly on this datatype.
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Zetbox.App.Base.Method> MethodsToGenerate()
        {
            return this.DataType.Methods;
        }
    }
}
