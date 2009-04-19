using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators.Templates.Implementation;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class ObjectReferencePropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Implementation.SerializationMembersList list, string memberName)
        {
            // TODO: XML Namespace
            if (list != null)
                list.Add("Implementation.ObjectClasses.SimplePropertySerialization", SerializerType.All, "http://dasz.at/Kistl", name, memberName);
        }
    }
}
