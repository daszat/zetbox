
namespace Zetbox.DalProvider.Ef.Generator.Templates.ObjectClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator;

    public partial class SecurityRulesClass
    {
        public static void Call(IGenerationHost host, IZetboxContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }
            host.CallTemplate("ObjectClasses.SecurityRulesClass", ctx, cls,
                Construct.SecurityRulesFKName(cls),
                Construct.SecurityRulesClassName(cls),
                Construct.SecurityRulesClassName(cls) + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix);
        }

    }
}
