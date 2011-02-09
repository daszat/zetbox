
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class InfoLoggingKistlServiceDecorator
        : IKistlService
    {
        private readonly IKistlService _implementor;

        public InfoLoggingKistlServiceDecorator(IKistlService implementor)
        {
            _implementor = implementor;
        }

        private static void DebugLogIdentity()
        {
            Logging.Facade.DebugFormat("Called IsAuthenticated = {0}, Identity = {1}", System.Threading.Thread.CurrentPrincipal.Identity.IsAuthenticated, System.Threading.Thread.CurrentPrincipal.Identity.Name);
        }

        public byte[] SetObjects(byte[] msg, ObjectNotificationRequest[] notificationRequests)
        {
            using (Logging.Facade.InfoTraceMethodCall("SetObjects"))
            {
                DebugLogIdentity();
                try
                {
                    return _implementor.SetObjects(msg, notificationRequests);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("SetObjects", ex);
                    throw;
                }
            }
        }

        public byte[] GetList(SerializableType type, int maxListCount, bool eagerLoadLists, SerializableExpression[] filter, OrderByContract[] orderBy)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetList", "{0}", type))
            {
                DebugLogIdentity();
                try
                {
                    return _implementor.GetList(type, maxListCount, eagerLoadLists, filter, orderBy);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("GetList", ex);
                    throw;
                }
            }
        }

        [Obsolete]
        public byte[] GetListOf(SerializableType type, int ID, string property)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("GetListOf", "{0}", type))
            {
                DebugLogIdentity();
                try
                {
                    return _implementor.GetListOf(type, ID, property);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("GetListOf", ex);
                    throw;
                }
            }
        }

        public byte[] FetchRelation(Guid relId, int role, int ID)
        {
            using (Logging.Facade.InfoTraceMethodCallFormat("FetchRelation", "relId = [{0}], role = [{1}], parentObjID = [{2}]", relId, role, ID))
            {
                DebugLogIdentity();
                try
                {
                    return _implementor.FetchRelation(relId, role, ID);
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
                DebugLogIdentity();
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

        public BlobResponse SetBlobStream(BlobMessage blob)
        {
            using (Logging.Facade.InfoTraceMethodCall("SetBlobStream"))
            {
                DebugLogIdentity();
                try
                {
                    return _implementor.SetBlobStream(blob);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("SetBlobStream", ex);
                    throw;
                }
            }
        }

        public byte[] InvokeServerMethod(SerializableType type, int ID, string method, SerializableType[] parameterTypes, byte[] parameter, byte[] changedObjects, ObjectNotificationRequest[] notificationRequests, out byte[] retChangedObjects)
        {
            using (Logging.Facade.InfoTraceMethodCall("InvokeServerMethod"))
            {
                DebugLogIdentity();
                try
                {
                    return _implementor.InvokeServerMethod(type, ID, method, parameterTypes, parameter, changedObjects, notificationRequests, out retChangedObjects);
                }
                catch (Exception ex)
                {
                    Logging.Facade.Error("InvokeServerMethod", ex);
                    throw;
                }
            }
        }
    }
}
