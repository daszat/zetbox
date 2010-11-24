
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

    public class PropertyEvents
        : Templates.Properties.PropertyEvents
    {
        public PropertyEvents(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string eventName, string propType, string objType, bool hasGetters, bool hasSetters)
            : base(_host, ctx, eventName, propType, objType, hasGetters, hasSetters)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;
        }
    }
}
