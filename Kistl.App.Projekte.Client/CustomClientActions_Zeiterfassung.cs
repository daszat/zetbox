using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Zeiterfassung
{
    public class CustomClientActions_Zeiterfassung
    {

        public void OnToString_Kostentraeger(Kostentraeger obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // Do nothing
        }

        public void OnToString_Zeitkonto(Zeitkonto obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Kontoname;
        }

        public void OnToString_Kostenstelle(Kostenstelle obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // Do nothing
        }

        public void OnToString_TaetigkeitsArt(TaetigkeitsArt obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }
    }
}
