
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

    public class NotifyingValueProperty
        : Templates.Properties.NotifyingValueProperty
    {
        public NotifyingValueProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Templates.Serialization.SerializationMembersList serializationList, string type, string name, string modulenamespace, string backingName, bool isCalculated)
            : base(_host, ctx, serializationList, type, name, modulenamespace, "Proxy." + name, isCalculated)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;
        }

        protected override void ApplyBackingStoreDefinition()
        {
            // the proxy store the valur, so we don't need a local backing store
            // base.ApplyBackingStoreDefinition();
        }

        protected override void AddSerialization(Templates.Serialization.SerializationMembersList list, string name)
        {
            if (list != null)
            {
                Templates.Serialization.SimplePropertySerialization
                    .AddToSerializers(list, Templates.Serialization.SerializerType.All, modulenamespace, name, type, backingName);
            }
        }
    }
}
