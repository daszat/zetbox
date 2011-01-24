using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

namespace Kistl.App.Extensions
{
    public static class MethodExtensions
    {
        /// <summary>
        /// returns true if the Method is one of the "default" methods, "ToString", "NotifyPreSave", "NotifyPostSave", "NotifyCreated" or "NotifyDeleting".
        /// </summary>
        public static bool IsDefaultMethod(this Method method)
        {
            if (method == null) { throw new ArgumentNullException("method"); }

            switch (method.Name)
            {
                case "ToString":
                case "NotifyPreSave":
                case "NotifyPostSave":
                case "NotifyCreated":
                case "NotifyDeleting":
                    return true;
                default:
                    return false;
            }
        }
    }
}
