using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class StructPropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            if (list != null)
                list.Add("Implementation.ObjectClasses.StructSerialization", SerializerType.All, memberName);
        }
    }
}
