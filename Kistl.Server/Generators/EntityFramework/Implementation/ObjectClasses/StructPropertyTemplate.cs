using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;
using Kistl.App.Base;
using Kistl.API;
using Arebis.CodeGeneration;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class StructPropertyTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Templates.Implementation.SerializationMembersList list, StructProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.StructPropertyTemplate", ctx,
                list, prop);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.StructSerialization", SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, memberName);
        }
    }
}
