using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.App.Extensions;
using Kistl.App.GUI;
using Kistl.API.Utils;

namespace Kistl.App.Base
{
    public static partial class CustomCommonActions_KistlBase
    {
        #region ObjectClass

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

        public static void OnCreateDefaultMethods_ObjectClass(Kistl.App.Base.ObjectClass obj)
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

        public static void OnPreSave_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        public static void OnCreated_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        public static void OnBaseObjectClass_PostSetter_ObjectClass(Kistl.App.Base.ObjectClass obj, PropertyPostSetterEventArgs<Kistl.App.Base.ObjectClass> e)
        {
            // Do not call CreateDefaultMethods
            // during deploy these Methods are also invoked
            // invoking CreateDefaultMethods leads to multiple instances and unexpected results
        }

        public static void OnCreateRelation_ObjectClass(ObjectClass obj, MethodReturnEventArgs<Relation> e)
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

        public static void OnCreateMethod_ObjectClass(ObjectClass obj, MethodReturnEventArgs<Method> e)
        {
            e.Result = obj.Context.Create<Method>();
            e.Result.Module = obj.Module;
            e.Result.ObjectClass = obj;
        }

        public static void OnCreateMethodInvocation_Method(Kistl.App.Base.Method obj, MethodReturnEventArgs<Kistl.App.Base.MethodInvocation> e)
        {
            e.Result = obj.Context.Create<MethodInvocation>();
            e.Result.InvokeOnObjectClass = obj.ObjectClass;
            e.Result.Method = obj;
            e.Result.Module = obj.Module;
        }

        // TODO: Replace this when NamedInstances are introduced 
        public static readonly Guid ViewModelDescriptor_ObjectReferenceModel = new Guid("83aae6fd-0fae-4348-b313-737a6e751027");
        public static readonly Guid ViewModelDescriptor_ObjectListModel = new Guid("9fce01fe-fd6d-4e21-8b55-08d5e38aea36");
        public static readonly Guid ViewModelDescriptor_ObjectCollectionModel = new Guid("67A49C49-B890-4D35-A8DB-1F8E43BFC7DF");

        public static void OnCreateNavigator_RelationEnd(RelationEnd obj, MethodReturnEventArgs<ObjectReferenceProperty> e)
        {
            Relation rel = obj.AParent ?? obj.BParent;
            RelationEnd other = rel != null ? rel.GetOtherEnd(obj) : null;

            var nav = obj.Context.Create<ObjectReferenceProperty>();
            nav.CategoryTags = String.Empty;
            nav.ObjectClass = obj.Type;
            nav.RelationEnd = obj;
            nav.Module = rel != null ? rel.Module : null;

            if (other != null)
            {
                if (nav.GetIsList())
                {
                    if (nav.RelationEnd.Parent.GetOtherEnd(nav.RelationEnd).HasPersistentOrder)
                    {
                        nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectListModel);
                    }
                    else
                    {
                        nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectCollectionModel);
                    }
                }
                else
                {
                    nav.ValueModelDescriptor = obj.Context.FindPersistenceObject<ViewModelDescriptor>(ViewModelDescriptor_ObjectReferenceModel);
                }

                nav.Name = other.RoleName;
            }

