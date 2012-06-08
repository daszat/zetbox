
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;

    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class MethodActions
    {
        [Invocation]
        public static void NotifyPreSave(Kistl.App.Base.Method obj)
        {
            // TODO: replace with constraint
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.Name))
            {
                throw new ArgumentException(string.Format("Method Name {0} has some illegal chars", obj.Name));
            }
        }

    }
}
