using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.EF.Generator.Implementation
{
    public partial class RelationDebugTemplate
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, Relation rel)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Implementation.RelationDebugTemplate", ctx, rel);
        }
    }
}
