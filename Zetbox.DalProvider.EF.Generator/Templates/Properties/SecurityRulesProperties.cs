
namespace Zetbox.DalProvider.Ef.Generator.Templates.Properties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Generator;

    public partial class SecurityRulesProperties
    {
        public static void Call(IGenerationHost host, IZetboxContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }

            string assocName = Construct.SecurityRulesFKName(cls);
            string targetRoleName = Construct.SecurityRulesClassName(cls);
            string referencedImplementation = Construct.SecurityRulesClassName(cls) + host.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix;
            string efNameRightsPropertyName = "SecurityRightsCollection" + Zetbox.API.Helper.ImplementationSuffix;

            host.CallTemplate("Properties.SecurityRulesProperties", ctx, cls,
                assocName,
                targetRoleName,
                referencedImplementation,
                efNameRightsPropertyName);
        }
    }
}
