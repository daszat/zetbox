using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Implementation.CollectionEntries
{
    public abstract partial class Template
    {
        protected virtual void ApplyClassAttributeTemplate() { }

        protected abstract string GetCeClassName();

        /// <returns>The base class to inherit from.</returns>
        protected abstract string GetCeBaseClassName();

        /// <returns>which CollectionEntry interface to implement.</returns>
        protected abstract string GetCeInterface();

        /// <returns>The interfaces this class implements</returns>
        protected virtual string[] GetInterfaces()
        {
            return new string[] { GetCeInterface() };
        }

        /// <returns>a string defining the inheritance relations of this class</returns>
        protected virtual string GetInheritance()
        {
            string baseClass = GetCeBaseClassName();
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

        /// <returns>true, if any side is ordered</returns>
        protected abstract bool IsOrdered();

        protected virtual void ApplyClassTailTemplate() { }

    }
}
