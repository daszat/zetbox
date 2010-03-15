using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Extensions;

namespace Kistl.App.Base
{
    public static partial class CustomClientActions_KistlBase
    {

        #region Constraint

        public static void OnIsValid_Constraint(
            Constraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            // the base constraint accepts all values
            e.Result = true;
        }

        #endregion

        #region NotNullableConstraint

        public static void OnIsValid_NotNullableConstraint(
            NotNullableConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = constrainedValueParam != null;
        }

        public static void OnGetErrorText_NotNullableConstraint(
            NotNullableConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = String.IsNullOrEmpty(obj.Reason) ? "Value must be set" : String.Format("Value must be set: {0}", obj.Reason);
        }

        #endregion

        #region IntegerRangeConstraint

        public static void OnIsValid_IntegerRangeConstraint(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            if (constrainedValueParam != null)
            {
                int v = (int)constrainedValueParam;
                e.Result = (obj.Min <= v) && (v <= obj.Max);
            }
            else
            {
                // only accept null values if no lower bound is set
                e.Result = obj.Min == 0;
            }
        }

        public static void OnGetErrorText_IntegerRangeConstraint(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            int v = (int)constrainedValueParam;
            if (obj.IsValid(constrainedObjectParam, constrainedValueParam))
            {
                e.Result = null;
            }
            else
            {
                StringBuilder result = new StringBuilder();
                if (v < obj.Min)
                    result.AppendFormat("{0} should be equal or greater than {1}", obj.ConstrainedProperty.Name, obj.Min);
                if (v > obj.Max)
                    result.AppendFormat("{0} should be equal or less than {1}", obj.ConstrainedProperty.Name, obj.Max);

                if (!String.IsNullOrEmpty(obj.Reason))
                {
                    result.Append(": ");
                    result.Append(obj.Reason);
                }

                e.Result = result.ToString();
            }
        }


        #endregion

        #region StringRangeConstraint

        public static void OnIsValid_StringRangeConstraint(
            StringRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            int length = (constrainedValueParam ?? String.Empty).ToString().Length;
            e.Result = (obj.MinLength <= length) && (length <= (obj.MaxLength ?? int.MaxValue));
        }

