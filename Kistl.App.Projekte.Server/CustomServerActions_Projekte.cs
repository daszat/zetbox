using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.App.Projekte
{
    public static class CustomServerActions_Projekte
    {
        /// <summary>
        /// PreSave für Projekte, beim Projektnamen "_action" hinzufügen.
        /// Sinnlos, aber ganz lustig
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="obj"></param>
        public static void OnPreSetObject_Projekt(Projekt obj)
        {
            if (obj.ObjectState == Kistl.API.DataObjectState.Modified)
            {
                obj.AufwandGes = obj.Tasks.Sum(t => t.Aufwand);
            }
        }

        /// <summary>
        /// Überprüfung eines Tasks - sinnlos, aber ganz lustig.
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="obj"></param>
        public static void OnPreSetObject_Task(Task obj)
        {
            if (obj.Aufwand < 0) throw new ArgumentOutOfRangeException("obj", "Ungültiger Aufwand");
            if (obj.DatumBis < obj.DatumVon) throw new ArgumentOutOfRangeException("obj", "Falsches Zeitalter");
        }
    }
}
