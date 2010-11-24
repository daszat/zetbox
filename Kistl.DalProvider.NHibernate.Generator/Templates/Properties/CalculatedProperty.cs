
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

    public class CalculatedProperty
        : Templates.Properties.CalculatedProperty
    {
        public CalculatedProperty(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, string className, string referencedType, String propertyName, String getterEventName)
            : base(_host, ctx, className, referencedType, propertyName, getterEventName)
        {
        }

        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes memberAttributes)
        {
            return base.ModifyMemberAttributes(memberAttributes) & ~MemberAttributes.Final;
        }
    }
}
