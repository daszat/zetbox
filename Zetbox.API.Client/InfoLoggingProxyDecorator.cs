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

namespace Zetbox.API.Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Zetbox.API.Utils;

    /// <summary>
    /// A simple decorator to log all calls to the decorated proxy
    /// </summary>
    public class InfoLoggingProxyDecorator : IProxy
    {
        private static Thread _uiThread = null;
        [Conditional("DEBUG")]
        public static void SetUiThread(Thread uiThread)
        {
            _uiThread = uiThread;
        }

        [Conditional("DEBUG")]
        private static void CheckUiThread()
        {
            if (_uiThread == null) return;
            if (_uiThread == Thread.CurrentThread)
            {
                Logging.Facade.Warn("Calling Proxy on UI Thread");
            }
        }

        private readonly IProxy _implementor;
        internal InfoLoggingProxyDecorator(IProxy implementor)
        {
            _implementor = implementor;
        }

        public Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetObjects(IReadOnlyZetboxContext requestingCtx, InterfaceType ifType, Expression query)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetObjects", "Type=[{0}]", ifType.ToString()))
            {
                CheckUiThread();
                try
                {
                    return _implementor.GetObjects(requestingCtx, ifType, query);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("GetObjects", ex);
                    throw;
                }
            }
        }

        public Task<Tuple<IEnumerable<IDataObject>, List<IStreamable>>> GetListOf(InterfaceType ifType, int ID, string property)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetListOf", "{0} [{1}].{2}", ifType, ID, property))
            {
                CheckUiThread();
                try
                {
                    return _implementor.GetListOf(ifType, ID, property);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("GetListOf", ex);
                    throw;
                }
            }
        }

        public Task<IEnumerable<IPersistenceObject>> SetObjects(IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            using (Logging.Facade.InfoTraceMethodCall("SetObjects"))
            {
                CheckUiThread();
                try
                {
                    return _implementor.SetObjects(objects, notificationRequests);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("SetObjects", ex);
                    throw;
                }
            }
        }

        public Task<Tuple<object, IEnumerable<IPersistenceObject>, List<IStreamable>>> InvokeServerMethod(InterfaceType ifType, int ID, string method, Type retValType, IEnumerable<Type> parameterTypes, IEnumerable<object> parameter, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("InvokeServerMethod", "ID=[{0}]", ID))
            {
                CheckUiThread();
                try
                {
                    return _implementor.InvokeServerMethod(ifType, ID, method, retValType, parameterTypes, parameter, objects, notificationRequests);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("InvokeServerMethod", ex);
                    throw;
                }
            }
        }

        public Task<Tuple<IEnumerable<T>, List<IStreamable>>> FetchRelation<T>(Guid relationId, RelationEndRole role, int parentId, InterfaceType parentIfType) where T : class, IRelationEntry
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("FetchRelation", "Fetching relation: ID=[{0}],role=[{1}],parentId=[{2}]", relationId, role, parentId))
            {
                CheckUiThread();
                try
                {
                    return _implementor.FetchRelation<T>(relationId, role, parentId, parentIfType);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("FetchRelation", ex);
                    throw;
                }
            }
        }

        public Task<Stream> GetBlobStream(int ID)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetBlobStream", "ID=[{0}]", ID))
            {
                CheckUiThread();
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

        public Task<App.Base.Blob> SetBlobStream(Stream stream, string filename, string mimetype)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("SetBlobStream", "filename=[{0}]", filename))
            {
                CheckUiThread();
                try
                {
                    return _implementor.SetBlobStream(stream, filename, mimetype);
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
