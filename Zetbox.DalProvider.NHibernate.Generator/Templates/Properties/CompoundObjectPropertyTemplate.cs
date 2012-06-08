
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Properties
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class CompoundObjectPropertyTemplate
    {
        protected virtual void AddSerialization(
            Templates.Serialization.SerializationMembersList list,
            string memberType, string memberName,
            string backingStoreType, string backingStoreName)
        {
            if (list != null)
            {
                var xmlname = memberName;

                list.Add("Serialization.CompoundObjectSerialization", Templates.Serialization.SerializerType.All,
                    this.xmlNamespace, xmlname, memberType, memberName, backingStoreType, backingStoreName);
            }
        }
    }
}
