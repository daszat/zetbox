
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

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
