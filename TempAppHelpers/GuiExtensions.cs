
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
        /// Looks up the default ViewDesriptor matching the ViewModel and Toolkit; 
        /// uses the ViewModelDescriptor's DefaultVisualType
        /// </summary>
        /// <param name="pmd">the specified ViewModelDescriptor</param>
        /// <param name="tk">the specified Toolkit</param>
        /// <returns>the default ViewDescriptor to display this ViewModel with this Toolkit</returns>
        public static ViewDescriptor GetDefaultViewDescriptor(
            this ViewModelDescriptor pmd,
            Toolkit tk)
        {
            var defaultKind = GetDefaultKind(pmd);
            if (defaultKind != null)
            {
                return pmd.GetViewDescriptor(tk, defaultKind);
            }
            else
            {
                Logging.Log.WarnFormat("No default control kind for {0} found.", pmd.ViewModelRef.ToString());
                return null;
            }
        }

        /// <summary>
        /// Returns the default control kind of a given ViewModelDescriptor.
        /// </summary>
        /// <param name="pmd"></param>
        /// <returns></returns>
        public static ControlKind GetDefaultKind(this ViewModelDescriptor pmd)
        {
            return pmd.AndParents().Select(p => p.DefaultKind).FirstOrDefault(dk => dk != null);
        }

        /// <summary>
        /// Returns the default control kind for use in grid cells of a given ViewModelDescriptor.
        /// </summary>
        /// <param name="pmd"></param>
        /// <returns></returns>
        public static ControlKind GetDefaultGridCellKind(this ViewModelDescriptor pmd)
        {
            return pmd.AndParents().Select(p => p.DefaultGridCellKind).FirstOrDefault(dk => dk != null) ?? pmd.GetDefaultKind();
        }



        /// <summary>
        /// Look up the ViewDescriptor for this presentable model and ControlKind
        /// </summary>
        /// <param name="self"></param>
        /// <param name="tk"></param>
        /// <param name="ck"></param>
        /// <returns></returns>
        public static ViewDescriptor GetViewDescriptor(
            this ViewModelDescriptor self,
            Toolkit tk,
            ControlKind ck)
        {
            if (ck == null) throw new ArgumentNullException("ck");

            PrimeCaches(tk);

            var candidates = _viewCaches[tk].GetDescriptors(ck.GetInterfaceType().GetObjectClass(FrozenContext.Single));
            // TODO: resolve ambiguities from config if candidates.Count > 1
            return candidates.FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of the specified ViewModelDescriptor and its parents descriptors.
        /// </summary>
        /// <param name="pmd">the ViewModelDescriptor to inspect</param>
        /// <returns>a list, containing the requested ViewModelDescriptors sorted by ascending inheritance</returns>
        public static List<ViewModelDescriptor> AndParents(this ViewModelDescriptor pmd)
        {
            var result = new List<ViewModelDescriptor>();
            result.Add(pmd);
            var parent = pmd.ViewModelRef.Parent;
            while (parent != null)
            {
                var parentDescriptor = parent.GetViewModelDescriptor();
                if (parentDescriptor != null)
                {
                    result.Add(parentDescriptor);
                }
                parent = parent.Parent;
            }
            return result;
        }

        public static ViewModelDescriptor GetViewModelDescriptor(this TypeRef tr)
        {
            if (tr == null) { throw new ArgumentNullException("tr"); }

            PrimeCaches(null);

            ViewModelDescriptor result = null;
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

        private static Dictionary<int, ViewModelDescriptor> _pmdCache = null;
        private static Dictionary<Toolkit, ViewDescriptorCache> _viewCaches = new Dictionary<Toolkit, ViewDescriptorCache>();


        private static void PrimeCaches(Toolkit? tk)
        {
            if (_pmdCache == null)
            {
                _pmdCache = GetViewModelDescriptorByTypeRefCache();
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

        private static Dictionary<int, ViewModelDescriptor> GetViewModelDescriptorByTypeRefCache()
        {
            return FrozenContext.Single
                .GetQuery<ViewModelDescriptor>()
                .ToDictionary(obj => obj.ViewModelRef.ID);
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
