namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    [Implementor]
    public static class ValueTypePropertyActions
    {
        [Invocation]
        public static void postSet_IsCalculated(ValueTypeProperty obj, PropertyPostSetterEventArgs<bool> e)
        {
            obj.NotifyPropertyChanged("CodeTemplate", string.Empty, string.Empty);
        }

        [Invocation]
        public static void get_CodeTemplate(ValueTypeProperty obj, PropertyGetterEventArgs<string> e)
        {
            if (obj.IsCalculated)
            {
                StringBuilder sb = new StringBuilder();

                string type = obj.ObjectClass != null ? obj.ObjectClass.Name : "<<TYPE>>";
                string propType = obj.GetPropertyTypeString();

                sb.AppendFormat("public static void {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "get", obj.Name, type, "PropertyGetterEventArgs", propType);
                sb.AppendFormat("public static void {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "preSet", obj.Name, type, "PropertyPreSetterEventArgs", propType);
                sb.AppendFormat("public static void {0}_{1}({2} obj, {3}<{4}> e)\n{{\n}}\n\n", "postSet", obj.Name, type, "PropertyPostSetterEventArgs", propType);

                e.Result = sb.ToString();
            }
        }
    }
}
