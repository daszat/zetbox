using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.GUI;

namespace Kistl.App.Extensions
{
    public static class GuiExtensions
    {

        /// <summary>
        /// Looks up the ViewDesriptor matching the PresentableModel and Toolkit; 
        /// uses the PresentableModelDescriptor's DefaultVisualType
        /// </summary>
        /// <param name="pmd"></param>
        /// <param name="tk"></param>
        /// <returns></returns>
        public static ViewDescriptor GetDefaultViewDescriptor(
            this PresentableModelDescriptor pmd,
            Toolkit tk)
        {
            return pmd.GetViewDescriptor(tk, pmd.DefaultVisualType);
        }

        /// <summary>
        /// Looks up the ViewDescriptor matching the PresentableModel, Toolkit and VisualType
        /// </summary>
        /// <param name="pmd"></param>
        /// <param name="tk"></param>
        /// <param name="vt"></param>
        /// <returns></returns>
        public static ViewDescriptor GetViewDescriptor(
            this PresentableModelDescriptor pmd,
            Toolkit tk,
            VisualType vt)
        {

            var debug = FrozenContext.Single
                .GetQuery<ViewDescriptor>()
                .Where(obj => obj.Toolkit == tk
                    && obj.VisualType == vt
                    && obj.PresentedModelDescriptor == pmd);

            var result = debug.Single();

            return result;
        }

    }
}
