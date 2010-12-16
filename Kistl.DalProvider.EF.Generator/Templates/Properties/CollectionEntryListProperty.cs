
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Arebis.CodeGeneration;

    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;
    
    public partial class CollectionEntryListProperty
    {
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            if (list != null && eagerLoading)
            {
                list.Add("Serialization.EagerLoadingSerialization", Templates.Serialization.SerializerType.Binary, null, null, memberName, false);
            }
        }
    }
}
