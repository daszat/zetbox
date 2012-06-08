using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;
using Kistl.API;

namespace Kistl.App.Projekte
{
    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class TaskActions
    {
        /// <summary>
        /// Überprüfung eines Tasks - sinnlos, aber ganz lustig.
        /// </summary>
        [Invocation]
        public static void NotifyPreSave(Task obj)
        {
            if (obj.Aufwand < 0) throw new ArgumentOutOfRangeException("obj", "Ungültiger Aufwand");
            if (obj.DatumBis < obj.DatumVon) throw new ArgumentOutOfRangeException("obj", "Falsches Zeitalter");
        }
    }
}
