using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.App.Projekte
{
    public partial class CustomServerActions : API.ICustomServerActions
    {
        void Projekt_OnPreSetObject(System.Data.Linq.DataContext ctx, Projekt obj)
        {
            obj.Name += "_action";
        }

        void Task_OnPreSetObject(System.Data.Linq.DataContext ctx, Task obj)
        {
            if (obj.Aufwand < 0) throw new ApplicationException("Ungültiger Aufwand");
            if (obj.DatumBis < obj.DatumVon) throw new ApplicationException("Falsches Zeitalter");
        }

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
