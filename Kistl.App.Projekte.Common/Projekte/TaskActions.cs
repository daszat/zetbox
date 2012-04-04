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
    public static class TaskActions
    {
        [Invocation]
        public static void ToString(Task obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void ObjectIsValid(Task obj, ObjectIsValidEventArgs e)
        {
            e.IsValid = true;
        }

        [Invocation]
        public static void postSet_Aufwand(Task obj, PropertyPostSetterEventArgs<double?> e)
        {
            if (obj.Projekt != null) obj.Projekt.NotifyPropertyChanged("AufwandGes", null, null);
        }

        [Invocation]
        public static void postSet_Projekt(Task obj, PropertyPostSetterEventArgs<Projekt> e)
        {
            if (e.OldValue != null) e.OldValue.NotifyPropertyChanged("AufwandGes", null, null);
            if (e.NewValue != null) e.NewValue.NotifyPropertyChanged("AufwandGes", null, null);
        }

        [Invocation]
        public static void isValid_Aufwand(Task obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.Aufwand >= 0;
            e.Error = e.IsValid ? string.Empty : "Ungültiger Aufwand";
        }

        [Invocation]
        public static void isValid_DatumBis(Task obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.DatumBis >= obj.DatumVon;
            e.Error = e.IsValid ? string.Empty : "Falsches Zeitalter";
        }

        [Invocation]
        public static void isValid_DatumVon(Task obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.DatumBis >= obj.DatumVon;
            e.Error = e.IsValid ? string.Empty : "Falsches Zeitalter";
        }
    }
}
