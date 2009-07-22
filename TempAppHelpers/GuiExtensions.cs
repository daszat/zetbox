
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;

    /// <summary>
    /// A set of extension methods for the GUI module.
    /// </summary>
    public static class GuiExtensions
    {
        /// <summary>
        /// Looks up the default ViewDesriptor matching the PresentableModel and Toolkit; 
        /// uses the PresentableModelDescriptor's DefaultVisualType
        /// </summary>
        /// <param name="pmd">the specified PresentableModelDescriptor</param>
        /// <param name="tk">the specified Toolkit</param>
        /// <returns>the default ViewDescriptor to display this PresentableModel with this Toolkit</returns>
        public static ViewDescriptor GetDefaultViewDescriptor(
            this PresentableModelDescriptor pmd,
            Toolkit tk)
        {
            return pmd.GetViewDescriptor(tk, pmd.DefaultVisualType, false);
        }

        /// <summary>
        /// Looks up the default ViewDesriptor matching the PresentableModel and Toolkit; 
        /// uses the PresentableModelDescriptor's DefaultVisualType
        /// </summary>
        /// <param name="pmd">the specified PresentableModelDescriptor</param>
        /// <param name="tk">the specified Toolkit</param>
        /// <param name="readOnly">indicate whether or not a read-only view is searched</param>
        /// <returns>the default ViewDescriptor to display this PresentableModel with this Toolkit</returns>
        public static ViewDescriptor GetDefaultViewDescriptor(
            this PresentableModelDescriptor pmd,
            Toolkit tk,
            bool readOnly)
        {
            return pmd.GetViewDescriptor(tk, pmd.DefaultVisualType, readOnly);
        }

        /// <summary>
        /// Looks up the ViewDescriptor matching the PresentableModel, Toolkit and VisualType
        /// </summary>
        /// <param name="pmd">the specified PresentableModelDescriptor</param>
        /// <param name="tk">the specified Toolkit</param>
        /// <param name="vt">the specified VisualType</param>
        /// <returns>the ViewDescriptor to display this PresentableModel with this Toolkit</returns>
        public static ViewDescriptor GetViewDescriptor(
            this PresentableModelDescriptor pmd,
            Toolkit tk,
            VisualType vt)
        {
            return pmd.GetViewDescriptor(tk, vt, false);
        }

        /// <summary>
        /// Looks up the ViewDescriptor matching the PresentableModel, Toolkit and VisualType
        /// </summary>
        /// <param name="self">the specified PresentableModelDescriptor</param>
        /// <param name="tk">the specified Toolkit</param>
        /// <param name="vt">the specified VisualType</param>
        /// <param name="readOnly">indicate whether or not a read-only view is searched</param>
        /// <returns>the ViewDescriptor to display this PresentableModel with this Toolkit</returns>
        public static ViewDescriptor GetViewDescriptor(
            this PresentableModelDescriptor self,
            Toolkit tk,
            VisualType vt,
            bool readOnly)
        {
            PrimeCaches(tk);
            if (!_vdCache[tk].ContainsKey(vt))
            {
                return null;
            }
            var candidates = _vdCache[tk][vt].ToLookup(obj => obj.PresentedModelDescriptor);

            List<ViewDescriptor> result = new List<ViewDescriptor>();
            foreach (var pmd in self.AndParents())
            {
                if (candidates.Contains(pmd))
                {
                    var test = candidates[pmd];
                    result.AddRange(candidates[pmd]);
                }
            }

            // fall back to any available control, if we don't have one with matching read-only state
            var viewDesc = result.FirstOrDefault(vd => vd.IsReadOnly == readOnly) ?? result.FirstOrDefault();

            if (viewDesc == null)
            {
                System.Diagnostics.Trace.TraceWarning("Found no {0} ViewDescriptor for {1}",
                    vt,
                    self);
            }
            else if (viewDesc.PresentedModelDescriptor != self)
            {
                System.Diagnostics.Trace.TraceWarning("Using inherited {0} ViewDescriptor from {1} for {2}",
                    vt,
                    viewDesc.PresentedModelDescriptor,
                    self);
            }

            return viewDesc;
        }

        /// <summary>
        /// Returns a list of the specified PresentableModelDescriptor and its parents descriptors.
        /// </summary>
        /// <param name="pmd">the PresentableModelDescriptor to inspect</param>
        /// <returns>a list, containing the requested PresentableModelDescriptors sorted by ascending inheritance</returns>
        public static List<PresentableModelDescriptor> AndParents(this PresentableModelDescriptor pmd)
        {
            var result = new List<PresentableModelDescriptor>();
            result.Add(pmd);
            var parent = pmd.PresentableModelRef.Parent;
            while (parent != null)
            {
                var parentDescriptor = parent.GetPresentableModelDescriptor();
                if (parentDescriptor != null)
                {
                    result.Add(parentDescriptor);
                }
                parent = parent.Parent;
            }
            return result;
        }

        /// <summary>
        /// Retrieves a list of <see cref="ViewDescriptor"/>s matching the specified <see cref="PresentableModelDescriptor"/>
        /// </summary>
        /// <param name="pmd">the PresentableModelDescriptor to search for</param>
        /// <returns>a unordered list of <see cref="ViewDescriptor"/>s</returns>
        public static ICollection<ViewDescriptor> GetApplicableViewDescriptors(this PresentableModelDescriptor pmd)
        {
            var presentableModels = pmd.AndParents();
            return _vdCache.Values
                .SelectMany(sub => sub.Values.SelectMany(o => o))
                .Where(vd => presentableModels.Contains(vd.PresentedModelDescriptor))
                .ToList();
        }

        public static PresentableModelDescriptor GetPresentableModelDescriptor(this TypeRef tr)
        {
            PrimeCaches(null);

            PresentableModelDescriptor result = null;
            while (result == null && tr != null)
            {
                if (_pmdCache.ContainsKey(tr.ID))
                {
                    result = _pmdCache[tr.ID];
                }
                tr = tr.Parent;
            }
            return result;
        }

        private static Dictionary<int, PresentableModelDescriptor> _pmdCache = null;
        private static Dictionary<Toolkit, Dictionary<VisualType, List<ViewDescriptor>>> _vdCache = null;

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
                    _vdCache = new Dictionary<Toolkit, Dictionary<VisualType, List<ViewDescriptor>>>();
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

        private static Dictionary<VisualType, List<ViewDescriptor>> GetViewDescriptorByVisualTypeCache(Toolkit tk)
        {
            return FrozenContext.Single
                .GetQuery<ViewDescriptor>()
                .Where(obj => obj.Toolkit == tk)
                .GroupBy(obj => obj.VisualType)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
