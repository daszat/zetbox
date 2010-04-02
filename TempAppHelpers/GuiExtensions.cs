
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
        public static ViewDescriptor GetViewDescriptor(
            this ViewModelDescriptor pmd,
            Toolkit tk)
        {
            return GetViewDescriptor(pmd, tk, GetDefaultKind(pmd));
        }

        private sealed class ViewDescCacheKey
        {
            public ViewDescCacheKey(TypeRef vmd,
                Toolkit tk,
                InterfaceType? ck)
            {
                this.vmd = vmd;
                this.tk = tk;
                this.ck = ck;
            }

            private readonly TypeRef vmd;
            private readonly Toolkit tk;
            private readonly InterfaceType? ck;

            public override bool Equals(object obj)
            {
                var other = obj as ViewDescCacheKey;
                if (other == null) return false;
                return object.Equals(this.vmd, other.vmd)
                    && object.Equals(this.tk, other.tk)
                    && object.Equals(this.ck, other.ck);
            }

            public override int GetHashCode()
            {
                return vmd.AsType(true).GetHashCode()
                    + (ck.HasValue ? ck.Value.Type.GetHashCode() : 0)
                    + (int)this.tk;
            }
        }

        private static Dictionary<ViewDescCacheKey, ViewDescriptor> _viewDescriptorCache = new Dictionary<ViewDescCacheKey, ViewDescriptor>();


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
            if (self == null) throw new ArgumentNullException("self");
            PrimeCaches(tk);

            var key = new ViewDescCacheKey(self.ViewModelRef, tk, ck != null ? (InterfaceType?)ck.GetInterfaceType() : null);
            if (_viewDescriptorCache.ContainsKey(key)) return _viewDescriptorCache[key];

            ViewDescriptor result = null;

            ICollection<ViewDescriptor> candidates;
            if (ck != null)
            {
                candidates = _viewCaches[tk].GetDescriptors(ck.GetInterfaceType().GetObjectClass(FrozenContext.Single)).ToList();
            }
            else
            {
                // TODO #1509: Frozen Context objects does not have self.Context set.
                candidates = FrozenContext.Single.GetQuery<ViewDescriptor>().ToList();
            }

            if (candidates.Count == 0)
            {
                Logging.Log.WarnFormat("Couldn't find ViewDescriptor for '{1}' matching ControlKind: '{0}'", ck, self.GetType().FullName);
            }
            else if (candidates.Count == 1)
            {
                result = candidates.First();
            }
            else
            {
                var allTypes = GetAllTypes(self);
                // As allTypes is sorted from most specific to least specific first or default is perfect.
                var match = allTypes.SelectMany(t => candidates.Where(c => c.SupportedViewModels.Contains(t))).FirstOrDefault();
                if (match == null)
                {
                    Logging.Log.WarnFormat("Couldn't find ViewDescriptor for '{1}' matching ControlKind: '{0}'", ck, self.GetType().FullName);
                }
                result = match;
            }

            _viewDescriptorCache[key] = result;
            return result;
        }

        private static IList<TypeRef> GetAllTypes(ViewModelDescriptor self)
        {
            var allTypes = new List<TypeRef>();
            var type = self.ViewModelRef;
            while (type != null)
            {
                allTypes.Add(type);
                type = type.Parent;
            }

            // TODO #1509: Frozen Context objects does not have self.Context set.
            allTypes.AddRange(
                self.ViewModelRef.AsType(false).GetInterfaces()
                    .OrderBy(i => i.FullName)
                    .Select(i => i.ToRef(FrozenContext.Single))
                    .Where(i => i != null)
            );
            return allTypes;
        }

        public static ViewDescriptor GetViewDescriptor(
            this ViewModelDescriptor self,
            Toolkit tk,
            InterfaceType ckcInterface)
        {
            ViewDescriptor visualDesc;
            if (ckcInterface == null)
            {
                visualDesc = GetViewDescriptor(self, tk);
            }
            else
            {
                var defaultKind = self.GetDefaultKind();
                if (defaultKind != null && ckcInterface.IsAssignableFrom(defaultKind.GetInterfaceType()))
                {
                    visualDesc = GetViewDescriptor(self, tk);
                }
                else
                {
                    ControlKind controlKind = self.SecondaryControlKinds.Where(ck => ckcInterface.IsAssignableFrom(ck.GetInterfaceType())).SingleOrDefault();
                    if (controlKind == null && self.ViewModelRef.Parent != null)
                    {
                        var parentDescriptor = self.ViewModelRef.Parent.GetViewModelDescriptor();
                        if (parentDescriptor != null)
                        {
                            // recursively iterate up the inheritance tree
                            visualDesc = GetViewDescriptor(parentDescriptor, tk, ckcInterface);
                        }
                        else
                        {
                            Logging.Log.WarnFormat("Couldn't find matching controlKind: '{0}'", ckcInterface);
                            visualDesc = null;
                        }
                    }
                    else
                    {
                        visualDesc = GetViewDescriptor(self, tk, controlKind);
                    }
                }
            }
            return visualDesc;
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
