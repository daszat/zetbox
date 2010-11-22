
namespace Kistl.DalProvider.NHibernate.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public class Method
        : Templates.ObjectClasses.Method
    {
        public Method(Arebis.CodeGeneration.IGenerationHost _host, IKistlContext ctx, Kistl.App.Base.DataType dt, Kistl.App.Base.Method m, int index, string indexSuffix, string eventName)
            : base(_host, ctx, dt, m, index, indexSuffix, eventName)
        {
        }

        protected override IEnumerable<string> GetMethodAttributes()
        {
            string virt = "override";
            if (dt is ObjectClass && ((ObjectClass)dt).BaseObjectClass == null)
            {
                virt = "virtual";
            }
            return base.GetMethodAttributes().Concat(new[] { virt });
        }
    }
}
