
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Templates = Zetbox.Generator.Templates;

    public partial class EnumerationPropertyTemplate
    {
        protected virtual void AddSerialization(Templates.Serialization.SerializationMembersList list)
        {
            if (list != null)
            {
                var backingStoreName = String.Format("(({0})this).{1}",
                    prop.ObjectClass.Module.Namespace + "." + prop.ObjectClass.Name,
                    prop.Name);
                Templates.Serialization.EnumBinarySerialization.AddToSerializers(list, prop, backingStoreName);
            }
        }
    }
}
