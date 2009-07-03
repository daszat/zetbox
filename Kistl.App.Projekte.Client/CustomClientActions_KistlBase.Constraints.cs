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
    public partial class CustomClientActions_KistlBase
    {

        #region Constraint

        public void OnIsValid_Constraint(
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

        public void OnIsValid_NotNullableConstraint(
            NotNullableConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = constrainedValueParam != null;
        }

        public void OnGetErrorText_NotNullableConstraint(
            NotNullableConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = String.IsNullOrEmpty(obj.Reason) ? "Value must be set" : String.Format("Value must be set: {0}", obj.Reason);
        }

        public void OnToString_NotNullableConstraint(NotNullableConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            if (obj.ConstrainedProperty == null)
            {
                e.Result = String.Format("The ConstrainedProperty should not be NULL");
            }
            else
            {
                e.Result = String.Format("{0} should not be NULL", obj.ConstrainedProperty.PropertyName);
            }
        }

        #endregion

        #region IntegerRangeConstraint

        public void OnIsValid_IntegerRangeConstraint(
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

        public void OnGetErrorText_IntegerRangeConstraint(
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
                    result.AppendFormat("{0} should be equal or greater than {1}", obj.ConstrainedProperty.PropertyName, obj.Min);
                if (v > obj.Max)
                    result.AppendFormat("{0} should be equal or less than {1}", obj.ConstrainedProperty.PropertyName, obj.Max);

                if (!String.IsNullOrEmpty(obj.Reason))
                {
                    result.Append(": ");
                    result.Append(obj.Reason);
                }

                e.Result = result.ToString();
            }
        }

        public void OnToString_IntegerRangeConstraint(IntegerRangeConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} <= {1} <= {2}", obj.Min, obj.ConstrainedProperty.PropertyName, obj.Max);
        }

        #endregion

        #region StringRangeConstraint

        public void OnIsValid_StringRangeConstraint(
            StringRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            int length = (constrainedValueParam ?? "").ToString().Length;
            e.Result = (obj.MinLength <= length) && (length <= obj.MaxLength);
        }

        public void OnGetErrorText_StringRangeConstraint(
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
                constrainedValueParam = (constrainedValueParam ?? "");
                int length = constrainedValueParam.ToString().Length;
                StringBuilder result = new StringBuilder();
                if (length < obj.MinLength)
                    result.AppendFormat("{0} should be at least {1} characters long", obj.ConstrainedProperty.PropertyName, obj.MinLength);
                if (length > obj.MaxLength)
                    result.AppendFormat("{0} should be at most {1} characters long", obj.ConstrainedProperty.PropertyName, obj.MaxLength);

                if (!String.IsNullOrEmpty(obj.Reason))
                {
                    result.Append(": ");
                    result.Append(obj.Reason);
                }

                e.Result = result.ToString();
            }
        }

        public void OnToString_StringRangeConstraint(StringRangeConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            // Only display min if there is an actual restriction.
            if (obj.MinLength > 0)
            {
                e.Result = String.Format("{0} should have at least {1} and at most {2} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.PropertyName,
                    obj.MinLength,
                    obj.MaxLength);
            }
            else
            {
                e.Result = String.Format("{0} should have at most {1} characters",
                    obj.ConstrainedProperty == null
                        ? "a property"
                        : obj.ConstrainedProperty.PropertyName,
                    obj.MaxLength);
            }
        }

        #endregion

        #region MethodInvocationConstraint

        public void OnIsValid_MethodInvocationConstraint(
            MethodInvocationConstraint obj,
            MethodReturnEventArgs<bool> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            var constrainedObject = (MethodInvocation)constrainedObjectParam;
            var method = (Method)constrainedValueParam;
            e.Result = (method != null) && method.ObjectClass.IsAssignableFrom(constrainedObject.InvokeOnObjectClass);
        }


        public void OnGetErrorText_MethodInvocationConstraint(
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

        public void OnToString_MethodInvocationConstraint(MethodInvocationConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "MethodIvocation's Method.ObjectClass should be assignable from InvokeOnObjectClass";
        }

        #endregion

        #region IsValidIdentifierConstraint
        public void OnIsValid_IsValidIdentifierConstraint(
                    IsValidIdentifierConstraint obj,
                    MethodReturnEventArgs<bool> e,
                    object constrainedObjectParam,
                    object constrainedValueParam)
        {
            e.Result = (constrainedValueParam != null) &&
                System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier((string)constrainedValueParam);
        }

        public void OnIsValid_IsValidNamespaceConstraint(
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


        public void OnGetErrorText_IsValidIdentifierConstraint(
            IsValidIdentifierConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            e.Result = string.Format("'{0}' is not a valid identifier", constrainedValueParam);
        }

        public void OnToString_IsValidIdentifierConstraint(IsValidIdentifierConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = "Method names, property names, enum names etc. must be valid names.";
        }
        #endregion

        #region ConsistentNavigatorConstraint

        public void OnIsValid_ConsistentNavigatorConstraint(
                   ConsistentNavigatorConstraint obj,
                   MethodReturnEventArgs<bool> e,
                   object constrainedObjectParam,
                   object constrainedValueParam)
        {
            var relEnd = (RelationEnd)constrainedObjectParam;
            var otherEnd = (relEnd.AParent ?? relEnd.BParent).GetOtherEnd(relEnd);
            var orp = (ObjectReferenceProperty)constrainedValueParam;

            e.Result = true;

            switch (otherEnd.Multiplicity)
            {
                case Multiplicity.One:
                    e.Result &= orp.Constraints.OfType<NotNullableConstraint>().Count() > 0;
                    break;
                case Multiplicity.ZeroOrMore:
                    e.Result &= orp.Constraints.OfType<NotNullableConstraint>().Count() == 0;
                    e.Result &= orp.IsList;
                    break;
                case Multiplicity.ZeroOrOne:
                    e.Result &= orp.Constraints.OfType<NotNullableConstraint>().Count() == 0;
                    break;
            }

            e.Result &= otherEnd.HasPersistentOrder == (orp.IsList && orp.IsIndexed);
            e.Result &= relEnd.Type == orp.ObjectClass;
            e.Result &= otherEnd.Type == orp.ReferenceObjectClass;
        }

        public void OnGetErrorText_ConsistentNavigatorConstraint(
            ConsistentNavigatorConstraint obj,
            MethodReturnEventArgs<string> e,
            object constrainedObjectParam,
            object constrainedValueParam)
        {
            var relEnd = (RelationEnd)constrainedObjectParam;
            var otherEnd = (relEnd.AParent ?? relEnd.BParent).GetOtherEnd(relEnd);
            var orp = (ObjectReferenceProperty)constrainedValueParam;

            var result = new List<string>();

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
                    if (!orp.IsList)
                    {
                        result.Add("Navigator should have IsList set because Multiplicity of opposite RelationEnd is ZeroOrMore");
                    }
                    break;
                case Multiplicity.ZeroOrOne:
                    if (orp.Constraints.OfType<NotNullableConstraint>().Count() > 0)
                    {
                        result.Add("Navigator should not have NotNullableConstraint because Multiplicity of opposite RelationEnd is ZeroOrOne");
                    }
                    break;
            }

            if (otherEnd.HasPersistentOrder != orp.IsIndexed)
            {
                result.Add("Navigator should IsIndexed set because HasPersistentOrder of opposite RelationEnd is set");
            }

            if (relEnd.Type != orp.ObjectClass)
            {
                result.Add(String.Format("Navigator is attached to {0} but should be attached to {1}",
                    orp.ObjectClass,
                    relEnd.Type));
            }
            if (otherEnd.Type != orp.ReferenceObjectClass)
            {
                result.Add(String.Format("Navigator is references {0} but should reference {1}",
                    orp.ReferenceObjectClass,
                    (relEnd.AParent ?? relEnd.BParent).GetOtherEnd(relEnd).Type));
            }

            e.Result = String.Join("\n", result.ToArray());
        }

        public void OnToString_ConsistentNavigatorConstraint(ConsistentNavigatorConstraint obj, MethodReturnEventArgs<string> e)
        {
            e.Result = "The navigator should be consistent with the defining Relation.";
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