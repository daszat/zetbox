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

namespace Zetbox.App.Projekte
{
    /// <summary>
    /// Server implementation
    /// </summary>
    [Implementor]
    public static class ProjektActions
    {
        [Invocation]
        public static void ToString(Projekt obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void postSet_Tasks(Projekt obj)
        {
            obj.Recalculate("AufwandGes");
        }

        [Invocation]
        public static void postSet_KickOffAm(Projekt obj, PropertyPostSetterEventArgs<DateTime> e)
        {
            if (obj.KickOffBis.HasValue)
            {
                obj.KickOffBis = e.NewValue.Date + obj.KickOffBis.Value.TimeOfDay;
            }
        }

        [Invocation]
        public static void preSet_KickOffBis(Projekt obj, PropertyPreSetterEventArgs<DateTime?> e)
        {
            if (e.NewValue.HasValue)
            {
                e.Result = obj.KickOffAm.Date + e.NewValue.Value.TimeOfDay;
            }
        }

        [Invocation]
        public static void get_AufwandGes(Projekt obj, PropertyGetterEventArgs<double?> e)
        {
            e.Result = obj.Tasks.Sum(t => t.Aufwand);
        }

        [Invocation]
        public static void isValid_KickOffBis(Projekt obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.KickOffBis == null || obj.KickOffBis >= obj.KickOffAm;
            e.Error = e.IsValid ? string.Empty : "Bis-Datum ist leer oder liegt vor dem Von-Datum";
        }

    }
}
