
namespace Kistl.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Templates = Kistl.Generator.Templates;

    public partial class AttachToContextTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx,
            ObjectClass dataType)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("ObjectClasses.AttachToContextTemplate", ctx,
                dataType);
        }
    }
}
