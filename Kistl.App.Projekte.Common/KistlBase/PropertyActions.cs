namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Extensions;

    [Implementor]
    public static class PropertyActions
    {
        internal static void DecorateElementType(Property obj, MethodReturnEventArgs<string> e, bool isStruct)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            if (e == null) throw new ArgumentNullException("e");

            if (isStruct && obj.IsNullable())
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
        public static void GetLabel(Kistl.App.Base.Property obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;
        }

        [Invocation]
        public static void ToString(Property obj, MethodReturnEventArgs<string> e)
        {
            if (obj.ObjectClass == null)
            {
                e.Result = String.Join(" ", new[] { "unattached", obj.Name });
            }
            else
            {
                e.Result = String.Format("{0} {1}.{2}",
                    obj.GetPropertyTypeString(),
                    obj.ObjectClass.Name,
                    obj.Name);
            }
            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetPropertyType(Kistl.App.Base.Property obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            throw new NotImplementedException();
        }

        [Invocation]
        public static void GetPropertyTypeString(Kistl.App.Base.Property obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "<Invalid Datatype, please implement Property.GetPropertyTypeString()>";
        }

        [Invocation]
        public static void postSet_Name(Property obj, PropertyPostSetterEventArgs<string> e)
        {
            obj.NotifyPropertyChanged("CodeTemplate", string.Empty, string.Empty);
        }

        [Invocation]
        public static void postSet_ObjectClass(Property obj, PropertyPostSetterEventArgs<DataType> e)
        {
            obj.NotifyPropertyChanged("CodeTemplate", string.Empty, string.Empty);
        }

        [Invocation]
        public static void get_CodeTemplate(Property obj, PropertyGetterEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            string type = obj.ObjectClass != null ? obj.ObjectClass.Name : "<<TYPE>>";
            string propType = obj.GetPropertyTypeString();

            sb.AppendFormat("[Invocation]\npublic static void {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "get", obj.Name, type, "PropertyGetterEventArgs", propType);
            sb.AppendFormat("[Invocation]\npublic static void {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "preSet", obj.Name, type, "PropertyPreSetterEventArgs", propType);
            sb.AppendFormat("[Invocation]\npublic static void {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "postSet", obj.Name, type, "PropertyPostSetterEventArgs", propType);
            sb.AppendFormat("[Invocation]\npublic static void {0}_{1}({2} obj, {3} e)\n{{\n\te.IsValid = obj.{1} == ...;\n\te.Error = e.IsValid ? string.Empty : \"<Error>\";\n}}\n\n", "isValid", obj.Name, type, "PropertyIsValidEventArgs");

            e.Result = sb.ToString();
        }

        [Invocation]
        public static void GetName(Property obj, MethodReturnEventArgs<string> e)
        {
            var cls = obj.ObjectClass as ObjectClass;
            if (cls != null)
            {
                e.Result = string.Format("{0}_Properties.{1}", cls.GetName(), obj.Name);
            }
        }
    }
}
