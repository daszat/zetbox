
namespace Kistl.DalProvider.Client.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Templates = Kistl.Generator.Templates;
    using Kistl.App.Base;

    public class Template : Templates.ObjectClasses.Template
    {
        public Template(Arebis.CodeGeneration.IGenerationHost _host, Kistl.API.IKistlContext ctx, ObjectClass cls)
            : base(_host, ctx, cls)
        {
        }

        protected override void ApplyMethodTemplate(Method m, int index)
        {
            if (m.InvokeOnServer == true)
            {
                ObjectClasses.InvokeServerMethod.Call(Host, ctx, this.DataType, m, index);
            }
            else
            {
                base.ApplyMethodTemplate(m, index);
            }
        }
    }
}
