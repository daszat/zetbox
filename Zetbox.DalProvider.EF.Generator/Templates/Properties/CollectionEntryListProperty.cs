
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Arebis.CodeGeneration;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Templates = Zetbox.Generator.Templates;

    public partial class CollectionEntryListProperty
    {
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list, string memberName, bool eagerLoading)
        {
            if (list != null && eagerLoading)
            {
                string efName = memberName + ImplementationPropertySuffix;
                list.Add("Serialization.EagerLoadingSerialization", Templates.Serialization.SerializerType.Binary, null, null, memberName, false, rel.GetRelationType() == RelationType.n_m, efName);
            }
        }
    }
}
