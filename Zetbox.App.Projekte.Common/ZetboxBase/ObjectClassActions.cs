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
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    [Implementor]
    public static class ObjectClassActions
    {
        [Invocation]
        public static System.Threading.Tasks.Task get_CodeTemplate(ObjectClass obj, PropertyGetterEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            string type = obj.Name;

            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task ToString({0} obj, MethodReturnEventArgs<string> e)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task NotifyPreSave({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task NotifyPostSave({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task NotifyCreated({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task NotifyDeleting({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task ObjectIsValid({0} obj, ObjectIsValidEventArgs e)\n{{\n}}\n\n", type);

            e.Result = sb.ToString();

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task NotifyPreSave(Zetbox.App.Base.ObjectClass obj)
        {

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task NotifyCreated(Zetbox.App.Base.ObjectClass obj)
        {

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_BaseObjectClass(Zetbox.App.Base.ObjectClass obj, PropertyPostSetterEventArgs<Zetbox.App.Base.ObjectClass> e)
        {

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task CreateRelation(ObjectClass obj, MethodReturnEventArgs<Relation> e)
        {
            e.Result = obj.Context.Create<Relation>();
            e.Result.Module = obj.Module;

            if (e.Result.A == null)
            {
                e.Result.A = obj.Context.Create<RelationEnd>();
            }
            e.Result.A.Type = obj;

            if (e.Result.B == null)
            {
                e.Result.B = obj.Context.Create<RelationEnd>();
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task CreateMethod(ObjectClass obj, MethodReturnEventArgs<Method> e)
        {
            e.Result = obj.Context.Create<Method>();
            e.Result.Module = obj.Module;
            e.Result.ObjectClass = obj;

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static async System.Threading.Tasks.Task GetInheritedMethods(ObjectClass obj, MethodReturnEventArgs<IEnumerable<Method>> e)
        {
            ObjectClass baseObjectClass = obj.BaseObjectClass;
            if (baseObjectClass != null)
            {
                e.Result = (await baseObjectClass.GetInheritedMethods()).Concat(baseObjectClass.Methods);
            }
            else
            {
                e.Result = new List<Method>();
            }
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetName(ObjectClass obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("Base.Classes.{0}.{1}", obj.Module.Namespace, obj.Name);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task isValid_TableMapping(ObjectClass obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.TableMapping == null || (obj.TableMapping != null && obj.BaseObjectClass == null);
            e.Error = e.IsValid ? string.Empty : "TableMapping is valid only on base classes.";

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