            e.Result = nav;
        }

        #endregion

        #region PropertyInvocation
        public static void OnGetCodeTemplate_PropertyInvocation(Kistl.App.Base.PropertyInvocation obj, MethodReturnEventArgs<System.String> e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public static void {0}(", obj.GetMemberName());

            if (obj.InvokeOnProperty != null && obj.InvokeOnProperty.ObjectClass != null && obj.InvokeOnProperty.ObjectClass.Module != null)
            {
                sb.AppendFormat("{0}.{1} obj", obj.InvokeOnProperty.ObjectClass.Module.Namespace, obj.InvokeOnProperty.ObjectClass.Name);
            }
            else
            {
                sb.AppendFormat("<<TYPE>> obj");
            }

            string propType = obj.InvokeOnProperty != null ? obj.InvokeOnProperty.GetPropertyTypeString() : "<<TYPE>>";

            switch (obj.InvocationType)
            {
                case PropertyInvocationType.Getter:
                    sb.AppendFormat(", PropertyGetterEventArgs<{0}> e", propType);
                    break;
                case PropertyInvocationType.PreSetter:
                    sb.AppendFormat(", PropertyPreSetterEventArgs<{0}> e", propType);
                    break;
                case PropertyInvocationType.PostSetter:
                    sb.AppendFormat(", PropertyPostSetterEventArgs<{0}> e", propType);
                    break;
            }

            sb.AppendLine(")");
            sb.AppendLine("{");
            sb.AppendLine("}");

            e.Result = sb.ToString();
        }
        public static void OnGetMemberName_PropertyInvocation(Kistl.App.Base.PropertyInvocation obj, MethodReturnEventArgs<System.String> e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("On");
            sb.Append(obj.InvokeOnProperty != null ? obj.InvokeOnProperty.Name : "<<PROPERTYNAME>>");
            sb.Append("_");
            sb.Append(obj.InvocationType.ToString());
            sb.Append("_");
            sb.Append(obj.InvokeOnProperty != null && obj.InvokeOnProperty.ObjectClass != null ? obj.InvokeOnProperty.ObjectClass.Name : "<<OBJECTCLASSNAME>>");

            e.Result = sb.ToString();
        }
        #endregion

        #region MethodInvocation
        public static void OnGetCodeTemplate_MethodInvocation(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public static void {0}(", mi.GetMemberName());

            if (mi.InvokeOnObjectClass != null)
            {
                sb.AppendFormat("{0}.{1} obj", mi.InvokeOnObjectClass.Module != null ? mi.InvokeOnObjectClass.Module.Namespace : String.Empty, mi.InvokeOnObjectClass.Name);
            }
            else
            {
                sb.Append("<<TYPE>> obj");
            }

            if (mi.Method != null)
            {
                var returnParam = mi.Method.GetReturnParameter();
                if (returnParam != null)
                {
                    if (returnParam.IsList)
                    {
                        sb.AppendFormat(", MethodReturnEventArgs<IList<{0}>> e", returnParam.GetParameterTypeString());
                    }
                    else
                    {
                        sb.AppendFormat(", MethodReturnEventArgs<{0}> e", returnParam.GetParameterTypeString());
                    }
                }

                foreach (var param in mi.Method.Parameter.Where(p => !p.IsReturnParameter))
                {
                    sb.AppendFormat(", {0} {1}",
                        param.IsList ? string.Format("IList<{0}>", param.GetParameterTypeString()) : param.GetParameterTypeString(), 
                        param.Name);
                }
            }

            sb.AppendLine(")");
            sb.AppendLine("{");
            sb.AppendLine("}");

            e.Result = sb.ToString();
        }

        public static void OnGetMemberName_MethodInvocation(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("On");
            sb.Append(mi.Method != null ? mi.Method.Name : "<<METHODNAME>>");
            sb.Append("_");
            sb.Append(mi.InvokeOnObjectClass != null ? mi.InvokeOnObjectClass.Name : "<<OBJECTCLASSNAME>>");

            e.Result = sb.ToString();
        }
        #endregion

        #region GetPropertyTypeString
        public static void OnGetPropertyTypeString_ObjectReferencePlaceholderProperty(ObjectReferencePlaceholderProperty obj, MethodReturnEventArgs<string> e)
        {
            if (obj.ReferencedObjectClass == null)
            {
                e.Result = "Empty ObjectReferencePlaceholderProperty";
                return;
            }

            if (obj.ReferencedObjectClass.Module == null)
            {
                e.Result = "Invalid ReferencedObjectClass (no module)";
                return;
            }

            e.Result = obj.ReferencedObjectClass.Module.Namespace + "." + obj.ReferencedObjectClass.Name;
        }
        #endregion

        #region TypeRef
        public static void OnUpdateToStringCache_TypeRef(Kistl.App.Base.TypeRef obj)
        {
            obj.ToStringCache = String.Format("{0}{1}, {2}",
                obj.FullName,
                obj.GenericArguments.Count > 0
                    ? "<" + String.Join(", ", obj.GenericArguments.Select(tr => tr.FullName).ToArray()) + ">"
                    : String.Empty,
                    obj.Assembly == null ? "(no assembly)" : obj.Assembly.Name);
        }

        public static void OnFullName_PostSetter_TypeRef(Kistl.App.Base.TypeRef obj, PropertyPostSetterEventArgs<System.String> e)
        {
            obj.UpdateToStringCache();
        }

        public static void OnAssembly_PostSetter_TypeRef(Kistl.App.Base.TypeRef obj, PropertyPostSetterEventArgs<Kistl.App.Base.Assembly> e)
        {
            obj.UpdateToStringCache();
        }

        // Not supported yet
        //public static void OnGenericArguments_PostSetter_TypeRef(Kistl.App.Base.TypeRef obj, PropertyPostSetterEventArgs<Kistl.App.Base.TypeRef> e)
        //{
        //    obj.UpdateToStringCache();
        //}

        private const string transientTypeTypeRefCacheKey = "__TypeTypeRefCache__";
        public static void OnAsType_TypeRef(TypeRef obj, MethodReturnEventArgs<Type> e, bool throwOnError)
        {
            Dictionary<TypeRef, Type> cache;
            if (obj.Context.TransientState.ContainsKey(transientTypeTypeRefCacheKey))
            {
                cache = (Dictionary<TypeRef, Type>)obj.Context.TransientState[transientTypeTypeRefCacheKey];
            }
            else
            {
                cache = new Dictionary<TypeRef, Type>();
                obj.Context.TransientState[transientTypeTypeRefCacheKey] = cache;
            }

            if (cache.ContainsKey(obj))
            {
                e.Result = cache[obj];
                return;
            }

            e.Result = Type.GetType(String.Format("{0}, {1}", obj.FullName, obj.Assembly.Name), false);
            if (e.Result == null)
            {
                // Try ReflectionOnly
                System.Reflection.Assembly a = null;
                try
                {
                    a = System.Reflection.Assembly.ReflectionOnlyLoad(obj.Assembly.Name);
                }
                catch
                {
                    if (throwOnError) throw;
                    cache[obj] = e.Result;
                    return;
                }
                e.Result = a.GetType(obj.FullName, throwOnError);
            }

            if (e.Result == null)
            {
                cache[obj] = e.Result;
                return;
            }
            if (obj.GenericArguments.Count > 0)
            {
                var args = obj.GenericArguments.Select(tRef => tRef.AsType(throwOnError)).ToArray();
                if (args.Contains(null))
                {
                    e.Result = null;
                    if (throwOnError)
                    {
                        throw new InvalidOperationException("Cannot create Type: missing generic argument");
                    }
                    else
                    {
                        cache[obj] = e.Result;
                        return;
                    }
                }
                e.Result = e.Result.MakeGenericType(args);
            }
            cache[obj] = e.Result;
        }
        #endregion

        #region Enums
        public static void OnGetLabel_EnumerationEntry(Kistl.App.Base.EnumerationEntry obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;
        }
        public static void OnGetEntryByName_Enumeration(Kistl.App.Base.Enumeration obj, MethodReturnEventArgs<Kistl.App.Base.EnumerationEntry> e, System.String name)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Name == name);
        }
        public static void OnGetEntryByValue_Enumeration(Kistl.App.Base.Enumeration obj, MethodReturnEventArgs<Kistl.App.Base.EnumerationEntry> e, System.Int32 val)
        {
            e.Result = obj.EnumerationEntries.SingleOrDefault(i => i.Value == val);
        }
        public static void OnGetLabelByName_Enumeration(Kistl.App.Base.Enumeration obj, MethodReturnEventArgs<string> e, System.String name)
        {
            var entry = obj.GetEntryByName(name);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;
        }
        public static void OnGetLabelByValue_Enumeration(Kistl.App.Base.Enumeration obj,MethodReturnEventArgs<string> e,  System.Int32 val)
        {
            var entry = obj.GetEntryByValue(val);
            e.Result = entry != null ? entry.GetLabel() : string.Empty;
        }
        #endregion

        #region Properties
        public static void OnGetLabel_Property(Kistl.App.Base.Property obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;
        }
        #endregion

        #region Method
        public static void OnGetLabel_Method(Kistl.App.Base.Method obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;
        }
        #endregion
    }
}
