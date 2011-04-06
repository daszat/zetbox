
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
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string efName)
        {
            if (list != null)
            {
                bool hasPersistentOrder = prop is ValueTypeProperty
                    ? ((ValueTypeProperty)prop).HasPersistentOrder
                    : ((CompoundObjectProperty)prop).HasPersistentOrder;
                Serialization.CollectionSerialization.Add(list, ctx, this.prop.Module.Namespace, this.prop.Name, efName, !hasPersistentOrder, true);
            }
        }
    }
}
