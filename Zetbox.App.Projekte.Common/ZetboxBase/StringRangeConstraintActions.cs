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
    public static class StringRangeConstraintActions
    {
        [Invocation]
        public static void ToString(StringRangeConstraint obj, Zetbox.API.MethodReturnEventArgs<string> e)
        {
            string maxLength = obj.MaxLength != null ? obj.MaxLength.ToString() : "unlimited";
            // Only display min if there is an actual restriction.
            if (obj.MinLength > 0)
            {
                e.Result = String.Format("{0} should have at least {1} and at most {2} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.Name,
                    obj.MinLength,
                    maxLength);
            }
            else
            {
                e.Result = String.Format("{0} should have at most {1} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.Name,
                    maxLength);
            }
        }

        [Invocation]
        public static void IsValid(
            StringRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            int length = (constrainedValueParam ?? String.Empty).ToString().Length;
            e.Result = length == 0 || ((obj.MinLength <= length) && (length <= (obj.MaxLength ?? int.MaxValue)));
        }

        [Invocation]
        public static void GetErrorText(
            StringRangeConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {

            if (obj.IsValid(constrainedObjectParam, constrainedValueParam))
            {
                e.Result = null;
            }
            else
            {
                constrainedValueParam = (constrainedValueParam ?? String.Empty);
                int length = constrainedValueParam.ToString().Length;
                StringBuilder result = new StringBuilder();
                if (length < obj.MinLength)
                    result.AppendFormat("{0} should be at least {1} characters long", obj.ConstrainedProperty.Name, obj.MinLength);
                if (obj.MaxLength != null && length > obj.MaxLength)
                    result.AppendFormat("{0} should be at most {1} characters long", obj.ConstrainedProperty.Name, obj.MaxLength);

                if (!String.IsNullOrEmpty(obj.Reason))
                {
                    result.Append(": ");
                    result.Append(obj.Reason);
                }

                e.Result = result.ToString();
            }
        }
    }
}
