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
        public static void get_CodeTemplate(ObjectClass obj, PropertyGetterEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            string type = obj.Name;

            sb.AppendFormat("[Invocation]\npublic static void ToString({0} obj, MethodReturnEventArgs<string> e)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static void NotifyPreSave({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static void NotifyPostSave({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static void NotifyCreated({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static void NotifyDeleting({0} obj)\n{{\n}}\n\n", type);
            sb.AppendFormat("[Invocation]\npublic static void ObjectIsValid({0} obj, ObjectIsValidEventArgs e)\n{{\n}}\n\n", type);

            e.Result = sb.ToString();
        }

        [Invocation]
        public static void NotifyPreSave(Zetbox.App.Base.ObjectClass obj)
        {
        }

        [Invocation]
        public static void NotifyCreated(Zetbox.App.Base.ObjectClass obj)
        {
        }

        [Invocation]
        public static void postSet_BaseObjectClass(Zetbox.App.Base.ObjectClass obj, PropertyPostSetterEventArgs<Zetbox.App.Base.ObjectClass> e)
        {
        }

        [Invocation]
        public static void CreateRelation(ObjectClass obj, MethodReturnEventArgs<Relation> e)
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
        }

        [Invocation]
        public static void CreateMethod(ObjectClass obj, MethodReturnEventArgs<Method> e)
        {
            e.Result = obj.Context.Create<Method>();
            e.Result.Module = obj.Module;
            e.Result.ObjectClass = obj;
        }

        [Invocation]
        public static void GetInheritedMethods(ObjectClass obj, MethodReturnEventArgs<IEnumerable<Method>> e)
        {
            ObjectClass baseObjectClass = obj.BaseObjectClass;
            if (baseObjectClass != null)
            {
                e.Result = baseObjectClass.GetInheritedMethods().Concat(baseObjectClass.Methods);
            }
            else
            {
                e.Result = new List<Method>();
            }
        }

        [Invocation]
        public static void GetName(ObjectClass obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("Base.Classes.{0}.{1}", obj.Module.Namespace, obj.Name);
        }

        [Invocation]
        public static void isValid_TableMapping(ObjectClass obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = obj.TableMapping == null || (obj.TableMapping != null && obj.BaseObjectClass == null);
            e.Error = e.IsValid ? string.Empty : "TableMapping is valid only on base classes.";
        }
    }
}
