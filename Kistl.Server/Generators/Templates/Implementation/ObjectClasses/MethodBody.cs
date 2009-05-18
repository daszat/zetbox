using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    public partial class MethodBody
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, DataType dataType, Kistl.App.Base.Method m)
        {
            host.CallTemplate("Implementation.ObjectClasses.MethodBody", ctx, dataType, m);
        }
    }
}
