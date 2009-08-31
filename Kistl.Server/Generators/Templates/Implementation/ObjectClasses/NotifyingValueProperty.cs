using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Server.Generators.Extensions;
using Kistl.API;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class NotifyingValueProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx, SerializationMembersList serializationList, string type, string name, string modulenamespace)
        {
            host.CallTemplate("Implementation.ObjectClasses.NotifyingValueProperty",
                ctx, serializationList, type, name, modulenamespace);
        }

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

        protected virtual void AddSerialization(SerializationMembersList list, string name)
        {
            if (list != null)
                list.Add(SerializerType.All, modulenamespace, name, BackingMemberFromName(name));
        }

        protected string GetModifiers()
        {
            MemberAttributes attrs = ModifyMethodAttributes(MemberAttributes.Public);
            return attrs.ToCsharp();
        }

        /// <summary>
        /// Gets a set of <see cref="MemberAttributes"/> and returns an appropriate set for output.
        /// </summary>
        protected virtual MemberAttributes ModifyMethodAttributes(MemberAttributes methodAttributes)
        {
            return methodAttributes;
        }

        protected virtual void ApplyOnGetTemplate() { }
        protected virtual void ApplyPreSetTemplate() { }
        protected virtual void ApplyPostSetTemplate() { }
    }
}
