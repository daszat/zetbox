using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class EnumerationPropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.EnumBinarySerialization", SerializerType.All, this.prop.Module.Namespace, this.prop.PropertyName, this.prop);
        }
    }
}
