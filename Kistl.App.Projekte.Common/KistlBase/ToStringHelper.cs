using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;

namespace Kistl.App.Base
{
    static class ToStringHelper
    {
        /// <summary>
        /// Since floating objects might have no valid/useful ToString() result yet, prefix them with the typename and id.
        /// </summary>
        /// <param name="obj">The current object</param>
        /// <param name="e">The ToString MethodReturnEventArgs.</param>
        internal static void FixupFloatingObjectsToString(IDataObject obj, MethodReturnEventArgs<string> e)
        {
            if (obj.Context != null && obj.Context.IsReadonly) return;
            if (Helper.IsFloatingObject(obj))
            {
                e.Result = String.Format("new {0}(#{1}): {2}", obj.GetType().Name, obj.ID, e.Result);
            }
        }
    }
}
