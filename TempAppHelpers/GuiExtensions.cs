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
            PrimeCaches(tk);
            var candidates = _vdCache[tk][vt].ToDictionary(obj => obj.PresentedModelDescriptor);

            ViewDescriptor result = null;
            while (result == null && pmd != null)
            {
                if (candidates.ContainsKey(pmd))
                {
                    result = candidates[pmd];
                }

                // crawl up the inheritance tree
                pmd = pmd.PresentableModelRef.Parent
                    .GetPresentableModelDescriptor();
            }

            return result;
        }

        public static PresentableModelDescriptor GetPresentableModelDescriptor(this TypeRef tr)
        {
            PrimeCaches(null);

            PresentableModelDescriptor result = null;
            while (result == null && tr != null)
            {
                if (_pmdCache.ContainsKey(tr.ID))
                    result = _pmdCache[tr.ID];
                tr = tr.Parent;
            }
            return result;
        }

        private static Dictionary<int, PresentableModelDescriptor> _pmdCache = null;
        private static Dictionary<Toolkit, ILookup<VisualType, ViewDescriptor>> _vdCache = null;

        private static void PrimeCaches(Toolkit? tk)
        {
            if (_pmdCache == null)
            {
                _pmdCache = GetPresentableModelDescriptorByTypeRefCache();
            }
            if (tk.HasValue)
            {
                if (_vdCache == null)
                {
                    _vdCache = new Dictionary<Toolkit, ILookup<VisualType, ViewDescriptor>>();
                }
                if (!_vdCache.ContainsKey(tk.Value))
                {
                    _vdCache[tk.Value] = GetViewDescriptorByVisualTypeCache(tk.Value);
                }
            }
        }

        private static Dictionary<int, PresentableModelDescriptor> GetPresentableModelDescriptorByTypeRefCache()
        {
            return FrozenContext.Single
                .GetQuery<PresentableModelDescriptor>()
                .ToDictionary(obj => obj.PresentableModelRef.ID);
        }

        private static ILookup<VisualType, ViewDescriptor> GetViewDescriptorByVisualTypeCache(Toolkit tk)
        {
            return FrozenContext.Single
                .GetQuery<ViewDescriptor>()
                .Where(obj => obj.Toolkit == tk)
                .ToLookup(obj => obj.VisualType);
        }
    }
}
