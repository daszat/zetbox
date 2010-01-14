using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public partial class StructPropertyTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, StructProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.StructPropertyTemplate", ctx,
                list, prop);
        }

        protected virtual void AddSerialization(Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, string memberName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.StructSerialization", Kistl.Server.Generators.Templates.Implementation.SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, memberName);
        }
    }
}
