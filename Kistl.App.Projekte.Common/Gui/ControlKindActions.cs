
namespace Kistl.App.Gui.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.GUI;

    /// <summary>
    /// The collected default actions for ControlKind
    /// </summary>
    public class ControlKindActions
    {
        /// <summary>
        /// Creates the ToString() result for a specified ControlKind.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        public void OnToString(ControlKind kind, MethodReturnEventArgs<string> e)
        {
            if (kind == null)
            {
                e.Result = "(null)";
                return;
            }

            e.Result = kind.GetInterfaceType().Type.Name;
        }
    }
}
