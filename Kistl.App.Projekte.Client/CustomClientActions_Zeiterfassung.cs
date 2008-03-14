using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Zeiterfassung
{
    public class CustomClientActions_Zeiterfassung
    {
        public void OnToString_Taetigkeit(Kistl.App.Zeiterfassung.Taetigkeit obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Zeitkonto + ": " + obj.Datum.ToShortDateString() + ", " + obj.Dauer + "h";
        }

        public void OnToString_Kostentraeger(Kistl.App.Zeiterfassung.Kostentraeger obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // Do nothing
        }

        public void OnToString_Zeitkonto(Kistl.App.Zeiterfassung.Zeitkonto obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Kontoname;
        }

        public void OnToString_Kostenstelle(Kistl.App.Zeiterfassung.Kostenstelle obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // Do nothing
        }

        public void OnToString_TaetigkeitsArt(Kistl.App.Zeiterfassung.TaetigkeitsArt obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }
    }
}
