using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class ProjektActions
    {
        [Invocation]
        public static void ToString(Projekt obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }
    }
}
