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
        #region NewGuidDefaultValue
        public static void OnGetDefaultValue_NewGuidDefaultValue(Kistl.App.Base.NewGuidDefaultValue obj, MethodReturnEventArgs<System.Object> e)
        {
            e.Result = Guid.NewGuid();
        }

        public static void OnToString_NewGuidDefaultValue(Kistl.App.Base.NewGuidDefaultValue obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Property != null)
            {
                e.Result = string.Format("{0} will be initialized with a new Guid", obj.Property.Name);
            }
            else
            {
                e.Result = "Initializes a property with a new Guid";
            }
        }

        #endregion
    }
}
