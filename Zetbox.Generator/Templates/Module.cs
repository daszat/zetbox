
namespace Zetbox.Generator.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial class Module
    {
        protected virtual IEnumerable<string> GetAdditionalImports()
        {
            return RequiredNamespaces
                .Concat(new string[]{
                    "Zetbox.App.Extensions"
                });
        }

        protected virtual void ApplyRegistrations()
        {
            Registrations.Call(Host, ctx, shortName);
        }

        protected virtual void ApplyTypeCheckerTemplate()
        {
            TypeChecker.Call(Host, ctx, shortName);
        }
    }
}
