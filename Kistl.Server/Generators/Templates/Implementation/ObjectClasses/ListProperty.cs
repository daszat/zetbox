using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Server.Generators.Extensions;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class ListProperty
    {
        /// <summary>
        /// Is called to insert requisites into the containing class, like wrappers or similar.
        /// </summary>
        protected virtual void ApplyRequisitesTemplate() { }

        /// <summary>
        /// Is called to apply optional decoration in front of the property declaration, like Attributes.
        /// </summary>
        protected virtual void ApplyAttributesTemplate() { }

        protected virtual string BackingMemberFromName(string name)
        {
            return "_" + name;
        }

        protected virtual void ApplySettor()
        {
        }

        protected virtual void AddSerialization(SerializationMembersList list, string name)
        {
            //if (list != null)
            //    list.Add("Implementation.ObjectClasses.CollectionSerialization", BackingMemberFromName(name));
        }

        protected virtual string GetModifiers()
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

        /// <returns>the type of the property as string</returns>
        protected virtual string GetPropertyTypeString()
        {
            return property.GetCollectionTypeString();
        }

        /// <returns>the type of the backing store as string</returns>
        protected virtual string GetBackingTypeString()
        {
            return GetPropertyTypeString();
        }

        /// <returns>an expression which can be used to initialise the backing store</returns>
        protected virtual string GetInitialisationExpression()
        {
            return String.Format("new List<{0}>()", property.GetPropertyTypeString());
        }
    }
}
