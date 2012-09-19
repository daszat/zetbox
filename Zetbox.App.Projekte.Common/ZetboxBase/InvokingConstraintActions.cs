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
    using Zetbox.API.Common;

    [Implementor]
    public static class InvokingConstraintActions
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Common.CustomActions");

        private static readonly Type[] ObjectObject = new Type[] { typeof(object), typeof(object) };
        private delegate void InvocationPrototype(object constrainedObject, object constrainedValue); 
        
        [Invocation]
        public static void IsValid(InvokingConstraint obj, MethodReturnEventArgs<bool> e, object constrainedObject, object constrainedValue)
        {
            if (obj.IsValidInvocation.HasValidInvocation())
            {
                e.Result = obj.IsValidInvocation.CallInvocation<bool>(typeof(InvocationPrototype), constrainedObject, constrainedValue);
            }
        }

        [Invocation]
        public static void GetErrorText(InvokingConstraint obj, MethodReturnEventArgs<string> e, object constrainedObject, object constrainedValue)
        {
            if (obj.GetErrorTextInvocation.HasValidInvocation())
            {
                e.Result = obj.GetErrorTextInvocation.CallInvocation<string>(typeof(InvocationPrototype), constrainedObject, constrainedValue);
            }
        }
    }
}
