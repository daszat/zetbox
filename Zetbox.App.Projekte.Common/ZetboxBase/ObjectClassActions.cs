
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
    }
}
