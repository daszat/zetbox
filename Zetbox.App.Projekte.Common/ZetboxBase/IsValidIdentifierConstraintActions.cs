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
    public static class IsValidIdentifierConstraintActions
    {
        [Invocation]
        public static void ToString(IsValidIdentifierConstraint obj, Zetbox.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "Method names, property names, enum names etc. must be valid names.";
        }

        [Invocation]
        public static void IsValid(
                   IsValidIdentifierConstraint obj,
                   MethodReturnEventArgs<bool> e,
                   object constrainedObjectParam,
                   object constrainedValueParam)
        {
            e.Result = (constrainedValueParam != null) &&
                System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier((string)constrainedValueParam);
        }

        [Invocation]
        public static void GetErrorText(
            IsValidIdentifierConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = string.Format("'{0}' is not a valid identifier", constrainedValueParam);
        }
    }
}
