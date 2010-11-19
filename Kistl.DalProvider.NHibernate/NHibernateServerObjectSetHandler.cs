
namespace Kistl.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Server;

    public class NHibernateServerObjectSetHandler
        : BaseServerObjectSetHandler
    {
        /// <inheritdoc/>
        public override IEnumerable<IPersistenceObject> SetObjects(IKistlContext ctx, IEnumerable<IPersistenceObject> objects, IEnumerable<ObjectNotificationRequest> notificationRequests)
        {
            return base.SetObjects(ctx, objects, notificationRequests);
        }
    }

}
