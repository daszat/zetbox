using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.Templates.Server.ObjectClasses
{
    public partial class NotifyingValueProperty
    {

        /// <summary>
        /// Is called to apply optional decoration in front of the property declaration, like Attributes.
        /// </summary>
        protected virtual void ApplyAttributeTemplate() { }

        protected virtual string MungeNameToBacking(string name)
        {
            return "_" + name;
        }

    }
}
