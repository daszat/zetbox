using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public partial class CompoundObjectPropertyTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, CompoundObjectProperty prop, string propName)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.CompoundObjectPropertyTemplate", ctx,
                list, prop, propName);
        }

        protected virtual void AddSerialization(Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, string memberName, string backingPropertyName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.CompoundObjectSerialization", Kistl.Server.Generators.Templates.Implementation.SerializerType.All, this.prop.Module.Namespace, memberName, memberName, backingPropertyName);
        }
    }
}
