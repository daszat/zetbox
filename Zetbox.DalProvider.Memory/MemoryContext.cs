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

namespace Zetbox.DalProvider.Memory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.API.Async;
    using System.Threading.Tasks;

    public interface IMemoryActionsManager : ICustomActionsManager { }

    public class MemoryContext
        : BaseMemoryContext, IReadOnlyZetboxContext
    {
        // private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.DalProvider.Memory");

        // private static readonly List<IPersistenceObject> emptylist = new List<IPersistenceObject>(0);
        private readonly FuncCache<Type, MemoryImplementationType> _implTypeFactoryCache;
        private readonly MemoryImplementationType.MemoryFactory _implTypeFactory;

        public MemoryContext(InterfaceType.Factory iftFactory, Func<IFrozenContext> lazyCtx, MemoryImplementationType.MemoryFactory implTypeFactory, IEnumerable<IZetboxContextEventListener> eventListeners)
            : base(iftFactory, lazyCtx, eventListeners)
        {
            _implTypeFactoryCache = new FuncCache<Type, MemoryImplementationType>(t => implTypeFactory(t));
            _implTypeFactory = t => _implTypeFactoryCache.Invoke(t);
        }

        public override Task<int> SubmitChanges()
        {
            int count = 0;
            var added = new List<IDataObject>();
            var modified = new List<IDataObject>();
            var deleted = new List<IPersistenceObject>();
            foreach (var o in objects)
            {
                var ido = o as IDataObject;
                switch (o.ObjectState)
                {
                    case DataObjectState.New:
                        ((BaseMemoryPersistenceObject)o).SetUnmodified();
                        count += 1;
                        if (ido != null) added.Add(ido);
                        break;
                    case DataObjectState.Modified:
                        ((BaseMemoryPersistenceObject)o).SetUnmodified();
                        count += 1;
                        if (ido != null) modified.Add(ido);
                        break;
                    case DataObjectState.Unmodified:
                        break;
                    case DataObjectState.Deleted:
                        deleted.Add(o);
                        break;
                    case DataObjectState.NotDeserialized:
                    case DataObjectState.Detached:
                    default:
                        throw new NotSupportedException(string.Format("Unexpected ObjectState encountered: {0}", o.ObjectState));
                }
            }
            foreach (var r in deleted)
            {
                objects.Remove(r);
            }

            ZetboxContextEventListenerHelper.OnSubmitted(eventListeners, this, added, modified, deleted.OfType<IDataObject>());

            return Task.FromResult(count);
        }

        protected override object CreateUnattachedInstance(InterfaceType ifType)
        {
            // TODO: replace with generated switch factory
            return Activator.CreateInstance(this.ToImplementationType(ifType).Type, lazyCtx);
        }

        private Dictionary<InterfaceType, ImplementationType> _toImplementationTypeMemo = new Dictionary<InterfaceType, ImplementationType>();
        public override ImplementationType ToImplementationType(InterfaceType ift)
        {
            // TODO: replace with generated switch factory
            ImplementationType result;
            if (!_toImplementationTypeMemo.TryGetValue(ift, out result))
            {
                var typeName = ift.Type.FullName + "Memory" + Zetbox.API.Helper.ImplementationSuffix + "," + MemoryProvider.GeneratedAssemblyName;
                var t = Type.GetType(typeName);
                _toImplementationTypeMemo[ift] = result = GetImplementationType(t);
            }
            return result;
        }

        private Dictionary<string, InterfaceType> _getInterfaceTypeMemo = new Dictionary<string, InterfaceType>();
        public override InterfaceType GetInterfaceType(string typeName)
        {
            InterfaceType result;
            if (!_getInterfaceTypeMemo.TryGetValue(typeName, out result))
            {
                _getInterfaceTypeMemo[typeName] = result = IftFactory(Type.GetType(typeName + "," + typeof(Zetbox.App.Base.ObjectClass).Assembly.FullName, true));
            }
            return result;
        }

        public override ImplementationType GetImplementationType(Type t)
        {
            return _implTypeFactory(t);
        }

        /// <summary>Only implemented for the parent==null case.</summary>
        IList<T> IReadOnlyZetboxContext.FetchRelation<T>(Guid relId, RelationEndRole endRole, IDataObject parent)
        {
            var t = ((IReadOnlyZetboxContext)this).FetchRelationAsync<T>(relId, endRole, parent);
            return t.Result;
        }

        /// <summary>Only implemented for the parent==null case.</summary>
        System.Threading.Tasks.Task<IList<T>> IReadOnlyZetboxContext.FetchRelationAsync<T>(Guid relId, RelationEndRole endRole, IDataObject parent)
        {
            return new System.Threading.Tasks.Task<IList<T>>(() =>
            {
                if (parent == null)
                {
                    return GetPersistenceObjectQuery(IftFactory(typeof(T))).Cast<T>().ToList();
                }
                else
                {
                    Func<T, bool> aFilter = i => i.AObject == parent;
                    Func<T, bool> bFilter = i => i.BObject == parent;
                    // TODO: #1571 This method expects IF Types, but Impl types are passed
                    switch (endRole)
                    {
                        case RelationEndRole.A:
                            return GetPersistenceObjectQuery(GetImplementationType(typeof(T)).ToInterfaceType()).Cast<T>().Where(aFilter).ToList();
                        case RelationEndRole.B:
                            return GetPersistenceObjectQuery(GetImplementationType(typeof(T)).ToInterfaceType()).Cast<T>().Where(bFilter).ToList();
                        default:
                            throw new NotImplementedException(String.Format("Unknown RelationEndRole [{0}]", endRole));
                    }
                }
            });
        }

        public override ContextIsolationLevel IsolationLevel
        {
            get { return ContextIsolationLevel.PreferContextCache; }
        }
    }
}
