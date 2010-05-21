
namespace Kistl.App.GUI.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    /// <summary>
    /// The collected default actions for ControlKind
    /// </summary>
    public static class ControlKindActions
    {
        /// <summary>
        /// Creates the ToString() result for a specified ControlKind.
        /// </summary>
        public static void OnToString(ControlKind kind, MethodReturnEventArgs<string> e)
        {
            if (kind == null)
            {
                e.Result = "(null)";
                return;
            }

            e.Result = kind.Name;
        }
    }
}
