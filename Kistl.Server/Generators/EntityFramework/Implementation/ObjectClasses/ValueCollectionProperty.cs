using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Templates.Implementation;
using Arebis.CodeGeneration;
using Kistl.API;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class ValueCollectionProperty
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Templates.Implementation.SerializationMembersList list, ValueTypeProperty prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.ValueCollectionProperty", ctx,
                list, prop);
        }

        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string efName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.CollectionSerialization", SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, efName);
        }
    }
}
