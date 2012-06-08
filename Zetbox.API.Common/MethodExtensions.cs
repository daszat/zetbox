using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zetbox.App.Base;

namespace Zetbox.App.Extensions
{
    public static class MethodExtensions
    {
        /// <summary>
        /// returns true if the Method is one of the "default" methods, "ToString", "ObjectIsValid", "NotifyPreSave", "NotifyPostSave", "NotifyCreated" or "NotifyDeleting".
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
                case "ObjectIsValid":
                    return true;
                default:
                    return false;
            }
        }
    }
}
