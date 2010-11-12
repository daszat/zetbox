
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class CompoundObjectPropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string memberName, string backingPropertyName)
        {
            if (list != null)
                list.Add("Serialization.CompoundObjectSerialization", Templates.Serialization.SerializerType.All, this.xmlNamespace, memberName, memberName, backingPropertyName);
        }
    }
}
