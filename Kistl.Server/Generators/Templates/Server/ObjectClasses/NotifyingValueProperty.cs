using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Server.Generators.Extensions;

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

        protected string GetModifiers()
        {
            MemberAttributes attrs = ModifyMethodAttributes(MemberAttributes.Public);
            return attrs.ToCsharp();
        }

        /// <summary>
        /// Gets a set of <see cref="MethodAttributs"/> and returns an appropriate set for output.
        /// </summary>
        protected virtual MemberAttributes ModifyMethodAttributes(MemberAttributes methodAttributes)
        {
            return methodAttributes;
        }
    }
}
