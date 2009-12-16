using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using System.Diagnostics;

namespace Kistl.App.Base
{
    public static partial class CustomCommonActions_KistlBase
    {
        #region ToString

        public static void OnToString_Assembly(Assembly obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.AssemblyName;
        }

        public static void OnToString_BaseParameter(BaseParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0}{1} {2}",
                obj.IsReturnParameter ? "[Return] " : "",
                obj.GetParameterTypeString(),
                obj.ParameterName);
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_DataType(DataType obj, MethodReturnEventArgs<string> e)
        {
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}", obj.GetType());
            }
            else
            {
                e.Result = String.Format("{0}.{1}",
                    obj.Module == null ? "[no module]" : obj.Module.Namespace,
                    obj.ClassName);
            }
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_Enumeration(Enumeration obj, MethodReturnEventArgs<string> e)
        {
            // TODO: if (!IsValid)
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}", obj.GetType());
            }
            else
            {
                e.Result = obj.ClassName;
            }
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_EnumerationEntry(EnumerationEntry obj, MethodReturnEventArgs<string> e)
        {
            // TODO: if (!IsValid)
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}", obj.GetType());
            }
            else
            {
                e.Result = obj.Enumeration + "." + obj.Name;
            }
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_MethodInvokation(MethodInvocation obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = String.Format("{0} {1}.{2}",
                    obj.Implementor.Assembly.DeploymentRestrictions,
                    obj.InvokeOnObjectClass == null ? "unattached" : obj.InvokeOnObjectClass.ClassName,
                    obj.Method == null ? "unattached" : obj.Method.MethodName);
            }
            else
            {
                e.Result = String.Format("MethodInvocation {0}", obj.ID);
            }
        }

        public static void OnToString_Method(Method obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (obj.ObjectClass != null && obj.ObjectClass.Module != null)
            {
                e.Result = obj.ObjectClass.Module.Namespace + "." +
                                obj.ObjectClass.ClassName + "." + obj.MethodName;
            }
            else
            {
                e.Result = String.Format("Method {0}: {1}", obj.ID, obj.MethodName);
            }
        }

        public static void OnToString_Module(Module obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.ModuleName;
        }

        public static void OnToString_ObjectReferenceProperty(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;
        }

        public static void OnToString_Property(Property obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (Helper.IsPersistedObject(obj))
            {
                e.Result = string.Format("{0} {1}.{2}",
                   obj.GetPropertyTypeString(),
                   (obj.ObjectClass == null) ? "unattached" : obj.ObjectClass.ClassName,
                   obj.PropertyName);
            }
            else
            {
                e.Result = String.Format("Property {0}", obj.ID);
            }

            // TODO: fix in overrides for struct/valuetype and objectreference*
            //if (obj.IsList) e.Result += " [0..n]";
        }

        public static void OnToString_Relation(Relation obj, MethodReturnEventArgs<string> e)
        {
            if (obj.A == null ||
                obj.B == null ||
                obj.A.Type == null ||
                obj.B.Type == null)
            {
                e.Result = "invalid Relation";
            }
            else
            {
                e.Result = String.Format("Relation: {0}({1}) {4} {2}({3})",
                    obj.A.RoleName,
                    obj.A.Type.ClassName,
                    obj.B.RoleName,
                    obj.B.Type.ClassName,
                    obj.Verb);
            }
        }

        public static void OnToString_RelationEnd(RelationEnd obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("RelationEnd {0}({1})",
                obj.RoleName,
                obj.Type == null ? "no type" : obj.Type.ClassName);
        }

        public static void OnToString_TypeRef(TypeRef obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}{1}, {2}",
                obj.FullName,
                //"<???>"
                /* 
                 * Currently this goes to the Database once per TypeRef
                 * TODO: re-enable that, when GenericArguments is loaded eagerly. 
                 */
                (obj.GenericArguments.Count > 0
                    ? "<" + String.Join(", ", obj.GenericArguments.Select(tr => tr.ToString()).ToArray()) + ">"
                    : "")
                            ,
                obj.Assembly);
        }

        public static void OnToString_Identity(Kistl.App.Base.Identity obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.DisplayName;
        }
        #endregion
    }
}
