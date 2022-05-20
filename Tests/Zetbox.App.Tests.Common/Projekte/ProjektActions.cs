// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.API;
using Zetbox.App.Extensions;

namespace Zetbox.App.Projekte
{
    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class ProjektActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(Projekt obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetFulltextIndexBody(Projekt obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name + "\n" + obj.Kundenname;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_Tasks(Projekt obj)
        {
            if (obj.Context.IsCurrentlyImporting()) return System.Threading.Tasks.Task.CompletedTask;

            obj.Recalculate("AufwandGes");

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_KickOffAm(Projekt obj, PropertyPostSetterEventArgs<DateTime> e)
        {
            if (obj.Context.IsCurrentlyImporting()) return System.Threading.Tasks.Task.CompletedTask;

            if (obj.KickOffBis.HasValue)
            {
                obj.KickOffBis = e.NewValue.Date + obj.KickOffBis.Value.TimeOfDay;
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task preSet_KickOffBis(Projekt obj, PropertyPreSetterEventArgs<DateTime?> e)
        {
            if (obj.Context.IsCurrentlyImporting()) return System.Threading.Tasks.Task.CompletedTask;

            if (e.NewValue.HasValue)
            {
                e.Result = obj.KickOffAm.Date + e.NewValue.Value.TimeOfDay;
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task get_AufwandGes(Projekt obj, PropertyGetterEventArgs<double?> e)
        {
            e.Result = obj.Tasks.Sum(t => t.Aufwand);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task isValid_KickOffBis(Projekt obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.KickOffBis == null || obj.KickOffBis >= obj.KickOffAm;
            e.Error = e.IsValid ? string.Empty : "Bis-Datum ist leer oder liegt vor dem Von-Datum";

            return System.Threading.Tasks.Task.CompletedTask;
        }

    }
}
