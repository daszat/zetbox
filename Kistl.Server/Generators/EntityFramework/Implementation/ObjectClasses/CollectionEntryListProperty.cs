using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class CollectionEntryListProperty
    {
        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            //if (list != null)
            //    list.Add("Implementation.ObjectClasses.CollectionSerialization", memberName);
        }
    }
}
