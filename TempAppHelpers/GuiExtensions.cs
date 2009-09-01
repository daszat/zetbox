
namespace Kistl.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.GUI;
    using Kistl.API.Utils;

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
            var defaultKind = pmd.AndParents().Select(p => p.DefaultKind).First(dk => dk != null);
            return pmd.GetViewDescriptor(tk, defaultKind);
        }

        /// <summary>
        /// Look up the ViewDescriptor for this presentable model and ControlKind
        /// </summary>
        /// <param name="self"></param>
        /// <param name="tk"></param>
        /// <param name="ck"></param>
        /// <returns></returns>
        public static ViewDescriptor GetViewDescriptor(
            this PresentableModelDescriptor self,
            Toolkit tk,
            ControlKind ck)
        {
            PrimeCaches(tk);

            var candidates = _viewCaches[tk].GetDescriptors(ck.GetObjectClass(FrozenContext.Single));
            // TODO: resolve ambiguities from config if candidates.Count > 1
            return candidates.FirstOrDefault();
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
        private static Dictionary<Toolkit, ViewDescriptorCache> _viewCaches = new Dictionary<Toolkit, ViewDescriptorCache>();


        private static void PrimeCaches(Toolkit? tk)
        {
            if (_pmdCache == null)
            {
                _pmdCache = GetPresentableModelDescriptorByTypeRefCache();
            }
            if (tk.HasValue)
            {
                if (!_viewCaches.ContainsKey(tk.Value))
                {
                    _viewCaches[tk.Value] = ViewDescriptorCache.CreateCache(tk.Value);
                }

                if (!_viewCaches.ContainsKey(tk.Value))
                {
                    _viewCaches[tk.Value] = ViewDescriptorCache.CreateCache(tk.Value);
                }
            }
        }

        private static Dictionary<int, PresentableModelDescriptor> GetPresentableModelDescriptorByTypeRefCache()
        {
            return FrozenContext.Single
                .GetQuery<PresentableModelDescriptor>()
                .ToDictionary(obj => obj.PresentableModelRef.ID);
        }
    }

    internal class ViewDescriptorCache
    {
        private ViewDescriptorCache() { }

        private Dictionary<ObjectClass, ReadOnlyCollection<ViewDescriptor>> _vdCache = new Dictionary<ObjectClass, ReadOnlyCollection<ViewDescriptor>>();

        private static readonly ReadOnlyCollection<ViewDescriptor> EmptyList = new ReadOnlyCollection<ViewDescriptor>(new List<ViewDescriptor>(0));

        public static ViewDescriptorCache CreateCache(Toolkit tk)
        {
            var result = new ViewDescriptorCache();
            result._vdCache = FrozenContext.Single
                .GetQuery<ViewDescriptor>()
                .Where(obj => obj.Toolkit == tk && obj.Kind != null)
                .GroupBy(obj => obj.Kind)
                .ToDictionary(g => (ObjectClass)g.Key, g => new ReadOnlyCollection<ViewDescriptor>(g.ToList()));
            return result;
        }

        public ReadOnlyCollection<ViewDescriptor> GetDescriptors(ObjectClass cls)
        {
            if (_vdCache.ContainsKey(cls))
            {
                return _vdCache[cls];
            }
            else
            {
                return EmptyList;
            }
        }
    }
}
