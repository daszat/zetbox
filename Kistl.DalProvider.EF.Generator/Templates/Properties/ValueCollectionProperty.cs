
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class ValueCollectionProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, Templates.Serialization.SerializationMembersList list, Property prop)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.ValueCollectionProperty", ctx,
                list, prop);
        }

        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string efName)
        {
            if (list != null)
            {
                bool hasPersistentOrder = prop is ValueTypeProperty ? ((ValueTypeProperty)prop).HasPersistentOrder : ((CompoundObjectProperty)prop).HasPersistentOrder;
                list.Add("Serialization.CollectionSerialization", Templates.Serialization.SerializerType.All, this.prop.Module.Namespace, this.prop.Name, efName, !hasPersistentOrder);
            }
        }
    }
}
