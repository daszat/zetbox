using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class ValueCollectionProperty
    {
        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string wrapperName)
        {
            if (list != null)
            {
                list.Add("Implementation.ObjectClasses.CollectionSerialization", wrapperName + ".UnderlyingCollection");
                list.Add("Implementation.ObjectClasses.CollectionSerialization", wrapperName + ".DeletedCollection");
            }
        }
    }
}
