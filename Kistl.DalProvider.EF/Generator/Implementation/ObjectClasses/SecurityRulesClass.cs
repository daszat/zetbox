using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.Server.Generators;
using Arebis.CodeGeneration;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.DalProvider.EF.Generator.Implementation.ObjectClasses
{
    public partial class SecurityRulesClass
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }
            host.CallTemplate("Implementation.ObjectClasses.SecurityRulesClass", ctx, cls,
                Construct.SecurityRulesFKName(cls),
                Construct.SecurityRulesClassName(cls),
                Construct.SecurityRulesClassName(cls) + Kistl.API.Helper.ImplementationSuffix);
        }

    }
}
