using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class EnumerationPropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.EnumBinarySerialization", this.prop);
        }
    }
}
