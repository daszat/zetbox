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
	using System.Threading.Tasks;
	using Zetbox.API;
    using Zetbox.API.Common;
    using Zetbox.App.Extensions;

    [Implementor]
    public class PropertyActions
    {
        private static IAssetsManager _assets;
        public PropertyActions(IAssetsManager assets)
        {
            _assets = assets;
        }

        internal static async Task DecorateElementType(Property obj, MethodReturnEventArgs<string> e, bool isStruct)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (e == null) throw new ArgumentNullException("e");

            if (isStruct && await obj.IsNullable())
            {
                e.Result += "?";
            }
        }

        internal static void DecorateParameterType(Property obj, MethodReturnEventArgs<string> e, bool isStruct, bool isList, bool isOrdered)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (e == null) throw new ArgumentNullException("e");

            if (isList && isOrdered)
            {
                e.Result = string.Format("IList<{0}>", e.Result);
            }
            else if (isList && !isOrdered)
            {
                e.Result = string.Format("ICollection<{0}>", e.Result);
            }
        }

        internal static void DecorateParameterType(Property obj, MethodReturnEventArgs<Type> e, bool isStruct, bool isList, bool isOrdered)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (e == null) throw new ArgumentNullException("e");

            if (isList && isOrdered)
            {
                e.Result = typeof(IList<>).MakeGenericType(e.Result);
            }
            else if (isList && !isOrdered)
            {
                e.Result = typeof(ICollection<>).MakeGenericType(e.Result);
            }
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetLabel(Zetbox.App.Base.Property obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;

            if (obj.Module == null || obj.ObjectClass == null)
                return System.Threading.Tasks.Task.CompletedTask;

            e.Result = _assets.GetString(obj.Module, ZetboxAssetKeys.ConstructBaseName(obj), ZetboxAssetKeys.ConstructLabelKey(obj), e.Result);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetDescription(Property obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Description;

            if (obj.Module == null || obj.ObjectClass == null)
                return System.Threading.Tasks.Task.CompletedTask;

            e.Result = _assets.GetString(obj.Module, ZetboxAssetKeys.ConstructBaseName(obj), ZetboxAssetKeys.ConstructDescriptionKey(obj), e.Result);

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static async Task ToString(Property obj, MethodReturnEventArgs<string> e)
        {
            var cls = await obj.GetProp_ObjectClass();
            if (cls == null)
            {
                e.Result = String.Join(" ", new[] { "unattached", obj.Name });
            }
            else
            {
                e.Result = String.Format("{0} {1}.{2}",
                    obj.GetPropertyTypeString(),
                    cls.Name,
                    obj.Name);
            }
            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetPropertyType(Zetbox.App.Base.Property obj, Zetbox.API.MethodReturnEventArgs<System.Type> e)
        {
            throw new NotImplementedException();
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetPropertyTypeString(Zetbox.App.Base.Property obj, Zetbox.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement Property.GetPropertyTypeString()>";

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_Name(Property obj, PropertyPostSetterEventArgs<string> e)
        {
            obj.Recalculate("CodeTemplate");

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_ObjectClass(Property obj, PropertyPostSetterEventArgs<DataType> e)
        {
            obj.Recalculate("CodeTemplate");

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task postSet_Constraints(Property obj)
        {
            obj.Recalculate("CodeTemplate");

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static async System.Threading.Tasks.Task get_CodeTemplate(Property obj, PropertyGetterEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            string type = obj.ObjectClass != null ? obj.ObjectClass.Name : "<<TYPE>>";
            string propType = await obj.GetPropertyTypeString();

            sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "get", obj.Name, type, "PropertyGetterEventArgs", propType);
            if (!obj.IsCalculated())
            {
                sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "preSet", obj.Name, type, "PropertyPreSetterEventArgs", propType);
                sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "postSet", obj.Name, type, "PropertyPostSetterEventArgs", propType);
                sb.AppendFormat("[Invocation]\npublic static System.Threading.Tasks.Task {0}_{1}({2} obj, {3} e)\n{{\n\te.IsValid = obj.{1} == ...;\n\te.Error = e.IsValid ? string.Empty : \"<Error>\";\n}}\n\n", "isValid", obj.Name, type, "PropertyIsValidEventArgs");
            }

            e.Result = sb.ToString();
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetName(Property obj, MethodReturnEventArgs<string> e)
        {
            var cls = obj.ObjectClass as ObjectClass;
            if (cls != null)
            {
                e.Result = string.Format("{0}_Properties.{1}", cls.GetName(), obj.Name);
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task isValid_Name(Property obj, PropertyIsValidEventArgs e)
        {
            e.IsValid = true;
            var dataType = obj.ObjectClass;
            while (dataType != null)
            {
                if (dataType.Properties.Where(p => p != obj && p.Name == obj.Name).Count() > 0)
                {
                    e.IsValid = false;
                }

                var cls = dataType as ObjectClass;
                if (cls != null)
                {
                    dataType = cls.BaseObjectClass;
                }
                else
                {
                    break;
                }
            }
            e.Error = e.IsValid ? string.Empty : "Propertyname is not unique";

            return System.Threading.Tasks.Task.CompletedTask;
        }

        [Invocation]
        public static System.Threading.Tasks.Task isValid_DefaultSortPriority(Property obj, PropertyIsValidEventArgs e)
        {
            var cls = obj.ObjectClass;
            if (cls == null || obj.DefaultSortPriority == null)
            {
                e.IsValid = true;
                return System.Threading.Tasks.Task.CompletedTask;
            }

            var other = cls.Properties
                                .Except(new[] { obj })
                                .Where(p => p.DefaultSortPriority == obj.DefaultSortPriority)
                                .ToList();
            e.IsValid = other.Count == 0;
            e.Error = e.IsValid 
                ? string.Empty 
                : string.Format("{0} other property/ies have the same default sort priority: {1}", other.Count, string.Join(", ", other.Select(p => p.Name)));

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
