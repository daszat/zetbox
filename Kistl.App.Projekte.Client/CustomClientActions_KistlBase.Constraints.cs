using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using Kistl.API.Client;

namespace Kistl.App.Base
{
    public partial class CustomClientActions_KistlBase
    {

        #region Constraint

        public void OnIsValid_Constraint(
            Constraint obj,
            MethodReturnEventArgs<bool> e,
            object value)
        {
            // the base constraint accepts all values
            e.Result = true;
        }

        #endregion

        #region NotNullableConstraint

        public void OnIsValid_NotNullableConstraint(
            NotNullableConstraint obj,
            MethodReturnEventArgs<bool> e,
            object value)
        {
            e.Result &= value != null;
        }

        public void OnGetErrorText_NotNullableConstraint(
            NotNullableConstraint obj,
            MethodReturnEventArgs<string> e,
            object value)
        {
            e.Result = String.IsNullOrEmpty(obj.Reason) ? "Value must be set" : String.Format("Value must be set: {0}", obj.Reason);
        }

        public void OnToString_NotNullableConstraint(NotNullableConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} should not be NULL", obj.ConstrainedProperty.PropertyName);
        }

        #endregion

        #region IntegerRangeConstraint

        public void OnIsValid_IntegerRangeConstraint(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object value)
        {
            int v = (int)value;
            e.Result &= (obj.Min <= v) && (v <= obj.Max);
        }

        public void OnGetErrorText_IntegerRangeConstraint(
            IntegerRangeConstraint obj,
            MethodReturnEventArgs<string> e,
            object value)
        {
            int v = (int)value;
            if (obj.IsValid(value))
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

        public void OnToString_IntegerRangeConstraint(IntegerRangeConstraint obj, Kistl.API.MethodReturnEventArgs<string> e)
        {
            e.Result = String.Format("{0} <= {1} <= {2}", obj.Min, obj.ConstrainedProperty.PropertyName, obj.Max);
        }

        #endregion

        #region StringRangeConstraint

        public void OnIsValid_StringRangeConstraint(
            StringRangeConstraint obj,
            MethodReturnEventArgs<bool> e,
            object value)
        {
            int length = value.ToString().Length;
            e.Result &= (obj.MinLength <= length) && (length <= obj.MaxLength);
        }

        public void OnGetErrorText_StringRangeConstraint(
            StringRangeConstraint obj,
            MethodReturnEventArgs<string> e,
            object value)
        {
            int length = value.ToString().Length;
            if (obj.IsValid(value))
            {
                e.Result = null;
            }
            else
            {
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
                    obj.ConstrainedProperty.PropertyName,
                    obj.MinLength,
                    obj.MaxLength);
            }
            else
            {
                e.Result = String.Format("{0} should have at most {1} characters",
                    obj.ConstrainedProperty.PropertyName,
                    obj.MaxLength);
            }
        }

        #endregion
    }
}