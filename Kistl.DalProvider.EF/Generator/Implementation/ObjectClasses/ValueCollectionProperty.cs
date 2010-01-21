using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public partial class ValueCollectionProperty
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, Property prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.ValueCollectionProperty", ctx,
                list, prop);
        }

        protected virtual void AddSerialization(Kistl.Server.Generators.Templates.Implementation.SerializationMembersList list, string efName)
        {
            if (list != null)
            {
                bool hasPersistentOrder = prop is ValueTypeProperty ? ((ValueTypeProperty)prop).HasPersistentOrder : ((CompoundObjectProperty)prop).HasPersistentOrder;
                list.Add("Implementation.ObjectClasses.CollectionSerialization", Kistl.Server.Generators.Templates.Implementation.SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, efName, !hasPersistentOrder);
            }
        }
    }
}
