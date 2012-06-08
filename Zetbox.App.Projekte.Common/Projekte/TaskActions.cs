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
    public static class TaskActions
    {
        [Invocation]
        public static void ToString(Task obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;
        }

        [Invocation]
        public static void ObjectIsValid(Task obj, ObjectIsValidEventArgs e)
        {
            e.IsValid = true;
        }

        [Invocation]
        public static void postSet_Aufwand(Task obj, PropertyPostSetterEventArgs<double?> e)
        {
            if (obj.Projekt != null) obj.Projekt.Recalculate("AufwandGes");
        }

        [Invocation]
        public static void postSet_Projekt(Task obj, PropertyPostSetterEventArgs<Projekt> e)
        {
            if (e.OldValue != null) e.OldValue.Recalculate("AufwandGes");
            if (e.NewValue != null) e.NewValue.Recalculate("AufwandGes");
        }

        [Invocation]
        public static void isValid_Aufwand(Task obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.Aufwand >= 0;
            e.Error = e.IsValid ? string.Empty : "Ungültiger Aufwand";
        }

        [Invocation]
        public static void isValid_DatumVon(Task obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = !obj.IsInitialized("DatumBis") || obj.DatumBis >= obj.DatumVon || obj.DatumBis == null;
            e.Error = e.IsValid ? string.Empty : "Falsches Zeitalter";
        }

        [Invocation]
        public static void isValid_DatumBis(Task obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = !obj.IsInitialized("DatumVon") || obj.DatumBis >= obj.DatumVon || obj.DatumBis == null;
            e.Error = e.IsValid ? string.Empty : "Falsches Zeitalter";
        }
    }
}
