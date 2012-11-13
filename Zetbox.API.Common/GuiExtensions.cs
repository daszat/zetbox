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

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;
    using Zetbox.API.Utils;

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
        public static ViewDescriptor GetViewDescriptor(
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
        public static ViewDescriptor GetViewDescriptor(
            this ViewModelDescriptor self,
            Toolkit tk,
            ControlKind requestedControlKind)
        {
            // Checks
            if (self == null) throw new ArgumentNullException("self");

            #region Cache Management
            PrimeCaches(tk, self.ReadOnlyContext);

            var key = new ViewDescriptorCache.Key(self.ViewModelRef, tk, requestedControlKind);
            if (_viewDescriptorCache.ContainsKey(key)) return _viewDescriptorCache[key];
            #endregion

            // If the ViewModel has a more specific DefaultKind respect its choice
            if (self.DefaultEditorKind != null
                && requestedControlKind != null
                && self.DefaultEditorKind.AndParents().Select(i => i.ExportGuid).Contains(requestedControlKind.ExportGuid))
            {
                if (requestedControlKind != self.DefaultEditorKind)
                {
                    Logging.Log.DebugFormat("Using more specific default kind: {0} -> {1}", requestedControlKind.Name, self.DefaultEditorKind.Name);
                }
                requestedControlKind = self.DefaultEditorKind;
            }
            else
            {
                requestedControlKind = self.SecondaryControlKinds
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
                var parent =  self.ViewModelRef.Parent != null ? self.ViewModelRef.Parent.GetViewModelDescriptor() : null;
                if (parent != null)
                {
                    result = GetViewDescriptor(parent, tk, requestedControlKind);
                }
                else
                {
                    Logging.Log.WarnFormat("Couldn't find ViewDescriptor for '{1}' matching ControlKind: '{0}'", requestedControlKind, self.ViewModelRef);
                }
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

                // Try the most common
                if (match == null)
                {
                    match = allTypes.SelectMany(t => candidates.Where(c => c.SupportedViewModels.Count == 0)).FirstOrDefault();
                }

                // Log a warning if nothing found
                if (match == null)
                {
                    Logging.Log.WarnFormat("Couldn't find ViewDescriptor for '{1}' matching ControlKind: '{0}'", requestedControlKind, self.GetType().FullName);
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

            allTypes.AddRange(
                self.ViewModelRef.AsType(false).GetInterfaces()
                    .OrderBy(i => i.FullName)
                    .Select(i => i.ToRef(self.ReadOnlyContext))
                    .Where(i => i != null)
            );
            return allTypes;
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

        private static ViewModelDescriptor GetViewModelDescriptor(this TypeRef tr)
        {
            if (tr == null) { throw new ArgumentNullException("tr"); }

            PrimeCaches(null, tr.ReadOnlyContext);

            ViewModelDescriptor result = null;
            while (result == null && tr != null)
            {
                if (_pmdCache.ContainsKey(tr.ExportGuid))
                {
                    result = _pmdCache[tr.ExportGuid];
                }
                tr = tr.Parent;
            }
            return result;
        }

        public static ViewModelDescriptor GetViewModelDescriptor(object mdl, IReadOnlyZetboxContext frozenCtx)
        {
            if (mdl == null) throw new ArgumentNullException("mdl");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            Type mdlType = mdl.GetType();
            while (mdlType != null)
            {
                var typeRef = mdlType.ToRef(frozenCtx);
                if (typeRef != null)
                    return typeRef.GetViewModelDescriptor();

                mdlType = mdlType.BaseType;
            }

            if (mdl.GetType().IsGenericType)
            {
                Logging.Log.ErrorFormat("Unable to resolve TypeRef of given ViewModel '{0}'. You have to manually create a generic TypeRef.", mdl.GetType());
            }
            else
            {
                Logging.Log.ErrorFormat("Unable to resolve TypeRef of given ViewModel '{0}'. Regenerate Assembly Refs.", mdl.GetType());
            }

            return null;
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
            public Key( TypeRef vmd,
                        Toolkit tk,
                        ControlKind ck)
            {
                this.vmd = vmd.ExportGuid;
                this.tk = tk;
                this.ck = ck != null ? ck.ExportGuid : Guid.Empty;
            }

            private readonly Guid vmd;
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
        private Dictionary<Guid, ViewModelDescriptor> _cache = null;
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
                .ToDictionary(obj => obj.ViewModelRef.ExportGuid);
        }

        public bool ContainsKey(Guid key)
        {
            return _cache.ContainsKey(key);
        }

        public ViewModelDescriptor this[Guid key]
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
