using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Server.Generators.EntityFramework.Implementation.ObjectClasses
{
    public partial class EfListWrapper
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, string nameImpl, string relAssociationName, string relEndRoleName,
            string relEndDataTypeStringImpl)
        {
            host.CallTemplate("Implementation.ObjectClasses.EfListWrapper", ctx,
                nameImpl,
                relAssociationName, relEndRoleName, relEndDataTypeStringImpl);
        }
    }
}
