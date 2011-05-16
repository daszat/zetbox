
namespace Kistl.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Kistl.API.Utils;

    /// <summary>
    /// A simple decorator to log all calls to the decorated proxy
    /// </summary>
    [DebuggerStepThrough]
    internal class InfoLoggingProxyDecorator : IProxy
    {
        private readonly IProxy _implementor;
        internal InfoLoggingProxyDecorator(IProxy implementor)
        {
            _implementor = implementor;
        }

        public IEnumerable<IDataObject> GetList(IKistlContext ctx, InterfaceType ifType, int maxListCount, bool eagerLoadLists, IEnumerable<Expression> filter, IEnumerable<OrderBy> orderBy, out List<IStreamable> auxObjects)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetList", "Type=[{0}]", ifType.ToString()))
            {
                try
                {
                    return _implementor.GetList(ctx, ifType, maxListCount, eagerLoadLists, filter, orderBy, out auxObjects);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("GetList", ex);
                    throw;
                }
            }
        }

        public IEnumerable<IDataObject> GetListOf(IKistlContext ctx, InterfaceType ifType, int ID, string property, out List<IStreamable> auxObjects)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetListOf", "{0} [{1}].{2}", ifType, ID, property))
            {
                try
                {
                    return _implementor.GetListOf(ctx, ifType, ID, property, out  auxObjects);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("GetListOf", ex);
                    throw;
                }
            }
        }

        public IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            using (Logging.Facade.InfoTraceMethodCall("SetObjects"))
            {
                try
                {
                    return _implementor.SetObjects(ctx, objects, notificationRequests);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("SetObjects", ex);
                    throw;
                }
            }
        }

        public object InvokeServerMethod(IKistlContext ctx, InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests, out IEnumerable<IPersistenceObject> changedObjects)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("InvokeServerMethod", "ID=[{0}]", ID))
            {
                try
                {
                    return _implementor.InvokeServerMethod(ctx, ifType, ID, method, retValType, parameterTypes, parameter, objects, notificationRequests, out changedObjects);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("InvokeServerMethod", ex);
                    throw;
                }
            }
        }

        public IEnumerable<T> FetchRelation<T>(IKistlContext ctx, Guid relationId, RelationEndRole role, IDataObject parent, out List<IStreamable> auxObjects) where T : class, IRelationEntry
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("FetchRelation", "Fetching relation: ID=[{0}],role=[{1}],parentId=[{2}]", relationId, role, parent.ID))
            {
                try
                {
                    return _implementor.FetchRelation<T>(ctx, relationId, role, parent, out auxObjects);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("FetchRelation", ex);
                    throw;
                }
            }
        }

        public Stream GetBlobStream(int ID)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetBlobStream", "ID=[{0}]", ID))
            {
                try
                {
                    return _implementor.GetBlobStream(ID);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("GetBlobStream", ex);
                    throw;
                }
            }
        }

        public App.Base.Blob SetBlobStream(IKistlContext ctx, Stream stream, string filename, string mimetype)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("SetBlobStream", "filename=[{0}]", filename))
            {
                try
                {
                    return _implementor.SetBlobStream(ctx, stream, filename, mimetype);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("SetBlobStream", ex);
                    throw;
                }
            }
        }

        public void Dispose()
        {
            using (Logging.Facade.InfoTraceMethodCall("Dispose"))
            {
                _implementor.Dispose();
            }
        }
    }
}
