using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.App.Zeiterfassung
{
    public class CustomClientActions_Zeiterfassung
    {

        public void OnToString_Kostentraeger(Kostentraeger obj, MethodReturnEventArgs<string> e)
        {
            // Do nothing
        }

        public void OnToString_Zeitkonto(Zeitkonto obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Kontoname;
        }

        public void OnToString_Kostenstelle(Kostenstelle obj, MethodReturnEventArgs<string> e)
        {
            // Do nothing
        }

    }
}
