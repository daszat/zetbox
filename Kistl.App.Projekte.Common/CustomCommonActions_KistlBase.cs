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

namespace Kistl.App.Base
{
    public partial class CustomCommonActions_KistlBase
    {
        #region ObjectClass

        #region EnsureDefaultMethods
        private void CheckDefaultMethod(ObjectClass obj, string methodName)
        {
            var m = obj.Methods.SingleOrDefault(i => i.MethodName == methodName);
            if (m == null && obj.BaseObjectClass == null)
            {
                // Only for BaseClasses
                m = obj.Context.Create<Method>();
                m.MethodName = methodName;
                m.Module = obj.Module;
                obj.Methods.Add(m);
            }
            else if (m != null && obj.BaseObjectClass != null)
            {
                // Delete if BaseClass is declared
                // TODO: Move MethodInvocations to base class' method
                // obj.Context.Delete(m);
            }
        }

        private void EnsureDefaultMethods(ObjectClass obj)
        {
            CheckDefaultMethod(obj, "ToString");
            CheckDefaultMethod(obj, "NotifyPreSave");
            CheckDefaultMethod(obj, "NotifyPostSave");
            CheckDefaultMethod(obj, "NotifyCreated");
            CheckDefaultMethod(obj, "NotifyDeleting");
        }
        #endregion

        public void OnPreSave_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            EnsureDefaultMethods(obj);
        }

        public void OnCreated_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            EnsureDefaultMethods(obj);
        }

        public void OnBaseObjectClass_PostSetter_ObjectClass(Kistl.App.Base.ObjectClass obj, PropertyPostSetterEventArgs<Kistl.App.Base.ObjectClass> e)
        {
            EnsureDefaultMethods(obj);
        }

        public void OnCreateRelation_ObjectClass(ObjectClass obj, MethodReturnEventArgs<Relation> e)
        {
            e.Result = obj.Context.Create<Relation>();
            e.Result.A = obj.Context.Create<RelationEnd>();
            e.Result.A.Type = obj;
            e.Result.A.Role = (int)RelationEndRole.A;
            e.Result.B = obj.Context.Create<RelationEnd>();
            e.Result.B.Role = (int)RelationEndRole.B;
            e.Result.Module = obj.Module;
        }

        public void OnCreateMethod_ObjectClass(ObjectClass obj, MethodReturnEventArgs<Method> e)
        {
            e.Result = obj.Context.Create<Method>();
            e.Result.Module = obj.Module;
            e.Result.ObjectClass = obj;
        }

        public void OnCreateMethodInvocation_Method(Kistl.App.Base.Method obj, MethodReturnEventArgs<Kistl.App.Base.MethodInvocation> e)
        {
            e.Result = obj.Context.Create<MethodInvocation>();
            e.Result.InvokeOnObjectClass = obj.ObjectClass;
            e.Result.Method = obj;
            e.Result.Module = obj.Module;
        }

        public void OnCreateNavigator_RelationEnd(RelationEnd obj, MethodReturnEventArgs<ObjectReferenceProperty> e)
        {
            Relation rel = obj.AParent ?? obj.BParent;
            RelationEnd other = null; // rel.GetOtherEnd(obj);
            
            e.Result = obj.Context.Create<ObjectReferenceProperty>();
            e.Result.AltText = "";
            e.Result.CategoryTags = "";
            e.Result.ObjectClass = obj.Type;
            e.Result.RelationEnd = obj;
            e.Result.Module = rel.Module;

            if (other != null)
            {
                e.Result.ReferenceObjectClass = other.Type;
                e.Result.PropertyName = other.RoleName;
            }
        }

        #endregion

        #region Relation
        public void OnCreated_Relation(Kistl.App.Base.Relation obj)
        {
            obj.A = obj.Context.Create<RelationEnd>();
            obj.B = obj.Context.Create<RelationEnd>();

            obj.A.Role = (int)RelationEndRole.A;
            obj.B.Role = (int)RelationEndRole.B;
        }
        #endregion

        #region PropertyInvocation
        public void OnGetCodeTemplate_PropertyInvocation(Kistl.App.Base.PropertyInvocation obj, MethodReturnEventArgs<System.String> e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public void {0}(", obj.GetMemberName());

            if (obj.InvokeOnProperty != null && obj.InvokeOnProperty.ObjectClass != null && obj.InvokeOnProperty.ObjectClass.Module != null)
            {
                sb.AppendFormat("{0}.{1} obj", obj.InvokeOnProperty.ObjectClass.Module.Namespace, obj.InvokeOnProperty.ObjectClass.ClassName);
            }
            else
            {
                sb.AppendFormat("<<TYPE>> obj", obj.InvokeOnProperty.ObjectClass.Module.Namespace, obj.InvokeOnProperty.ObjectClass.ClassName);
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
        public void OnGetMemberName_PropertyInvocation(Kistl.App.Base.PropertyInvocation obj, MethodReturnEventArgs<System.String> e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("On");
            sb.Append(obj.InvokeOnProperty != null ? obj.InvokeOnProperty.PropertyName : "<<PROPERTYNAME>>");
            sb.Append("_");
            sb.Append(obj.InvocationType.ToString());
            sb.Append("_");
            sb.Append(obj.InvokeOnProperty != null && obj.InvokeOnProperty.ObjectClass != null ? obj.InvokeOnProperty.ObjectClass.ClassName : "<<OBJECTCLASSNAME>>");

            e.Result = sb.ToString();
        }
        #endregion

        #region MethodInvocation
        public void OnGetCodeTemplate_MethodInvocation(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public void {0}(", mi.GetMemberName());

            if (mi.InvokeOnObjectClass != null)
            {
                sb.AppendFormat("{0}.{1} obj", mi.InvokeOnObjectClass.Module != null ? mi.InvokeOnObjectClass.Module.Namespace : "", mi.InvokeOnObjectClass.ClassName);
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
                    sb.AppendFormat(", MethodReturnEventArgs<{0}> e", returnParam.GetParameterTypeString());
                }

                foreach (var param in mi.Method.Parameter.Where(p => !p.IsReturnParameter))
                {
                    sb.AppendFormat(", {0} {1}", param.GetParameterTypeString(), param.ParameterName);
                }
            }

            sb.AppendLine(")");
            sb.AppendLine("{");
            sb.AppendLine("}");

            e.Result = sb.ToString();
        }

        public void OnGetMemberName_MethodInvocation(MethodInvocation mi, MethodReturnEventArgs<string> e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("On");
            sb.Append(mi.Method != null ? mi.Method.MethodName : "<<METHODNAME>>");
            sb.Append("_");
            sb.Append(mi.InvokeOnObjectClass != null ? mi.InvokeOnObjectClass.ClassName : "<<OBJECTCLASSNAME>>");

            e.Result = sb.ToString();
        }
        #endregion

    }
}
