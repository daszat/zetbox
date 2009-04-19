using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class ValueCollectionProperty
    {
        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string efName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.CollectionSerialization", SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, efName);
        }
    }
}
