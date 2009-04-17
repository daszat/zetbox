using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
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
            ViewDescriptor result = null;
            while (result == null && pmd != null)
            {
                result = FrozenContext.Single
                     .GetQuery<ViewDescriptor>()
                     .SingleOrDefault(obj => obj.Toolkit == tk
                         && obj.VisualType == vt
                         && obj.PresentedModelDescriptor == pmd);

                // crawl up the inheritance tree
                pmd = pmd.PresentableModelRef.Parent.GetPresentableModelDescriptor();
            }

            return result;
        }

        public static PresentableModelDescriptor GetPresentableModelDescriptor(this TypeRef tr)
        {
            PresentableModelDescriptor result = null;
            while (result == null && tr != null)
            {
                result = FrozenContext.Single
                    .GetQuery<PresentableModelDescriptor>()
                    .SingleOrDefault(obj => obj.PresentableModelRef.ID == tr.ID);
                tr = tr.Parent;
            }
            return result;
        }

    }
}
