
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;

    public partial class EfListWrapper
    {
        public static void Call(
            Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx,
            string nameImpl,
            string relAssociationName, string relEndRoleName,
            string relEndDataTypeStringImpl)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.EfListWrapper", ctx,
                nameImpl,
                relAssociationName, relEndRoleName, relEndDataTypeStringImpl);
        }
    }
}
