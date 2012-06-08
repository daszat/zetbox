using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API.Server;
using Zetbox.API;

namespace Zetbox.App.Projekte
{
    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class ProjektActions
    {
        [Invocation]
        public static void NotifyPreSave(Projekt obj)
        {
        }
    }
}
