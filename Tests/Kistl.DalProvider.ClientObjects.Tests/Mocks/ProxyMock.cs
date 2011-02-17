
namespace Kistl.DalProvider.Client.Mocks
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Packaging;
    using Kistl.App.Test;
    using Kistl.DalProvider.Memory;

    public class ProxyMock
        : IProxy
    {
        private BaseMemoryContext _backingStore;

        public ProxyMock(BaseMemoryContext backingStore, IFrozenContext frozen)
        {
            this._backingStore = backingStore;

            var generatedAssembly = Assembly.Load(MemoryProvider.GeneratedAssemblyName);
            Importer.LoadFromXml(_backingStore, generatedAssembly.GetManifestResourceStream("Kistl.Objects.MemoryImpl.FrozenObjects.xml"));
        }

        public IEnumerable<IDataObject> GetList(IKistlContext ctx, InterfaceType ifType, int maxListCount, bool withEagerLoading, IEnumerable<Expression> filter, IEnumerable<OrderBy> orderBy, out List<IStreamable> auxObjects)
        {
            if (orderBy != null) throw new ArgumentException("OrderBy is not supported yet");

            auxObjects = new List<IStreamable>();
            var query = GetUntypedQuery(ifType);
            IEnumerable<IDataObject> sourceList;

            if (filter != null)
            {
                var source = query.AsQueryable().AddCast(ifType.Type);
                filter.ForEach(f => source = source.AddFilter(f.StripQuotes()));
                sourceList = source.Cast<IDataObject>();
            }
            else
            {
                sourceList = query.Cast<IDataObject>();
            }

            return CreateUnattachedClones(ctx, ifType, sourceList);
        }

        public IEnumerable<IDataObject> GetListOf(IKistlContext ctx, InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            auxObjects = new List<IStreamable>();

            var obj = GetUntypedQuery(ifType).Where(o => o.ID == ID).Single();
            var sourceList = obj.GetPropertyValue<IEnumerable>(property).Cast<IDataObject>();
            return CreateUnattachedClones(ctx, ifType, sourceList);
        }

        public IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            var result = new List<IPersistenceObject>();
            //foreach (var obj in objects)
            //{
            //    var type = ctx.GetInterfaceType(obj);

            //    if (obj.ObjectState != DataObjectState.Deleted)
            //    {
            //        var newObj = CreateInstance(ctx, type, 0);

            //        // Copy old object to new object
            //        newObj.ApplyChangesFrom(obj);
            //        if (newObj.ID < Helper.INVALIDID)
            //        {
            //            newObj.SetPrivatePropertyValue<int>("ID", ++newID);
            //        }
            //        result.Add(newObj);
            //        SetPrivateFieldValue<DataObjectState>(newObj, "_ObjectState", DataObjectState.Unmodified);
            //    }
            //}

            return result;
        }

        #region Utilities

        private IQueryable<IDataObject> GetUntypedQueryHack<T>()
            where T : class, IDataObject
        {
            return _backingStore.GetQuery<T>().Cast<IDataObject>();
        }

        public IQueryable<IDataObject> GetUntypedQuery(InterfaceType ifType)
        {
            var mi = this.GetType().FindGenericMethod(true, "GetUntypedQueryHack", new[] { ifType.Type }, new Type[0]);
            return (IQueryable<IDataObject>)mi.Invoke(this, new object[0]);
        }

        private static IEnumerable<IDataObject> CreateUnattachedClones(IKistlContext ctx, InterfaceType ifType, IEnumerable<IDataObject> sourceList)
        {
            return sourceList
                .Select(obj =>
                {
                    var target = (IDataObject)ctx.Internals().CreateUnattached(ifType);
                    target.ApplyChangesFrom(obj);
                    return target;
                })
                .ToList();
        }

        public void Dispose()
        {
        }

        #endregion

        #region Not implemented

        public object InvokeServerMethod(IKistlContext ctx, InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests, out IEnumerable<IPersistenceObject> changedObjects)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FetchRelation<T>(IKistlContext ctx, Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects) where T : class, IRelationEntry
        {
            throw new NotImplementedException();
        }

        public System.IO.Stream GetBlobStream(int ID)
        {
            throw new NotImplementedException();
        }

        public App.Base.Blob SetBlobStream(IKistlContext ctx, System.IO.Stream stream, string filename, string mimetype)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
