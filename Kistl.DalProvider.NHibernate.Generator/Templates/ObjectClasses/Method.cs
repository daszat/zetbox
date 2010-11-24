
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public class Method
        : Templates.ObjectClasses.Method
    {
        public Method(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, DataType dt, Kistl.App.Base.Method m, int index, string indexSuffix, string eventName)
            : base(_host, ctx, dt, m, index, indexSuffix, eventName)
        {
        }

        protected override MemberAttributes ModifyMethodAttributes(MemberAttributes methodAttributes)
        {
            var baseAttrs = base.ModifyMethodAttributes(methodAttributes);
            if (dt is ObjectClass)
            {
                if (((ObjectClass)dt).BaseObjectClass != null)
                    baseAttrs = baseAttrs & ~MemberAttributes.Final | MemberAttributes.Override;
                else
                    baseAttrs = baseAttrs & ~MemberAttributes.Final;
            }
            return baseAttrs;
        }
    }
}
