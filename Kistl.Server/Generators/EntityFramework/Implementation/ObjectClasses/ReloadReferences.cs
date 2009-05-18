using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class ReloadReferences
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, DataType dataType)
        {
            host.CallTemplate("Implementation.ObjectClasses.ReloadReferences", ctx, dataType);
        }
    }
}
