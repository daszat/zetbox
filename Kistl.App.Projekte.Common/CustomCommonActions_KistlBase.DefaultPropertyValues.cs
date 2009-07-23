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
    public partial class CustomCommonActions_KistlBase
    {
        #region NewGuidDefaultValue
        public void OnGetDefaultValue_NewGuidDefaultValue(Kistl.App.Base.NewGuidDefaultValue obj, MethodReturnEventArgs<System.Object> e)
        {
            e.Result = Guid.NewGuid();
        }

        public void OnToString_NewGuidDefaultValue(Kistl.App.Base.NewGuidDefaultValue obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Property != null)
            {
                e.Result = string.Format("{0} will be initialized with a new Guid", obj.Property.PropertyName);
            }
            else
            {
                e.Result = "Initializes a property with a new Guid";
            }
        }

        #endregion
    }
}
