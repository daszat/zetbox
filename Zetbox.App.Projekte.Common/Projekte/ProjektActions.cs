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
        public static void postSet_KickOffAm(Projekt obj, PropertyPostSetterEventArgs<DateTime> e)
        {
            if (obj.KickOffBis.HasValue)
            {
                obj.KickOffBis = e.NewValue.Date + obj.KickOffBis.Value.TimeOfDay;
            }
        }

        [Invocation]
        public static void preSet_KickOffBis(Projekt obj, PropertyPreSetterEventArgs<DateTime?> e)
        {
            if (e.NewValue.HasValue)
            {
                e.Result = obj.KickOffAm.Date + e.NewValue.Value.TimeOfDay;
            }
        }

        [Invocation]
        public static void get_AufwandGes(Projekt obj, PropertyGetterEventArgs<double?> e)
        {
            e.Result = obj.Tasks.Sum(t => t.Aufwand);
        }

        [Invocation]
        public static void isValid_KickOffBis(Projekt obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.KickOffBis == null || obj.KickOffBis >= obj.KickOffAm;
            e.Error = e.IsValid ? string.Empty : "Bis-Datum ist leer oder liegt vor dem Von-Datum";
        }

    }
}
