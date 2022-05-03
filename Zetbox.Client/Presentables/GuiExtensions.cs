// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;

    /// <summary>
    /// A set of extension methods for the GUI module.
    /// </summary>
    public static class GuiExtensions
    {
        private static ViewDescriptorCache _viewDescriptorCache = new ViewDescriptorCache();
        private static ViewModelDescriptorCache _pmdCache = null;
        private static ViewDescriptorToolkitCache _viewCaches = new ViewDescriptorToolkitCache();

        /// <summary>
        /// Looks up the default ViewDesriptor matching the ViewModel and Toolkit; 
        /// uses the ViewModelDescriptor's DefaultVisualType
        /// </summary>
        /// <param name="pmd">the specified ViewModelDescriptor</param>
        /// <param name="tk">the specified Toolkit</param>
        /// <returns>the default ViewDescriptor to display this ViewModel with this Toolkit</returns>
        public static Task<ViewDescriptor> GetViewDescriptor(
            this ViewModelDescriptor pmd,
            Toolkit tk)
        {
            return GetViewDescriptor(pmd, tk, GetDefaultEditorKind(pmd));
        }

        //// Steps for resolving a ViewModel to View
        //// 1. Find all ViewModel requested Views matching the ControlKind
        //// 2. Find all Views supporting the ViewModel matching the ControlKind
        //// 3. Find all Views supporting the ViewModel without ControlKind

        /// <summary>
        /// Look up the ViewDescriptor for this presentable model and ControlKind
        /// </summary>
        /// <param name="self"></param>
        /// <param name="tk"></param>
        /// <param name="requestedControlKind"></param>
        /// <returns></returns>
        public static async Task<ViewDescriptor> GetViewDescriptor(
            this ViewModelDescriptor self,
            Toolkit tk,
            ControlKind requestedControlKind)
        {
            // Checks
            if (self == null) throw new ArgumentNullException("self");

            #region Cache Management
            PrimeCaches(tk, self.ReadOnlyContext);

            var key = new ViewDescriptorCache.Key(self.ViewModelTypeRef, tk, requestedControlKind);
            if (_viewDescriptorCache.ContainsKey(key)) return _viewDescriptorCache[key];
            #endregion

            var defaultEditorKind = await self.GetProp_DefaultEditorKind();
            // If the ViewModel has a more specific DefaultKind respect its choice
            if (defaultEditorKind != null
                && requestedControlKind != null
                && defaultEditorKind.AndParents().Select(i => i.ExportGuid).Contains(requestedControlKind.ExportGuid))
            {
                if (requestedControlKind != defaultEditorKind)
                {
                    Logging.Log.DebugFormat("Using more specific default kind: {0} -> {1}", requestedControlKind.Name, defaultEditorKind.Name);
                }
                requestedControlKind = defaultEditorKind;
            }
            else
            {
                requestedControlKind = (await self.GetProp_SecondaryControlKinds())
                    .FirstOrDefault(
                        sck => sck.AndParents()
                                .Select(i => i.ExportGuid)
                                .Contains(requestedControlKind.ExportGuid)
                    ) ?? requestedControlKind;
            }

            ViewDescriptor result = null;

            ICollection<ViewDescriptor> candidates;
            if (requestedControlKind != null)
            {
                candidates = _viewCaches[tk].GetDescriptors(requestedControlKind);
            }
            else
            {
                candidates = _viewCaches[tk].GetDescriptors();
            }

            if (candidates.Count == 0)
            {
                // Try parent
                var type = Type.GetType(self.ViewModelTypeRef);
                var parent = type != null && type.BaseType != null ? type.BaseType.GetViewModelDescriptor(self.ReadOnlyContext) : null;
                if (parent != null)
                {
                    result = await GetViewDescriptor(parent, tk, requestedControlKind);
                }
                else
                {
                    Logging.Log.WarnFormat("Couldn't find View for '{1}' matching ControlKind: '{0}'", requestedControlKind, self.ViewModelTypeRef);
                }
            }
            else if (candidates.Count == 1)
            {
                result = candidates.First();
            }
            else
            {
                var allTypes = GetAllTypes(self);
                // As allTypes is sorted from most specific to least specific, so first or default is perfect.
                var match = allTypes.SelectMany(vmType => candidates.Where(candidate =>
                {
                    var viewType = Type.GetType(candidate.ControlTypeRef, throwOnError: false);
                    if (viewType == null) return false;
                    var supportedViewModels = viewType.GetInterfaces()
                        .Where(ifType => ifType.IsGenericType && ifType.GetGenericTypeDefinition() == typeof(IHasViewModel<>))
                        .Select(ifType => ifType.GetGenericArguments().Single());

                    return supportedViewModels.Contains(vmType);
                })).FirstOrDefault();

                // Log a warning if nothing found
                if (match == null)
                {
                    Logging.Log.WarnFormat("Couldn't find View for '{1}' matching ControlKind: '{0}'", requestedControlKind, self.ViewModelTypeRef);
                }
                result = match;
            }

            _viewDescriptorCache[key] = result;
            return result;
        }

        private static List<Type> GetAllTypes(ViewModelDescriptor self)
        {
            var result = new List<Type>();
            var type = Type.GetType(self.ViewModelTypeRef);

            GetAllTypes(type, result);
            result.Reverse();

            return result;
        }

        // Inverse recursion
        // This goes from the most general type to the derived types
        // so that the interfaces are sorted in the place where they are
        // actually implemented.
        private static void GetAllTypes(Type type, List<Type> result)
        {
            if (type != typeof(ViewModel))
                GetAllTypes(type.BaseType, result);

            result.AddRange(type.GetInterfaces().Except(result).OrderBy(i => i.FullName));
            result.Add(type);
        }


        /// <summary>
        /// Returns the default control kind of a given ViewModelDescriptor.
        /// </summary>
        /// <param name="pmd"></param>
        /// <returns></returns>
        public static ControlKind GetDefaultEditorKind(this ViewModelDescriptor pmd)
        {
            return pmd.AndParents().Select(p => p.DefaultEditorKind).FirstOrDefault(ck => ck != null);
        }

        /// <summary>
        /// Returns the default display control kind of a given ViewModelDescriptor.
        /// </summary>
        /// <param name="pmd"></param>
        /// <returns></returns>
        public static ControlKind GetDefaultDisplayKind(this ViewModelDescriptor pmd)
        {
            return pmd.AndParents().Select(p => p.DefaultDisplayKind).FirstOrDefault(ck => ck != null) ?? pmd.GetDefaultEditorKind();
        }

        /// <summary>
        /// Returns the default control kind for use in grid cells of a given ViewModelDescriptor.
        /// </summary>
        /// <param name="pmd"></param>
        /// <returns></returns>
        public static ControlKind GetDefaultGridCellPreEditorKind(this ViewModelDescriptor pmd)
        {
            return pmd.AndParents().Select(p => p.DefaultGridCellPreEditorKind).FirstOrDefault(ck => ck != null) ?? pmd.GetDefaultGridCellDisplayKind();

        }
        /// <summary>
        /// Returns the default display control kind for use in grid cells of a given ViewModelDescriptor. If empty, default grid cell is returned
        /// </summary>
        /// <param name="pmd"></param>
        /// <returns></returns>
        public static ControlKind GetDefaultGridCellDisplayKind(this ViewModelDescriptor pmd)
        {
            return pmd.AndParents().Select(p => p.DefaultGridCellDisplayKind).FirstOrDefault(ck => ck != null) ?? pmd.GetDefaultDisplayKind();
        }
        /// <summary>
        /// Returns the default editor control kind for use in grid cells of a given ViewModelDescriptor. If empty, default grid cell is returned
        /// </summary>
        /// <param name="pmd"></param>
        /// <returns></returns>
        public static ControlKind GetDefaultGridCellEditorKind(this ViewModelDescriptor pmd)
        {
            return pmd.AndParents().Select(p => p.DefaultGridCellEditorKind).FirstOrDefault(ck => ck != null) ?? pmd.GetDefaultEditorKind();
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
            var parent = Type.GetType(pmd.ViewModelTypeRef);

            if (parent != null) parent = parent.BaseType;

            while (parent != null)
            {
                var parentDescriptor = parent.GetViewModelDescriptor(pmd.ReadOnlyContext);
                if (parentDescriptor != null)
                {
                    result.Add(parentDescriptor);
                }
                parent = parent.BaseType;
            }
            return result;
        }

        public static List<ControlKind> AndParents(this ControlKind ck)
        {
            var result = new List<ControlKind>();
            result.Add(ck);
            var parent = ck.Parent;
            while (parent != null)
            {
                result.Add(parent);
                parent = parent.Parent;
            }
            return result;
        }

        private static ViewModelDescriptor GetViewModelDescriptor(this Type type, IReadOnlyZetboxContext ctx)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (type == null) { throw new ArgumentNullException("type"); }

            PrimeCaches(null, ctx);

            ViewModelDescriptor result = null;
            while (result == null && type != null)
            {
                var name = type.GetSimpleName();
                if (_pmdCache.ContainsKey(name))
                {
                    result = _pmdCache[name];
                }
                type = type.BaseType;
            }
            return result;
        }

        public static ViewModelDescriptor GetViewModelDescriptor(object mdl, IReadOnlyZetboxContext frozenCtx)
        {
            if (mdl == null) throw new ArgumentNullException("mdl");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            Type mdlType = mdl.GetType();
            var result = mdlType.GetViewModelDescriptor(frozenCtx);

            if (result == null)
            {
                Logging.Log.ErrorFormat("Unable to resolve the ViewModel '{0}'.", mdl.GetType());
            }

            return result;
        }


        private static void PrimeCaches(Toolkit? tk, IReadOnlyZetboxContext ctx)
        {
            if (_pmdCache == null)
            {
                _pmdCache = new ViewModelDescriptorCache(ctx);
            }
            if (tk.HasValue && !_viewCaches.ContainsKey(tk.Value))
            {
                _viewCaches[tk.Value] = ViewDescriptorToolkitCache.Content.CreateCache(tk.Value, ctx);
            }
        }
    }

    #region Caches
    internal class ViewDescriptorToolkitCache : Cache
    {
        private Dictionary<Toolkit, Content> _cache = new Dictionary<Toolkit, Content>();

        public bool ContainsKey(Toolkit key)
        {
            return _cache.ContainsKey(key);
        }

        public Content this[Toolkit key]
        {
            get
            {
                return _cache[key];
            }
            set
            {
                _cache[key] = value;
                ItemAdded();
            }
        }

        public override int ItemCount
        {
            get { return _cache.Count; }
        }

        public override void Clear()
        {
            _cache.Clear();
        }

        internal class Content
        {
            private Content() { }

            private Dictionary<Guid, ReadOnlyCollection<ViewDescriptor>> _vdCache = new Dictionary<Guid, ReadOnlyCollection<ViewDescriptor>>();
            private ReadOnlyCollection<ViewDescriptor> _allVDCache = null;

            private static readonly ReadOnlyCollection<ViewDescriptor> EmptyList = new ReadOnlyCollection<ViewDescriptor>(new List<ViewDescriptor>(0));

            public static Content CreateCache(Toolkit tk, IReadOnlyZetboxContext ctx)
            {
                var result = new Content();

                // All View Descriptors for the given Toolkit
                result._allVDCache = new ReadOnlyCollection<ViewDescriptor>(
                    ctx.GetQuery<ViewDescriptor>().WithEagerLoading().Where(obj => obj.Toolkit == tk).ToList());

                // Dictionary by Kind
                result._vdCache = result._allVDCache.Where(obj => obj.ControlKind != null).GroupBy(obj => obj.ControlKind)
                    .ToDictionary(g => g.Key.ExportGuid, g => new ReadOnlyCollection<ViewDescriptor>(g.ToList()));

                return result;
            }

            public ReadOnlyCollection<ViewDescriptor> GetDescriptors(ControlKind c)
            {
                if (_vdCache.ContainsKey(c.ExportGuid))
                {
                    return _vdCache[c.ExportGuid];
                }
                else
                {
                    return EmptyList;
                }
            }

            public ReadOnlyCollection<ViewDescriptor> GetDescriptors()
            {
                return _allVDCache;
            }
        }
    }

    internal class ViewDescriptorCache : Cache
    {
        private Dictionary<Key, ViewDescriptor> _cache = new Dictionary<Key, ViewDescriptor>();

        public bool ContainsKey(Key key)
        {
            return _cache.ContainsKey(key);
        }

        public ViewDescriptor this[Key key]
        {
            get
            {
                return _cache[key];
            }
            set
            {
                _cache[key] = value;
                ItemAdded();
            }
        }

        public override int ItemCount
        {
            get { return _cache.Count; }
        }

        public override void Clear()
        {
            _cache.Clear();
        }

        internal sealed class Key
        {
            public Key(string vmd,
                        Toolkit tk,
                        ControlKind ck)
            {
                this.vmd = vmd;
                this.tk = tk;
                this.ck = ck != null ? ck.ExportGuid : Guid.Empty;
            }

            private readonly string vmd;
            private readonly Toolkit tk;
            private readonly Guid ck;

            public override bool Equals(object obj)
            {
                var other = obj as Key;
                if (other == null) return false;
                return object.Equals(this.vmd, other.vmd)
                    && object.Equals(this.tk, other.tk)
                    && object.Equals(this.ck, other.ck);
            }

            public override int GetHashCode()
            {
                return vmd.GetHashCode()
                    + ck.GetHashCode()
                    + (int)this.tk;
            }
        }

    }

    internal class ViewModelDescriptorCache : Cache
    {
        private Dictionary<string, ViewModelDescriptor> _cache = null;
        private IReadOnlyZetboxContext _context = null;

        public ViewModelDescriptorCache(IReadOnlyZetboxContext ctx)
        {
            _context = ctx;
            FillCache();
        }

        private void FillCache()
        {
            _cache = _context
                .GetQuery<ViewModelDescriptor>()
                .Where(obj => obj.ViewModelTypeRef != "ERROR" && obj.ViewModelTypeRef != null && obj.ViewModelTypeRef != string.Empty)
                .ToDictionary(obj => obj.ViewModelTypeRef);
        }

        public bool ContainsKey(string key)
        {
            return _cache.ContainsKey(key);
        }

        public ViewModelDescriptor this[string key]
        {
            get
            {
                return _cache[key];
            }
        }

        public override int ItemCount
        {
            get { return _cache.Count; }
        }

        public override void Clear()
        {
            FillCache();
        }
    }
    #endregion
}
