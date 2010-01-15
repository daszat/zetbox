using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Client
{
    /// <summary>
    /// Temp. Kist Objects Extensions
    /// </summary>
    internal static class TheseMethodsShouldBeImplementedOnKistlObjects
    {
        public static string ToUserString(this DataObjectState state)
        {
            switch (state)
            {
                case DataObjectState.New:
                case DataObjectState.Modified:
                    return "+";
                case DataObjectState.Deleted:
                    return "//";
                case DataObjectState.Unmodified:
                default:
                    return String.Empty;
            }
        }
    }
}
