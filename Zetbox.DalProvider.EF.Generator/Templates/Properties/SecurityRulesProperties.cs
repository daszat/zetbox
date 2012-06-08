
namespace Kistl.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Generator;

    public partial class SecurityRulesProperties
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }

            string assocName = Construct.SecurityRulesFKName(cls);
            string targetRoleName = Construct.SecurityRulesClassName(cls);
            string referencedImplementation = Construct.SecurityRulesClassName(cls) + host.Settings["extrasuffix"] + Kistl.API.Helper.ImplementationSuffix;
            string efNameRightsPropertyName = "SecurityRightsCollection" + Kistl.API.Helper.ImplementationSuffix;

            host.CallTemplate("Properties.SecurityRulesProperties", ctx, cls,
                assocName,
                targetRoleName,
                referencedImplementation,
                efNameRightsPropertyName);
        }
    }
}
