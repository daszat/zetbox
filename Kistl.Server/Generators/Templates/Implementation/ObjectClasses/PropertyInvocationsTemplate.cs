using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class PropertyInvocationsTemplate
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host,
            IKistlContext ctx,
            Property p)
        {
            host.CallTemplate("Implementation.ObjectClasses.PropertyInvocationsTemplate", ctx, p);
        }
    }
}
