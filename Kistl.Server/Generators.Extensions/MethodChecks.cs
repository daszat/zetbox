using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;

namespace Kistl.Server.Generators.Extensions
{
    public static class MethodChecks
    {
        /// <summary>
        /// returns true if the Method is one of the "default" methods, "ToString", "PreSave" or "PostSave".
        /// </summary>
        public static bool IsDefaultMethod(this Method method)
        {
            return (method.Module.ModuleName == "KistlBase")
                && (method.MethodName == "ToString"
                    || method.MethodName == "PreSave"
                    || method.MethodName == "PostSave");
        }
    }
}
