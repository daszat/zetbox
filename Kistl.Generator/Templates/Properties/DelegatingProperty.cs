
namespace Kistl.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

    public partial class DelegatingProperty
    {
        public static void Call(Arebis.CodeGeneration.IGenerationHost host, IKistlContext ctx, string propName, string presentedType, string backingPropName, string backingType)
        {
            if (host == null) { throw new ArgumentNullException("host"); }

            host.CallTemplate("Properties.DelegatingProperty", ctx, propName, presentedType, backingPropName, backingType);
        }
    }
}
