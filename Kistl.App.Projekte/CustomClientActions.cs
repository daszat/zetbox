using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Projekte
{
    public partial class CustomClientActions : API.ICustomClientActions
    {
        void Projekt_OnToString(Projekt obj, Kistl.API.ToStringEventArgs e)
        {
            e.Result = obj.Name;
        }

        void Task_OnToString(Task obj, Kistl.API.ToStringEventArgs e)
        {
            e.Result = string.Format("{0} [{1}] ({2} - {3})",
                obj.Name, obj.Aufwand, obj.DatumVon.ToShortDateString(), obj.DatumBis.ToShortDateString());
        }
    }
}
