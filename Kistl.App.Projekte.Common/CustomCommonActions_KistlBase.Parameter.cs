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
        public static void OnGetParameterTypeString_DecimalParameter(Kistl.App.Base.DecimalParameter obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = "System.Decimal";
        }
    }
}
