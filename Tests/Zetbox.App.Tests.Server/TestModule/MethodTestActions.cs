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

namespace Zetbox.App.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Projekte;

    [Implementor]
    public static class MethodTestActions
    {
        // Do not implement this method -> a test will test for not implemented
        //[Invocation]
        //public static void ServerParameterless(MethodTest obj)
        //{
        //}

        [Invocation]
        public static System.Threading.Tasks.Task ServerObjParameter(MethodTest obj, MethodReturnEventArgs<TestObjClass> e, TestObjClass input)
        {
            var ctx = obj.Context;

            var newA = ctx.Create<TestObjClass>();
            newA.StringProp = "A";
            var kundeA = ctx.Create<Kunde>();
            kundeA.Kundenname = "Kunde A";
            kundeA.PLZ = "1210";
            newA.ObjectProp = kundeA;

            var newB = ctx.Create<TestObjClass>();
            newB.StringProp = "B";
            var kundeB = ctx.Create<Kunde>();
            kundeB.Kundenname = "Kunde";
            kundeB.PLZ = "1210";
            newB.ObjectProp = kundeB;

            e.Result = input ?? newA;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task ServerMethod(MethodTest obj)
        {

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
