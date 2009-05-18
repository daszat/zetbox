using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.ClientObjects.Implementation.ObjectClasses
{
    public partial class AttachToContextTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx,
            ObjectClass dataType)
        {
            host.CallTemplate("Implementation.ObjectClasses.AttachToContextTemplate", ctx,
                dataType);
        }
    }
}
 