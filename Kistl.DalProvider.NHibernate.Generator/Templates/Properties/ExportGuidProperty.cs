
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

    public class ExportGuidProperty
        : Templates.Properties.ExportGuidProperty
    {
        public ExportGuidProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx,
            Templates.Serialization.SerializationMembersList serializationList)
            : base(_host, ctx, serializationList)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;
        }
    }
}
