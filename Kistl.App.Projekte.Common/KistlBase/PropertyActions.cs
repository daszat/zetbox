namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class PropertyActions
    {
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

            // TODO: fix in overrides for struct/valuetype and objectreference*
            //if (obj.IsList) e.Result += " [0..n]";
            ToStringHelper.FixupFloatingObjectsToString(obj, e);
        }

        [Invocation]
        public static void GetPropertyType(Kistl.App.Base.Property obj, Kistl.API.MethodReturnEventArgs<System.Type> e)
        {
            string fullname = obj.GetPropertyTypeString();

            if (obj is EnumerationProperty)
            {
                e.Result = Type.GetType(fullname + ", " + Kistl.API.Helper.InterfaceAssembly);
            }
            // ValueTypes all use mscorlib types,
            else if (obj is ValueTypeProperty)
            {
                e.Result = Type.GetType(fullname);
            }
            else
            {
                // other properties not
                string assembly = Kistl.API.Helper.InterfaceAssembly;
                e.Result = Type.GetType(fullname + ", " + assembly, true);
            }
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
