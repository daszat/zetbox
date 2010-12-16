
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class EnumerationPropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list)
        {
            if (list != null)
                list.Add("Serialization.EnumBinarySerialization", Templates.Serialization.SerializerType.All, this.prop.Module.Namespace, this.prop.Name, this.prop);
        }
    }
}
