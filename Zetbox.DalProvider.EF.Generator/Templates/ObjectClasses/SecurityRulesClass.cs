
namespace Kistl.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Generator;

    public partial class SecurityRulesClass
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }
            host.CallTemplate("ObjectClasses.SecurityRulesClass", ctx, cls,
                Construct.SecurityRulesFKName(cls),
                Construct.SecurityRulesClassName(cls),
                Construct.SecurityRulesClassName(cls) + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix);
        }

    }
}
