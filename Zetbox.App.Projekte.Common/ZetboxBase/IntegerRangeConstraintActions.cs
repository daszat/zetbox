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
    public static class IntegerRangeConstraintActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task ToString(IntegerRangeConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} <= {1} <= {2}", obj.Min, obj.ConstrainedProperty == null ? "(no property)" : obj.ConstrainedProperty.Name, obj.Max);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task IsValid(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            if (constrainedValueParam != null)
            {
                int v = (int)constrainedValueParam;
                e.Result = (obj.Min <= v) && (v <= obj.Max);
            }
            else
            {
                // Accept null values -> other constraint will check for nulls
                e.Result = true;
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetErrorText(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            if (constrainedValueParam == null)
            {
                e.Result = null;
                return System.Threading.Tasks.Task.CompletedTask;
            }

            int v = (int)constrainedValueParam;
            if (obj.IsValid(constrainedObjectParam, constrainedValueParam))
            {
                e.Result = null;
            }
            else
            {
                StringBuilder result = new StringBuilder();
                if (v < obj.Min)
                    result.AppendFormat("{0} should be equal or greater than {1}", obj.ConstrainedProperty.Name, obj.Min);
                if (v > obj.Max)
                    result.AppendFormat("{0} should be equal or less than {1}", obj.ConstrainedProperty.Name, obj.Max);

                if (!String.IsNullOrEmpty(obj.Reason))
                {
                    result.Append(": ");
                    result.Append(obj.Reason);
                }

                e.Result = result.ToString();
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
