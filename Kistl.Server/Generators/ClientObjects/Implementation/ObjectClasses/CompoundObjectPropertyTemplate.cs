
namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Server.Generators.Templates;

    public partial class CompoundObjectPropertyTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            CompoundObjectProperty prop, string propName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (prop == null) { throw new ArgumentNullException("prop"); }

            string backingStoreName = "_" + propName + "Store";

            string coType = prop.GetPropertyTypeString();

            host.CallTemplate("Implementation.ObjectClasses.CompoundObjectPropertyTemplate", ctx,
                serializationList, prop, propName, backingStoreName, coType);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName, string backingPropertyName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.CompoundObjectSerialization", Templates.Implementation.SerializerType.All, this.prop.Module.Namespace, memberName, memberName, backingPropertyName);
        }
    }
}
