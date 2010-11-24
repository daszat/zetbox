
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

    public class NotifyingDataProperty
        : Templates.Properties.NotifyingDataProperty
    {
        public NotifyingDataProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx,Templates. Serialization.SerializationMembersList serializationList, Property prop)
            : base(_host, ctx, serializationList, prop)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;
        }
    }
}
