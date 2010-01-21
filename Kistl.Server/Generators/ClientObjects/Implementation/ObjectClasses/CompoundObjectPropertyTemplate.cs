using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class CompoundObjectPropertyTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx,
            Templates.Implementation.SerializationMembersList serializationList,
            CompoundObjectProperty prop, string propName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.CompoundObjectPropertyTemplate", ctx,
                serializationList, prop, propName);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.CompoundObjectSerialization", SerializerType.All, this.prop.Module.Namespace, memberName, memberName);
        }
    }
}