        public static void OnGetErrorText_StringRangeConstraint(
            StringRangeConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {

            if (obj.IsValid(constrainedObjectParam, constrainedValueParam))
            {
                e.Result = null;
            }
            else
            {
                constrainedValueParam = (constrainedValueParam ?? String.Empty);
                int length = constrainedValueParam.ToString().Length;
                StringBuilder result = new StringBuilder();
                if (length < obj.MinLength)
                    result.AppendFormat("{0} should be at least {1} characters long", obj.ConstrainedProperty.Name, obj.MinLength);
                if (obj.MaxLength != null && length > obj.MaxLength)
                    result.AppendFormat("{0} should be at most {1} characters long", obj.ConstrainedProperty.Name, obj.MaxLength);

                if (!String.IsNullOrEmpty(obj.Reason))
                {
                    result.Append(": ");
                    result.Append(obj.Reason);
                }

                e.Result = result.ToString();
            }
        }



        #endregion

        #region MethodInvocationConstraint

        public static void OnIsValid_MethodInvocationConstraint(
            MethodInvocationConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            var constrainedObject = (MethodInvocation)constrainedObjectParam;
            var method = (Method)constrainedValueParam;
            e.Result = (method != null) && method.ObjectClass.IsAssignableFrom(constrainedObject.InvokeOnObjectClass);
        }


        public static void OnGetErrorText_MethodInvocationConstraint(
            MethodInvocationConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            var constrainedObject = (MethodInvocation)constrainedObjectParam;
            var method = (Method)constrainedValueParam;
            e.Result = String.Format("This Invocation's  InvokeOnObjectClass ('{1}') should be a descendent of (or equal to) the Method's class ('{0}'), but isn't",
                (method != null) ? method.ObjectClass.ToString() : "(no Method yet)",
                constrainedObject.InvokeOnObjectClass);
        }


        #endregion

        #region IsValidIdentifierConstraint
        public static void OnIsValid_IsValidIdentifierConstraint(
                    IsValidIdentifierConstraint obj,
                    MethodReturnEventArgs<bool> e,
                    object constrainedObjectParam,
                    object constrainedValueParam)
        {
            e.Result = (constrainedValueParam != null) &&
                System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier((string)constrainedValueParam);
        }

        public static void OnIsValid_IsValidNamespaceConstraint(
                    IsValidNamespaceConstraint obj,
                    MethodReturnEventArgs<bool> e,
                    object constrainedObjectParam,
                    object constrainedValueParam)
        {
            if (constrainedValueParam != null)
            {
                string @namespace = (string)constrainedValueParam;

                e.Result = true;
                foreach (string ns in @namespace.Split('.'))
                {
                    e.Result &= System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(ns);
                }
            }
        }


        public static void OnGetErrorText_IsValidIdentifierConstraint(
            IsValidIdentifierConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = string.Format("'{0}' is not a valid identifier", constrainedValueParam);
        }

        #endregion

        #region ConsistentNavigatorConstraint

        public static void OnIsValid_ConsistentNavigatorConstraint(
                   ConsistentNavigatorConstraint obj,
                   MethodReturnEventArgs<bool> e,
                   object constrainedObjectParam,
                   object constrainedValueParam)
        {
            var relEnd = (RelationEnd)constrainedObjectParam;
            var otherEnd = relEnd.GetParent().GetOtherEnd(relEnd);
            var orp = (ObjectReferenceProperty)constrainedValueParam;

            e.Result = true;

            if (orp != null)
            {
                switch (otherEnd.Multiplicity)
                {
                    case Multiplicity.One:
                        e.Result &= orp.Constraints.OfType<NotNullableConstraint>().Count() > 0;
                        break;
                    case Multiplicity.ZeroOrMore:
                        e.Result &= orp.Constraints.OfType<NotNullableConstraint>().Count() == 0;
                        break;
                    case Multiplicity.ZeroOrOne:
                        e.Result &= orp.Constraints.OfType<NotNullableConstraint>().Count() == 0;
                        break;
                }

                e.Result &= relEnd.Type == orp.ObjectClass;
            }
        }

        public static void OnGetErrorText_ConsistentNavigatorConstraint(
            ConsistentNavigatorConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            var relEnd = (RelationEnd)constrainedObjectParam;
            var otherEnd = (relEnd.AParent ?? relEnd.BParent).GetOtherEnd(relEnd);
            var orp = (ObjectReferenceProperty)constrainedValueParam;

            var result = new List<string>();

            if (orp != null)
            {
                switch (otherEnd.Multiplicity)
                {
                    case Multiplicity.One:
                        if (orp.Constraints.OfType<NotNullableConstraint>().Count() == 0)
                        {
                            result.Add("Navigator should have NotNullableConstraint because Multiplicity of opposite RelationEnd is One");
                        }
                        break;
                    case Multiplicity.ZeroOrMore:
                        if (orp.Constraints.OfType<NotNullableConstraint>().Count() > 0)
                        {
                            result.Add("Navigator should not have NotNullableConstraint because Multiplicity of opposite RelationEnd is ZeroOrMore");
                        }
                        break;
                    case Multiplicity.ZeroOrOne:
                        if (orp.Constraints.OfType<NotNullableConstraint>().Count() > 0)
                        {
                            result.Add("Navigator should not have NotNullableConstraint because Multiplicity of opposite RelationEnd is ZeroOrOne");
                        }
                        break;
                }

                if (relEnd.Type != orp.ObjectClass)
                {
                    result.Add(String.Format("Navigator is attached to {0} but should be attached to {1}",
                        orp.ObjectClass,
                        relEnd.Type));
                }
            }

            e.Result = String.Join("\n", result.ToArray());
        }


        #endregion
    }

    public static class Helpers
    {
        // copied from TheseMethodsShouldBeImplementedOnKistlObjects
        public static bool IsAssignableFrom(this DataType self, DataType other)
        {
            // if one or both parameters are null, it never can be assignable
            // also, this is a nice stop condition for the recursion for ObjectClasses
            if (self == null || other == null)
                return false;

            if (self == other)
                return true;

            if (!(self is ObjectClass && other is ObjectClass))
                return false;

            // self might be an ancestor of other, check here
            return IsAssignableFrom(self, (other as ObjectClass).BaseObjectClass);
        }
    }
}