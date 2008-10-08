using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Client;
using System.Xml;
using System.IO;
using Kistl.API;

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
            e.Result = String.Format("{0} should not be NULL", obj.ConstrainedProperty);
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
                    result.AppendFormat("Value should be equal or greater than {0}", obj.Min);
                if (v > obj.Max)
                    result.AppendFormat("Value should be equal or less than {0}", obj.Max);

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
            e.Result = String.Format("{0} <= {1} <= {2}", obj.Min, obj.ConstrainedProperty, obj.Max);
        }

        #endregion

    }
}