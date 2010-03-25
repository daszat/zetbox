using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Extensions;

namespace Kistl.App.Base
{
    public static partial class CustomCommonActions_KistlBase
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Common.CustomActions");

        #region ToString
        public static void OnToString_NotNullableConstraint(NotNullableConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (obj.ConstrainedProperty == null)
            {
                e.Result = String.Format("The ConstrainedProperty should not be NULL");
            }
            else
            {
                e.Result = String.Format("{0} should not be NULL", obj.ConstrainedProperty.Name);
            }
        }

        public static void OnToString_IntegerRangeConstraint(IntegerRangeConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} <= {1} <= {2}", obj.Min, obj.ConstrainedProperty == null ? "(no property)" : obj.ConstrainedProperty.Name, obj.Max);
        }

        public static void OnToString_StringRangeConstraint(StringRangeConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            string maxLength = obj.MaxLength != null ? obj.MaxLength.ToString() : "unlimited";
            // Only display min if there is an actual restriction.
            if (obj.MinLength > 0)
            {
                e.Result = String.Format("{0} should have at least {1} and at most {2} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.Name,
                    obj.MinLength,
                    maxLength);
            }
            else
            {
                e.Result = String.Format("{0} should have at most {1} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.Name,
                    maxLength);
            }
        }

        public static void OnToString_MethodInvocationConstraint(MethodInvocationConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "MethodIvocation's Method.ObjectClass should be assignable from InvokeOnObjectClass";
        }

        public static void OnToString_IsValidIdentifierConstraint(IsValidIdentifierConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "Method names, property names, enum names etc. must be valid names.";
        }

        public static void OnToString_ConsistentNavigatorConstraint(ConsistentNavigatorConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "The navigator should be consistent with the defining Relation.";
        }
        #endregion

        #region InvokingConstraint

        private static readonly Type[] ObjectObject = new Type[] { typeof(object), typeof(object) };
        public static void OnIsValid_InvokingConstraint(InvokingConstraint obj, MethodReturnEventArgs<bool> e, object constrainedObject, object constrainedValue)
        {
            var implementor = obj.IsValidInvocation.Implementor.AsType(false);
            if (implementor == null)
            {
                Log.ErrorFormat("Implementor [{0}] not found", obj.IsValidInvocation.Implementor);
                return;
            }
            var methodInfo = implementor.FindMethod(obj.IsValidInvocation.MemberName, ObjectObject);
            if (methodInfo == null)
            {
                Log.ErrorFormat("Method [{0}](object,object) not found in [{1}]", obj.IsValidInvocation.MemberName, obj.IsValidInvocation.Implementor);
                return;
            }
            e.Result = (bool)methodInfo.Invoke(null, new object[] { constrainedObject, constrainedValue });
        }

        public static void OnGetErrorText_InvokingConstraint(InvokingConstraint obj, MethodReturnEventArgs<string> e, object constrainedObject, object constrainedValue)
        {
            var implementor = obj.GetErrorTextInvocation.Implementor.AsType(false);
            if (implementor == null)
            {
                Log.ErrorFormat("Implementor [{0}] not found", obj.GetErrorTextInvocation.Implementor);
                return;
            }
            var methodInfo = implementor.FindMethod(obj.GetErrorTextInvocation.MemberName, ObjectObject);
            if (methodInfo == null)
            {
                Log.ErrorFormat("Method [{0}](object,object) not found in [{1}]", obj.GetErrorTextInvocation.MemberName, obj.IsValidInvocation.Implementor);
                return;
            }
            e.Result = (string)methodInfo.Invoke(null, new object[] { constrainedObject, constrainedValue });
        }

        public static void OnGetMemberName_ConstraintInvocation(ConstraintInvocation obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Empty;
        }

        public static void OnGetCodeTemplate_ConstraintInvocation(ConstraintInvocation obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "// choose wisely, young padawan!\n\npublic static bool OnIsValid_<ClassName>_<PropertyName>(object constrainedObject, object constrainedValue)\n{\n}\n\npublic static string OnGetErrorText_<ClassName>_<PropertyName>(object constrainedObject, object constrainedValue)\n{\n}\n";
        }

        #endregion

        #region Relation_Storage Constraint

        public static bool OnIsValid_Relation_Storage(object constrainedObject, object constrainedValue)
        {
            if (constrainedObject == null) { throw new ArgumentNullException("constrainedObject"); }
            var rel = constrainedObject as Relation;
            if (rel != null && rel.A != null && rel.B != null)
            {
                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                        return rel.B.Multiplicity.UpperBound() <= 1;
                    case StorageType.MergeIntoB:
                        return rel.A.Multiplicity.UpperBound() <= 1;
                    case StorageType.Separate:
                        return rel.A.Multiplicity.UpperBound() > 1 && rel.B.Multiplicity.UpperBound() > 1;
                    case StorageType.Replicate:
                    default:
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string OnGetErrorText_Relation_Storage(object constrainedObject, object constrainedValue)
        {
            if (constrainedObject == null) { throw new ArgumentNullException("constrainedObject"); }
            var rel = constrainedObject as Relation;
            if (rel != null && rel.A != null && rel.B != null)
            {
                switch (rel.Storage)
                {
                    case StorageType.MergeIntoA:
                        return rel.B.Multiplicity.UpperBound() <= 1 ? String.Empty : "B side could be more than one. Not able to merge foreign key into A";
                    case StorageType.MergeIntoB:
                        return rel.A.Multiplicity.UpperBound() <= 1 ? String.Empty : "A side could be more than one. Not able to merge foreign key into B";
                    case StorageType.Separate:
                        if (rel.A.Multiplicity.UpperBound() <= 1)
                        {
                            return "A side is only one-ary. Please use MergeIntoB";
                        }
                        else if (rel.B.Multiplicity.UpperBound() <= 1)
                        {
                            return "B side is only one-ary. Please use MergeIntoA";
                        }
                        else
                        {
                            return String.Empty;
                        }
                    case StorageType.Replicate:
                    default:
                        return String.Format("StorageType.{0} not implemented.", rel.Storage);
                }
            }
            else
            {
                return "Incomplete Relation";
            }
        }

        #endregion
    }
}
