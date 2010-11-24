
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

    public class DelegatingProperty
        : Templates.Properties.DelegatingProperty
    {
        public DelegatingProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string propName, string presentedType, string backingPropName, string backingType)
            : base(_host, ctx, propName, presentedType, backingPropName, backingType)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;
        }
    }
}
