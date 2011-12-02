
namespace Kistl.App.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;

    /// <summary>
    /// The collected default actions for ControlKind
    /// </summary>
    [Implementor]
    public static class ControlKindActions
    {
        /// <summary>
        /// Creates the ToString() result for a specified ControlKind.
        /// </summary>
        [Invocation]
        public static void ToString(ControlKind kind, MethodReturnEventArgs<string> e)
        {
            if (kind == null)
            {
                e.Result = "(null)";
                return;
            }

            e.Result = kind.Name;
        }

        [Invocation]
        public static void GetName(ControlKind kind, MethodReturnEventArgs<string> e)
        {
            e.Result = "Gui.ControlKinds." + kind.Name;
        }
    }
}
