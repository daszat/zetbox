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

        [Invocation]
        public static void postSet_Tasks(Projekt obj)
        {
            obj.Recalculate("AufwandGes");
        }

        [Invocation]
        public static void get_AufwandGes(Projekt obj, PropertyGetterEventArgs<double?> e)
        {
            e.Result = obj.Tasks.Sum(t => t.Aufwand);
        }
    }
}
