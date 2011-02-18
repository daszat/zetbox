
namespace Kistl.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.API.Utils;

    [Implementor]
    public static class ObjectClassActions
    {
        #region CreateDefaultMethods
        private static void CheckDefaultMethod(ObjectClass obj, string methodName)
        {
            var m = obj.Methods.SingleOrDefault(i => i.Name == methodName);
            if (m == null && obj.BaseObjectClass == null)
            {
                // Only for BaseClasses
                m = obj.Context.Create<Method>();
                m.Name = methodName;
                m.Module = obj.Module;
                obj.Methods.Add(m);
            }
            // Do not delete if BaseClass is declared
            // deleting should be a user action
            // TODO: Add Object Level Constraint
        }

        [Invocation]
        public static void CreateDefaultMethods(Kistl.App.Base.ObjectClass obj)
        {
            CheckDefaultMethod(obj, "ToString");
            CheckDefaultMethod(obj, "NotifyPreSave");
            CheckDefaultMethod(obj, "NotifyPostSave");
            CheckDefaultMethod(obj, "NotifyCreated");
            CheckDefaultMethod(obj, "NotifyDeleting");

            var toStr = obj.Methods.SingleOrDefault(i => i.Name == "ToString");
            if (toStr != null && toStr.Parameter.Count == 0)
            {
                var p = obj.Context.Create<StringParameter>();
                p.IsReturnParameter = true;
                p.Name = "retVal";
                toStr.Parameter.Add(p);
            }
        }
        #endregion

        [Invocation]
        public static void NotifyPreSave(Kistl.App.Base.ObjectClass obj)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        [Invocation]
        public static void NotifyCreated(Kistl.App.Base.ObjectClass obj)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        [Invocation]
        public static void postSet_BaseObjectClass(Kistl.App.Base.ObjectClass obj, PropertyPostSetterEventArgs<Kistl.App.Base.ObjectClass> e)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
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
        public static void GetInheritedMethods(ObjectClass obj, MethodReturnEventArgs<IList<Method>> e)
        {
            ObjectClass baseObjectClass = obj.BaseObjectClass;
            if (baseObjectClass != null)
            {
                e.Result = baseObjectClass.GetInheritedMethods();
                baseObjectClass.Methods.ForEach<Method>(m => e.Result.Add(m));
            }
            else
            {
                e.Result = new List<Method>();
            }
        }


    }
}
