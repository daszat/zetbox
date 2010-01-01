using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class EnumerationPropertyTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Templates.Implementation.SerializationMembersList list, EnumerationProperty prop, bool callGetterSetterEvents)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.EnumerationPropertyTemplate", ctx,
                list, prop, callGetterSetterEvents);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.EnumBinarySerialization", SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, this.prop);
        }
    }
}
