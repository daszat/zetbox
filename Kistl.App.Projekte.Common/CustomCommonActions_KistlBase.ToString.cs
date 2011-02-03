using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;

namespace Kistl.App.Base
{
    public static partial class CustomCommonActions_KistlBase
    {
        #region ToString
        public static void OnToString_Assembly(Assembly obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_BaseParameter(BaseParameter obj, MethodReturnEventArgs<string> e)
        {
            e.Result = string.Format("{0}{1} {2}",
                obj.IsReturnParameter
                    ? "[Return] "
                    : String.Empty,
                obj.GetParameterTypeString(),
                obj.Name);

            FixupFloatingObjectsToString(obj, e);
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_DataType(DataType obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}.{1}",
                obj.Module == null
                    ? "[no module]"
                    : obj.Module.Namespace,
                obj.Name);

            FixupFloatingObjectsToString(obj, e);
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_Enumeration(Enumeration obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            FixupFloatingObjectsToString(obj, e);
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_EnumerationEntry(EnumerationEntry obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Enumeration + "." + obj.Name;

            FixupFloatingObjectsToString(obj, e);
        }

        /// <summary>
        /// ToString Event überschreiben
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        public static void OnToString_MethodInvocation(MethodInvocation obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} {1}.{2}",
                (obj.Implementor != null && obj.Implementor.Assembly != null)
                    ? obj.Implementor.Assembly.DeploymentRestrictions.ToString()
                    : "unknown",
                obj.InvokeOnObjectClass == null
                    ? "unattached"
                    : obj.InvokeOnObjectClass.Name,
                obj.Method == null
                    ? "unattached"
                    : obj.Method.Name);

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_Method(Method obj, MethodReturnEventArgs<string> e)
        {
            // TODO: IsValid?
            if (obj.ObjectClass != null && obj.ObjectClass.Module != null)
            {
                e.Result = obj.ObjectClass.Module.Namespace + "." +
                                obj.ObjectClass.Name + "." + obj.Name;

                FixupFloatingObjectsToString(obj, e);
            }
            else
            {
                e.Result = String.Format("new Method #{0}: {1}", obj.ID, obj.Name);
            }
        }

        public static void OnToString_Module(Module obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_ObjectReferenceProperty(ObjectReferenceProperty obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "-> " + e.Result;

            // already handled by base OnToString_Property()
            // FixupFloatingObjects(obj, e);
        }

        public static void OnToString_Property(Property obj, MethodReturnEventArgs<string> e)
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
            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_Relation(Relation obj, MethodReturnEventArgs<string> e)
        {
            if (obj.A == null ||
                obj.B == null ||
                obj.A.Type == null ||
                obj.B.Type == null)
            {
                e.Result = "incomplete relation:";
                if (obj.A == null)
                {
                    e.Result += " A missing";
                }
                else
                {
                    e.Result += " A.Type missing";
                }

                if (obj.B == null)
                {
                    e.Result += " B missing";
                }
                else
                {
                    e.Result += " B.Type missing";
                }
            }
            else
            {
                string aDesc = (obj.A.RoleName ?? String.Empty).Equals(obj.A.Type.Name)
                    ? obj.A.RoleName
                    : String.Format("{0}({1})", obj.A.RoleName, obj.A.Type.Name);

                string bDesc = (obj.B.RoleName ?? String.Empty).Equals(obj.B.Type.Name)
                    ? obj.B.RoleName
                    : String.Format("{0}({1})", obj.B.RoleName, obj.B.Type.Name);

                e.Result = String.Format("Relation: {0} {1} {2}",
                    aDesc,
                    obj.Verb,
                    bDesc);
            }

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_RelationEnd(RelationEnd obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("RelationEnd {0}({1})",
                obj.RoleName,
                obj.Type == null
                    ? "no type"
                    : obj.Type.Name);

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_TypeRef(TypeRef obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0}{1}",
                obj.Deleted == true ? "(DELETED) " : string.Empty,
                obj.FullName);

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_Identity(Kistl.App.Base.Identity obj, MethodReturnEventArgs<string> e)
        {
            e.Result = (obj.DisplayName ?? string.Empty) + " (" + (obj.UserName ?? string.Empty) + ")";

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_Group(Kistl.App.Base.Group obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Name;

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_AccessControl(Kistl.App.Base.AccessControl obj, MethodReturnEventArgs<string> e)
        {
            e.Result = (obj.Name ?? string.Empty) + " (" + (obj.Rights ?? AccessRights.None) + ") " + (obj.Description ?? string.Empty);

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_Document(Kistl.App.Base.Blob obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.OriginalName;

            FixupFloatingObjectsToString(obj, e);
        }

        public static void OnToString_ServiceDescriptor(Kistl.App.Base.ServiceDescriptor obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = obj.Description;

            FixupFloatingObjectsToString(obj, e);
        }
        #endregion

        /// <summary>
        /// Since floating objects might have no valid/useful ToString() result yet, prefix them with the typename and id.
        /// </summary>
        /// <param name="obj">The current object</param>
        /// <param name="e">The ToString MethodReturnEventArgs.</param>
        private static void FixupFloatingObjectsToString(IDataObject obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Context != null && obj.Context.IsReadonly) return;
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}(#{1}): {2}", obj.GetType().Name, obj.ID, e.Result);
            }
        }
    }
}
