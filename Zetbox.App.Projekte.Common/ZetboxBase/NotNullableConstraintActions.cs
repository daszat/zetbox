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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    [Implementor]
    public static class NotNullableConstraintActions
    {
        [Invocation]
        public static void ToString(NotNullableConstraint obj, Zetbox.API.MethodReturnEventArgs<string> e)
        {
            if (obj.ConstrainedProperty == null)
            {
                e.Result = String.Format("The ConstrainedProperty should not be NULL");
            }
            else
            {
                e.Result = String.Format("{0} should not be NULL", obj.ConstrainedProperty.Name);
            }
        }

        [Invocation]
        public static void IsValid(
            NotNullableConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            if (constrainedObjectParam is IDataObject)
            {
                var dataObj = (IDataObject)constrainedObjectParam;
                if (!dataObj.IsInitialized(obj.ConstrainedProperty.Name))
                {
                    e.Result = false;
                    return;
                }
            }
            e.Result = constrainedValueParam != null;
        }

        [Invocation]
        public static void GetErrorText(
            NotNullableConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = String.IsNullOrEmpty(obj.Reason) ? "Wert muss gesetzt sein" : String.Format("Wert muss gesetzt sein: {0}", obj.Reason);
        }
    }
}
