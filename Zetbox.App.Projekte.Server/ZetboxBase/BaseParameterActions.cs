namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class BaseParameterActions
    {
        [Invocation]
        public static void NotifyPreSave(Kistl.App.Base.BaseParameter obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.Name))
            {
                throw new ArgumentException(string.Format("Name {0} has some illegal chars", obj.Name));
            }

            // TODO: replace with constraint
            if (obj.Method != null && obj.Method.Parameter.Count(p => p.IsReturnParameter) > 1)
            {
                throw new ArgumentException(string.Format("Method {0}.{1}.{2} has more then one Return Parameter",
                    obj.Method.ObjectClass.Module.Namespace,
                    obj.Method.ObjectClass.Name,
                    obj.Method.Name));
            }
        }
    }
}
