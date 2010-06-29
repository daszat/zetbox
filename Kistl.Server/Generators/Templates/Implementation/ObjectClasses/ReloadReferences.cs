
namespace Kistl.Server.Generators.Templates.Implementation.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Arebis.CodeGeneration;
    
    using Kistl.API;
    using Kistl.App.Base;

    public partial class ReloadReferences
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, DataType dataType)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.ObjectClasses.ReloadReferences", ctx, dataType);
        }
    }
}
